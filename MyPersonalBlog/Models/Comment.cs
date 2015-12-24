using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MyPersonalBlog.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Имя")]
        [StringLength(50, ErrorMessage = "Превышена максимальная длина (50 символов)")]
        public string Username { get; set; }

        [StringLength(255)]
        public string AvatarUrl { get; set; }

        [Required]
        [Display(Name = "Комментарий")]
        [StringLength(1000, ErrorMessage = "Превышена максимальная длина (1000 символов)")]
        public string Text { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public bool IsApproved { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        [Required]
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}