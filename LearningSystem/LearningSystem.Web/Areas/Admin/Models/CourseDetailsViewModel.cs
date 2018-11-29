namespace LearningSystem.Web.Areas.Admin.Models
{
    using System.Collections.Generic;

    public class CourseDetailsViewModel
    {
        public string Name { get; set; }

        public ICollection<CourseInstanceViewModel> Instances { get; set; }
    }
}