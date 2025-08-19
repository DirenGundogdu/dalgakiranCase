namespace Application.Interfaces;

public interface IRepository<T> where T : class
{
    Task Create(T entity);
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(int id);
    Task Update(T entity);
    Task Delete(T entity);
    Task<T> Find(Func<T, bool> predicate);
}