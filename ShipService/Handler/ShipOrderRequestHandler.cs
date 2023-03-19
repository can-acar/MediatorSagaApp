using Commons.Request;
using Core.Interfaces;

namespace ShipService.Handler;

public class ShipOrderRequestHandler : IRequestHandler<ShipOrderRequest, string>
{
    public Task<string> Handle(ShipOrderRequest request)
    {
        // Sipariş sevkiyat işlemini gerçekleştirin

        request.OrderId = "123456789";


        return Task.FromResult("Order shipped");
    }
}