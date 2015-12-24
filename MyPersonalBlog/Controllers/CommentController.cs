using System.Web.Mvc;
using MyPersonalBlog.Models;
using MyPersonalBlog.Repositories;
using CaptchaMvc.Attributes;
using System.Linq;
using MyPersonalBlog.ViewModels;

namespace MyPersonalBlog.Controllers
{
    public class CommentController : Controller
    {
        private ICommentRepository _commentRepository;
        private ILikeRepository _likeRepository;

        public CommentController(ICommentRepository commentRepository, ILikeRepository likeRepository)
        {
            _commentRepository = commentRepository;
            _likeRepository = likeRepository;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult SaveWithoutCaptcha(Comment model)
        {
            if (ModelState.IsValid & Session["oAuthUser"] != null)
            {
                model.AvatarUrl = ((OAuthUser)Session["oAuthUser"]).ProfilePhoto;
                _commentRepository.Save(model);
                ViewBag.IsCommentSuccess = true;
                return PartialView("~/Views/Posts/_CommentForm.cshtml");
            }

            return PartialView("~/Views/Posts/_CommentForm.cshtml", model);
        }

        [HttpPost]
        public PartialViewResult SetLike(CommentLikesViewModel model)
        {
            if (Session["oAuthUser"] != null)
            {                
                OAuthUser oAuthUser = (OAuthUser)Session["oAuthUser"];

                Like like = _likeRepository.Likes.Where(l => l.CommentId == model.CommentId && l.UserId == oAuthUser.UserId && l.UserProvider == oAuthUser.Provider).FirstOrDefault();

                if (like == null)
                {
                    like = new Like();
                    like.UserId = oAuthUser.UserId;
                    like.UserProvider = oAuthUser.Provider;
                    like.CommentId = model.CommentId;
                    _likeRepository.Save(like);
                }
                else
                {
                    _likeRepository.Delete(like.Id);
                }
            }

            model.LikesCount = _commentRepository.GetById(model.CommentId).Likes.Count;
            return PartialView("~/Views/Posts/_Like.cshtml", model);
        }
    }
}