using System;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using System.Collections.Generic;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyPersonalBlog.Models;
using MyPersonalBlog.Repositories;
using MyPersonalBlog.Areas.Admin.Controllers;
using PagedList;
using MyPersonalBlog.Infrastructure;

namespace MyPersonalBlog.Tests
{
    [TestClass]
    public class AdminCommentTests
    {
        private IList<Comment> _comments;
        private Mock<ICommentRepository> _mock;
        private Mock<ISettingsProvider> _mockSettingsProvider;
        private CommentsController _target;

        public AdminCommentTests()
        {
            _comments = new List<Comment>
            {
                new Comment { Id = 1, Username = "User 1", Text = "Comment 1", CreateDate = DateTime.Now, IsApproved = false, PostId = 1 },
                new Comment { Id = 2, Username = "User 2", Text = "Comment 2", CreateDate = DateTime.Now.AddMinutes(1), IsApproved = false, PostId = 1 },
                new Comment { Id = 3, Username = "User 3", Text = "Comment 3", CreateDate = DateTime.Now.AddMinutes(2), IsApproved = false, PostId = 1 },
                new Comment { Id = 4, Username = "User 4", Text = "Comment 4", CreateDate = DateTime.Now.AddMinutes(3), IsApproved = true, PostId = 1 },
                new Comment { Id = 5, Username = "User 5", Text = "Comment 5", CreateDate = DateTime.Now.AddMinutes(4), IsApproved = true, PostId = 1 }
            };
        }

        [TestInitialize]
        public void SetupContext()
        {
            // Организация - создание имитированного хранилища
            _mock = new Mock<ICommentRepository>();
            _mockSettingsProvider = new Mock<ISettingsProvider>();

            _target = new CommentsController(_mock.Object, _mockSettingsProvider.Object);
        }

        [TestMethod]
        public void Can_Get_Comment_By_Id()
        {
            // Организация
            _mock.Setup(m => m.GetById(It.IsAny<int>())).Returns((int id) => _comments.Where(c => c.Id == id).SingleOrDefault());

            // Действие - получаем отдельный комментарий
            var result = _target.Edit(2);

            // Утверждение - в результате метода возвращается представление
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsNotNull(((ViewResult)result).Model);
        }
        
        [TestMethod]
        public void Can_Not_Get_Comment_By_Not_Existing_Id()
        {
            // Организация
            _mock.Setup(m => m.GetById(It.IsAny<int>())).Returns((int id) => _comments.Where(c => c.Id == id).SingleOrDefault());

            // Действие - получаем отдельный комментарий
            var result = _target.Edit(0);

            // Утверждение - в результате метода возвращается результат NotFound
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }
        
        [TestMethod]
        public void Can_Get_All_Not_Approved_Comments()
        {
            // Организация
            _mock.Setup(m => m.Comments).Returns(_comments.OrderByDescending(c => c.CreateDate));

            // Действие - получаем неопубликованные комментарии
            var result = (IEnumerable<Comment>)_target.List(null, null, "hide").Model;

            // Утверждение - количество записей в хранилище и полученных в методе равны, и также равны сами записи
            Assert.IsNotNull(result);
            Comment[] commentsArray = result.ToArray();
            Assert.IsTrue(commentsArray.Length == 3);
            Assert.AreEqual(3, commentsArray[0].Id);
            Assert.AreEqual(2, commentsArray[1].Id);
            Assert.AreEqual(1, commentsArray[2].Id);
        }
        
        [TestMethod]
        public void Can_Paginate()
        {
            // Организация            
            _mock.Setup(m => m.Comments).Returns(_comments);
            _target.PageSize = 3;

            // Действие - получаем все записи постов на второй странице
            var result = (IEnumerable<Comment>)_target.List(2, null, null).Model;

            // Утверждение - количество записей в хранилище и полученных в результате метода равны, и также равны сами записи
            Assert.IsNotNull(result);
            Comment[] commentsArray = result.ToArray();
            Assert.IsTrue(commentsArray.Length == 2);
            Assert.AreEqual(2, commentsArray[0].Id);
            Assert.AreEqual(1, commentsArray[1].Id);
        }
        
        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            // Организация - создание комментария
            Comment comment = new Comment { Id = 1, Username = "Test User", Text = "Comment text", CreateDate = DateTime.Now, IsApproved = true, PostId = 1 };

            // Действие - попытка сохранения товара
            ActionResult result = _target.Save(comment);

            // Утверждение - проверка того, что к хранилищу производится обращение
            _mock.Verify(m => m.Save(comment));

            // Утверждение - проверка результата метода
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {            
            // Организация - создание комментария
            Comment comment = new Comment { Id = 1, Username = "Test User", Text = "Comment text", CreateDate = DateTime.Now, IsApproved = true, PostId = 1 };

            // Организация - добавление ошибки в состояние модели
            _target.ModelState.AddModelError("error", "error");

            // Действие - попытка сохранения товара
            ActionResult result = _target.Save(comment);

            // Утверждение - проверка того, что обращения к хранилищу не производится
            _mock.Verify(m => m.Save(It.IsAny<Comment>()), Times.Never);

            // Утверждение - проверка типа результата метода
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Delete_Valid_Comment()
        {
            // Организация
            _mock.Setup(m => m.Comments).Returns(_comments);

            // Действие - удаление комментария
            _target.Delete(1);

            // Утверждение - проверка того, что метод удаления в хранилище вызывается для существующего комментария
            _mock.Verify(m => m.Delete(1));
        }
    }
}
