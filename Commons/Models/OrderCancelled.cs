using Core.Interfaces;

namespace Commons.Models;

public class OrderCancelled : IEvent
{
    public Guid OrderId { get; set; }
}