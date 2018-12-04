namespace LearningSystem.Web.Areas.Lecturer.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Lecturer.Contracts;

    public class LecturesController : LecturerController
    {
        private readonly ILecturerCourseInstancesService lecturerCourseInstancesService;

        public LecturesController(ILecturerCourseInstancesService lecturerCourseInstancesService)
        {
            this.lecturerCourseInstancesService = lecturerCourseInstancesService;
        }

        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            if (!await this.lecturerCourseInstancesService.InstanceExist(id))
            {
                return this.NotFound();
            }

            var model = new CreateLectureBindingModel { CourseId = id };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int id, CreateLectureBindingModel model)
        {
            await this.lecturerCourseInstancesService.AddLecture(id, model);

            return this.RedirectToAction("Details", "CourseInstances", new {id});
        }
    }
}