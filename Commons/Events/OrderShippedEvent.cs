using Core.Interfaces;

namespace Commons.Events;

public class OrderShippedEvent : IEvent
{
    public int OrderId { get; set; }
}