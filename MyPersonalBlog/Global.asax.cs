﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using MyPersonalBlog.Repositories;
using MyPersonalBlog.Infrastructure;

namespace MyPersonalBlog
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new BlogDbInitializer());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(DateTime), new DateTimeModelBinder());
            //TODO: Добавить bundl'ы
            //TODO: Скомпилить bootstrap с нужными частями на http://getbootstrap.com/customize/
        }
    }
}