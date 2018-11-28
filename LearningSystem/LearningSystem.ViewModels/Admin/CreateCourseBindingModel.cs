namespace LearningSystem.ViewModels.Admin
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Models;

    public class CreateCourseBindingModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }

        public string Slug { get; set; }

        public ICollection<CourseInstance> Instances { get; set; }
    }
}