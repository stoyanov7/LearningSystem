namespace LearningSystem.Web.Areas.Admin.Models
{
    using System;

    public class AdminCoursesViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }
    }
}