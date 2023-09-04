using Service1.API.Entities;

namespace Service1.API.Cache.Contracts;

public interface IEmployeeCache
{
    ICollection<Employee>? GetEmployees();
    void SetEmployees(ICollection<Employee> employees);
    void ClearCache();
}