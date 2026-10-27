using AutoMapper;
using para.DTOs;
using para.Models;
using para.Repositories;

namespace para.Services;

public class ReservationService(IRepository<ProductReservation> reservations,
    IRepository<Product> products, IRepository<User> users, IMapper mapper) : IReservationService
{
    public async Task<PageResult<ProductReservationDTO>> GetAllAsync(int userId, int page, int pageSize, CancellationToken ct = default)
    {
        if (page < 1 || pageSize < 1 || pageSize > 100)
            throw new StoreException(StoreError.Validation, "page должен быть больше 0, pageSize — от 1 до 100.");
        var total = await reservations.CountAsync(r => r.UserID == userId, ct);
        var items = await reservations.PageAsync(page, pageSize, r => r.UserID == userId, ct);
        return new(mapper.Map<List<ProductReservationDTO>>(items), total);
    }

    public async Task<ProductReservationDTO?> GetByIdAsync(int userId, int id, CancellationToken ct = default)
    {
        var item = await reservations.FirstAsync(r => r.ProductReservationID == id && r.UserID == userId, ct);
        return item == null ? null : mapper.Map<ProductReservationDTO>(item);
    }

    public async Task<ProductReservationDTO> ReserveAsync(int userId, CreateProductReservationDTO dto, CancellationToken ct = default)
    {
        var user = await users.GetByIdAsync(userId, ct);
        if (user == null || !user.IsActive) throw new StoreException(StoreError.NotFound, "Пользователь не найден или неактивен.");
        var product = await products.GetByIdAsync(dto.ProductId, ct)
            ?? throw new StoreException(StoreError.NotFound, "Товар не найден.");
        CheckStock(product, dto.Quantity);
        var reservation = mapper.Map<ProductReservation>(dto);
        reservation.UserID = userId;
        reservation.User = user;
        reservation.Product = product;
        reservation.CreatedAt = DateTime.UtcNow;
        product.StockQuantity -= reservation.Quantity;
        reservations.Add(reservation);
        await reservations.SaveChangesAsync(ct);
        return mapper.Map<ProductReservationDTO>(reservation);
    }

    public Task<bool> UpdateAsync(int userId, int id, UpdateProductReservationDTO dto, CancellationToken ct = default) =>
        ChangeAsync(userId, id, dto, ct);
    public Task<bool> PatchAsync(int userId, int id, PatchProductReservationDTO dto, CancellationToken ct = default) =>
        ChangeAsync(userId, id, dto, ct);

    private async Task<bool> ChangeAsync<T>(int userId, int id, T dto, CancellationToken ct)
    {
        var reservation = await ActiveAsync(userId, id, ct);
        if (reservation == null) return false;
        var candidate = mapper.Map<UpdateProductReservationDTO>(reservation);
        mapper.Map(dto, candidate);
        var product = await products.GetByIdAsync(candidate.ProductId, ct)
            ?? throw new StoreException(StoreError.NotFound, "Товар не найден.");
        var available = (long)product.StockQuantity + (product.ProductID == reservation.ProductID ? reservation.Quantity : 0);
        CheckQuantity(candidate.Quantity);
        if (available < candidate.Quantity) throw new StoreException(StoreError.Conflict, $"Недостаточно товара. Доступно: {available}.");
        var remaining = available - candidate.Quantity;
        if (remaining > 1000000) throw new StoreException(StoreError.Conflict, "Превышен допустимый остаток товара.");
        if (product.ProductID != reservation.ProductID) RestoreStock(reservation);
        mapper.Map(candidate, reservation);
        reservation.Product = product;
        product.StockQuantity = (int)remaining;
        await reservations.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> CancelAsync(int userId, int id, CancellationToken ct = default)
    {
        var reservation = await ActiveAsync(userId, id, ct);
        if (reservation == null) return false;
        RestoreStock(reservation);
        reservation.CancelledAt = DateTime.UtcNow;
        await reservations.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(int userId, int id, CancellationToken ct = default)
    {
        var reservation = await reservations.FirstAsync(r => r.ProductReservationID == id && r.UserID == userId, ct);
        if (reservation == null) return false;
        if (reservation.CancelledAt == null) RestoreStock(reservation);
        reservations.Remove(reservation);
        await reservations.SaveChangesAsync(ct);
        return true;
    }

    private Task<ProductReservation?> ActiveAsync(int userId, int id, CancellationToken ct) =>
        reservations.FirstAsync(r => r.ProductReservationID == id && r.UserID == userId && r.CancelledAt == null, ct);

    private static void CheckQuantity(int quantity)
    {
        if (quantity is < 1 or > 1000000) throw new StoreException(StoreError.Validation, "Количество должно быть от 1 до 1000000.");
    }

    private static void CheckStock(Product product, int quantity)
    {
        CheckQuantity(quantity);
        if (product.StockQuantity < quantity)
            throw new StoreException(StoreError.Conflict, $"Недостаточно товара. Доступно: {product.StockQuantity}.");
    }

    private static void RestoreStock(ProductReservation reservation)
    {
        var quantity = (long)reservation.Product.StockQuantity + reservation.Quantity;
        if (quantity > 1000000) throw new StoreException(StoreError.Conflict, "Возврат превысит допустимый остаток товара.");
        reservation.Product.StockQuantity = (int)quantity;
    }
}
