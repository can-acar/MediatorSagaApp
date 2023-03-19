// See https://aka.ms/new-console-template for more information

using Commons;
using Commons.Models;
using Core;

Console.WriteLine("== Process Payment Request Handler ==");
var messageBus = new InMemoryMessageBus();

var orderProcessingSaga = new OrderProcessionSaga(messageBus);

while (true)
{
    Console.ReadKey();
    messageBus.Subscribe<OrderCreated>(handler: async @event =>
    {
        
        Console.WriteLine($"Order created: {@event.OrderId}");
    });
}