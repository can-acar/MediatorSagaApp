using Commons.Request;
using Core.Interfaces;

namespace OrdersService.Handlers;

public class CreateOrderRequestHandler : IRequestHandler<CreateOrderRequest, string>
{
    public Task<string> Handle(CreateOrderRequest request)
    {
        // Sipariş oluşturma işlemini gerçekleştirin
        return Task.FromResult("Order created");
    }
}