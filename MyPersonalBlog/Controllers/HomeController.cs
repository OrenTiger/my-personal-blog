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
            try
            {
                throw new Exception();
                
            }
            catch (Exception ex)
            {
                Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(ex));
            }

            return View();
        }

        public ViewResult Contacts()
        {            
            return View();
        }
    }
}