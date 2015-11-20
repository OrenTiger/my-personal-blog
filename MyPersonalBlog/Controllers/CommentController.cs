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
    public class CommentController : Controller
    {
        private ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpPost]
        public ActionResult Save(Comment comment)
        {
            // TODO: Добавить проверку введеных данных на недопустимые знаки
            // TODO: Добавить капчу
            if (ModelState.IsValid)
            {
                _commentRepository.SaveComment(comment);
                return Redirect(Request.UrlReferrer.ToString());
            }
            else return View();
        }
    }
}