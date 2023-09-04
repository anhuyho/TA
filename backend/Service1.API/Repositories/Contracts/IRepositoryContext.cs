namespace Service1.API.Repositories.Contracts;

public interface IRepositoryContext
{
    List<T>? Set<T>() where T : class;
    void SaveChanges();
}