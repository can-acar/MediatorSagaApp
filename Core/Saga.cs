using Core.Interfaces;

namespace Core;

public class Saga<TData> : ISaga<TData> where TData : class, new()
{
}