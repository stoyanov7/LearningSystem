namespace LearningSystem.Web.Areas.Lecturer.Models
{
    using System;
    using System.Collections.Generic;
    using Admin.Models;

    public class DetailsCourseInstanceViewModel
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