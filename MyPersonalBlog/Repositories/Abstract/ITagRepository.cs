using System.Collections.Generic;
using MyPersonalBlog.Models;

namespace MyPersonalBlog.Repositories
{
    public interface ITagRepository
    {
        IEnumerable<Tag> Tags { get; }

        Tag GetById(int id);

        void Save(Tag tag);

        void Delete(int id);
    }
}