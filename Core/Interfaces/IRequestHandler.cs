namespace Core.Interfaces;

public interface IRequestHandler<in TRequest, TResponse> where TRequest : IRequest<TResponse>
{
}