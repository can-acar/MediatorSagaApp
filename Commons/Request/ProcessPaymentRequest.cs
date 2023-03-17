using Commons.Events;
using Core.Interfaces;

namespace Commons.Request;

public class ProcessPaymentRequest : IRequest<PaymentProcessedEvent>
{
    public int OrderId { get; set; }
}