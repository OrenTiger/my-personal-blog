using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Configuration;

namespace MyPersonalBlog.Infrastructure
{
    public class SettingsProvider : ISettingsProvider
    {
        public T GetSetting<T>(string key)
        {
            var result = default(T);
            var setting = WebConfigurationManager.AppSettings[key];
            if (!String.IsNullOrEmpty(setting))
            {
                result = (T)Convert.ChangeType(setting, typeof(T));
            }
            return result;
        }

        public void SetSetting<T>(string key, T value)
        {
            if (value != null)
            {
                string settingValue = value.ToString();

                Configuration config = WebConfigurationManager.OpenWebConfiguration("~");

                var section = (AppSettingsSection)config.GetSection("appSettings");

                section.Settings[key].Value = settingValue;
                config.Save();
            }
        }

        public IDictionary<string, string> GetAllSettings()
        {
            var settings = new Dictionary<string, string>();
            var appSettings = WebConfigurationManager.AppSettings;
            
            foreach (var setting in appSettings.AllKeys)
            {
                settings.Add(setting, appSettings[setting]);
            }

            return settings;
        }

        public void SaveAllSettings(IDictionary<string, string> settings)
        {
            if (settings != null)
            {
                Configuration config = WebConfigurationManager.OpenWebConfiguration("~");

                var section = (AppSettingsSection)config.GetSection("appSettings");

                foreach (var item in settings)
                {                    
                    section.Settings[item.Key].Value = item.Value;
                }
                
                config.Save();
            }
        }
    }
}