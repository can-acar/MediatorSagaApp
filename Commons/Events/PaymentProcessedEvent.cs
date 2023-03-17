using Core.Interfaces;

namespace Commons.Events;

public class PaymentProcessedEvent : IEvent
{
    public int OrderId { get; set; }
    public bool IsSuccess { get; set; }
}