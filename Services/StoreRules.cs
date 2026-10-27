using para.Models;
using para.Repositories;

namespace para.Services;

public class StoreRules(
    IRepository<Product> products, IRepository<Category> categories, IRepository<User> users,
    IRepository<Role> roles, IRepository<Cart> carts, IRepository<CartItem> cartItems,
    IRepository<Order> orders, IRepository<OrderItem> orderItems,
    IRepository<ProductReservation> reservations) : IStoreRules
{
    private static void Require(bool condition, string message)
    {
        if (!condition) throw new StoreException(StoreError.Validation, message);
    }

    private static void Found(bool condition, string message)
    {
        if (!condition) throw new StoreException(StoreError.NotFound, message);
    }

    public async Task ValidateAsync<T>(T entity, CancellationToken ct = default) where T : class
    {
        switch (entity)
        {
            case Product p:
                Require(!string.IsNullOrWhiteSpace(p.ProductName), "Укажите название товара.");
                Require(p.Price is > 0 and <= 1000000 && p.StockQuantity is >= 0 and <= 1000000, "Недопустимая цена или остаток товара.");
                Require(!string.IsNullOrWhiteSpace(p.ImagePath), "Укажите изображение товара.");
                p.Category = await categories.GetByIdAsync(p.CategoryID, ct);
                Found(p.Category != null, "Категория не найдена.");
                break;
            case Category c:
                Require(!string.IsNullOrWhiteSpace(c.CategoryName), "Укажите название категории.");
                break;
            case Role r:
                Require(!string.IsNullOrWhiteSpace(r.RoleName), "Укажите название роли.");
                Require(!await roles.AnyAsync(x => x.RoleName == r.RoleName && x.RoleID != r.RoleID, ct), "Такая роль уже существует.");
                break;
            case User u:
                Require(!string.IsNullOrWhiteSpace(u.Login), "Укажите логин.");
                Require(!string.IsNullOrWhiteSpace(u.PasswordHash), "Укажите пароль.");
                Require(!await users.AnyAsync(x => x.Login == u.Login && x.UserID != u.UserID, ct), "Пользователь с таким логином уже существует.");
                u.Role = await roles.GetByIdAsync(u.RoleID, ct);
                Found(u.Role != null, "Роль не найдена.");
                if (string.IsNullOrWhiteSpace(u.FullName)) u.FullName = u.Login;
                break;
            case Cart c:
                c.User = (await users.GetByIdAsync(c.UserID, ct))!;
                Found(c.User != null, "Пользователь не найден.");
                Require(!await carts.AnyAsync(x => x.UserID == c.UserID && x.CartID != c.CartID, ct), "У пользователя уже есть корзина.");
                break;
            case CartItem i:
                Require(i.Quantity is > 0 and <= 1000000, "Количество должно быть от 1 до 1000000.");
                i.Cart = (await carts.GetByIdAsync(i.CartID, ct))!;
                i.Product = (await products.GetByIdAsync(i.ProductID, ct))!;
                Found(i.Cart != null && i.Product != null, "Корзина или товар не найдены.");
                Require(!await cartItems.AnyAsync(x => x.CartID == i.CartID && x.ProductID == i.ProductID && x.CartItemID != i.CartItemID, ct), "Этот товар уже есть в корзине.");
                break;
            case Order o:
                o.User = (await users.GetByIdAsync(o.UserID, ct))!;
                Found(o.User != null, "Пользователь не найден.");
                Require(!string.IsNullOrWhiteSpace(o.Status) && o.TotalAmount >= 0 && o.OrderDate != default, "Проверьте дату, статус и сумму заказа.");
                break;
            case OrderItem i:
                Require(i.Quantity is > 0 and <= 1000000 && i.Price > 0 && i.PriceAtOrder > 0, "Количество и цены должны быть больше нуля.");
                i.Order = (await orders.GetByIdAsync(i.OrderID, ct))!;
                i.Product = (await products.GetByIdAsync(i.ProductID, ct))!;
                Found(i.Order != null && i.Product != null, "Заказ или товар не найдены.");
                break;
        }
    }

    public async Task ValidateDeleteAsync<T>(T entity, CancellationToken ct = default) where T : class
    {
        bool used = entity switch
        {
            Product p => await cartItems.AnyAsync(i => i.ProductID == p.ProductID, ct)
                || await orderItems.AnyAsync(i => i.ProductID == p.ProductID, ct)
                || await reservations.AnyAsync(r => r.ProductID == p.ProductID, ct),
            Category c => await products.AnyAsync(p => p.CategoryID == c.CategoryID, ct),
            Role r => await users.AnyAsync(u => u.RoleID == r.RoleID, ct),
            User u => await carts.AnyAsync(c => c.UserID == u.UserID, ct)
                || await orders.AnyAsync(o => o.UserID == u.UserID, ct)
                || await reservations.AnyAsync(r => r.UserID == u.UserID, ct),
            Cart c => await cartItems.AnyAsync(i => i.CartID == c.CartID, ct),
            Order o => await orderItems.AnyAsync(i => i.OrderID == o.OrderID, ct),
            _ => false
        };
        if (used) throw new StoreException(StoreError.Conflict, "Запись используется в связанных данных. Сначала удалите зависимые записи.");
    }
}
