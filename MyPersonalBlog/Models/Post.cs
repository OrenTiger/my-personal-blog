using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyPersonalBlog.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(140, ErrorMessage = "Превышена максимальная длина (140 символов)")]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000, ErrorMessage = "Превышена максимальная длина (1000 символов)")]
        [AllowHtml]
        public string IntroText { get; set; }

        [Required]
        [MaxLength(10000, ErrorMessage = "Превышена максимальная длина (10000 символов)")]
        [AllowHtml]
        public string MainText { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreateDate { get; set; }

        public bool IsPublished { get; set; }

        [Display(Name = "Количество просмотров")]
        public int ViewsCount { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public ICollection<Comment> Comments { get; set; }
        
        public Post()
        {
            IsPublished = false;
            Tags = new List<Tag>();
            Comments = new List<Comment>();
        }
    }
}