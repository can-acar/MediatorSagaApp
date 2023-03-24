using Core.Interfaces;

namespace Commons.Request;

public class CreateOrderRequest 
{
    public string OrderId { get; set; }
    public string CustomerName { get; set; }
}