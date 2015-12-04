using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using MyPersonalBlog.Models;
using MyPersonalBlog.Repositories;

namespace MyPersonalBlog.Areas.Admin.Controllers
{
    public class CommentsController : Controller
    {
        ICommentRepository _commentRepository;
        public int PageSize { get; set; }

        public CommentsController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
            PageSize = 3;
        }

        public ViewResult List(int? page, string order, string approved)
        {
            int pageNumber = (page ?? 1);
            TempData["page"] = pageNumber;
            TempData["approved"] = approved;
            TempData["order"] = String.IsNullOrEmpty(order) ? "DateAsc" : String.Empty;

            var result = _commentRepository.Comments;

            if (approved == "hide")
            {
                result = result.Where(c => c.IsApproved == false);
            }

            result = order == "DateAsc" ? result.OrderBy(c => c.CreateDate) : result.OrderByDescending(c => c.CreateDate);
           
            result = result.ToPagedList(pageNumber, PageSize);

            return View(result);
        }

        public ActionResult GetComment(int id, string action)
        {
            var result = _commentRepository.GetById(id);

            if (result != null)
            {
                string viewName = "_ViewComment";

                switch (action)
                {
                    case "viewComment":
                        viewName = "_ViewComment";
                        break;
                    case "confirmDelete":
                        viewName = "_ConfirmDelete";
                        break;
                }

                return PartialView(viewName, result);
            }

            return new HttpNotFoundResult();
        }

        public ActionResult Edit(int id)
        {
            var result = _commentRepository.GetById(id);

            if (result != null)
            {
                return View("Edit", result);
            }

            return new HttpNotFoundResult();
        }

        [HttpPost]
        public ActionResult Save(Comment comment)
        {
            if (ModelState.IsValid)
            {
                _commentRepository.Save(comment);

                return RedirectToAction("List", new { page = TempData["page"], approved = TempData["approved"] });
            }

            return View(comment);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _commentRepository.Delete(id);

            return RedirectToAction("List", new { page = TempData["page"], approved = TempData["approved"] });
        }
    }
}