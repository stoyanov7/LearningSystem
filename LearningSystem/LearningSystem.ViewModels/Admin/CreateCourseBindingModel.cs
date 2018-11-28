namespace LearningSystem.ViewModels.Admin
{
    using System.ComponentModel.DataAnnotations;

    public class CreateCourseBindingModel
    {
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }

        public string Slug { get; set; }
    }
}