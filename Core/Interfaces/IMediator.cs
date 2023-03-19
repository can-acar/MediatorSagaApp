namespace Core.Interfaces;

public interface IMediator
{
    Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request);
    Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent;

    void RegisterHandler<TRequest, TResponse>(Func<TRequest, Task<TResponse>> handler) where TRequest : IRequest<TResponse>;
}