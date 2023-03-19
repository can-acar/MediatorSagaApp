namespace Core.Interfaces;

public interface IEventHandlerFactory
{
    IRequestHandler<TRequest, TResponse> Create<TRequest, TResponse>() where TRequest : IRequest<TResponse>;
}