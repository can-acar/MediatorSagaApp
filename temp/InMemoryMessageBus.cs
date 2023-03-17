namespace Core;

public class InMemoryMessageBus : IMessageBus
{
    private readonly IDictionary<Type, IList<Func<IEvent, Task>>> _subscriptions;

    public InMemoryMessageBus()
    {
        _subscriptions = new Dictionary<Type, IList<Func<IEvent, Task>>>();
    }

    public void Subscribe<TEvent>(Func<TEvent, Task> handler) where TEvent : IEvent
    {
        var key = typeof(TEvent);

        if (!_subscriptions.TryGetValue(key, out var handlers))
        {
            handlers = new List<Func<IEvent, Task>>();
            _subscriptions.Add(key, handlers);
        }

        handlers.Add(@event => handler((TEvent) @event));
    }

    public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
    {
        var key = @event.GetType();

        if (!_subscriptions.TryGetValue(key, out var handlers))
        {
            return;
        }

        foreach (var handler in handlers)
        {
            await handler(@event);
        }
    }
}