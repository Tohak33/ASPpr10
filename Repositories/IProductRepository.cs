using para.Models;

namespace para.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<(List<Product> Items, int TotalCount)> CatalogAsync(string? search, int? categoryId, string? sort, int page, int pageSize);
}
