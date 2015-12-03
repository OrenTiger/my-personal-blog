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
        
        // TODO: Сделать возможность установки количества постов на страницу в админке
        public int PageSize { get; set; }

        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
            PageSize = 3;
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
                return View("Detailed", result);
            }

            return new HttpNotFoundResult();
        }
    }
}