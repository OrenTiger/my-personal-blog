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

        public ViewResult Index()
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

        [HttpPost]
        public ActionResult Save(Settings settings)
        {            
            if (ModelState.IsValid)
            {
                var settingsDictionary = new Dictionary<string, string>();
                Type type = typeof(Settings);
                PropertyInfo[] properties = type.GetProperties();

                foreach (var property in properties)
                {
                    settingsDictionary[property.Name] = property.GetValue(settings).ToString();
                }

                // TODO: Добавить проверку на ошибки и сообщение об успешном сохранении настройки
                _settingsProvider.SaveAllSettings(settingsDictionary);

                return RedirectToAction("Index", "Settings");
            }

            return View("Edit", settings);
        }
    }
}