using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPersonalBlog.Infrastructure;
using MyPersonalBlog.Models;

namespace MyPersonalBlog.Repositories
{
    public class PostRepository : IPostRepository
    {
        private BlogContext _db = new BlogContext();

        public IEnumerable<Post> Posts
        {
            get
            {
                return _db.Posts;
            }
        }

        public void SavePost(Post post)
        {
            throw new NotImplementedException();
        }

        public Post deletePost(int postId)
        {
            throw new NotImplementedException();
        }
    }
}