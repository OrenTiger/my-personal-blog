using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyPersonalBlog.Models
{
    public class Tag
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
    }
}