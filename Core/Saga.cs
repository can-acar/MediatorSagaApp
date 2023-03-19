using Core.Interfaces;

namespace Core;

public class Saga<TData> : ISaga<TData> where TData : class, new()
{
    private readonly IMessageBus _messageBus;
    private readonly Dictionary<Type, Func<IEvent, Task>> _eventHandlers = new Dictionary<Type, Func<IEvent, Task>>();

    public TData Data { get; protected set; }

    public async Task StartAsync()
    {
        foreach (var eventHandler in _eventHandlers)
        {
            await _messageBus.SubscribeAsync(async @event =>
            {
                var eventType = @event.GetType();

                if (_eventHandlers.TryGetValue(eventType, out var handler))
                {
                    await handler(@event);
                }
            });
        }
    }

    public async Task StopAsync()
    {
        foreach (var eventHandler in _eventHandlers)
        {
            await _messageBus.UnSubscribeAsync(async @event =>
            {
                var eventType = @event.GetType();

                if (_eventHandlers.TryGetValue(eventType, out var handler))
                {
                    await handler(@event);
                }
            });
        }
    }


    public Saga(IMessageBus messageBus)
    {
        _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
    }


    public void RegisterEventHandler<TEvent>(Func<TEvent, Task> handler) where TEvent : IEvent
    {
        var eventType = typeof(TEvent);
        _eventHandlers[eventType] = @event => handler((TEvent) @event);
    }

    private async Task HandleEventAsync(IEvent @event)
    {
        if (_eventHandlers.TryGetValue(@event.GetType(), out var handler))
        {
            await handler(@event);
        }
    }

    protected void SubscribeToEvent<TEvent>() where TEvent : IEvent
    {
        _messageBus.Subscribe<TEvent>(async @event =>
        {
            var eventType = @event.GetType();

            if (_eventHandlers.TryGetValue(eventType, out var handler))
            {
                await handler(@event);
            }
        });
    }
}