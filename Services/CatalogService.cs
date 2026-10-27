using para.Models;
using para.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace para.Services;

public class CatalogService(IProductRepository products, IRepository<Category> categories, IMemoryCache cache) : ICatalogService
{
    public const string CategoryCacheKey = "CatalogCategories";

    public async Task<CatalogResult> GetAsync(string? search, int? categoryId, string? sort, int page)
    {
        page = Math.Max(1, page);
        var result = await products.CatalogAsync(search, categoryId, sort, page, 6);
        var list = await cache.GetOrCreateAsync(CategoryCacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            return await categories.ListAsync();
        });
        return new(result.Items, list!, page, (int)Math.Ceiling(result.TotalCount / 6.0));
    }
}
