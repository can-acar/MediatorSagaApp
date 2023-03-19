namespace Core.Interfaces;

public interface IRequestHandlerFactory
{
    // IRequestHandler<TRequest, TResponse> Create<TRequest, TResponse>() where TRequest : IRequest<TResponse>;


    object Create(Type handlerType);
}