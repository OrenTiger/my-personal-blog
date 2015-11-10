using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyPersonalBlog.Models;
using MyPersonalBlog.Controllers;
using MyPersonalBlog.Infrastructure;


namespace MyPersonalBlog.Tests
{
    [TestClass]
    public class LoginTests
    {
        [TestMethod]
        public void Can_Login_With_Valid_Credentials()
        {
            // Организация
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "12345")).Returns(true);

            LoginViewModel model = new LoginViewModel
            {
                Login = "admin",
                PasswordHash = "12345"
            };

            LoginController target = new LoginController(mock.Object);         

            // Действие
            ActionResult result = target.SignIn(model, "/SignInSuccessURL");

            // Утверждение
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/SignInSuccessURL", ((RedirectResult)result).Url);
        }

        [TestMethod]
        public void Cannot_Login_With_Wrong_Credentials()
        {
            // Организация
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("wrongLogin", "wrongPassword")).Returns(false);

            LoginViewModel model = new LoginViewModel
            {
                Login = "wrongLogin",
                PasswordHash = "wrongPassword"
            };

            LoginController target = new LoginController(mock.Object);

            // Действие
            ActionResult result = target.SignIn(model, "/SignInFailureURL");

            // Утверждение
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }
    }
}
