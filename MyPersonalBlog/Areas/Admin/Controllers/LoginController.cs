﻿using System.Web.Mvc;
using MyPersonalBlog.Infrastructure;
using MyPersonalBlog.ViewModels;

namespace MyPersonalBlog.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private IAuthProvider _authProvider;

        public LoginController(IAuthProvider authProvider)
        {
            _authProvider = authProvider;
        }

        public ActionResult SignIn()        
        {            
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("List", "Posts", new { area = "Admin" });
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult SignIn(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_authProvider.Authenticate(model.Login, model.PasswordHash))
                {
                    return Redirect(returnUrl ?? Url.Action("List", "Posts"));
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