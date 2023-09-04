using Service1.Models.Dtos;

namespace Service2.SERVICE.Services.Contracts;

public interface IReportServices
{
    Task<ICollection<EmployeeResponse>?> GetEmployeesAsync();
    void CreateEmployeesReport(ICollection<EmployeeResponse>? employee);
}