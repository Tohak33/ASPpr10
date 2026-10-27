using para.Models;
using para.Repositories;

namespace para.Services;

public class CartService(IRepository<Cart> carts, IRepository<CartItem> items,
    IRepository<Product> products, IRepository<Order> orders) : ICartService
{
    public async Task<List<CartItem>> GetItemsAsync(int userId) =>
        await items.ListAsync(i => i.Cart.UserID == userId);

    public async Task AddAsync(int userId, int productId)
    {
        var product = await products.GetByIdAsync(productId)
            ?? throw new StoreException(StoreError.NotFound, "Товар не найден.");
        var cart = await carts.FirstAsync(c => c.UserID == userId);
        if (cart == null)
        {
            cart = new Cart { UserID = userId };
            carts.Add(cart);
        }
        var item = cart.CartItems.FirstOrDefault(i => i.ProductID == productId);
        if (item == null) cart.CartItems.Add(new CartItem { Cart = cart, Product = product, ProductID = productId, Quantity = 1 });
        else if (item.Quantity < 1000000) item.Quantity++;
        else throw new StoreException(StoreError.Validation, "Достигнуто максимальное количество товара.");
        await carts.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(int userId, int id, int quantity)
    {
        var item = await items.FirstAsync(i => i.CartItemID == id && i.Cart.UserID == userId);
        if (item == null) return false;
        if (quantity > 1000000) throw new StoreException(StoreError.Validation, "Слишком большое количество товара.");
        if (quantity <= 0) items.Remove(item);
        else item.Quantity = quantity;
        await items.SaveChangesAsync();
        return true;
    }

    public Task<bool> DeleteAsync(int userId, int id) => UpdateAsync(userId, id, 0);

    public async Task<bool> CheckoutAsync(int userId)
    {
        var cartItems = await GetItemsAsync(userId);
        if (cartItems.Count == 0) return false;
        var order = new Order
        {
            UserID = userId, OrderDate = DateTime.UtcNow, Status = "New",
            TotalAmount = cartItems.Sum(i => i.Quantity * i.Product.Price),
            OrderItems = cartItems.Select(i => new OrderItem
            {
                ProductID = i.ProductID, Quantity = i.Quantity,
                Price = i.Product.Price, PriceAtOrder = i.Product.Price
            }).ToList()
        };
        orders.Add(order);
        foreach (var item in cartItems) items.Remove(item);
        await orders.SaveChangesAsync();
        return true;
    }
}
