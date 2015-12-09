using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyPersonalBlog.Models;
using MyPersonalBlog.Repositories;
using System.Data.Entity;
using MyPersonalBlog.Infrastructure;
using CaptchaMvc.Attributes;

namespace MyPersonalBlog.Controllers
{
    public class CommentController : Controller
    {
        private ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CaptchaVerify("Капча указана неверно ")]
        public PartialViewResult Save(Comment model)
        {
            if (ModelState.IsValid)
            {
                _commentRepository.Save(model);
                ViewBag.IsCommentSuccess = true;
                return PartialView("~/Views/Posts/_CommentForm.cshtml");
            }
            
            return PartialView("~/Views/Posts/_CommentForm.cshtml", model);
        }
    }
}