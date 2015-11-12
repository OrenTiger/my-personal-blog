using System;
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
        public string IntroText { get; set; }

        [MaxLength(10000, ErrorMessage = "Превышена максимальная длина (10000 символов)")]
        public string MainText { get; set; }
        
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
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