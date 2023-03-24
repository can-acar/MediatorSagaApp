using Core.Interfaces;

namespace Commons.Request;

public class CreatePaymentRequest
{
    public string PaymentId { get; set; }
    public string OrderId { get; set; }
    public decimal Amount { get; set; }
}