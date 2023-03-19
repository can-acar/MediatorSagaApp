using Core.Interfaces;

namespace Commons.Request;

public class ProcessPaymentRequest : IRequest<bool>
{
    public string OrderId { get; set; }
    public decimal Amount { get; set; }
}