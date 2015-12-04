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
    // TODO: Добавить тестовые методы (аналогично AdminCommentsTests)
    [TestClass]
    public class PostsTests
    {
        private IList<Post> _posts;

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
        }

        [TestMethod]
        public void Can_Get_All_Published_Posts()
        {
            // Организация - создание имитированного хранилища
            Mock<IPostRepository> mock = new Mock<IPostRepository>();

            mock.Setup(m => m.Posts).Returns(_posts);

            // Организация - создание контроллера
            PostsController target = new PostsController(mock.Object);
            target.PageSize = 5;

            // Действие - получаем все записи постов из хранилища
            int result = ((IEnumerable<Post>)target.Index(null).Model).Count();

            // Утверждение - количество записей в хранилище и полученных в методе равны
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void Can_Paginate()
        {
            // Организация - создание имитированного хранилища
            Mock<IPostRepository> mock = new Mock<IPostRepository>();

            mock.Setup(m => m.Posts).Returns(_posts);

            // Организация - создание контроллера
            PostsController target = new PostsController(mock.Object);
            target.PageSize = 2;

            // Действие - получаем все записи постов на второй странице
            var result = (IEnumerable<Post>)target.Index(2).Model;

            // Утверждение - количество записей в хранилище и полученных в методе равны, и также равны сами записи           
            Post[] postsArray = result.ToArray();
            Assert.IsTrue(postsArray.Length == 2);
            Assert.AreEqual("Title 2", postsArray[0].Title);
            Assert.AreEqual("Title 1", postsArray[1].Title);
        }

        [TestMethod]
        public void Can_Get_Single_Existing_Post()
        {
            // Организация - создание имитированного хранилища
            Mock<IPostRepository> mock = new Mock<IPostRepository>();

            mock.Setup(m => m.GetById(It.IsAny<int>())).Returns((int id) => _posts.Where(p => p.Id == id && p.IsPublished == true).SingleOrDefault());

            // Организация - создание контроллера
            PostsController target = new PostsController(mock.Object);

            // Действие - получаем отдельный пост
            var result = target.View(2);

            // Утверждение - в результате метода возвращается представление
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsNotNull(((ViewResult)result).Model);
        }

        [TestMethod]
        public void Cannot_Get_Single_Not_Existing_Post()
        {
            // Организация - создание имитированного хранилища
            Mock<IPostRepository> mock = new Mock<IPostRepository>();

            mock.Setup(m => m.GetById(It.IsAny<int>())).Returns((int id) => _posts.Where(p => p.Id == id && p.IsPublished == true).SingleOrDefault());

            // Организация - создание контроллера
            PostsController target = new PostsController(mock.Object);

            // Действие - получаем отдельный пост
            var result = target.View(0);

            // Утверждение - в результате метода возвращается код статуса "Not Found"
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }
    }
}
