namespace LearningSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Identity;

    public class CourseInstance
    {
        public CourseInstance()
        {
            this.Students = new List<StudentsInCourses>();
            this.Lectures = new List<Lecture>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        public string Slug { get; set; }

        [Required]
        [MinLength(3)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }


        public string LecturerId { get; set; }
        public ApplicationUser Lecturer { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public ICollection<StudentsInCourses> Students { get; set; }

        public ICollection<Lecture> Lectures { get; set; }
    }
}