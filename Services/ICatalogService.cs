using para.Models;

namespace para.Services;

public record CatalogResult(List<Product> Items, List<Category> Categories, int PageNumber, int TotalPages);

public interface ICatalogService
{
    Task<CatalogResult> GetAsync(string? search, int? categoryId, string? sort, int page);
}
