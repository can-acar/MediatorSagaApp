namespace Core;

public interface ISaga<TData> where TData : class, new()
{
    TData Data { get; }

    Task StartAsync();
}