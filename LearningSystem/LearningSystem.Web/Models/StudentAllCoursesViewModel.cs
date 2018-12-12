namespace LearningSystem.Web.Models
{
    using System.Collections.Generic;

    public class StudentAllCoursesViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<AllCoursesInstanceViewModel> Instances { get; set; }
    }
}