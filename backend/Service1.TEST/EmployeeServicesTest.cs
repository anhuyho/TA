using AutoMapper;
using Moq;
using Service1.API.Cache.Contracts;
using Service1.API.Entities;
using Service1.API.Models.Dtos;
using Service1.API.Repositories.Contracts;
using Service1.API.Services;

namespace Service1.TEST;

public class EmployeeServicesTest
{
    private readonly Mock<ICacheManager> _cacheManager;
    private readonly Mock<IEmployeeCache> _employeeCache;
    private readonly Mock<IEmployeeRepository> _employeeRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IRepositoryManager> _repositoryManager;
    private readonly Mock<IUnitOfWork> _unitOfWork;

    public EmployeeServicesTest()
    {
        _cacheManager = new Mock<ICacheManager>();
        _employeeCache = new Mock<IEmployeeCache>();
        _repositoryManager = new Mock<IRepositoryManager>();
        _employeeRepository = new Mock<IEmployeeRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _mapper = new Mock<IMapper>();
    }

    [Fact]
    public void GetEmployees_WhenCalled_ReturnICollection()
    {
        //Arrange
        var dtos = GetEmployeesReadDtos();
        _mapper.Setup(m => m.Map<ICollection<EmployeeResponseDto>>(It.IsAny<ICollection<Employee>>()))
            .Returns(dtos);
        _cacheManager.Setup(c => c.EmployeeCache)
            .Returns(_employeeCache.Object);
        _repositoryManager.Setup(m => m.EmployeeRepository)
            .Returns(_employeeRepository.Object);
        _repositoryManager.Setup(m => m.UnitOfWork)
            .Returns(_unitOfWork.Object);
        _repositoryManager.Setup(m => m.EmployeeRepository.GetEmployees())
            .Returns(new List<Employee>());
        var employeeServices = new EmployeeServices(_cacheManager.Object, _repositoryManager.Object, _mapper.Object);

        //Act
        var result = employeeServices.GetEmployees();

        //Assert
        Assert.True(result.Count == 1);
        Assert.True(result.First().Salary == 2000);
    }

    [Fact]
    public void GetEmployee_IdWasNotFound_ReturnArgumentException()
    {
        //Arrange
        _mapper.Setup(m => m.Map<EmployeeResponseDto>(It.IsAny<Employee>()))
            .Returns(new EmployeeResponseDto());
        _cacheManager.Setup(c => c.EmployeeCache)
            .Returns(_employeeCache.Object);
        _repositoryManager.Setup(m => m.EmployeeRepository)
            .Returns(_employeeRepository.Object);
        _repositoryManager.Setup(m => m.UnitOfWork)
            .Returns(_unitOfWork.Object);
        var employeeServices = new EmployeeServices(_cacheManager.Object, _repositoryManager.Object, _mapper.Object);

        //Act & Assert
        Assert.Throws<ArgumentException>(() => employeeServices.GetEmployee(Guid.NewGuid()));
    }

    [Fact]
    public void GetEmployee_WhenCalled_ReturnEmployeeReadDto()
    {
        //Arrange
        _mapper.Setup(m => m.Map<EmployeeResponseDto>(It.IsAny<Employee>()))
            .Returns(new EmployeeResponseDto());
        _cacheManager.Setup(c => c.EmployeeCache)
            .Returns(_employeeCache.Object);
        _repositoryManager.Setup(m => m.EmployeeRepository)
            .Returns(_employeeRepository.Object);
        _repositoryManager.Setup(m => m.UnitOfWork)
            .Returns(_unitOfWork.Object);
        _repositoryManager.Setup(m => m.EmployeeRepository.GetEmployee(It.IsAny<Guid>()))
            .Returns(new Employee());
        var employeeServices = new EmployeeServices(_cacheManager.Object, _repositoryManager.Object, _mapper.Object);

        //Act
        var result = employeeServices.GetEmployee(Guid.NewGuid());

        //Assert
        Assert.IsType<EmployeeResponseDto>(result);
    }

