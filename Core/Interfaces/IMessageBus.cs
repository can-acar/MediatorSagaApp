namespace Core.Interfaces;

public interface IMessageBus
{
    void Subscribe<TEvent>(Func<TEvent, Task> handler) where TEvent : IEvent;
    Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent;
    Task SubscribeAsync(Func<IEvent, Task> handler);
    Task UnSubscribeAsync(Func<IEvent, Task> handler);
    
}