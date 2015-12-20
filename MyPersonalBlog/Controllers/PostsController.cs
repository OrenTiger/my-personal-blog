using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyPersonalBlog.Models;
using MyPersonalBlog.ViewModels;
using MyPersonalBlog.Repositories;
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

            PageSize = _settings.GetSettings().PostListPageSize;
        }

        public ViewResult Archive(int year, int month, int? page)
        {
            int pageNumber = (page ?? 1);

            var result = _postRepository.Posts
                .Where(p => p.IsPublished == true)
                .Where(p => p.CreateDate.Year == year)
                .Where(p => p.CreateDate.Month == month)
                .OrderByDescending(p => p.Id)
                .ToPagedList(pageNumber, PageSize);

            return View("Index", result);
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

        public ViewResult View(int id)
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

        [ChildActionOnly]
        [OutputCache(Duration = 900)]
        public PartialViewResult ArchiveModule()
        {
            var results = (from post in _postRepository.Posts.Where(p => p.IsPublished)
                           group post by post.CreateDate.Year into years
                           select 
                           new ArchivePostsViewModel
                               {
                                   Year = years.Key,
                                   Months = years.GroupBy(m => m.CreateDate.Month).
                                            Select(g => new { Month = g.Key, Count = g.Count() }).ToDictionary(t => t.Month, t => t.Count)
                               }
                           );

            return PartialView("_Archive", results);
        }
    }
}