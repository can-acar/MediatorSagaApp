using Core.Interfaces;

namespace Commons.Models;

public class OrderCreated : IEvent
{
    public Guid OrderId { get; set; }
    public string CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
}