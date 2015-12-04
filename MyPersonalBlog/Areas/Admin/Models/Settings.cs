using System;
using System.ComponentModel.DataAnnotations;

namespace MyPersonalBlog.Areas.Admin.Models
{
    public class Settings
    {
        [Required]
        [Display(Name = "Название сайта")]
        [StringLength(100, ErrorMessage = "Превышена максимальная длина (100 символов)")]
        public string BlogTitle { get; set; }

        [Required]
        [Display(Name = "Короткое описание")]
        [StringLength(200, ErrorMessage = "Превышена максимальная длина (200 символов)")]
        public string ShortDescription { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Укажите значение от 1 до 10")]
        [Display(Name = "Количество постов на страницу")]
        public int PostListPageSize { get; set; }

        [Required]
        [Range(1, 50, ErrorMessage = "Укажите значение от 1 до 50")]
        [Display(Name = "Количество записей на страницу")]
        public int PageSize { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Укажите валидный email")]
        [StringLength(254, ErrorMessage = "Превышена максимальная длина (254 символа)")]
        [Display(Name = "E-mail администратора")]
        public string AdminEmail { get; set; }
    }
}