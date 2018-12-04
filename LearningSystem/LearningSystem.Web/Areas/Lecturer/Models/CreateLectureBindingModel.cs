namespace LearningSystem.Web.Areas.Lecturer.Models
{
    using System;

    public class CreateLectureBindingModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Order { get; set; }

        public string LectureHall { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int CourseId { get; set; }
    }
}