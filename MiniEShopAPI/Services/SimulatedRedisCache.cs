using System.Threading;

namespace MiniEShopAPI.Services;

/**
 * SimulatedRedisCache simulates a Redis-based caching mechanism.
 */
public class SimulatedRedisCache : ICache
{
    private readonly Dictionary<string, (object? Value, DateTime Expiration)> _cache = new(); // Dictionary for storing cache entries
    private readonly TimeSpan _operationDelay = TimeSpan.FromMilliseconds(100); // Simulates network delay for Redis operations

    public T? Get<T>(string key)
    {
        Thread.Sleep(_operationDelay); // Simulates delay
        if (_cache.TryGetValue(key, out var entry) && entry.Expiration > DateTime.UtcNow) // Checks if the key exists and is not expired
        {
            return (T)entry.Value!; // Returns the cached value
        }
        return default; // Returns default if the key is not found or expired
    }

    public void Set<T>(string key, T value, TimeSpan expiration)
    {
        Thread.Sleep(_operationDelay); // Simulates delay
        var expirationTime = DateTime.UtcNow.Add(expiration); // Calculates the expiration time
        _cache[key] = (value, expirationTime); // Adds or updates the cache entry
    }

    public void Remove(string key)
    {
        Thread.Sleep(_operationDelay); // Simulates delay
        _cache.Remove(key); // Removes the cache entry by key
    }
}