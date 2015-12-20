using MyPersonalBlog.Models;

namespace MyPersonalBlog.Infrastructure
{
    public interface ISettingsProvider
    {
        Settings GetSettings();
        void SaveSettings(Settings settings);
    }
}
