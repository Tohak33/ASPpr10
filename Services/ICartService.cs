using para.Models;

namespace para.Services;

public interface ICartService
{
    Task<List<CartItem>> GetItemsAsync(int userId);
    Task AddAsync(int userId, int productId);
    Task<bool> UpdateAsync(int userId, int id, int quantity);
    Task<bool> DeleteAsync(int userId, int id);
    Task<bool> CheckoutAsync(int userId);
}
