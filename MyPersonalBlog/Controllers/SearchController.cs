using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyPersonalBlog.Infrastructure;
using MyPersonalBlog.ViewModels;
using MyPersonalBlog.Repositories;
using PagedList;

namespace MyPersonalBlog.Controllers
{
    public class SearchController : Controller
    {
        private IPostRepository _postRepository;
        public int PageSize { get; set; }

        public SearchController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
            PageSize = 3;
        }

        [HttpGet]
        public ActionResult Search(string query, string order, int? page)
        {
            if (String.IsNullOrEmpty(query))
            {
                return View("SearchResult");
            }
            else
            {
                query = query.Trim();

                int pageNumber = page ?? 1;

                var searchResult = _postRepository
                    .GetPosts
                    .Where(p => p.Published == true)
                    .Where(p => p.Title.ToLower().Contains(query.ToLower()) || p.IntroText.ToLower().Contains(query.ToLower()) || p.MainText.ToLower().Contains(query.ToLower()));

                if (String.Compare(order, "desc", true) == 0)
                {
                    //searchResult.OrderByDescending(p => p.CreateDate);
                    searchResult = searchResult.OrderBy(p => p.Id);
                }
                else
                {
                    //searchResult.OrderBy(p => p.CreateDate);
                    searchResult = searchResult.OrderByDescending(p => p.Id);
                }

                SearchViewModel searchModel = new SearchViewModel
                {
                    SearchQuery = query,
                    SearchResults = searchResult.ToPagedList(pageNumber, PageSize),
                    Founded = searchResult.Count(),
                    Order = order
                };

                return View("SearchResult", searchModel);
            }
        }
    }
}