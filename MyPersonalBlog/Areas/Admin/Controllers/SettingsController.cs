using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyPersonalBlog.Infrastructure;
using MyPersonalBlog.Areas.Admin.Models;
using System.Reflection;

namespace MyPersonalBlog.Areas.Admin.Controllers
{
    public class SettingsController : Controller
    {
        ISettingsProvider _settingsProvider;

        public SettingsController(ISettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
        }

        public ActionResult Index()
        {            
            var settingsDictionary = _settingsProvider.GetAllSettings();

            Settings settings = new Settings();

            // Заполняем поля объекта модели значениями настроек через reflection
            Type type = typeof(Settings);
            PropertyInfo[] properties = type.GetProperties();

            // TODO: Добавить обработку ошибок и error page (отдельную админскую error page с exception message)

            foreach (var property in properties)
            {
                object temp = property.GetValue(settings);

                if (temp is int)
                {
                    int value = 0;
                    Int32.TryParse(settingsDictionary[property.Name], out value);
                    typeof(Settings).GetProperty(property.Name).SetValue(settings, value);
                }
                else
                {
                    typeof(Settings).GetProperty(property.Name).SetValue(settings, settingsDictionary[property.Name]);
                }
            }

            return View("Edit", settings);
        }

        public ActionResult Save()
        {
            return new HttpNotFoundResult();
        }
    }
}