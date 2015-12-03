using System;
using System.Collections.Generic;
using System.Linq;
using MyPersonalBlog.Models;
using System.Data.Entity;

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

        public Post GetById(int id)
        {
            return _db.Posts
                .Where(p => p.Id == id)
                .Include(p => p.Comments)
                .FirstOrDefault();
        }

        public void Save(Post post, int[] selectedTags)
        {
            if (post.Id == 0)            
            {
                if (selectedTags != null)
                {
                    foreach (var tag in _db.Tags.Where(t => selectedTags.Contains(t.Id)))
                    {
                        post.Tags.Add(tag);
                    }
                }

                post.CreateDate = DateTime.Now;
                _db.Posts.Add(post);
            }
            else
            {
                Post dbEntry = _db.Posts.Find(post.Id);

                if (dbEntry != null)
                {
                    dbEntry.Title = post.Title;
                    dbEntry.IntroText = post.IntroText;
                    dbEntry.MainText = post.MainText;              
                    dbEntry.IsPublished = post.IsPublished;

                    if (selectedTags != null)
                    {
                        dbEntry.Tags.Clear();

                        foreach (var tag in _db.Tags.Where(t => selectedTags.Contains(t.Id)))
                        {
                            dbEntry.Tags.Add(tag);
                        }
                    }

                    _db.Entry(dbEntry).State = EntityState.Modified;
                }
            }

            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            Post dbEntry = _db.Posts.Find(id);

            if (dbEntry != null)
            {
                _db.Posts.Remove(dbEntry);
                _db.SaveChanges();
            }
        }
    }
}