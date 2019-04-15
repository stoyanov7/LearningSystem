namespace LearningSystem.Web.Areas.Admin.Models
{
    using System.Collections.Generic;

    public class AdminHomeViewModel
    {
        public IEnumerable<AdminArticlesViewModel> Articles { get; set; } 
            = new List<AdminArticlesViewModel>();

        public IEnumerable<AdminCoursesViewModel> Courses { get; set; }
            = new List<AdminCoursesViewModel>();
    }
}