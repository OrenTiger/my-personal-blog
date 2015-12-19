using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPersonalBlog.Models;
using System.Data.Entity;

namespace MyPersonalBlog.Repositories
{
    public class TagRepository : ITagRepository
    {
        private BlogContext _db = new BlogContext();

        public IEnumerable<Tag> Tags
        {
            get
            {
                return _db.Tags;
            }
        }

        public Tag GetById(int id)
        {
            return _db.Tags
                .Where(t => t.Id == id)
                .FirstOrDefault();
        }

        public void Save(Tag tag)
        {
            if (tag.Id == 0)
            {
                _db.Tags.Add(tag);
            }
            else
            {
                Tag dbEntry = _db.Tags.Find(tag.Id);

                if (dbEntry != null)
                {
                    dbEntry.Name = tag.Name;
                }
            }

            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            Tag dbEntry = _db.Tags.Find(id);

            if (dbEntry != null)
            {
                _db.Tags.Remove(dbEntry);
                _db.SaveChanges();
            }
        }
    }
}