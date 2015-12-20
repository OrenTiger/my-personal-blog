using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPersonalBlog.Models;

namespace MyPersonalBlog.Infrastructure
{
    public interface ISettingsProvider
    {
        Settings GetSettings();
        void SaveSettings(Settings settings);
    }
}
