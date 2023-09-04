using System.Linq.Expressions;

namespace Service1.API.Repositories.Contracts;

public interface IRepositoryBase<T>
{
    IEnumerable<T>? FindAll();
    IEnumerable<T>? FindByCondition(Expression<Func<T, bool>> expression);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}