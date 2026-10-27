using Microsoft.EntityFrameworkCore;
using para.Models;

namespace para.Repositories;

public class ProductRepository(Practos2AspContext context) : Repository<Product>(context), IProductRepository
{
    public async Task<(List<Product> Items, int TotalCount)> CatalogAsync(string? search, int? categoryId, string? sort, int page, int pageSize)
    {
        var query = Context.Products.Include(p => p.Category).AsNoTracking();
        if (!string.IsNullOrWhiteSpace(search)) query = query.Where(p => p.ProductName.Contains(search));
        if (categoryId > 0) query = query.Where(p => p.CategoryID == categoryId);
        var count = await query.CountAsync();
        query = sort switch
        {
            "name_asc" => query.OrderBy(p => p.ProductName).ThenBy(p => p.ProductID),
            "name_desc" => query.OrderByDescending(p => p.ProductName).ThenBy(p => p.ProductID),
            "price_asc" => query.OrderBy(p => p.Price).ThenBy(p => p.ProductID),
            "price_desc" => query.OrderByDescending(p => p.Price).ThenBy(p => p.ProductID),
            _ => query.OrderBy(p => p.ProductID)
        };
        var offset = ((long)page - 1) * pageSize;
        return (offset > int.MaxValue ? [] : await query.Skip((int)offset).Take(pageSize).ToListAsync(), count);
    }
}
