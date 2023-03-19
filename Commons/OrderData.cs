namespace Commons;

public class OrderData
{
    public Guid OrderId { get; set; }
    public string CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }
}