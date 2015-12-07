using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyPersonalBlog.Models;
using MyPersonalBlog.Repositories;
using System.Data.Entity;
using MyPersonalBlog.Infrastructure;
using PagedList;

namespace MyPersonalBlog.Controllers
{    
    public class PostsController : Controller
    {
        private IPostRepository _postRepository;
        private ISettingsProvider _settings;
        
        public int PageSize { get; set; }

        public PostsController(IPostRepository postRepository, ISettingsProvider settingsProvider)
        {
            _postRepository = postRepository;
            _settings = settingsProvider;

            PageSize = _settings.GetSetting<int>("PostListPageSize");
            PageSize = PageSize == 0 ? 5 : PageSize;
        }

        public ViewResult Index(int? page)
        {
            int pageNumber = (page ?? 1);

            var result = _postRepository.Posts
                .Where(p => p.IsPublished == true)
                .OrderByDescending(p => p.Id)
                .ToPagedList(pageNumber, PageSize);

            return View(result);
        }

        public ActionResult View(int id)
        {
            var result = _postRepository.GetById(id);

            if (result != null && result.IsPublished == true)
            {
                result.ViewsCount++;
                _postRepository.Save(result);
                return View("Detailed", result);
            }

            ViewBag.HttpCode = 404;
            return View("_Error");
        }
    }
}