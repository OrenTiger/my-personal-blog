using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using MyPersonalBlog.Models;

namespace MyPersonalBlog.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        BlogContext _db = new BlogContext();

        public IEnumerable<Comment> Comments
        {
            get
            {
                return _db.Comments
                    .Include(c => c.Post);
            }
        }

        public Comment GetById(int id)
        {
            return _db.Comments
                .Where(c => c.Id == id)
                .Include(c => c.Post)
                .FirstOrDefault();
        }

        public void Save(Comment comment)
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
                    dbEntry.IsApproved = comment.IsApproved;
                }
            }

            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            Comment dbEntry = _db.Comments.Find(id);
            
            if (dbEntry != null)
            {
                _db.Comments.Remove(dbEntry);
                _db.SaveChanges();
            }
        }
    }
}