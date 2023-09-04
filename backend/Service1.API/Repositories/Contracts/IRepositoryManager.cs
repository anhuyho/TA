namespace Service1.API.Repositories.Contracts;

public interface IRepositoryManager
{
    public IEmployeeRepository EmployeeRepository { get; }
    public IUnitOfWork UnitOfWork { get; }
}