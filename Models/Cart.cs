namespace para.Models
{
    public class Cart
    {
        public int CartID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; } = null!;
        public ICollection<CartItem> CartItems { get; set; } = [];
    }
}
