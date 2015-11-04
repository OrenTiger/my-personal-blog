﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyPersonalBlog.Models
{
    public class Post
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(500, ErrorMessage = "Превышена максимальная длина (500 символов)")]
        public string IntroText { get; set; }

        [MaxLength(10000, ErrorMessage = "Превышена максимальная длина (10000 символов)")]
        public string MainText { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }

        public bool Published { get; set; } = false;

        public virtual ICollection<Tag> Tags { get; set; }
        
        public Post()
        {
            Tags = new List<Tag>();
        }
    }
}