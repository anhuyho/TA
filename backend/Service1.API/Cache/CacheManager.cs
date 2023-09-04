using Microsoft.Extensions.Options;
using Service1.API.Cache.Contracts;
using Service1.API.Models;

namespace Service1.API.Cache;

public class CacheManager : ICacheManager
{
    private readonly Lazy<IEmployeeCache> _lazyEmployeeCache;

    public CacheManager(ICacheContext cacheContext,
        IOptions<CacheConfigure> configure)
    {
        _lazyEmployeeCache = new Lazy<IEmployeeCache>(() => new EmployeeCache(cacheContext, configure));
    }

    public IEmployeeCache EmployeeCache => _lazyEmployeeCache.Value;
}