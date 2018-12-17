namespace LearningSystem.Web.Models
{
    using System.Collections.Generic;

    public class SearchViewModel
    {
        public IEnumerable<SearchCourseInstanceViewModel> Courses { get; set; }
            = new List<SearchCourseInstanceViewModel>();
        
        public string SearchText { get; set; }
    }
}