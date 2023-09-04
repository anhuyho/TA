using Service1.API.Entities;

namespace Service1.API.Repositories.Contracts;

public interface IEmployeeRepository
{
    ICollection<Employee> GetEmployees();
    Employee? GetEmployee(Guid id);
    void CreateEmployee(Employee employee);
    void UpdateEmployee(Employee employee);
    void DeleteEmployee(Employee employee);
}