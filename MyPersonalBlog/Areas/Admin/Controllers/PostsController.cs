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

namespace MyPersonalBlog.Areas.Admin.Controllers
{    
    public class PostsController : Controller
    {
        private IPostRepository _postRepository;
        
        // TODO: Сделать возможность установки количества постов на страницу в админке
        public int PageSize { get; set; }

        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
            PageSize = 5;
        }

        public ViewResult List(int? page)
        {
            int pageNumber = (page ?? 1);

            var result = _postRepository.GetPosts
                .OrderByDescending(p => p.Id)
                .ToPagedList(pageNumber, PageSize);

            return View(result);
        }

        public ActionResult Edit(int id)
        {
            var result = _postRepository.GetPostById(id);
                
            if (result != null)
            {
                return View("Detailed", result);
            }

            return new HttpNotFoundResult();
        }
    }
}