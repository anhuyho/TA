using Service1.API.Repositories.Contracts;

namespace Service1.API.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IEmployeeRepository> _lazyEmployeeRepository;
    private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;

    public RepositoryManager(IRepositoryContext context)
    {
        _lazyEmployeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(context));
        _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(context));
    }

    public IEmployeeRepository EmployeeRepository => _lazyEmployeeRepository.Value;
    public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
}