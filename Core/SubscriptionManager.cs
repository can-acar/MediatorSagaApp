using Core.Interfaces;

namespace Core;

public class SubscriptionManager
{
    private readonly Dictionary<string, List<Action<object>>> _subscriptions = new();

    public SubscriptionManager()
    {
    }


    public void AddSubscription<TEvent>(string channel,Action<TEvent> handler) where TEvent : IEvent
    {
        var eventType = typeof(TEvent);
   
        var handlerType = handler.GetType();

        if (!HasSubscriptionForEvent(channel))
        {
            _subscriptions.Add(channel, new List<Action<object>>());
        }

        _subscriptions[channel].Add(@event => handler((TEvent) @event));
    }

    private bool HasSubscriptionForEvent(string eventName)
    {
        return _subscriptions.ContainsKey(eventName);
    }

    public void RemoveSubscription<TEvent>(Action<TEvent> handler) where TEvent : IEvent
    {
        var eventName = typeof(TEvent).Name;
        var handlerType = handler.GetType();

        if (!HasSubscriptionForEvent(eventName))
        {
            return;
        }

        _subscriptions[eventName].Remove(@event => handler((TEvent) @event));
    }
    
    public List<Action<object>> GetHandlersForEvent<TEvent>() where TEvent : IEvent
    {
        var eventName = typeof(TEvent).Name;
        return _subscriptions[eventName];
    }
}