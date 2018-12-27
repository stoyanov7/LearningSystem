namespace LearningSystem.Web.Models.Course
{
    using Microsoft.AspNetCore.Http;

    public class UploadHomeworkBindingModel
    {
        public int LecturerId { get; set; }

        public string AuthorId { get; set; }

        public IFormFile HomeworkFile { get; set; }
    }
}