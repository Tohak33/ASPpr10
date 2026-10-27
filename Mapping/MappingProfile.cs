using AutoMapper;
using para.DTOs;
using para.Models;

namespace para.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateProductReservationDTO, ProductReservation>(MemberList.None);
        CreateMap<UpdateProductReservationDTO, ProductReservation>(MemberList.None)
            .ForMember(d => d.Product, o => o.Ignore())
            .ForMember(d => d.User, o => o.Ignore());
        CreateMap<ProductReservation, UpdateProductReservationDTO>();
        CreateMap<UpdateProductReservationDTO, UpdateProductReservationDTO>();
        CreateMap<PatchProductReservationDTO, UpdateProductReservationDTO>()
            .ForMember(d => d.ProductId, o => o.PreCondition(s => s.ProductId != null))
            .ForMember(d => d.Quantity, o => o.PreCondition(s => s.Quantity != null));
        CreateMap<Product, ProductDTO>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.ProductID))
            .ForMember(d => d.CreatedAt, o => o.MapFrom(s => DateTime.SpecifyKind(s.CreatedAt, DateTimeKind.Utc)));
        CreateMap<Category, CategoryDTO>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.CategoryID));
        CreateMap<Role, RoleDTO>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.RoleID));
        CreateMap<User, UserDTO>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.UserID));
        CreateMap<Cart, CartDTO>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.CartID));
        CreateMap<CartItem, CartItemDTO>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.CartItemID))
            .ForMember(d => d.LineTotal, o => o.MapFrom(s => s.Quantity * s.Product.Price));
        CreateMap<Order, OrderDTO>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.OrderID));
        CreateMap<OrderItem, OrderItemDTO>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.OrderItemID));
        CreateMap<ProductReservation, ProductReservationDTO>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.ProductReservationID))
            .ForMember(d => d.CreatedAt, o => o.MapFrom(s => DateTime.SpecifyKind(s.CreatedAt, DateTimeKind.Utc)))
            .ForMember(d => d.CancelledAt, o => o.MapFrom(s => s.CancelledAt.HasValue
                ? (DateTime?)DateTime.SpecifyKind(s.CancelledAt.Value, DateTimeKind.Utc) : null));
        CreateMap<Category, CategorySummaryDTO>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.CategoryID));
        CreateMap<Role, RoleSummaryDTO>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.RoleID));
        CreateMap<User, UserSummaryDTO>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.UserID));
        CreateMap<Product, ProductSummaryDTO>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.ProductID));
        CreateMap<CreateProductDTO, Product>(MemberList.None)
            .ForMember(d => d.ProductID, o => o.Ignore())
            .ForMember(d => d.Category, o => o.Ignore())
            .ForMember(d => d.CartItems, o => o.Ignore())
            .ForMember(d => d.OrderItems, o => o.Ignore())
            .ForMember(d => d.Reservations, o => o.Ignore())
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.RowVersion, o => o.Ignore());
        CreateMap<UpdateProductDTO, Product>(MemberList.None)
            .ForMember(d => d.ProductID, o => o.Ignore())
            .ForMember(d => d.Category, o => o.Ignore())
            .ForMember(d => d.CartItems, o => o.Ignore())
            .ForMember(d => d.OrderItems, o => o.Ignore())
            .ForMember(d => d.Reservations, o => o.Ignore())
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.RowVersion, o => o.Ignore());
        CreateMap<PatchProductDTO, Product>(MemberList.None)
            .ForMember(d => d.ProductID, o => o.Ignore())
            .ForMember(d => d.Category, o => o.Ignore())
            .ForMember(d => d.CartItems, o => o.Ignore())
            .ForMember(d => d.OrderItems, o => o.Ignore())
            .ForMember(d => d.Reservations, o => o.Ignore())
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.RowVersion, o => o.Ignore())
            .ForMember(d => d.CategoryID, o => o.PreCondition(s => s.CategoryId != null))
            .ForMember(d => d.ProductName, o => o.PreCondition(s => s.ProductName != null))
            .ForMember(d => d.Description, o => o.PreCondition(s => s.Description != null))
            .ForMember(d => d.Price, o => o.PreCondition(s => s.Price != null))
            .ForMember(d => d.ImagePath, o => o.PreCondition(s => s.ImagePath != null))
            .ForMember(d => d.StockQuantity, o => o.PreCondition(s => s.StockQuantity != null));
        CreateMap<Product, Product>(MemberList.None)
            .ForMember(d => d.ProductID, o => o.Ignore())
            .ForMember(d => d.Category, o => o.Ignore())
            .ForMember(d => d.CartItems, o => o.Ignore())
            .ForMember(d => d.OrderItems, o => o.Ignore())
            .ForMember(d => d.Reservations, o => o.Ignore())
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.RowVersion, o => o.Ignore());
        CreateMap<CreateCategoryDTO, Category>(MemberList.None)
            .ForMember(d => d.CategoryID, o => o.Ignore())
            .ForMember(d => d.Products, o => o.Ignore());
        CreateMap<UpdateCategoryDTO, Category>(MemberList.None)
            .ForMember(d => d.CategoryID, o => o.Ignore())
            .ForMember(d => d.Products, o => o.Ignore());
        CreateMap<PatchCategoryDTO, Category>(MemberList.None)
            .ForMember(d => d.CategoryID, o => o.Ignore())
            .ForMember(d => d.Products, o => o.Ignore())
            .ForMember(d => d.CategoryName, o => o.PreCondition(s => s.CategoryName != null));
        CreateMap<Category, Category>(MemberList.None)
            .ForMember(d => d.CategoryID, o => o.Ignore())
            .ForMember(d => d.Products, o => o.Ignore());
        CreateMap<CreateRoleDTO, Role>(MemberList.None)
            .ForMember(d => d.RoleID, o => o.Ignore())
            .ForMember(d => d.Users, o => o.Ignore());
        CreateMap<UpdateRoleDTO, Role>(MemberList.None)
            .ForMember(d => d.RoleID, o => o.Ignore())
            .ForMember(d => d.Users, o => o.Ignore());
        CreateMap<PatchRoleDTO, Role>(MemberList.None)
            .ForMember(d => d.RoleID, o => o.Ignore())
            .ForMember(d => d.Users, o => o.Ignore())
            .ForMember(d => d.RoleName, o => o.PreCondition(s => s.RoleName != null));
        CreateMap<Role, Role>(MemberList.None)
            .ForMember(d => d.RoleID, o => o.Ignore())
            .ForMember(d => d.Users, o => o.Ignore());
        CreateMap<CreateUserDTO, User>(MemberList.None)
            .ForMember(d => d.UserID, o => o.Ignore())
            .ForMember(d => d.Role, o => o.Ignore())
            .ForMember(d => d.Orders, o => o.Ignore())
            .ForMember(d => d.Carts, o => o.Ignore())
            .ForMember(d => d.Reservations, o => o.Ignore())
            .ForMember(d => d.PasswordHash, o => o.Ignore());
        CreateMap<UpdateUserDTO, User>(MemberList.None)
            .ForMember(d => d.UserID, o => o.Ignore())
            .ForMember(d => d.Role, o => o.Ignore())
            .ForMember(d => d.Orders, o => o.Ignore())
            .ForMember(d => d.Carts, o => o.Ignore())
            .ForMember(d => d.Reservations, o => o.Ignore())
            .ForMember(d => d.PasswordHash, o => o.Ignore());
        CreateMap<PatchUserDTO, User>(MemberList.None)
            .ForMember(d => d.UserID, o => o.Ignore())
            .ForMember(d => d.Role, o => o.Ignore())
            .ForMember(d => d.Orders, o => o.Ignore())
            .ForMember(d => d.Carts, o => o.Ignore())
            .ForMember(d => d.Reservations, o => o.Ignore())
            .ForMember(d => d.PasswordHash, o => o.Ignore())
            .ForMember(d => d.Login, o => o.PreCondition(s => s.Login != null))
            .ForMember(d => d.RoleID, o => o.PreCondition(s => s.RoleId != null))
            .ForMember(d => d.FullName, o => o.PreCondition(s => s.FullName != null))
            .ForMember(d => d.IsActive, o => o.PreCondition(s => s.IsActive != null));
        CreateMap<User, User>(MemberList.None)
            .ForMember(d => d.UserID, o => o.Ignore())
            .ForMember(d => d.FullName, o => o.Ignore())
            .ForMember(d => d.IsActive, o => o.Ignore())
            .ForMember(d => d.Role, o => o.Ignore())
            .ForMember(d => d.Orders, o => o.Ignore())
            .ForMember(d => d.Carts, o => o.Ignore())
            .ForMember(d => d.Reservations, o => o.Ignore());
        CreateMap<CreateCartDTO, Cart>(MemberList.None)
            .ForMember(d => d.CartID, o => o.Ignore())
            .ForMember(d => d.User, o => o.Ignore())
            .ForMember(d => d.CartItems, o => o.Ignore());
        CreateMap<UpdateCartDTO, Cart>(MemberList.None)
            .ForMember(d => d.CartID, o => o.Ignore())
            .ForMember(d => d.User, o => o.Ignore())
            .ForMember(d => d.CartItems, o => o.Ignore());
        CreateMap<PatchCartDTO, Cart>(MemberList.None)
            .ForMember(d => d.CartID, o => o.Ignore())
            .ForMember(d => d.User, o => o.Ignore())
            .ForMember(d => d.CartItems, o => o.Ignore())
            .ForMember(d => d.UserID, o => o.PreCondition(s => s.UserId != null));
        CreateMap<Cart, Cart>(MemberList.None)
            .ForMember(d => d.CartID, o => o.Ignore())
            .ForMember(d => d.User, o => o.Ignore())
            .ForMember(d => d.CartItems, o => o.Ignore());
        CreateMap<CreateCartItemDTO, CartItem>(MemberList.None)
            .ForMember(d => d.CartItemID, o => o.Ignore())
            .ForMember(d => d.Cart, o => o.Ignore())
            .ForMember(d => d.Product, o => o.Ignore());
        CreateMap<UpdateCartItemDTO, CartItem>(MemberList.None)
            .ForMember(d => d.CartItemID, o => o.Ignore())
            .ForMember(d => d.Cart, o => o.Ignore())
            .ForMember(d => d.Product, o => o.Ignore());
        CreateMap<PatchCartItemDTO, CartItem>(MemberList.None)
            .ForMember(d => d.CartItemID, o => o.Ignore())
            .ForMember(d => d.Cart, o => o.Ignore())
            .ForMember(d => d.Product, o => o.Ignore())
            .ForMember(d => d.CartID, o => o.PreCondition(s => s.CartId != null))
            .ForMember(d => d.ProductID, o => o.PreCondition(s => s.ProductId != null))
            .ForMember(d => d.Quantity, o => o.PreCondition(s => s.Quantity != null));
        CreateMap<CartItem, CartItem>(MemberList.None)
            .ForMember(d => d.CartItemID, o => o.Ignore())
            .ForMember(d => d.Cart, o => o.Ignore())
            .ForMember(d => d.Product, o => o.Ignore());
        CreateMap<CreateOrderDTO, Order>(MemberList.None)
            .ForMember(d => d.OrderID, o => o.Ignore())
            .ForMember(d => d.User, o => o.Ignore())
            .ForMember(d => d.OrderItems, o => o.Ignore());
        CreateMap<UpdateOrderDTO, Order>(MemberList.None)
            .ForMember(d => d.OrderID, o => o.Ignore())
            .ForMember(d => d.User, o => o.Ignore())
            .ForMember(d => d.OrderItems, o => o.Ignore());
        CreateMap<PatchOrderDTO, Order>(MemberList.None)
            .ForMember(d => d.OrderID, o => o.Ignore())
            .ForMember(d => d.User, o => o.Ignore())
            .ForMember(d => d.OrderItems, o => o.Ignore())
            .ForMember(d => d.UserID, o => o.PreCondition(s => s.UserId != null))
            .ForMember(d => d.OrderDate, o => o.PreCondition(s => s.OrderDate != null))
            .ForMember(d => d.TotalAmount, o => o.PreCondition(s => s.TotalAmount != null))
            .ForMember(d => d.Status, o => o.PreCondition(s => s.Status != null));
        CreateMap<Order, Order>(MemberList.None)
            .ForMember(d => d.OrderID, o => o.Ignore())
            .ForMember(d => d.TotalAmount, o => o.Ignore())
            .ForMember(d => d.User, o => o.Ignore())
            .ForMember(d => d.OrderItems, o => o.Ignore());
        CreateMap<CreateOrderItemDTO, OrderItem>(MemberList.None)
            .ForMember(d => d.OrderItemID, o => o.Ignore())
            .ForMember(d => d.Order, o => o.Ignore())
            .ForMember(d => d.Product, o => o.Ignore());
        CreateMap<UpdateOrderItemDTO, OrderItem>(MemberList.None)
            .ForMember(d => d.OrderItemID, o => o.Ignore())
            .ForMember(d => d.Order, o => o.Ignore())
            .ForMember(d => d.Product, o => o.Ignore());
        CreateMap<PatchOrderItemDTO, OrderItem>(MemberList.None)
            .ForMember(d => d.OrderItemID, o => o.Ignore())
            .ForMember(d => d.Order, o => o.Ignore())
            .ForMember(d => d.Product, o => o.Ignore())
            .ForMember(d => d.OrderID, o => o.PreCondition(s => s.OrderId != null))
            .ForMember(d => d.ProductID, o => o.PreCondition(s => s.ProductId != null))
            .ForMember(d => d.Quantity, o => o.PreCondition(s => s.Quantity != null))
            .ForMember(d => d.Price, o => o.PreCondition(s => s.Price != null))
            .ForMember(d => d.PriceAtOrder, o => o.PreCondition(s => s.PriceAtOrder != null));
        CreateMap<OrderItem, OrderItem>(MemberList.None)
            .ForMember(d => d.OrderItemID, o => o.Ignore())
            .ForMember(d => d.Order, o => o.Ignore())
            .ForMember(d => d.Product, o => o.Ignore());
    }
}
