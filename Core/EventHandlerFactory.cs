using Core.Interfaces;

namespace Core;

public class EventHandlerFactory : IEventHandlerFactory
{
    public IRequestHandler<TRequest, TResponse> Create<TRequest, TResponse>() where TRequest : IRequest<TResponse>
    {
        throw new NotImplementedException();
    }
}