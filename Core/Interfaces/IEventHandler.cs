namespace Core.Interfaces;

public interface IEventHandler<in TEvent> where TEvent : IEvent
{
}