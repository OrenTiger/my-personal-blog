using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyPersonalBlog.Models;
using MyPersonalBlog.Repositories;

namespace MyPersonalBlog.Controllers
{
    public class TagsController : Controller
    {
        private ITagRepository _tagRepository;

        public TagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        // GET: Tags
        public ActionResult SearchList()
        {
            var tags = _tagRepository.GetTags.OrderBy(t => t.Name);
            return View(tags);
        }
    }
}