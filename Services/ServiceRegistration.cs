using para.DTOs;
using para.Mapping;
using para.Models;
using para.Repositories;

namespace para.Services;

public static class ServiceRegistration
{
    public static IServiceCollection AddStoreServices(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped(typeof(IEntityService<>), typeof(EntityService<>));
        services.AddScoped<IStoreRules, StoreRules>();
        services.AddScoped<ICatalogService, CatalogService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddAutoMapper(_ => { }, new[] { typeof(MappingProfile).Assembly }, ServiceLifetime.Scoped);
        services.AddScoped<IService<ProductDTO, CreateProductDTO, UpdateProductDTO, PatchProductDTO>,
            Service<Product, ProductDTO, CreateProductDTO, UpdateProductDTO, PatchProductDTO>>();
        services.AddScoped<IService<CategoryDTO, CreateCategoryDTO, UpdateCategoryDTO, PatchCategoryDTO>,
            Service<Category, CategoryDTO, CreateCategoryDTO, UpdateCategoryDTO, PatchCategoryDTO>>();
        services.AddScoped<IService<RoleDTO, CreateRoleDTO, UpdateRoleDTO, PatchRoleDTO>,
            Service<Role, RoleDTO, CreateRoleDTO, UpdateRoleDTO, PatchRoleDTO>>();
        services.AddScoped<IService<UserDTO, CreateUserDTO, UpdateUserDTO, PatchUserDTO>,
            Service<User, UserDTO, CreateUserDTO, UpdateUserDTO, PatchUserDTO>>();
        services.AddScoped<IService<CartDTO, CreateCartDTO, UpdateCartDTO, PatchCartDTO>,
            Service<Cart, CartDTO, CreateCartDTO, UpdateCartDTO, PatchCartDTO>>();
        services.AddScoped<IService<CartItemDTO, CreateCartItemDTO, UpdateCartItemDTO, PatchCartItemDTO>,
            Service<CartItem, CartItemDTO, CreateCartItemDTO, UpdateCartItemDTO, PatchCartItemDTO>>();
        services.AddScoped<IService<OrderDTO, CreateOrderDTO, UpdateOrderDTO, PatchOrderDTO>,
            Service<Order, OrderDTO, CreateOrderDTO, UpdateOrderDTO, PatchOrderDTO>>();
        services.AddScoped<IService<OrderItemDTO, CreateOrderItemDTO, UpdateOrderItemDTO, PatchOrderItemDTO>,
            Service<OrderItem, OrderItemDTO, CreateOrderItemDTO, UpdateOrderItemDTO, PatchOrderItemDTO>>();
        return services;
    }
}
