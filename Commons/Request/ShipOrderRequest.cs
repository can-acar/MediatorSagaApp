using Core.Interfaces;

namespace Commons.Request;

public class ShipOrderRequest : IRequest<string>
{
    public string OrderId { get; set; }
}