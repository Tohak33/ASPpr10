using para.DTOs;

namespace para.Services;

public interface IReservationService
{
    Task<PageResult<ProductReservationDTO>> GetAllAsync(int userId, int page, int pageSize, CancellationToken ct = default);
    Task<ProductReservationDTO?> GetByIdAsync(int userId, int id, CancellationToken ct = default);
    Task<ProductReservationDTO> ReserveAsync(int userId, CreateProductReservationDTO dto, CancellationToken ct = default);
    Task<bool> UpdateAsync(int userId, int id, UpdateProductReservationDTO dto, CancellationToken ct = default);
    Task<bool> PatchAsync(int userId, int id, PatchProductReservationDTO dto, CancellationToken ct = default);
    Task<bool> CancelAsync(int userId, int id, CancellationToken ct = default);
    Task<bool> DeleteAsync(int userId, int id, CancellationToken ct = default);
}
