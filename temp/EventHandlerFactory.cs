namespace Core;

public class EventHandlerFactory : IEventHandlerFactory
{
    private readonly Dictionary<Type, object> _eventHandlerTypes = new Dictionary<Type, object>();

    public EventHandlerFactory(IEnumerable<IEventHandler<IEvent>> handlers)
    {
        foreach (var handler in handlers)
        {
            _eventHandlerTypes.Add(handler.GetType().GetInterfaces()[0].GenericTypeArguments[0], handler);
        }
    }

    public void RegisterHandler<TEvent, THandler>() where TEvent : IEvent where THandler : IEventHandler<TEvent>
    {
        _eventHandlerTypes[typeof(TEvent)] = typeof(THandler);
    }

    public IEventHandler<TEvent> GetHandlerForEvent<TEvent>() where TEvent : IEvent
    {
        if (_eventHandlerTypes.TryGetValue(typeof(TEvent), out var handler))
        {
            return (IEventHandler<TEvent>) handler;
        }

        return null;
    }
}


// public class EventHandlerFactory : IEventHandlerFactory
// {
//     private readonly Dictionary<Type, Type> _eventHandlerTypes = new Dictionary<Type, Type>();
//
//     public void RegisterHandler<TEvent, THandler>() where TEvent : IEvent where THandler : IEventHandler<TEvent>
//     {
//         _eventHandlerTypes[typeof(TEvent)] = typeof(THandler);
//     }
//
//     public IEventHandler<TEvent> GetHandlerForEvent<TEvent>() where TEvent : IEvent
//     {
//         if (_eventHandlerTypes.TryGetValue(typeof(TEvent), out var handlerType))
//         {
//             return (IEventHandler<TEvent>)Activator.CreateInstance(handlerType);
//         }
//
//         return null;
//     }
// }