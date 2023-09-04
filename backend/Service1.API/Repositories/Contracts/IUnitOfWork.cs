namespace Service1.API.Repositories.Contracts;

public interface IUnitOfWork
{
    void SaveChanges();
}