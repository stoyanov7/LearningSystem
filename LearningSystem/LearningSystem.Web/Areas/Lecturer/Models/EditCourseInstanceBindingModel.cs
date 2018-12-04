namespace LearningSystem.Web.Areas.Lecturer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class EditCourseInstanceBindingModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ICollection<LectureShortViewModel> Lectures { get; set; }
    }
}