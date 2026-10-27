namespace para.Services;

public interface IStoreRules
{
    Task ValidateAsync<T>(T entity, CancellationToken ct = default) where T : class;
    Task ValidateDeleteAsync<T>(T entity, CancellationToken ct = default) where T : class;
}
