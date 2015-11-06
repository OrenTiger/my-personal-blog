using System.ComponentModel.DataAnnotations;

namespace MyPersonalBlog.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}