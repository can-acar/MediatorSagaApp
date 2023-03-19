// See https://aka.ms/new-console-template for more information

using Commons;
using Core;

Console.WriteLine("== Ship Order Request Handler ==");

var messageBus = new InMemoryMessageBus();

var orderProcessingSaga = new OrderProcessionSaga(messageBus);




Console.ReadKey();