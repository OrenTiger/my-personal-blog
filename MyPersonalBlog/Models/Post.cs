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
        [MaxLength(500, ErrorMessage = "Превышена максимальная длина (500 символов)")]
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

        public bool Published { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public ICollection<Comment> Comments { get; set; }
        
        public Post()
        {
            Published = false;
            Tags = new List<Tag>();
            Comments = new List<Comment>();
        }
    }
}