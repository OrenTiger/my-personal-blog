using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Moq;
using MyPersonalBlog.Models;
using MyPersonalBlog.Repositories;
using MyPersonalBlog.Controllers;

namespace MyPersonalBlog.Tests
{
    [TestClass]
    public class ManagePostsTests
    {
        [TestMethod]
        public void Can_Get_All_Posts()
        {
            // Организация - создание имитированного хранилища
            Mock<IPostRepository> mock = new Mock<IPostRepository>();

            mock.Setup(m => m.Posts).Returns(new Post[] 
            {
                new Post { Id = 1, Title = "Title 1", IntroText = "Intro Text 1", MainText = "Main Text 1", CreateDate = DateTime.Now, Published = true, Tags = null },
                new Post { Id = 2, Title = "Title 2", IntroText = "Intro Text 2", MainText = "Main Text 2", CreateDate = DateTime.Now, Published = true, Tags = null }
            });

            // Организация - создание контроллера
            PostsController target = new PostsController(mock.Object);

            // Действие - получаем все записи постов из хранилища
            int result = ((IEnumerable<Post>)target.Index().Model).Count();

            // Утверждение - количество записей в хранилище и полученных в методе равны
            Assert.AreEqual(result, mock.Object.Posts.Count());
        }
    }
}
