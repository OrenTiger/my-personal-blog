using System.Collections.Generic;
using MyPersonalBlog.Models;

namespace MyPersonalBlog.Repositories
{
    public class SettingsRepository : ISettingsRepository
    {
        private BlogContext _db = new BlogContext();

        public IEnumerable<Settings> Settings
        {
            get
            {
                return _db.Settings;
            }
        }

        public void Save(Settings settings)
        {
            Settings dbEntry = _db.Settings.Find(settings.Id);

            if (dbEntry != null)
            {
                dbEntry.PostListPageSize = settings.PostListPageSize;
                dbEntry.PageSize = settings.PageSize;
                dbEntry.AdminEmail = settings.AdminEmail;
                _db.SaveChanges();
            }
        }

    }
}