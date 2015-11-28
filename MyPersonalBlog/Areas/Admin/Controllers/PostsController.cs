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
    [Authorize]
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

        public ViewResult List(int? page, string order, string published)
        {
            int pageNumber = (page ?? 1);

            ViewBag.IdOrder = String.IsNullOrEmpty(order) ? "IdAsc" : "";
            ViewBag.DateOrder = order == "Date" ? "DateAsc" : "Date";

            var result = _postRepository.GetPosts;

            if (published == "hide")
            {
                result = result.Where(p => p.Published == true);
            }

            switch (order)
            {
                case "IdAsc":
                    result = result.OrderBy(p => p.Id);
                    break;
                case "DateAsc":
                    result = result.OrderBy(p => p.CreateDate);
                    break;
                case "Date":
                    result = result.OrderByDescending(p => p.CreateDate);
                    break;
                default:
                    result = result.OrderByDescending(p => p.Id);
                    break;
            }

            result = result.ToPagedList(pageNumber, PageSize);

            return View(result);
        }

        public ActionResult Edit(int id)
        {
            var result = _postRepository.GetPostById(id);
                
            if (result != null)
            {
                return View(result);
            }

            return new HttpNotFoundResult();
        }
    }
}