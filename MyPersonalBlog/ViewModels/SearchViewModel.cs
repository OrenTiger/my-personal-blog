using MyPersonalBlog.Models;
using PagedList;

namespace MyPersonalBlog.ViewModels
{
    public class SearchViewModel
    {
        public string SearchQuery { get; set; }

        public int? TagId { get; set; }
        
        public IPagedList<Post> SearchResults { get; set; }

        public int CountFounded { get; set; }

        public string Order { get; set; }
    }
}