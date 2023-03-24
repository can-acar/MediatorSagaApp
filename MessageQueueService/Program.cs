// See https://aka.ms/new-console-template for more information

using System.Collections.Concurrent;
using System.IO.Pipes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using Core;


namespace MessageQueueService;

public class Program
{
    static async Task Main(string[] args)
    {
        var loading = new[]
        {
            "|",
            "/",
            "-",
            "\\",
            "|",
            "/",
            "-",
            "\\",
            "|"
        };

        var i = 0;

        var messageBus = new InMemoryMessageBus();
        var messageQueue = new BlockingCollection<Message>();

        // Start the message queue processing loop
        _ = Task.Run(() => ProcessMessages(messageQueue, messageBus));

        // Start the named pipe server for inter-process communication
        var server = new NamedPipeServerStream("MessageQueuePipe", PipeDirection.InOut, 1, PipeTransmissionMode.Message);

        Console.WriteLine("MessageQueueService: Waiting for connections...\n");

        await server.WaitForConnectionAsync();

        // Send the initial message to the connected client
        await SendInitialMessage(server, messageBus);

        // Receive messages from connected clients
        while (true)
        {
            Console.Write($"\rReceiveMessage: {loading[i++]}");

            if (i >= loading.Length) i = 0;

            var message = await ReceiveMessage(server);

            messageQueue.Add(message);
        }
    }

    private static Task ProcessMessages(BlockingCollection<Message?> messageQueue, InMemoryMessageBus messageBus)
    {
        foreach (var message in messageQueue.GetConsumingEnumerable())
        {
            var publishMethod = messageBus.GetType().GetMethod("Publish")
                .MakeGenericMethod(Type.GetType(message.TypeAssemblyQualifiedName));

            publishMethod.Invoke(messageBus, new object[] {message.Channel, message.Content});
        }

        return Task.CompletedTask;
    }


    private static async Task SendInitialMessage(Stream server, InMemoryMessageBus messageBus)
    {
        var message = new Message
            {Channel = "MessageBus", Content = messageBus, TypeAssemblyQualifiedName = typeof(InMemoryMessageBus).AssemblyQualifiedName};
        var serializedMessage = JsonSerializer.Serialize(message);
        var buffer = Encoding.UTF8.GetBytes(serializedMessage);
        await server.WriteAsync(buffer, 0, buffer.Length);
    }

    private static async Task<Message> ReceiveMessage(Stream server)
    {
        using var memoryStream = new MemoryStream();
        byte[] buffer = new byte[1024];
        int bytesRead;

        do
        {
            bytesRead = await server.ReadAsync(buffer, 0, buffer.Length);
            if (bytesRead == 0)
            {
                break;
            }

            memoryStream.Write(buffer, 0, bytesRead);
        } while (bytesRead > 0);

        if (memoryStream.Length == 0)
        {
            // No data was read from the stream.
            // This might indicate that the server has closed the connection or no data is available at the moment.
            // You can choose to throw an exception or handle this case differently.
            throw new InvalidOperationException("No data was read from the stream.");
        }

        memoryStream.Position = 0;
        return JsonSerializer.Deserialize<Message>(memoryStream.ToArray());
    }
}