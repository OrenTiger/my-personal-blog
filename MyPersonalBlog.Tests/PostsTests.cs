using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Moq;
using System.Web.Mvc;
using MyPersonalBlog.Models;
using MyPersonalBlog.Repositories;
using MyPersonalBlog.Controllers;
using MyPersonalBlog.Infrastructure;

namespace MyPersonalBlog.Tests
{
    [TestClass]
    public class PostsTests
    {
        private IList<Post> _posts;
        private Settings _settings;
        private Mock<ISettingsProvider> _mockSettingsProvider;
        private Mock<IPostRepository> _mock;
        private PostsController _target;

        public PostsTests()
        {
            _posts = new List<Post>
            {
                new Post { Id = 1, Title = "Title 1", IntroText = "Intro Text 1", MainText = "Main Text 1", CreateDate = DateTime.Now, IsPublished = true, Tags = null },
                new Post { Id = 2, Title = "Title 2", IntroText = "Intro Text 2", MainText = "Main Text 2", CreateDate = DateTime.Now, IsPublished = true, Tags = null },
                new Post { Id = 3, Title = "Title 3", IntroText = "Intro Text 3", MainText = "Main Text 3", CreateDate = DateTime.Now, IsPublished = false, Tags = null },
                new Post { Id = 4, Title = "Title 4", IntroText = "Intro Text 4", MainText = "Main Text 4", CreateDate = DateTime.Now, IsPublished = true, Tags = null },
                new Post { Id = 5, Title = "Title 5", IntroText = "Intro Text 5", MainText = "Main Text 5", CreateDate = DateTime.Now, IsPublished = true, Tags = null },
            };

            _settings = new Settings
            {
                Id = 1,
                PageSize = 5,
                PostListPageSize = 5,
                AdminEmail = "admin@yellowduckblog.com"
            };

            // Организация - создание имитированного поставщика настроек
            _mockSettingsProvider = new Mock<ISettingsProvider>();
            
        }

        [TestInitialize]
        public void SetupContext()
        {
            // Организация - создание имитированного хранилища
            _mock = new Mock<IPostRepository>();

            // Организация - создание имитированного поставщика настроек
            _mockSettingsProvider = new Mock<ISettingsProvider>();
            _mockSettingsProvider.Setup(s => s.GetSettings()).Returns(_settings);

            _target = new PostsController(_mock.Object, _mockSettingsProvider.Object);
        }

        [TestMethod]
        public void Can_Get_All_Published_Posts()
        {
            // Организация - настройка имитированного хранилища
            _mock.Setup(m => m.Posts).Returns(_posts);
            
            // Действие - получаем все записи постов из хранилища
            int result = ((IEnumerable<Post>)_target.Index(null).Model).Count();

            // Утверждение - количество записей в хранилище и полученных в методе равны
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void Can_Paginate()
        {
            // Организация - настройка имитированного хранилища
            _mock.Setup(m => m.Posts).Returns(_posts);
            _target.PageSize = 2;

            // Действие - получаем все записи постов на второй странице
            var result = (IEnumerable<Post>)_target.Index(2).Model;

            // Утверждение - количество записей в хранилище и полученных в методе равны, и также равны сами записи           
            Post[] postsArray = result.ToArray();
            Assert.IsTrue(postsArray.Length == 2);
            Assert.AreEqual("Title 2", postsArray[0].Title);
            Assert.AreEqual("Title 1", postsArray[1].Title);
        }

        [TestMethod]
        public void Can_Get_Single_Existing_Post()
        {
            // Организация - настройка имитированного хранилища
            _mock.Setup(m => m.GetById(It.IsAny<int>())).Returns((int id) => _posts.Where(p => p.Id == id && p.IsPublished == true).SingleOrDefault());

            // Действие - получаем отдельный пост
            var result = _target.View(2);

            // Утверждение - в результате метода возвращается представление
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsNotNull(((ViewResult)result).Model);
        }

        [TestMethod]
        public void Cannot_Get_Single_Not_Existing_Post()
        {
            // Организация - настройка имитированного хранилища
            _mock.Setup(m => m.GetById(It.IsAny<int>())).Returns((int id) => _posts.Where(p => p.Id == id && p.IsPublished == true).SingleOrDefault());

            // Действие - получаем отдельный пост
            var result = _target.View(0);

            // Утверждение - в результате метода возвращается представление "_Error"
            Assert.AreEqual("_Error", result.ViewName);
        }
    }
}
