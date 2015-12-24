using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MyPersonalBlog.Models
{
    public class Like
    {
        public int Id { get; set; }

        [Required]
        public int CommentId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string UserProvider { get; set; }        
        
        public Comment Comment { get; set; }
    }
}