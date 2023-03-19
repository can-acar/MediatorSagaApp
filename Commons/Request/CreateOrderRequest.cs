using Core.Interfaces;

namespace Commons.Request;

public class CreateOrderRequest : IRequest<string>
{
    public string OrderId { get; set; }
    public string CustomerName { get; set; }
}