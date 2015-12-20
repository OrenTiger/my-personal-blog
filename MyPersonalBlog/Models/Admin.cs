using System.ComponentModel.DataAnnotations;

namespace MyPersonalBlog.Models
{
    public class Admin
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Превышена максимальная длина (30 символов)")]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Превышена максимальная длина (30 символов)")]
        public string Username { get; set; }

        [Required]
        [MaxLength(60)]
        [Display(Name = "Пароль")]
        public string PasswordHash { get; set; }
    }
}