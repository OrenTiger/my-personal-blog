using System.ComponentModel.DataAnnotations;

namespace MyPersonalBlog.Models
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(10, MinimumLength=3)]
        public string Login { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}