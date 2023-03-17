namespace Core;

public interface IRequestHandler<in TRequest, TResponse> where TRequest : IRequest<TResponse>, IResponse
{
    Task<TResponse> HandleAsync(TRequest request);
}