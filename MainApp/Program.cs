// See https://aka.ms/new-console-template for more information

using System.IO.Pipes;
using System.Text;
using Commons.Request;
using Commons.Response;
using Core;
using MessageQueueService;
using JsonSerializer = System.Text.Json.JsonSerializer;


Console.WriteLine("== Main App ==");

var orderId = Guid.NewGuid().ToString();
var paymentId = Guid.NewGuid().ToString();


// var messageBus = new InMemoryMessageBus();

async Task<InMemoryMessageBus> ReceiveMessageBus(Stream server)
{
    var message = await ReceiveMessage(server);
    var messageType = Type.GetType(message.TypeAssemblyQualifiedName);
    return (InMemoryMessageBus) message.Content;
}


async Task SendMessage(Stream server, string channel, object content, Type type)
{
    var message = new Message {Channel = channel, Content = content, TypeAssemblyQualifiedName = type.AssemblyQualifiedName};
    var serializedMessage = JsonSerializer.Serialize(message);
    var buffer = Encoding.UTF8.GetBytes(serializedMessage);
    await server.WriteAsync(buffer, 0, buffer.Length);
}


// Send a message to OrderService

var client = new NamedPipeClientStream(".", "MessageQueuePipe", PipeDirection.InOut, PipeOptions.Asynchronous);

await client.ConnectAsync();

var messageBus = await ReceiveMessageBus(client);


void HandleCreateOrderResponse(CreateOrderResponse response)
{
    Console.WriteLine($"MainApp: Order {response.OrderId} created, status: {response.Status}\n");

    if (response.Status)
    {
        // Send a message to PaymentService
        var createPaymentRequest = new CreatePaymentRequest {PaymentId = paymentId, OrderId = response.OrderId, Amount = 100};
        //messageBus.Publish("PaymentProcessing", createPaymentRequest);
    }
}

void HandleCreatePaymentResponse(CreatePaymentResponse response)
{
    Console.WriteLine($"MainApp: Payment {response.PaymentId} processed, status: {response.Status}\n");
}

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


while (true)
{
    messageBus.Subscribe<CreateOrderResponse>("OrderProcessing", HandleCreateOrderResponse);

    messageBus.Subscribe<CreatePaymentResponse>("PaymentProcessing", HandleCreatePaymentResponse);


    var createOrderRequest = new CreateOrderRequest {OrderId = orderId, CustomerName = "John Doe"};

    //messageBus.Publish("OrderProcessing", createOrderRequest);

    await SendMessage(client, "OrderProcessing", createOrderRequest, typeof(CreateOrderRequest));

    Console.Write($"\rMainApp: {loading[i++]}");

    if (i >= loading.Length) i = 0;

    Thread.Sleep(1000);
}

async Task<Message> ReceiveMessage(Stream server)
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