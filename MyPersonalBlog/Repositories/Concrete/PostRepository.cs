using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPersonalBlog.Models;
using System.Data.Entity;

namespace MyPersonalBlog.Repositories
{
    public class PostRepository : IPostRepository
    {
        private BlogContext _db = new BlogContext();

        public IEnumerable<Post> GetPosts
        {
            get
            {
                return _db.Posts;
            }
        }

        public Post GetPostById(int id)
        {
            return _db.Posts
                .Where(p => p.Id == id)
                .Include(p => p.Comments)
                .FirstOrDefault();
        }

        public void SavePost(Post post)
        {
            throw new NotImplementedException();
        }

        public Post DeletePost(int postId)
        {
            throw new NotImplementedException();
        }
    }
}