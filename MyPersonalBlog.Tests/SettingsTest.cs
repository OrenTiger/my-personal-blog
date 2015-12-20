using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyPersonalBlog.Infrastructure;
using MyPersonalBlog.Models;
using MyPersonalBlog.Areas.Admin.Controllers;

namespace MyPersonalBlog.Tests
{
    [TestClass]
    public class SettingsTest
    {
        Settings _settings;

        public SettingsTest()
        {
            _settings = new Settings
            {
                Id = 1,
                PostListPageSize = 5,
                PageSize = 7,
                AdminEmail = "admin@mypersonalblog.com"
            };
        }

        [TestMethod]
        public void Can_Get_Settings()
        {
            // Организация - создание имитированного поставщика
            Mock<ISettingsProvider> mock = new Mock<ISettingsProvider>();

            mock.Setup(s => s.GetSettings()).Returns(_settings);

            // Организация - создание контроллера
            SettingsController target = new SettingsController(mock.Object);

            // Действие - получаем все настройки
            var result = (Settings)target.Index().Model;
            
            // Утверждение - значения настроек равны
            Assert.AreEqual(7, result.PageSize);
            Assert.AreEqual(5, result.PostListPageSize);
        }

        [TestMethod]
        public void Can_Save_Settings()
        {
            // Организация - создание имитированного поставщика
            Mock<ISettingsProvider> mock = new Mock<ISettingsProvider>();

            // Организация - создание контроллера
            SettingsController target = new SettingsController(mock.Object);

            Settings settings = new Settings { PostListPageSize = 7, PageSize = 5, AdminEmail = "admin@mypersonalblog.com" };

            // Действие - сохраняем все настройки
            ActionResult result = target.Save(settings);

            // Утверждение - тип результата метода после успешного сохранения настроек равен RedirectToRouteResult
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }
    }
}
