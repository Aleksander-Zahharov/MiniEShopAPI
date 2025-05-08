/**
 * CacheTests contains unit tests for caching mechanisms.
 */
using System;
using Xunit;
using MiniEShopAPI.Services;

public class CacheTests
{
    [Fact]
    public void InMemoryCache_ShouldStoreAndRetrieveValues()
    {
        // Arrange
        var cache = new InMemoryCache();
        var key = "testKey";
        var value = "testValue";

        // Act
        cache.Set(key, value, TimeSpan.FromMinutes(1));
        var retrievedValue = cache.Get<string>(key);

        // Assert
        Assert.Equal(value, retrievedValue);
    }

    [Fact]
    public void InMemoryCache_ShouldRemoveValues()
    {
        // Arrange
        var cache = new InMemoryCache();
        var key = "testKey";
        var value = "testValue";

        // Act
        cache.Set(key, value, TimeSpan.FromMinutes(1));
        cache.Remove(key);
        var retrievedValue = cache.Get<string>(key);

        // Assert
        Assert.Null(retrievedValue);
    }

    [Fact]
    public void SimulatedRedisCache_ShouldStoreAndRetrieveValues()
    {
        // Arrange
        var cache = new SimulatedRedisCache();
        var key = "testKey";
        var value = "testValue";

        // Act
        cache.Set(key, value, TimeSpan.FromMinutes(1));
        var retrievedValue = cache.Get<string>(key);

        // Assert
        Assert.Equal(value, retrievedValue);
    }

    [Fact]
    public void SimulatedRedisCache_ShouldRemoveValues()
    {
        // Arrange
        var cache = new SimulatedRedisCache();
        var key = "testKey";
        var value = "testValue";

        // Act
        cache.Set(key, value, TimeSpan.FromMinutes(1));
        cache.Remove(key);
        var retrievedValue = cache.Get<string>(key);

        // Assert
        Assert.Null(retrievedValue);
    }
}