using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalBlog.Infrastructure
{
    public interface ISettingsProvider
    {
        IDictionary<string, string> GetAllSettings();
        void SaveAllSettings(IDictionary<string, string> settings);
        T GetSetting<T>(string key);
        void SetSetting<T>(string key, T value);
    }
}
