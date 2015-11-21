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
        private ITagRepository _tagRepository;
        public int PageSize { get; set; }

        public SearchController(IPostRepository postRepository, ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
            PageSize = 3;
        }

        [HttpGet]
        public ActionResult Search(string query, string tag, string order, int? page)
        {
            if (String.IsNullOrEmpty(query) == false)
            {
                return SearchByText(query, order, page);
            }
            else if (tag != null)
            {
                int _tag;
                if (Int32.TryParse(tag, out _tag))
                {
                    return SearchByTag(_tag, order, page);
                }
                else
                {
                    return View("SearchResult");
                }
            }
            else
            {
                return View("SearchResult");
            }                
        }

        [ChildActionOnly]
        public ActionResult SearchByText(string query, string order, int? page)
        {
            query = query.Trim();
            int pageNumber = page ?? 1;

            var searchResult = _postRepository
                .GetPosts
                .Where(p => p.Published == true);

            searchResult = searchResult.Where(p => p.Title.ToLower().Contains(query.ToLower()) || p.IntroText.ToLower().Contains(query.ToLower()) || p.MainText.ToLower().Contains(query.ToLower()));

            if (String.Compare(order, "asc", true) == 0)
            {
                searchResult = searchResult.OrderBy(p => p.CreateDate);
            }
            else
            {
                searchResult = searchResult.OrderByDescending(p => p.CreateDate);
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

        [ChildActionOnly]
        public ActionResult SearchByTag(int tag, string order, int? page)
        {
            int pageNumber = page ?? 1;

            var searchResult = _tagRepository
                .GetTags
                .Where(t => t.Id == tag)
                .SelectMany(t => t.Posts)
                .Where(p => p.Published == true);

            if (String.Compare(order, "asc", true) == 0)
            {
                searchResult = searchResult.OrderBy(p => p.CreateDate);
            }
            else
            {
                searchResult = searchResult.OrderByDescending(p => p.CreateDate);
            }

            SearchViewModel searchModel = new SearchViewModel
            {
                TagId = tag,
                SearchResults = searchResult.ToPagedList(pageNumber, PageSize),
                Founded = searchResult.Count(),
                Order = order
            };

            return View("SearchResult", searchModel);
        }
    }
}