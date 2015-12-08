using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPersonalBlog.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult About()
        {
            return View();
        }

        public ViewResult Contacts()
        {            
            return View();
        }
    }
}