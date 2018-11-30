namespace LearningSystem.Web.Areas.Admin.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateCourseInstancesBindingModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Slug { get; set; }

        [Required]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        public string LecturerId { get; set; }

        [Required]
        public int CourseId { get; set; }
    }
}