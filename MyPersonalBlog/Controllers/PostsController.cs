using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyPersonalBlog.Models;
using MyPersonalBlog.Repositories;

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
            var result = _postRepository.Posts
                .Where(p => p.Published == true);

            return View(result);
        }
    }
}