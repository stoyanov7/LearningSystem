namespace LearningSystem.Web.Models.CourseInstance
{
    using System;
    using System.Collections.Generic;
    using Areas.Admin.Models.Lecture;

    public class StudentDetailsCourseInstanceViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ICollection<LecturesViewModel> Lectures { get; set; }
    }
}