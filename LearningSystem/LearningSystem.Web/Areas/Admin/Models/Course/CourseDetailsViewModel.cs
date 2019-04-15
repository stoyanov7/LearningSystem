namespace LearningSystem.Web.Areas.Admin.Models.Course
{
    using System.Collections.Generic;
    using CourseInstance;

    public class CourseDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<CourseInstanceViewModel> Instances { get; set; }
    }
}