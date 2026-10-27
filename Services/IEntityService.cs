namespace para.Services;

public interface IEntityService<T> where T : class
{
    Task<List<T>> ListAsync();
    Task<T?> GetAsync(int id);
    Task CreateAsync(T entity);
    Task<bool> UpdateAsync(int id, T entity);
    Task<bool> DeleteAsync(int id);
}
