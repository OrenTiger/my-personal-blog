using System.Collections.Generic;
using MyPersonalBlog.Models;

namespace MyPersonalBlog.Repositories
{
    public interface ISettingsRepository
    {
        IEnumerable<Settings> Settings { get; }

        void Save(Settings settings);
    }
}