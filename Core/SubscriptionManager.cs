using Core.Interfaces;

namespace Core;

public class SubscriptionManager
{
    private readonly IDictionary<Type, IList<Func<IEvent, Task>>> _subscriptions;

    public SubscriptionManager()
    {
        _subscriptions = new Dictionary<Type, IList<Func<IEvent, Task>>>();
    }

    public void AddSubscription<TEvent>(Func<TEvent, Task> handler) where TEvent : IEvent
    {
        var key = typeof(TEvent);

        if (!_subscriptions.TryGetValue(key, out var handlers))
        {
            handlers = new List<Func<IEvent, Task>>();
            _subscriptions.Add(key, handlers);
        }

        handlers.Add(@event => handler((TEvent) @event));
    }

    public IEnumerable<Func<IEvent, Task>> GetHandlersForEvent<TEvent>() where TEvent : IEvent
    {
        var key = typeof(TEvent);

        if (_subscriptions.TryGetValue(key, out var handlers))
        {
            return handlers;
        }

        return Enumerable.Empty<Func<IEvent, Task>>();
    }

    public void RemoveSubscription(Func<IEvent, Task> handler)
    {
        var key = _subscriptions.Keys.FirstOrDefault(k => _subscriptions[k].Contains(handler));

        if (key != null)
        {
            _subscriptions[key].Remove(handler);
        }
    }
}