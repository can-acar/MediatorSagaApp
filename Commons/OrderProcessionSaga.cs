using Commons.Models;
using Core;
using Core.Interfaces;

namespace Commons;

public class OrderProcessionSaga : Saga<OrderData>
{
    public OrderProcessionSaga(IMessageBus messageBus) : base(messageBus)
    {
        RegisterEventHandler<OrderCreated>(HandleOrderCreatedAsync);
        RegisterEventHandler<OrderShipped>(HandleOrderShippedAsync);
        RegisterEventHandler<OrderCancelled>(HandleOrderCancelledAsync);

        SubscribeToEvent<OrderCreated>();
        SubscribeToEvent<OrderShipped>();
        SubscribeToEvent<OrderCancelled>();
    }

    private async Task HandleOrderCreatedAsync(OrderCreated @event)
    {
        Data = new OrderData
        {
            OrderId = @event.OrderId,
            CustomerId = @event.CustomerId,
            TotalAmount = @event.TotalAmount,
            Status = "Created"
        };

        //await StartAsync();
    }

    private async Task HandleOrderShippedAsync(OrderShipped @event)
    {
        if (Data != null && Data.OrderId == @event.OrderId)
        {
            Data.Status = "Shipped";
            //await StopAsync();
        }
    }

    private async Task HandleOrderCancelledAsync(OrderCancelled @event)
    {
        if (Data != null && Data.OrderId == @event.OrderId)
        {
            Data.Status = "Cancelled";
            //await StopAsync();
        }
    }
}