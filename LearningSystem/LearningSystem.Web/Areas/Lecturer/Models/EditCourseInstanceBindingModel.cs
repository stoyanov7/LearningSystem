namespace LearningSystem.Web.Areas.Lecturer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Utilities.Common;

    public class EditCourseInstanceBindingModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        [DataType(DataType.Date)]
        [DateAfter("StartDate")]
        public DateTime EndDate { get; set; }

        public ICollection<LectureShortViewModel> Lectures { get; set; }
    }
}