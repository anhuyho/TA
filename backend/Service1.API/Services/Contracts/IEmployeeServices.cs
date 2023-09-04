using Service1.API.Models.Dtos;

namespace Service1.API.Services.Contracts;

public interface IEmployeeServices
{
    ICollection<EmployeeResponseDto> GetEmployees();
    EmployeeResponseDto? GetEmployee(Guid id);
    Guid CreateEmployee(EmployeeRequestDto employee);
    void UpdateEmployee(Guid id, EmployeeRequestDto employee);
    void DeleteEmployee(Guid id);
}