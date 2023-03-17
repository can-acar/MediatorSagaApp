using Commons.Events;
using Core.Interfaces;

namespace Commons.Request;

public class CreateOrderRequest : IRequest<bool>
{
    public int OrderId { get; set; }
}