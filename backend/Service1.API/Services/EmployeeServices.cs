using AutoMapper;
using Service1.API.Cache.Contracts;
using Service1.API.Entities;
using Service1.API.Models.Dtos;
using Service1.API.Repositories.Contracts;
using Service1.API.Services.Contracts;

namespace Service1.API.Services;

public class EmployeeServices : IEmployeeServices
{
    private static readonly object lockObj = new();
    private readonly IEmployeeCache _employeeCache;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeServices(ICacheManager cacheManager,
        IRepositoryManager repositoryManager,
        IMapper mapper)
    {
        _employeeCache = cacheManager.EmployeeCache;
        _employeeRepository = repositoryManager.EmployeeRepository;
        _unitOfWork = repositoryManager.UnitOfWork;
        _mapper = mapper;
    }

    public ICollection<EmployeeResponseDto> GetEmployees()
    {
        var employees = _employeeCache.GetEmployees();
        if (employees != null) return _mapper.Map<ICollection<EmployeeResponseDto>>(employees);

        try
        {
            Monitor.Enter(lockObj);
            employees = _employeeRepository.GetEmployees();
            _employeeCache.SetEmployees(employees);
        }
        finally
        {
            Monitor.Exit(lockObj);
        }

        return _mapper.Map<ICollection<EmployeeResponseDto>>(employees);
    }

    public EmployeeResponseDto? GetEmployee(Guid id)
    {
        var employee = _employeeRepository.GetEmployee(id);
        if (employee == null) throw new ArgumentException("Employee Id was not found");
        return _mapper.Map<EmployeeResponseDto>(employee);
    }

    public Guid CreateEmployee(EmployeeRequestDto employee)
    {
        var empl = _mapper.Map<Employee>(employee);
        empl.Id = Guid.NewGuid();
        _employeeRepository.CreateEmployee(empl);
        _unitOfWork.SaveChanges();
        _employeeCache.ClearCache();
        return empl.Id;
    }

    public void UpdateEmployee(Guid id, EmployeeRequestDto employee)
    {
        var curEmpl = _employeeRepository.GetEmployee(id);
        if (curEmpl == null) throw new ArgumentException("Employee Id was not found");
        if (employee == null) throw new NullReferenceException("Employee is required");
        var empl = _mapper.Map<Employee>(employee);
        empl.Id = id;
        _employeeRepository.UpdateEmployee(empl);
        _unitOfWork.SaveChanges();
        _employeeCache.ClearCache();
    }

    public void DeleteEmployee(Guid id)
    {
        var empl = _employeeRepository.GetEmployee(id);
        if (empl == null) throw new ArgumentException("Employee Id was not found");
        _employeeRepository.DeleteEmployee(empl);
        _unitOfWork.SaveChanges();
        _employeeCache.ClearCache();
    }
}