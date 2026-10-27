using System.ComponentModel.DataAnnotations.Schema;

namespace para.Models
{
    [Table("CartItems")]
    public class CartItem
    {
        public int CartItemID { get; set; }
        public int CartID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public Product Product { get; set; } = null!;
        public Cart Cart { get; set; } = null!;
    }
}
