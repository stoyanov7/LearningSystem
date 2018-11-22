namespace LearningSystem.Models.Identity
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.EnrolledCourses = new List<StudentsInCourses>();
            this.LecturerCourses = new List<CourseInstance>();
        }

        public string FullName { get; set; }

        public ICollection<StudentsInCourses> EnrolledCourses { get; set; }

        public ICollection<CourseInstance> LecturerCourses { get; set; }
    }
}