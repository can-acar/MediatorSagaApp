using Core.Interfaces;

namespace Commons.Models;

public class OrderShipped : IEvent
{
    public Guid OrderId { get; set; }
}