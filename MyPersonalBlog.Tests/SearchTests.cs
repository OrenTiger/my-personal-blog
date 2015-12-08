using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Moq;
using System.Linq;
using MyPersonalBlog.Models;
using MyPersonalBlog.Controllers;
using MyPersonalBlog.Repositories;
using MyPersonalBlog.ViewModels;
using System.Web.Mvc;

namespace MyPersonalBlog.Tests
{
    [TestClass]
    public class SearchTests
    {
        private IList<Post> _posts;
        private IList<Tag> _tags;

        public SearchTests()
        {
            _posts = new List<Post>
            {
                new Post { Id = 1, Title = "Title 1", IntroText = "Intro Text 1", MainText = "Main Text 1", CreateDate = DateTime.Now, IsPublished = true, Tags = null },
                new Post { Id = 2, Title = "Title 2", IntroText = "Intro Text 2", MainText = "Main Text 2", CreateDate = DateTime.Now.AddMinutes(1), IsPublished = true, Tags = null },
                new Post { Id = 3, Title = "Title 3", IntroText = "Intro Text 3", MainText = "Main Text 3", CreateDate = DateTime.Now.AddMinutes(1), IsPublished = false, Tags = null },
                new Post { Id = 4, Title = "Title 4", IntroText = "Intro Text 4", MainText = "Main Text 4", CreateDate = DateTime.Now.AddMinutes(1), IsPublished = true, Tags = null },
                new Post { Id = 5, Title = "Title 5", IntroText = "Intro Text 5", MainText = "Main Text 5", CreateDate = DateTime.Now.AddMinutes(1), IsPublished = true, Tags = null },
            };

            _tags = new List<Tag>
            {
                new Tag { Id = 1, Name = "C#", Posts = new List<Post>() { _posts[0], _posts[1] } },
                new Tag { Id = 2, Name = "ASP.NET MVC", Posts = new List<Post>() { _posts[2], _posts[3] } },
                new Tag { Id = 3, Name = "Web API", Posts = new List<Post>() { _posts[4] } },
                new Tag { Id = 4, Name = "Web Dev", Posts = new List<Post>() { _posts[1] } }
            };
        }

        [TestMethod]
        public void Can_Search_By_Text()
        {
            // Организация - создание имитированного хранилища
            Mock<IPostRepository> mockPostRepository = new Mock<IPostRepository>();
            Mock<ITagRepository> mockTagRepository = new Mock<ITagRepository>();

            mockPostRepository.Setup(m => m.Posts).Returns(_posts);
            mockTagRepository.Setup(m => m.Tags).Returns(_tags);

            // Организация - создание контроллера
            SearchController target = new SearchController(mockPostRepository.Object, mockTagRepository.Object);

            // Действие - делаем запрос на поиск по тексту
            var result = (SearchViewModel)target.Search("Main Text 1", null, null, null).Model;            

            // Утверждение - количество найденных записей и записей в БД равно            
            Assert.AreEqual(1, result.CountFounded);

            // Утверждение - найденная запись соответствует запросу
            Assert.AreEqual("Title 1", ((IEnumerable<Post>)result.SearchResults).FirstOrDefault().Title);
        }

        [TestMethod]
        public void Can_Search_By_Tag()
        {
            // Организация - создание имитированного хранилища
            Mock<IPostRepository> mockPostRepository = new Mock<IPostRepository>();
            Mock<ITagRepository> mockTagRepository = new Mock<ITagRepository>();

            mockPostRepository.Setup(m => m.Posts).Returns(_posts);
            mockTagRepository.Setup(m => m.Tags).Returns(_tags);

            // Организация - создание контроллера
            SearchController target = new SearchController(mockPostRepository.Object, mockTagRepository.Object);

            // Действие - делаем запрос на поиск по тексту
            var result = (SearchViewModel)target.Search(null, 1, null, null).Model;

            // Утверждение - количество найденных записей и записей в БД равно            
            Assert.AreEqual(2, result.CountFounded);

            // Утверждение - найденные записи соответствуют запросу
            var listResult = result.SearchResults.ToList<Post>();
            Assert.AreEqual("Title 2", listResult[0].Title);
            Assert.AreEqual("Title 1", listResult[1].Title);
        }
    }
}
