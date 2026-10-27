using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace para.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }

        public int OrderID { get; set; }

        public int ProductID { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceAtOrder { get; set; }

        [ForeignKey("OrderID")]
        public Order Order { get; set; } = null!;

        [ForeignKey("ProductID")]
        public Product Product { get; set; } = null!;
    }
}