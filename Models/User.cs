using System.ComponentModel.DataAnnotations;

namespace para.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Введите логин")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Логин должен содержать от 3 до 50 символов")]
        public string Login { get; set; } = "";

        [Required(ErrorMessage = "Введите пароль")]
        public string PasswordHash { get; set; } = "";

        public string FullName { get; set; } = "";
        public bool IsActive { get; set; } = true;

        [Required]
        public int RoleID { get; set; }

        public Role? Role { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Cart> Carts { get; set; } = [];
        public ICollection<ProductReservation> Reservations { get; set; } = [];
    }
}
