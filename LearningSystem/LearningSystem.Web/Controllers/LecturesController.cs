namespace LearningSystem.Web.Controllers
{
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models.Course;

    public class LecturesController : Controller
    {
        [HttpGet]
        public IActionResult UploadHomework() => this.View();

        [HttpPost]
        public async Task<IActionResult> UploadHomework(UploadHomeworkBindingModel model)
        {
            var fullFilePath = Path
                .Combine(Directory.GetCurrentDirectory(), "Files", model.HomeworkFile.FileName);

            var fileStream = new FileStream(fullFilePath, FileMode.Create);

            await model
                .HomeworkFile
                .CopyToAsync(fileStream);

            // TODO: Redirect to course instances
            return this.View();
        }
    }
}