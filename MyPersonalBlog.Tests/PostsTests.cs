using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Moq;
using System.Web.Mvc;
using MyPersonalBlog.Models;
using MyPersonalBlog.Repositories;
using MyPersonalBlog.Controllers;

namespace MyPersonalBlog.Tests
{
    [TestClass]
    public class PostsTests
    {
        private IList<Post> _posts;

        public PostsTests()
        {
            _posts = new List<Post>
            {
                new Post { Id = 1, Title = "Title 1", IntroText = "Intro Text 1", MainText = "Main Text 1", CreateDate = DateTime.Now, Published = true, Tags = null },
                new Post { Id = 2, Title = "Title 2", IntroText = "Intro Text 2", MainText = "Main Text 2", CreateDate = DateTime.Now, Published = true, Tags = null },
                new Post { Id = 3, Title = "Title 3", IntroText = "Intro Text 3", MainText = "Main Text 3", CreateDate = DateTime.Now, Published = false, Tags = null }
            };
        }


        [TestMethod]
        public void Can_Get_All_Published_Posts()
        {
            // Организация - создание имитированного хранилища
            Mock<IPostRepository> mock = new Mock<IPostRepository>();

            mock.Setup(m => m.GetPosts).Returns(_posts);

            // Организация - создание контроллера
            PostsController target = new PostsController(mock.Object);

            // Действие - получаем все записи постов из хранилища
            int result = ((IEnumerable<Post>)target.Index().Model).Count();

            // Утверждение - количество записей в хранилище и полученных в методе равны
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void Can_Get_Single_Existing_Post()
        {
            // Организация - создание имитированного хранилища
            Mock<IPostRepository> mock = new Mock<IPostRepository>();

            mock.Setup(m => m.GetPostById(It.IsAny<int>())).Returns((int id) => _posts.Where(p => p.Id == id && p.Published == true).SingleOrDefault());

            // Организация - создание контроллера
            PostsController target = new PostsController(mock.Object);

            // Действие - получаем отдельный пост
            var result = target.View(2);

            // Утверждение - в результате метода возвращается представление
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Get_Single_Not_Existing_Post()
        {
            // Организация - создание имитированного хранилища
            Mock<IPostRepository> mock = new Mock<IPostRepository>();

            mock.Setup(m => m.GetPostById(It.IsAny<int>())).Returns((int id) => _posts.Where(p => p.Id == id && p.Published == true).SingleOrDefault());

            // Организация - создание контроллера
            PostsController target = new PostsController(mock.Object);

            // Действие - получаем отдельный пост
            var result = target.View(0);

            // Утверждение - в результате метода возвращается код статуса "Not Found"
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }
    }
}
