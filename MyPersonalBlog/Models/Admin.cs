using System.ComponentModel.DataAnnotations;

namespace MyPersonalBlog.Models
{
    public class Admin
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Превышена максимальная длина (30 символов)")]
        public string Login { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Превышена максимальная длина (30 символов)")]
        public string Username { get; set; }

        [Required]
        [MaxLength(32)]
        public string PasswordHash { get; set; }
    }
}