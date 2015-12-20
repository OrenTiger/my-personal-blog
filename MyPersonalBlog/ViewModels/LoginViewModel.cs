using System.ComponentModel.DataAnnotations;

namespace MyPersonalBlog.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(10, MinimumLength=3)]
        [Display(Name="Логин")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        public string PasswordHash { get; set; }
    }
}