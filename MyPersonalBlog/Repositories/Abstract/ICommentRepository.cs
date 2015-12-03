using System.Collections.Generic;
using MyPersonalBlog.Models;

namespace MyPersonalBlog.Repositories
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> Comments
        {
            get;
        }

        Comment GetById(int id);

        void Save(Comment comment);

        void Delete(int id);
    }
}