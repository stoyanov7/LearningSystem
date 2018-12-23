namespace LearningSystem.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SearchFormBindingModel
    {
        public string SearchText { get; set; }

        [Display(Name = "Search In Courses")]
        public bool SearchInCourses { get; set; } = true;

        [Display(Name = "Search In Users")]
        public bool SearchInUsers { get; set; } = true;
    }
}