namespace para.Services;

public record PageResult<T>(List<T> Items, int TotalCount);

public interface IService<TRead, in TCreate, in TUpdate, in TPatch>
{
    Task<PageResult<TRead>> GetAllAsync(int page, int pageSize, CancellationToken ct = default);
    Task<TRead?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<TRead> CreateAsync(TCreate dto, CancellationToken ct = default);
    Task<bool> UpdateAsync(int id, TUpdate dto, CancellationToken ct = default);
    Task<bool> PatchAsync(int id, TPatch dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}