    [Fact]
    public void CreateEmployee_WhenCalled_ReturnGuid()
    {
        //Arrange
        _mapper.Setup(m => m.Map<Employee>(It.IsAny<EmployeeRequestDto>()))
            .Returns(new Employee());
        _cacheManager.Setup(c => c.EmployeeCache)
            .Returns(_employeeCache.Object);
        _repositoryManager.Setup(m => m.EmployeeRepository).Returns(_employeeRepository.Object);
        _repositoryManager.Setup(m => m.UnitOfWork).Returns(_unitOfWork.Object);
        var employeeServices = new EmployeeServices(_cacheManager.Object, _repositoryManager.Object, _mapper.Object);

        //Act
        var result = employeeServices.CreateEmployee(new EmployeeRequestDto());

        //Assert
        Assert.IsType<Guid>(result);
    }

    [Fact]
    public void UpdateEmployee_IdWasNotFound_ReturnArgumentException()
    {
        //Arrange
        _mapper.Setup(m => m.Map<Employee>(It.IsAny<EmployeeRequestDto>()))
            .Returns(new Employee());
        _cacheManager.Setup(c => c.EmployeeCache)
            .Returns(_employeeCache.Object);
        _repositoryManager.Setup(m => m.EmployeeRepository).Returns(_employeeRepository.Object);
        _repositoryManager.Setup(m => m.UnitOfWork).Returns(_unitOfWork.Object);
        var employeeServices = new EmployeeServices(_cacheManager.Object, _repositoryManager.Object, _mapper.Object);

        //Act & Assert
        Assert.Throws<ArgumentException>(() => employeeServices.UpdateEmployee(Guid.NewGuid(), new EmployeeRequestDto()));
    }

    [Fact]
    public void UpdateEmployee_employeeDtoIsNull_ReturnNullReferenceException()
    {
        //Arrange
        _mapper.Setup(m => m.Map<Employee>(It.IsAny<EmployeeRequestDto>()))
            .Returns(new Employee());
        _cacheManager.Setup(c => c.EmployeeCache)
            .Returns(_employeeCache.Object);
        _repositoryManager.Setup(m => m.EmployeeRepository).Returns(_employeeRepository.Object);
        _repositoryManager.Setup(m => m.UnitOfWork).Returns(_unitOfWork.Object);
        _repositoryManager.Setup(m => m.EmployeeRepository.GetEmployee(It.IsAny<Guid>()))
            .Returns(new Employee());
        EmployeeRequestDto? employeeDto = null;
        var employeeServices = new EmployeeServices(_cacheManager.Object, _repositoryManager.Object, _mapper.Object);

        //Act & Assert
        Assert.Throws<NullReferenceException>(() => employeeServices.UpdateEmployee(Guid.NewGuid(), employeeDto!));
    }

    [Fact]
    public void DeleteEmployee_IdWasNotFound_ReturnArgumentException()
    {
        //Arrange
        _mapper.Setup(m => m.Map<Employee>(It.IsAny<EmployeeRequestDto>()))
            .Returns(new Employee());
        _cacheManager.Setup(c => c.EmployeeCache)
            .Returns(_employeeCache.Object);
        _repositoryManager.Setup(m => m.EmployeeRepository).Returns(_employeeRepository.Object);
        _repositoryManager.Setup(m => m.UnitOfWork).Returns(_unitOfWork.Object);
        var employeeServices = new EmployeeServices(_cacheManager.Object, _repositoryManager.Object, _mapper.Object);

        //Act & Assert
        Assert.Throws<ArgumentException>(() => employeeServices.DeleteEmployee(Guid.NewGuid()));
    }

    private ICollection<EmployeeResponseDto> GetEmployeesReadDtos()
    {
        return new List<EmployeeResponseDto>
        {
            new EmployeeResponseDto()
            {
                Id = Guid.NewGuid(),
                HiringDate = new DateOnly(2022, 1, 1),
                Name = "Tho Le",
                Position = "IT",
                Salary = 2000
            }, new EmployeeResponseDto{
                Id = Guid.NewGuid(),
                HiringDate = new DateOnly(2022, 1, 2),
                Name = "David",
                Position = "HR",
                Salary = 2500
            }
        };
    }
}