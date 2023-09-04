using Microsoft.Extensions.Caching.Memory;
using Service1.API.Cache.Contracts;

namespace Service1.API.Cache;

public class CacheContext : ICacheContext
{
    private readonly IMemoryCache _cache;

    public CacheContext(IMemoryCache cache)
    {
        _cache = cache;
    }

    public T? GetCache<T>(string key) where T : class
    {
        _cache.TryGetValue(key, out T? cachedResponse);
        return cachedResponse;
    }

    public void SetCache<T>(string key, T value, MemoryCacheEntryOptions options) where T : class
    {
        _cache.Set(key, value, options);
    }

    public void ClearCache(string key)
    {
        _cache.Remove(key);
    }
}