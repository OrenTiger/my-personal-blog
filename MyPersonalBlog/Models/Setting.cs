using System.ComponentModel.DataAnnotations;

namespace MyPersonalBlog.Models
{
    public class Settings
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Укажите значение от 1 до 10")]
        [Display(Name = "Количество постов на страницу")]
        public int PostListPageSize { get; set; }

        [Required]
        [Range(1, 50, ErrorMessage = "Укажите значение от 1 до 50")]
        [Display(Name = "Количество записей в таблице на страницу")]
        public int PageSize { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Укажите валидный email")]
        [StringLength(254, ErrorMessage = "Превышена максимальная длина (254 символа)")]
        [Display(Name = "E-mail администратора")]
        public string AdminEmail { get; set; }

        public string VkAppId { get; set; }

        public string VkAppSecret { get; set; }

        public string GoogleAppId { get; set; }

        public string GoogleAppSecret { get; set; }

        public string OAuthRedirectUri { get; set; }
    }
}