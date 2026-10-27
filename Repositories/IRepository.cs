using System.Linq.Expressions;

namespace para.Repositories;

public interface IRepository<T> where T : class
{
    Task<List<T>> ListAsync(Expression<Func<T, bool>>? filter = null, CancellationToken ct = default);
    Task<List<T>> PageAsync(int page, int pageSize, CancellationToken ct = default);
    Task<List<T>> PageAsync(int page, int pageSize, Expression<Func<T, bool>>? filter, CancellationToken ct = default);
    Task<int> CountAsync(CancellationToken ct = default);
    Task<int> CountAsync(Expression<Func<T, bool>> filter, CancellationToken ct = default);
    Task<T?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<T?> FirstAsync(Expression<Func<T, bool>> filter, CancellationToken ct = default);
    Task<bool> AnyAsync(Expression<Func<T, bool>> filter, CancellationToken ct = default);
    void Add(T entity);
    void Remove(T entity);
    Task SaveChangesAsync(CancellationToken ct = default);
}
