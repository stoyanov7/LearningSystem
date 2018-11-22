namespace LearningSystem.Models
{
    using Identity;

    public class StudentsInCourses
    {
        public string StudentId { get; set; }
        public ApplicationUser Student { get; set; }

        public int CourseId { get; set; }
        public CourseInstance Course { get; set; }
    }
}