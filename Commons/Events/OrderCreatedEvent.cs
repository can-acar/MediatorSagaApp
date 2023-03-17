using Core.Interfaces;

namespace Commons.Events;

public class OrderCreatedEvent : IEvent
{
    public int OrderId { get; set; }
}