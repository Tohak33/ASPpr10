using AutoMapper;
using para.Models;
using para.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace para.Services;

public class Service<TEntity, TRead, TCreate, TUpdate, TPatch>(
    IRepository<TEntity> repository, IMapper mapper, IStoreRules rules, IMemoryCache cache)
    : IService<TRead, TCreate, TUpdate, TPatch> where TEntity : class
{
    public async Task<PageResult<TRead>> GetAllAsync(int page, int pageSize, CancellationToken ct = default)
    {
        if (page < 1 || pageSize < 1 || pageSize > 100)
            throw new StoreException(StoreError.Validation, "page должен быть больше 0, pageSize — от 1 до 100.");
        var total = await repository.CountAsync(ct);
        var entities = await repository.PageAsync(page, pageSize, ct);
        return new(mapper.Map<List<TRead>>(entities), total);
    }

    public async Task<TRead?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var entity = await repository.GetByIdAsync(id, ct);
        return entity == null ? default : mapper.Map<TRead>(entity);
    }

    public async Task<TRead> CreateAsync(TCreate dto, CancellationToken ct = default)
    {
        var entity = mapper.Map<TEntity>(dto);
        if (entity is Product product) product.CreatedAt = DateTime.UtcNow;
        SetPassword(dto, entity);
        await rules.ValidateAsync(entity, ct);
        repository.Add(entity);
        await repository.SaveChangesAsync(ct);
        if (entity is Category) cache.Remove(CatalogService.CategoryCacheKey);
        return mapper.Map<TRead>(entity);
    }

    public Task<bool> UpdateAsync(int id, TUpdate dto, CancellationToken ct = default) => ChangeAsync(id, dto, ct);
    public Task<bool> PatchAsync(int id, TPatch dto, CancellationToken ct = default) => ChangeAsync(id, dto, ct);

    private async Task<bool> ChangeAsync<TInput>(int id, TInput dto, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(id, ct);
        if (entity == null) return false;
        mapper.Map(dto, entity);
        SetPassword(dto, entity);
        await rules.ValidateAsync(entity, ct);
        await repository.SaveChangesAsync(ct);
        if (entity is Category) cache.Remove(CatalogService.CategoryCacheKey);
        return true;
    }

    private static void SetPassword<TInput>(TInput dto, TEntity entity)
    {
        if (entity is User user && dto is DTOs.IPasswordInput { Password: not null } input)
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(input.Password);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await repository.GetByIdAsync(id, ct);
        if (entity == null) return false;
        await rules.ValidateDeleteAsync(entity, ct);
        repository.Remove(entity);
        await repository.SaveChangesAsync(ct);
        if (entity is Category) cache.Remove(CatalogService.CategoryCacheKey);
        return true;
    }
}
