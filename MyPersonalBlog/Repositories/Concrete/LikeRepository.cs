using System.Collections.Generic;
using System.Linq;
using MyPersonalBlog.Models;

namespace MyPersonalBlog.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private BlogContext _db = new BlogContext();

        public IEnumerable<Like> Likes
        {
            get
            {
                return _db.Likes;
            }
        }

        public Like GetById(int id)
        {
            return _db.Likes
                .Where(l => l.Id == id)
                .FirstOrDefault();
        }

        public void Save(Like like)
        {
            if (like.Id == 0)
            {
                _db.Likes.Add(like);
            }
            else
            {
                Like dbEntry = _db.Likes.Find(like.Id);

                if (dbEntry != null)
                {
                    dbEntry.CommentId = like.CommentId;
                    dbEntry.UserId = like.UserId;
                    dbEntry.UserProvider = like.UserProvider;
                }
            }

            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            Like dbEntry = _db.Likes.Find(id);

            if (dbEntry != null)
            {
                _db.Likes.Remove(dbEntry);
                _db.SaveChanges();
            }
        }
    }
}