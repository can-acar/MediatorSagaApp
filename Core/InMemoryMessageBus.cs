using Core.Interfaces;

namespace Core;

public class InMemoryMessageBus : IMessageBus
{
    private readonly SubscriptionManager _subscriptionManager;

    public InMemoryMessageBus()
    {
        _subscriptionManager = new SubscriptionManager();
    }

    public void Subscribe<TEvent>(Func<TEvent, Task> handler) where TEvent : IEvent
    {
        _subscriptionManager.AddSubscription(handler);
    }

    public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
    {
        var handlers = _subscriptionManager.GetHandlersForEvent<TEvent>();

        foreach (var handler in handlers)
        {
            await handler(@event);
        }
    }

    public async Task SubscribeAsync(Func<IEvent, Task> handler)
    {
        _subscriptionManager.AddSubscription(handler);

        await Task.CompletedTask;
    }

    public Task UnSubscribeAsync(Func<IEvent, Task> handler)
    {
        _subscriptionManager.RemoveSubscription(handler);

        return Task.CompletedTask;
    }
}