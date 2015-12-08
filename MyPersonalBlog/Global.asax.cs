using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using MyPersonalBlog.Repositories;
using MyPersonalBlog.Infrastructure;
using MyPersonalBlog.Controllers;
using Elmah;

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

        protected void Application_Error(object sender, EventArgs e)
        {            
            HttpContext httpContext = HttpContext.Current;

            // Для администратора отображаем стандартный вывод ошибки
            if (httpContext.User != null &&
                httpContext.User.Identity != null && 
                httpContext.User.Identity.IsAuthenticated)
            {
                return;
            }

            Exception ex = httpContext.Server.GetLastError();
            httpContext.Response.Clear();

            RequestContext requestContext = ((MvcHandler)httpContext.CurrentHandler).RequestContext;
            IController controller = new HomeController();
            var context = new ControllerContext(requestContext, (ControllerBase)controller);

            ViewResult viewResult = new ViewResult();
            HttpException httpException = ex as HttpException;

            if (httpException != null)
            {
                viewResult.ViewBag.HttpCode = httpException.GetHttpCode();

                // Исправляем ошибку ELMAH (Request Validation errors does not get logged)
                // https://code.google.com/p/elmah/issues/detail?id=217
                if (httpException is HttpRequestValidationException)
                {
                    Elmah.ErrorLog.GetDefault(httpContext).Log(new Error(httpException));
                }
            }

            viewResult.ViewName = "_Error";
            viewResult.ViewData.Model = new HandleErrorInfo(ex, context.RouteData.GetRequiredString("controller"), context.RouteData.GetRequiredString("action"));
            viewResult.ExecuteResult(context);
            httpContext.ClearError();
        }
       
        public void ErrorLog_Filtering(object sender, ExceptionFilterEventArgs args)
        {
            Filter404Errors(args);
        }

        private void Filter404Errors(ExceptionFilterEventArgs args)
        {
            if (args.Exception.GetBaseException() is HttpException)
            {
                var httpException = args.Exception.GetBaseException() as HttpException;
                if (httpException != null && httpException.GetHttpCode() == 404)
                    args.Dismiss();
            }
        }
    }
}