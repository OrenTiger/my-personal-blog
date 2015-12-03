using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using MyPersonalBlog.Controllers;
using MyPersonalBlog.Models;
using MyPersonalBlog.Repositories;
using Moq;
using System.Web.Mvc;
using System.Web;

namespace MyPersonalBlog.Tests
{
    [TestClass]
    public class CommentTests
    {
        [TestMethod]
        public void Can_Save_Valid_Comment()
        {
            // Организация - создание имитированного хранилища
            Mock<ICommentRepository> mock = new Mock<ICommentRepository>();
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            request.Setup(r => r.UrlReferrer).Returns(new Uri("http://test.com"));
            context.Setup(c => c.Request).Returns(request.Object);

            // Организация - создание контроллера
            CommentController target = new CommentController(mock.Object);
            target.ControllerContext = new ControllerContext(context.Object, new System.Web.Routing.RouteData(), target);

            // Организация - создание комментария
            Comment comment = new Comment { Id = 1, CreateDate = DateTime.Now, Username = "Test User", Text = "Test Text", PostId = 1 };

            // Действие - сохраняем комментарий 
            ActionResult result = target.Save(comment);

            // Утверждение - проверка того, что к хранилищу производится обращение
            mock.Verify(m => m.Save(comment));

            // Утверждение - проверка типа результата метода
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Comment()
        {
            // Организация - создание имитированного хранилища
            Mock<ICommentRepository> mock = new Mock<ICommentRepository>();

            // Организация - создание контроллера
            CommentController target = new CommentController(mock.Object);

            // Организация - создание комментария
            Comment comment = new Comment { Id = 1, CreateDate = DateTime.Now, Username = "Test User", Text = "Test Text", PostId = 1 };

            // Организация - добавление ошибки в состояние модели
            target.ModelState.AddModelError("error", "error");

            // Действие - сохраняем комментарий 
            ActionResult result = target.Save(comment);

            // Утверждение - проверка того, что к хранилищу не производится обращение
            mock.Verify(m => m.Save(It.IsAny<Comment>()), Times.Never());

            // Утверждение - проверка типа результата метода
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
