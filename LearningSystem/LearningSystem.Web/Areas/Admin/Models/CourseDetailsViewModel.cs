namespace LearningSystem.Web.Areas.Admin.Models
{
    using System.Collections.Generic;

    public class CourseDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<CourseInstanceViewModel> Instances { get; set; }
    }
}