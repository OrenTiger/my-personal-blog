using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
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

        [ChildActionOnly]
        [OutputCache(Duration=3600)]
        public PartialViewResult List()
        {
            var tags = _tagRepository.Tags.OrderBy(t => t.Name);
            return PartialView(tags);
        }
    }
}