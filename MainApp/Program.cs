// See https://aka.ms/new-console-template for more information


using Commons;
using Commons.Models;
using Core;


Console.WriteLine("== Main App ==");

var subscriptionManager = new SubscriptionManager();

var messageBus = new InMemoryMessageBus();

var orderProcessingSaga = new Saga<OrderData>(messageBus);


//var mediator = new Mediator(new RequestHandlerFactory(), new InMemoryMessageBus(), new EventHandlerFactory());
await messageBus.PublishAsync(new OrderCreated
{
    OrderId = Guid.NewGuid(),
    CustomerId = Guid.NewGuid().ToString(),
    TotalAmount = 100
});

while (true)
{
    Console.ReadKey();


    messageBus.Subscribe<OrderCreated>(async @event =>
    {
        Console.WriteLine($"Order Created: {@event.OrderId}");
    });


}

await orderProcessingSaga.StopAsync();