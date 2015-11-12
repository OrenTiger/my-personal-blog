using System.Collections.Generic;
using MyPersonalBlog.Models;

namespace MyPersonalBlog.Repositories
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetPosts { get; }

        Post GetPostById(int id);

        void SavePost(Post post);

        Post DeletePost(int postId);
    }
}
