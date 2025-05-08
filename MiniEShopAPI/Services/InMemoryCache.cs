using System.Collections.Concurrent;

namespace MiniEShopAPI.Services;

/**
 * InMemoryCache provides an in-memory implementation of the ICache interface.
 */
public class InMemoryCache : ICache
{
    private readonly ConcurrentDictionary<string, (object? Value, DateTime Expiration)> _cache = new(); // Thread-safe dictionary for storing cache entries

    public T? Get<T>(string key)
    {
        if (_cache.TryGetValue(key, out var entry) && entry.Expiration > DateTime.UtcNow) // Checks if the key exists and is not expired
        {
            return (T)entry.Value!; // Returns the cached value
        }
        return default; // Returns default if the key is not found or expired
    }

    public void Set<T>(string key, T value, TimeSpan expiration)
    {
        var expirationTime = DateTime.UtcNow.Add(expiration); // Calculates the expiration time
        _cache[key] = (value, expirationTime); // Adds or updates the cache entry
    }

    public void Remove(string key)
    {
        _cache.TryRemove(key, out _); // Removes the cache entry by key
    }
}