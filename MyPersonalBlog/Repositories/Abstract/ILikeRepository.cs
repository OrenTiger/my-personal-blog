using System.Collections.Generic;
using MyPersonalBlog.Models;

namespace MyPersonalBlog.Repositories
{
    public interface ILikeRepository
    {
        IEnumerable<Like> Likes { get; }

        Like GetById(int id);

        void Save(Like like);

        void Delete(int id);
    }
}