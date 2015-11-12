using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyPersonalBlog.Models;
using MyPersonalBlog.Repositories;
using System.Data.Entity;
using MyPersonalBlog.Infrastructure;

namespace MyPersonalBlog.Controllers
{    
    public class PostsController : Controller
    {
        private IPostRepository _postRepository;

        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public ViewResult Index()
        {
            var result = _postRepository.GetPosts
                .Where(p => p.Published == true);

            return View(result);
        }

        public ActionResult View(int id)
        {
            // TODO: Добавить добавление комментариев

            var result = _postRepository.GetPostById(id);
                
            if (result != null && result.Published == true)
            {
                return View("Detailed", result);
            }

            return new HttpNotFoundResult();
        }
    }
}