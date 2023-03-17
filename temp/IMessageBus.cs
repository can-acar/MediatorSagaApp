namespace Core;

public interface IMessageBus
{
    Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent;
}