namespace Core;

public class RequestHandlerFactory : IRequestHandlerFactory
{
    private readonly Dictionary<Type, Type> _handlers = new Dictionary<Type, Type>();

    public RequestHandlerFactory(IEnumerable<IRequestHandler<IRequest>> handlers)
    {
        foreach (var handler in handlers)
        {
            _handlers.Add(handler.GetType().GetInterfaces()[0].GenericTypeArguments[0], handler.GetType());
        }
    }
    

    public IRequestHandler<TResponse> GetHandlerForRequest<TResponse>(IRequest<TResponse> request)
    {
        if (_handlers.TryGetValue(request.GetType(), out var handlerType))
        {
            var handler = (IRequestHandler<TResponse>)Activator.CreateInstance(handlerType);
            return handler;
        }
    
        return null;
    }
   
}