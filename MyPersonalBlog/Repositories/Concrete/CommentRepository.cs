using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPersonalBlog.Models;

namespace MyPersonalBlog.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        BlogContext _db = new BlogContext();

        public void SaveComment(Comment comment)
        {
            if (comment.Id == 0)
            {
                comment.CreateDate = DateTime.Now;
                _db.Comments.Add(comment);
            }
            else
            {
                Comment dbEntry = _db.Comments.Find(comment.Id);

                if (dbEntry != null)
                {
                    dbEntry.Text = comment.Text;
                    dbEntry.Username = comment.Username;
                    dbEntry.CreateDate = comment.CreateDate;
                }
            }

            _db.SaveChanges();
        }

        public Comment DeleteComment(int commentId)
        {
            throw new NotImplementedException();
        }
    }
}