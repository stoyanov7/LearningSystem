namespace LearningSystem.Web.Models
{
    using System.Collections.Generic;

    public class SearchViewModel
    {
        public IEnumerable<SearchCourseInstanceViewModel> Courses { get; set; }
            = new List<SearchCourseInstanceViewModel>();

        public IEnumerable<SearchUsersViewModel> Users { get; set; }
            = new List<SearchUsersViewModel>();

        public string SearchText { get; set; }
    }
}