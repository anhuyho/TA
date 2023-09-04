namespace Service1.API.Repositories.Contracts;

public class UnitOfWork : IUnitOfWork
{
    private readonly IRepositoryContext _context;

    public UnitOfWork(IRepositoryContext context)
    {
        _context = context;
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}