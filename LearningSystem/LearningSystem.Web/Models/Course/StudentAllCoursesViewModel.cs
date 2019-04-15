namespace LearningSystem.Web.Models.Course
{
    using System.Collections.Generic;
    using CourseInstance;

    public class StudentAllCoursesViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<AllCoursesInstanceViewModel> Instances { get; set; }
    }
}