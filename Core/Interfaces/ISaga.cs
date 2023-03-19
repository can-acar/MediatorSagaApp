namespace Core.Interfaces;

public interface ISaga<TData> where TData : class, new()
{
    TData Data { get; }

    Task StartAsync();

    Task StopAsync();
}

public interface ISagaState
{
    Guid Id { get; }
}