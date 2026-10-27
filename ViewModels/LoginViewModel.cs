using System.ComponentModel.DataAnnotations;

namespace para.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите логин")]
        [StringLength(50)]
        public string Login { get; set; } = null!;

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }
    }
}
