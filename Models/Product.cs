using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace para.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Выберите категорию")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Введите название товара")]
        [StringLength(100, ErrorMessage = "Длина названия не может превышать 100 символов")]
        public string ProductName { get; set; } = "";

        [StringLength(500, ErrorMessage = "Длина описания не может превышать 500 символов")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Укажите цену")]
        [Range(0.01, 1000000, ErrorMessage = "Цена должна быть больше 0")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string? ImagePath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Range(0, 1000000)]
        public int StockQuantity { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; } = [];

        public Category? Category { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public ICollection<ProductReservation> Reservations { get; set; } = [];
    }
}
