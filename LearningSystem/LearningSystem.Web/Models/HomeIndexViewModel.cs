namespace LearningSystem.Web.Models
{
    using System.Collections.Generic;
    using Search;

    public class HomeIndexViewModel : SearchFormBindingModel
    {
        public IEnumerable<HomeCourseInstanceViewModel> CourseInstances { get; set; }

        public IEnumerable<HomeArticlesViewModel> Articles { get; set; }
    }
}