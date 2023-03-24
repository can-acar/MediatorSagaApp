using System.Collections.Concurrent;
using Core.Interfaces;

namespace Core;

public class InMemoryMessageBus
{
    private readonly ConcurrentDictionary<string, List<Delegate>> _subscriptions;

    public InMemoryMessageBus()
    {
        _subscriptions = new ConcurrentDictionary<string, List<Delegate>>();
    }


    public void Publish<T>(string channel, T message)
    {
        if (string.IsNullOrEmpty(channel))
            throw new ArgumentException("Channel name cannot be null or empty.", nameof(channel));

        if (_subscriptions.TryGetValue(channel, out var subscribers))
        {
            foreach (var subscriber in subscribers)
            {
                var typedSubscriber = (Action<T>) subscriber;
                typedSubscriber(message);
            }
        }
    }


    public void Subscribe<T>(string channel, Action<T> callback)
    {
        if (string.IsNullOrEmpty(channel))
            throw new ArgumentException("Channel name cannot be null or empty.", nameof(channel));

        if (callback == null)
            throw new ArgumentNullException(nameof(callback));

        var subscribers = _subscriptions.GetOrAdd(channel, _ => new List<Delegate>());
        lock (subscribers)
        {
            subscribers.Add(callback);
        }
    }

    public async Task<T> Receive<T>(string channel, CancellationToken cancellationToken = default)
    {
        var tcs = new TaskCompletionSource<T>();

        void Handler(T message)
        {
            tcs.TrySetResult(message);
            Unsubscribe<T>(channel, Handler);
        }

        Subscribe<T>(channel, Handler);

        using (cancellationToken.Register(() => tcs.TrySetCanceled()))
        {
            return await tcs.Task.ConfigureAwait(false);
        }
    }

    public void Unsubscribe<T>(string channel, Action<T> callback)
    {
        if (string.IsNullOrEmpty(channel))
            throw new ArgumentException("Channel name cannot be null or empty.", nameof(channel));

        if (callback == null)
            throw new ArgumentNullException(nameof(callback));

        if (_subscriptions.TryGetValue(channel, out var subscribers))
        {
            lock (subscribers)
            {
                subscribers.Remove(callback);
            }
        }
    }
}