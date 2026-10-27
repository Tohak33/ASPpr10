using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using para.Models;

namespace para.Repositories;

public class Repository<T>(Practos2AspContext context) : IRepository<T> where T : class
{
    protected Practos2AspContext Context { get; } = context;

    private IQueryable<T> Query()
    {
        IQueryable<T> query = Context.Set<T>();
        if (typeof(T) == typeof(Product)) query = query.Include("Category");
        if (typeof(T) == typeof(Category)) query = query.Include("Products");
        if (typeof(T) == typeof(Role)) query = query.Include("Users");
        if (typeof(T) == typeof(User)) query = query.Include("Role");
        if (typeof(T) == typeof(Cart)) query = query.Include("User.Role").Include("CartItems.Product.Category");
        if (typeof(T) == typeof(CartItem)) query = query.Include("Cart").Include("Product.Category");
        if (typeof(T) == typeof(Order)) query = query.Include("User.Role").Include("OrderItems.Product.Category");
        if (typeof(T) == typeof(OrderItem)) query = query.Include("Order").Include("Product.Category");
        if (typeof(T) == typeof(ProductReservation)) query = query.Include("User.Role").Include("Product.Category");
        return query;
    }

    private string Key => Context.Model.FindEntityType(typeof(T))!.FindPrimaryKey()!.Properties.Single().Name;

    public Task<List<T>> ListAsync(Expression<Func<T, bool>>? filter = null, CancellationToken ct = default)
    {
        var query = Query();
        if (filter != null) query = query.Where(filter);
        return query.OrderBy(entity => EF.Property<int>(entity, Key)).ToListAsync(ct);
    }

    public async Task<List<T>> PageAsync(int page, int pageSize, CancellationToken ct = default)
        => await PageAsync(page, pageSize, null, ct);

    public async Task<List<T>> PageAsync(int page, int pageSize, Expression<Func<T, bool>>? filter, CancellationToken ct = default)
    {
        var offset = ((long)page - 1) * pageSize;
        if (offset > int.MaxValue) return [];
        var query = Query();
        if (filter != null) query = query.Where(filter);
        return await query.OrderBy(entity => EF.Property<int>(entity, Key))
            .Skip((int)offset).Take(pageSize).ToListAsync(ct);
    }

    public Task<int> CountAsync(CancellationToken ct = default) => Context.Set<T>().CountAsync(ct);
    public Task<int> CountAsync(Expression<Func<T, bool>> filter, CancellationToken ct = default) => Context.Set<T>().CountAsync(filter, ct);
    public Task<T?> GetByIdAsync(int id, CancellationToken ct = default) =>
        Query().FirstOrDefaultAsync(entity => EF.Property<int>(entity, Key) == id, ct);
    public Task<T?> FirstAsync(Expression<Func<T, bool>> filter, CancellationToken ct = default) => Query().FirstOrDefaultAsync(filter, ct);
    public Task<bool> AnyAsync(Expression<Func<T, bool>> filter, CancellationToken ct = default) => Context.Set<T>().AnyAsync(filter, ct);
    public void Add(T entity) => Context.Set<T>().Add(entity);
    public void Remove(T entity) => Context.Set<T>().Remove(entity);

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        try
        {
            await Context.SaveChangesAsync(ct);
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new StoreException(StoreError.Conflict, "Данные уже изменены другим запросом. Обновите страницу и повторите действие.");
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException { Number: 547 or 2601 or 2627 })
        {
            throw new StoreException(StoreError.Conflict, "Запись уже существует или используется в связанных данных.");
        }
    }
}
