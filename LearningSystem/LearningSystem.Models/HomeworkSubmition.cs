namespace LearningSystem.Models
{
    using System;
    using Identity;

    public class HomeworkSubmition
    {
        public int LectureId { get; set; }
        public Lecture Lecture { get; set; }

        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }

        public DateTime TimeUploaded { get; set; }

        public string PathFile { get; set; }
    }
}