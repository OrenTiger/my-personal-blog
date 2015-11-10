using System.Collections.Generic;
using MyPersonalBlog.Models;

namespace MyPersonalBlog.Repositories
{
    public interface IPostRepository
    {
        IEnumerable<Post> Posts { get; }

        void SavePost(Post post);

        Post deletePost(int postId);
    }
}
