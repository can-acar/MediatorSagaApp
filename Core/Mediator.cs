using Core.Interfaces;


namespace Core;

public class Mediator : IMediator
{
    private readonly IRequestHandlerFactory _handlerFactory;
    private readonly IMessageBus _messageBus;
    private readonly IEventHandlerFactory _eventHandlerFactory;

    public Mediator(IRequestHandlerFactory handlerFactory, IMessageBus messageBus, IEventHandlerFactory eventHandlerFactory)
    {
        _handlerFactory = handlerFactory;
        _messageBus = messageBus;
        _eventHandlerFactory = eventHandlerFactory;
    }

    public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        throw new NotImplementedException();
    }

    public Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
    {
        throw new NotImplementedException();
    }

    public void RegisterHandler<TRequest, TResponse>(Func<TRequest, Task<TResponse>> handler) where TRequest : IRequest<TResponse>
    {
        throw new NotImplementedException();
    }
}