using Commons.Events;
using Core.Interfaces;

namespace Commons.Request;

public class ShipOrderRequest : IRequest<OrderShippedEvent>
{
    public int OrderId { get; set; }
}