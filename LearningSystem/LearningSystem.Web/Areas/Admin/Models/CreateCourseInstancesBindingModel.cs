namespace LearningSystem.Web.Areas.Admin.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Utilities.Common;

    public class CreateCourseInstancesBindingModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Slug { get; set; }

        [Required]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Lecturer Id")]
        public string LecturerId { get; set; }

        [Required]
        public int CourseId { get; set; }
    }
}