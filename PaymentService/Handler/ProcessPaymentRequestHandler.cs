using Commons.Events;
using Commons.Request;
using Core.Interfaces;

namespace PaymentService.Handler;

public class ProcessPaymentRequestHandler : IRequestHandler<ProcessPaymentRequest, PaymentProcessedEvent>
{
    public Task<PaymentProcessedEvent> HandleAsync(ProcessPaymentRequest request)
    {
        // Simulate processing a payment
        var paymentProcessedEvent = new PaymentProcessedEvent {OrderId = request.OrderId};
        return Task.FromResult(paymentProcessedEvent);
    }
}