namespace Core;

public interface IRequestHandlerFactory
{
    IRequestHandler<TRequest, TResponse> GetHandlerForRequest<TRequest, TResponse>(IRequest<TResponse> request) where TRequest : IRequest<TResponse>, IResponse;
    
    // IRequestHandler<TResponse> GetHandlerForRequest<TResponse>(IRequest<TResponse> request);

    void RegisterHandler<TRequest, TResponse, THandler>() where TRequest : ICommand<TResponse> where THandler : IHandler<TRequest, TResponse>;
}