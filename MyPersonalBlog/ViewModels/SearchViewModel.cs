using System.Collections.Generic;
using MyPersonalBlog.Models;
using PagedList;

namespace MyPersonalBlog.ViewModels
{
    public class SearchViewModel
    {
        public string SearchQuery { get; set; }

        public IPagedList<Post> SearchResults { get; set; }

        public int Founded { get; set; }

        public string Order { get; set; }
    }
}