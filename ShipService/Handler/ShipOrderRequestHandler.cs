using Commons.Events;
using Commons.Request;
using Core.Interfaces;

namespace ShipService.Handler;

public class ShipOrderRequestHandler : IRequestHandler<ShipOrderRequest, OrderShippedEvent>
{
    public Task<OrderShippedEvent> HandleAsync(ShipOrderRequest request)
    {
        // Simulate shipping an order
        var orderShippedEvent = new OrderShippedEvent {OrderId = request.OrderId};
        return Task.FromResult(orderShippedEvent);
    }
}