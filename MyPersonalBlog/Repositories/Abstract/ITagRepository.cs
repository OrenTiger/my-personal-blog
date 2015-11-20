using System.Collections.Generic;
using MyPersonalBlog.Models;

namespace MyPersonalBlog.Repositories
{
    public interface ITagRepository
    {
        IEnumerable<Tag> GetTags { get; }

        Tag GetTagById(int id);

        void SaveTag(Tag post);

        Tag DeleteTag(int tagId);
    }
}