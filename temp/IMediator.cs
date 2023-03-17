namespace Core;

public interface IMediator
{
    void Register<TRequest, TResponse, THandler>()
        where TRequest : ICommand<TResponse>
        where THandler : IHandler<TRequest, TResponse>;

    Task<TResponse> SendAsync<TResponse>(ICommand<TResponse> command);
}