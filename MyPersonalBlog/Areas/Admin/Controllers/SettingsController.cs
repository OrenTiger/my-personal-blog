using System.Web.Mvc;
using MyPersonalBlog.Infrastructure;
using MyPersonalBlog.Models;

namespace MyPersonalBlog.Areas.Admin.Controllers
{
    [Authorize]    
    public class SettingsController : Controller
    {
        ISettingsProvider _settingsProvider;

        public SettingsController(ISettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
        }

        public ViewResult Index()
        {

            Settings settings = _settingsProvider.GetSettings();

            return View("Edit", settings);
        }

        [HttpPost]
        public ActionResult Save(Settings settings)
        {            
            if (ModelState.IsValid)
            {
                _settingsProvider.SaveSettings(settings);

                return RedirectToAction("Index", "Settings");
            }

            return View("Edit", settings);
        }
    }
}