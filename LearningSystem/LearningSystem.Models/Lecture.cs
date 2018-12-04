namespace LearningSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Lecture
    {
        public Lecture()
        {
            this.Resources = new List<Resource>();
            this.HomeworkSubmitions = new List<HomeworkSubmition>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [MinLength(3)]
        public string Description { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        public string LectureHall { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndTime { get; set; }

        public int CourseId { get; set; }
        public CourseInstance Course { get; set; }

        public ICollection<Resource> Resources { get; set; }

        public ICollection<HomeworkSubmition> HomeworkSubmitions { get; set; }
    }
}