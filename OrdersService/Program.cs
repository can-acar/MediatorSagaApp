// See https://aka.ms/new-console-template for more information


using Commons.Request;
using Commons.Response;
using Core;
using JKang.EventBus;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("== Create Order Request Handler ==");

var messageBus = new InMemoryMessageBus();
var loading = new[]
{
    ".",
    "..",
    "...",
    "....",
    "...",
    "..",
    ".",
    "..",
    "..."
};
var i = 0;

while (true)
{
    Console.Write($"\rOrderService: Waiting for CreateOrderRequest {loading[i++]}");

    if (i >= loading.Length) i = 0;

    Thread.Sleep(1000);

    var request = await messageBus.Receive<CreateOrderResponse>("OrderProcessing");

    // Process the order here and create a response
    // var response = new CreateOrderResponse {OrderId = request.OrderId, Status = true};

    Console.WriteLine($"OrderService: Order {request.OrderId} created, Status: {request.Status}");
    //
    // messageBus.Publish("Payment", response);
}