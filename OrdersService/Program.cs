// See https://aka.ms/new-console-template for more information

using Commons;
using Commons.Models;
using Core;

Console.WriteLine("== Create Order Request Handler ==");

var messageBus = new InMemoryMessageBus();

var orderProcessingSaga = new Saga<OrderData>(messageBus);

//.Subscribe<OrderCreated>(e => Console.WriteLine($"Order created: {e.OrderId}"));

await orderProcessingSaga.StartAsync();

while (true)
{
    Console.ReadKey();
    // listen for messages
    messageBus.Subscribe<OrderCreated>(e =>
    {
        Console.WriteLine($"Order created: {e.OrderId}");
        Console.WriteLine($"Order created: {e.CustomerId}");
        Console.WriteLine($"Order created: {e.TotalAmount}");
        return Task.CompletedTask;
    });
}

