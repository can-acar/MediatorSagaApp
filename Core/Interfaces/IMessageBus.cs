namespace Core.Interfaces;

public interface IMessageBus
{
    void Subscribe<T>(string channel, Action<T> handler);
    void Publish<T>(string channel, T message);

    void Receive<T>(string channel, Action<T> handler);
}