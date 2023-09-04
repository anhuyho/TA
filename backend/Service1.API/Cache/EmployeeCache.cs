using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Service1.API.Cache.Contracts;
using Service1.API.Entities;
using Service1.API.Models;

namespace Service1.API.Cache;

public class EmployeeCache : IEmployeeCache
{
    private readonly ICacheContext _cacheContext;
    private readonly MemoryCacheEntryOptions _cacheEntryOptions;

    public EmployeeCache(ICacheContext cacheContext, IOptions<CacheConfigure> configuration)
    {
        _cacheContext = cacheContext;
        var cacheConfigure = configuration.Value;
        _cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromSeconds(cacheConfigure.SlidingExpiration))
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(cacheConfigure.AbsoluteExpiration));
    }

    public ICollection<Employee>? GetEmployees()
    {
        return _cacheContext.GetCache<ICollection<Employee>>(CacheKeys.Employees);
    }

    public void SetEmployees(ICollection<Employee> employees)
    {
        _cacheContext.SetCache(CacheKeys.Employees, employees, _cacheEntryOptions);
    }

    public void ClearCache()
    {
        _cacheContext.ClearCache(CacheKeys.Employees);
    }
}