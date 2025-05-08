namespace MiniEShopAPI.Services;

/**
 * ICache defines the contract for caching mechanisms.
 */
public interface ICache
{
    T? Get<T>(string key); // Retrieves a value from the cache by key
    void Set<T>(string key, T value, TimeSpan expiration); // Stores a value in the cache with an expiration time
    void Remove(string key); // Removes a value from the cache by key
}