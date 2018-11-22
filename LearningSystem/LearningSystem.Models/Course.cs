namespace LearningSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Course
    {
        public Course() => this.Instances = new List<CourseInstance>();

        [Key]
        public int Id { get; set; }

        public ICollection<CourseInstance> Instances { get; set; }
    }
}
