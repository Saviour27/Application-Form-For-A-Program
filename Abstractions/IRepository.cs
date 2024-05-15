namespace Program_Application_Form.Abstractions;
public interface IRepository<T>
{
    Task AddAsync(T entity);
    Task<T> GetByIdAsync(string id);
    Task<IEnumerable<T>> GetAllAsync();
    Task UpdateAsync(T entity);
    Task DeleteAsync(string id);
}
