namespace Core;



// public class Mediator : IMediator
// {
//     private readonly IDictionary<Type, Type> _handlers;
//
//     public Mediator()
//     {
//         _handlers = new Dictionary<Type, Type>();
//     }
//
//     public void Register<TRequest, TResponse, THandler>()
//         where TRequest : ICommand<TResponse>
//         where THandler : IHandler<TRequest, TResponse>
//     {
//         var key = typeof(TRequest);
//         var value = typeof(THandler);
//
//         if (_handlers.ContainsKey(key))
//         {
//             throw new InvalidOperationException($"A handler has already been registered for the request {key.FullName}");
//         }
//
//         _handlers.Add(key, value);
//     }
//
//     public async Task<TResponse> SendAsync<TResponse>(ICommand<TResponse> command)
//     {
//         var key = command.GetType();
//
//         if (!_handlers.TryGetValue(key, out var handlerType))
//         {
//             throw new InvalidOperationException($"No handler found for command {key.FullName}");
//         }
//
//         var handler = (IHandler<ICommand<TResponse>, TResponse>) Activator.CreateInstance(handlerType);
//         return await handler.HandleAsync((ICommand<TResponse>) command);
//     }
// }