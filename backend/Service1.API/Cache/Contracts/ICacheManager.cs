namespace Service1.API.Cache.Contracts;

public interface ICacheManager
{
    public IEmployeeCache EmployeeCache { get; }
}