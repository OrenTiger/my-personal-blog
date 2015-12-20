using System.Linq;
using System.Web.Mvc;
using PagedList;
using MyPersonalBlog.Models;
using MyPersonalBlog.Repositories;
using MyPersonalBlog.Infrastructure;

namespace MyPersonalBlog.Areas.Admin.Controllers
{
    [Authorize]
    public class TagsController : Controller
    {
        ITagRepository _tagRepository;
        ISettingsProvider _settingsProvider;
        public int PageSize { get; set; }

        public TagsController(ITagRepository tagRepository, ISettingsProvider settingsProvider)
        {
            _tagRepository = tagRepository;
            _settingsProvider = settingsProvider;

            PageSize = _settingsProvider.GetSettings().PageSize;
        }
        
        public ViewResult List(int? page)
        {
            int pageNumber = (page ?? 1);
            TempData["page"] = pageNumber;

            var result = _tagRepository.Tags.OrderBy(t => t.Name).ToPagedList(pageNumber, PageSize);

            return View(result);
        }

        public ActionResult GetTag(int id, string action)
        {
            var result = _tagRepository.GetById(id);

            if (result != null)
            {
                string viewName = "_EditTag";

                switch (action)
                {
                    case "editTag":
                        viewName = "_EditTag";
                        break;
                    case "confirmDelete":
                        viewName = "_ConfirmDelete";
                        break;
                }

                return PartialView(viewName, result);
            }

            return new HttpNotFoundResult();
        }

        [HttpPost]
        public ActionResult Save(Tag tag)
        {
            if (ModelState.IsValid)
            {
                _tagRepository.Save(tag);

                return RedirectToAction("List", new { page = TempData["page"] });
            }

            return View(tag);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _tagRepository.Delete(id);

            return RedirectToAction("List");
        }
    }
}