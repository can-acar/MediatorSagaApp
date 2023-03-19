using Commons.Request;
using Core.Interfaces;

namespace PaymentService.Handler;

public class ProcessPaymentRequestHandler : IRequestHandler<ProcessPaymentRequest, bool>
{
    public Task<bool> Handle(ProcessPaymentRequest request)
    {
        // Ödeme işlemini gerçekleştirin
        request.OrderId = "123456789";
        request.Amount = 100;

        return Task.FromResult(true);
    }
}