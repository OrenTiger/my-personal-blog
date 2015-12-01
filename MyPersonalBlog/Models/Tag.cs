using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyPersonalBlog.Models
{
    public class Tag : IEquatable<Tag>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина (50 символов)")]
        public string Name { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public Tag()
        {
            Posts = new List<Post>();
        }

        public bool Equals(Tag obj)
        {
            if (obj == null)
                return false;

            return obj.Id == this.Id && obj.Name == obj.Name;
        }
    }
}