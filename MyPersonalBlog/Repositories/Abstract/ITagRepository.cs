using System.Collections.Generic;
using MyPersonalBlog.Models;

namespace MyPersonalBlog.Repositories
{
    public interface ITagRepository
    {
        IEnumerable<Tag> Tags { get; }

        Tag GetById(int id);

        void Save(Tag post);

        Tag Delete(int tagId);
    }
}