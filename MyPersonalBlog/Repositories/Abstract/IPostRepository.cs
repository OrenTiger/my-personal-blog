using System.Collections.Generic;
using MyPersonalBlog.Models;

namespace MyPersonalBlog.Repositories
{
    public interface IPostRepository
    {
        IEnumerable<Post> Get { get; }

        Post GetById(int id);

        void Save(Post post, int[] selectedTags = null);

        void Delete(int postId);
    }
}
