using System.Collections.Generic;

namespace MyPersonalBlog.ViewModels
{
    public class ArchivePostsViewModel
    {
        public int Year { get; set; }
        public IDictionary<int, int> Months { get; set; }
    }
}