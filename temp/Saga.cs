namespace Core;

public abstract class Saga<TData> : ISaga<TData> where TData : class, new()
{
    private readonly Dictionary<Type, Func<IEvent, Task>> _eventHandlers = new Dictionary<Type, Func<IEvent, Task>>();
    private readonly IMessageBus _messageBus;

    public TData Data { get; } = new TData();

    public Saga(IMessageBus messageBus)
    {
        _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
    }

    public async Task StartAsync()
    {
        foreach (var eventType in _eventHandlers.Keys)
        {
            await _messageBus.SubscribeAsync(eventType, HandleEventAsync);
        }
    }

    public async Task StopAsync()
    {
        foreach (var eventType in _eventHandlers.Keys)
        {
            await _messageBus.UnsubscribeAsync(eventType, HandleEventAsync);
        }
    }

    protected void RegisterEventHandler<TEvent>(Func<TEvent, Task> handler) where TEvent : IEvent
    {
        if (handler == null)
        {
            throw new ArgumentNullException(nameof(handler));
        }

        _eventHandlers[typeof(TEvent)] = e => handler((TEvent) e);
    }

    private async Task HandleEventAsync(IEvent @event)
    {
        if (_eventHandlers.TryGetValue(@event.GetType(), out var handler))
        {
            await handler(@event);
        }
    }
}