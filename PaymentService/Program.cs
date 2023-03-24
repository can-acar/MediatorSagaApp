// See https://aka.ms/new-console-template for more information

using Commons;
using Commons.Models;
using Commons.Request;
using Commons.Response;
using Core;

Console.WriteLine("== Process Payment Request Handler ==");

var messageBus = new InMemoryMessageBus();

while (true)
{
    Console.WriteLine("PaymentService: Waiting for CreateOrderRequest...");

    var request = await messageBus.Receive<CreatePaymentResponse>("OrderProcessing");

    //
    // Console.WriteLine($"OrderService: Order {response.OrderId} created, status: {response.Status}");
    //
    // messageBus.Publish("OrderProcessing", response);

    Thread.Sleep(1000);
}