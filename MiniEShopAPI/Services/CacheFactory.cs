namespace MiniEShopAPI.Services;

/**
 * CacheFactory provides a factory method to create cache instances.
 */
public static class CacheFactory
{
    public static ICache CreateCache(string cacheType)
    {
        return cacheType.ToLower() switch
        {
            "inmemory" => new InMemoryCache(), // Returns an in-memory cache instance
            "redis" => new SimulatedRedisCache(), // Returns a simulated Redis cache instance
            _ => throw new ArgumentException("Invalid cache type") // Throws an exception for invalid cache types
        };
    }
}