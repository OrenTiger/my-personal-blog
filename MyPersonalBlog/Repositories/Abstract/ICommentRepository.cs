using System.Collections.Generic;
using MyPersonalBlog.Models;

namespace MyPersonalBlog.Repositories
{
    public interface ICommentRepository
    {
        void SaveComment(Comment comment);

        Comment DeleteComment(int commentId);
    }
}
