using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace para.Models;

public partial class Practos2AspContext : DbContext
{
    public Practos2AspContext()
    {
    }

    public Practos2AspContext(DbContextOptions<Practos2AspContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }
    public virtual DbSet<CartItem> CartItems { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderItem> OrderItems { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public DbSet<ProductReservation> ProductReservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartID).HasName("PK__Carts__51BCD797CED15DC5");
            entity.HasIndex(e => e.UserID, "UQ__Carts__1788CCAD6853C6D7").IsUnique();
            entity.Property(e => e.CartID).HasColumnName("CartID");
            entity.Property(e => e.UserID).HasColumnName("UserID");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.CartItemID).HasName("PK__CartItem__488B0B2ACAA76023");
            entity.Property(e => e.CartItemID).HasColumnName("CartItemID");
            entity.Property(e => e.CartID).HasColumnName("CartID");
            entity.Property(e => e.ProductID).HasColumnName("ProductID");
            entity.Property(e => e.Quantity).HasDefaultValue(1);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryID).HasName("PK__Categori__19093A2B284A7654");
            entity.Property(e => e.CategoryID).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderID).HasName("PK__Orders__C3905BAF9262E9E6");
            entity.Property(e => e.OrderID).HasColumnName("OrderID");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("New");
            entity.Property(e => e.UserID).HasColumnName("UserID");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemID).HasName("PK__OrderIte__57ED06A12F2187BE");
            entity.Property(e => e.OrderItemID).HasColumnName("OrderItemID");
            entity.Property(e => e.OrderID).HasColumnName("OrderID");
            entity.Property(e => e.PriceAtOrder).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductID).HasColumnName("ProductID");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasColumnType("datetime2").HasDefaultValueSql("SYSUTCDATETIME()");
            entity.HasKey(e => e.ProductID).HasName("PK__Products__B40CC6ED265AC4C1");
            entity.Property(e => e.ProductID).HasColumnName("ProductID");
            entity.Property(e => e.CategoryID).HasColumnName("CategoryID");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductName).HasMaxLength(150);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleID).HasName("PK__Roles__8AFACE3A67AD966B");
            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B6160BC2E4030").IsUnique();
            entity.Property(e => e.RoleID).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserID).HasName("PK__Users__1788CCAC84E11D7F");
            entity.HasIndex(e => e.Login, "UQ__Users__5E55825B05AA719A").IsUnique();
            entity.Property(e => e.UserID).HasColumnName("UserID");
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.RoleID).HasColumnName("RoleID");
        });

        modelBuilder.Entity<Product>().Property(p => p.RowVersion).IsRowVersion();
        modelBuilder.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryID).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<User>().HasOne(u => u.Role).WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleID).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Cart>().HasOne(c => c.User).WithMany(u => u.Carts)
            .HasForeignKey(c => c.UserID).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<CartItem>().HasOne(i => i.Cart).WithMany(c => c.CartItems)
            .HasForeignKey(i => i.CartID).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<CartItem>().HasOne(i => i.Product).WithMany(p => p.CartItems)
            .HasForeignKey(i => i.ProductID).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Order>().HasOne(o => o.User).WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserID).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<OrderItem>().HasOne(i => i.Order).WithMany(o => o.OrderItems)
            .HasForeignKey(i => i.OrderID).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<OrderItem>().HasOne(i => i.Product).WithMany(p => p.OrderItems)
            .HasForeignKey(i => i.ProductID).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<User>().Property(u => u.FullName).HasMaxLength(100);
        modelBuilder.Entity<ProductReservation>(entity =>
        {
            entity.ToTable("ProductReservations", table => table.HasCheckConstraint("CK_ProductReservations_Quantity", "[Quantity] > 0"));
            entity.HasKey(r => r.ProductReservationID);
            entity.Property(r => r.RowVersion).IsRowVersion();
            entity.HasOne(r => r.Product).WithMany(p => p.Reservations)
                .HasForeignKey(r => r.ProductID).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(r => r.User).WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserID).OnDelete(DeleteBehavior.Restrict);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
