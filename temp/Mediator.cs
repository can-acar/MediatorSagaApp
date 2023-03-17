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


    public void Register<TRequest, TResponse, THandler>()
        where TRequest : ICommand<TResponse> where THandler : IHandler<TRequest, TResponse>
    {
        _handlerFactory.RegisterHandler<TRequest, TResponse, THandler>();
    }

    public async Task<TResponse> SendAsync<TResponse>(ICommand<TResponse> command)
    {
      
    }

    public void Register<TEvent, THandler>() where TEvent : IEvent where THandler : IEventHandler<TEvent>
    {
        _eventHandlerFactory.RegisterHandler<TEvent, THandler>();
    }

    public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
    {
        var handler = _eventHandlerFactory.GetHandlerForEvent<TEvent>();
        await handler.HandleAsync(@event);
    }
}