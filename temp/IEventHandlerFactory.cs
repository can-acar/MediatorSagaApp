namespace Core;

public interface IEventHandlerFactory
{
    public void RegisterHandler<TEvent, THandler>() where TEvent : IEvent where THandler : IEventHandler<TEvent>;
    public IEventHandler<TEvent> GetHandlerForEvent<TEvent>() where TEvent : IEvent;
}