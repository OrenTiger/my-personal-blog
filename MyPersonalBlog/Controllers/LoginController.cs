using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyPersonalBlog.Infrastructure;
using MyPersonalBlog.ViewModels;

namespace MyPersonalBlog.Controllers
{
    public class LoginController : Controller
    {
        private IAuthProvider _authProvider;

        public LoginController(IAuthProvider authProvider)
        {
            _authProvider = authProvider;
        }

        public ViewResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_authProvider.Authenticate(model.Login, model.PasswordHash))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("error", "Неправильный логин или пароль");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult SignOut()
        {
            _authProvider.SignOut();

            return RedirectToAction("SignIn", "Login");
        }
    }
}