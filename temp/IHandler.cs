namespace Core;

public interface IHandler<TRequest, TResponse> where TRequest : ICommand<TResponse>
{
    Task<TResponse> HandleAsync(TRequest command);
}