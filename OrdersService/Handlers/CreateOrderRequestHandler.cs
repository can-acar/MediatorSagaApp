using Commons.Events;
using Commons.Request;
using Core.Interfaces;

namespace OrdersService.Handlers;

public class CreateOrderRequestHandler : IRequestHandler<CreateOrderRequest, bool>
{
    public Task<bool> HandleAsync(CreateOrderRequest request)
    {
        // Simulate creating an order
        Console.WriteLine("CreateOrderRequestHandler handled!");
        var orderCreatedEvent = new OrderCreatedEvent {OrderId = request.OrderId};
        return Task.FromResult(true);
    }
}

public class CreateOrderEventHandler : IEventHandler<OrderCreatedEvent>
{
    public Task HandleAsync(OrderCreatedEvent @event)
    {
        // Simulate handling the event
        Console.WriteLine("CreateOrderEventHandler handled!");
        return Task.CompletedTask;
    }
}