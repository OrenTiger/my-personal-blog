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
        private ITagRepository _tagRepository;
        
        // TODO: Сделать возможность установки количества постов на страницу в админке
        public int PageSize { get; set; }

        public PostsController(IPostRepository postRepository, ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
            PageSize = 5;
        }

        public ViewResult List(int? page, string order, string published)
        {
            int pageNumber = (page ?? 1);

            ViewBag.CurrentUrl = Request.QueryString;

            ViewBag.IdOrder = String.IsNullOrEmpty(order) ? "IdAsc" : "";
            ViewBag.DateOrder = order == "Date" ? "DateAsc" : "Date";
            
            // Сохраняем URL со всеми параметрами QueryString для последующих запросов
            Session["PostsListUrlWithParams"] = Request.Url.AbsoluteUri;

            var result = _postRepository.Posts;

            if (published == "hide")
            {
                result = result.Where(p => p.IsPublished == true);
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

        public ActionResult Add()
        {
            Post post = new Post();

            ViewBag.ActionName = "Пост - Добавление";
            ViewBag.Tags = _tagRepository.GetTags.ToList();

            return View("Edit", post);
        }

        public ActionResult Edit(int id)
        {
            var post = _postRepository.GetById(id);
                
            if (post == null)
            {
                return new HttpNotFoundResult();
            }

            ViewBag.ActionName = "Пост - Редактирование";
            ViewBag.Tags = _tagRepository.GetTags.ToList();

            return View(post);
        }

        [HttpPost]
        public ActionResult Save(Post post, int[] selectedTags)
        {
            if (ModelState.IsValid)
            {                
                _postRepository.Save(post, selectedTags);

                return Redirect(Session["PostsListUrlWithParams"] != null ? Session["PostsListUrlWithParams"].ToString() : Url.Action("List", "Posts"));
            }

            ViewBag.Tags = _tagRepository.GetTags.ToList();
            return View(post);
        }

        public ActionResult PreView(int id)
        {
            var post = _postRepository.GetById(id);
      
            if (post == null)
            {
                return new HttpNotFoundResult();
            }

            return PartialView("_PreView", post);
        }

        public ActionResult ConfirmDelete(int id)
        {
            var post = _postRepository.GetById(id);

            if (post == null)
            {
                return new HttpNotFoundResult();
            }

            return PartialView("_ConfirmDeleteView", post);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _postRepository.Delete(id);

            string redirectUrl = Session["PostsListUrlWithParams"].ToString() ?? Url.Action("List", "Posts");

            return Redirect(redirectUrl);
        }
    }
}