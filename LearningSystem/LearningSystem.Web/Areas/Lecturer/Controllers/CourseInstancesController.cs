namespace LearningSystem.Web.Areas.Lecturer.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Html.Contracts;
    using Services.Lecturer.Contracts;

    public class CourseInstancesController : LecturerController
    {
        private readonly ILecturerCourseInstancesService courseInstancesService;
        private readonly IHtmlService htmlService;

        public CourseInstancesController(ILecturerCourseInstancesService courseInstancesService, 
            IHtmlService htmlService)
        {
            this.courseInstancesService = courseInstancesService;
            this.htmlService = htmlService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await this.courseInstancesService
                .CourseInstanceDetailsAsync<DetailsCourseInstanceViewModel>(id);

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await this.courseInstancesService
                .PrepareCourseInstanceForEditingAsync<EditCourseInstanceBindingModel>(id);

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditCourseInstanceBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            model.Name = this.htmlService.Sanitize(model.Name);
            model.Description = this.htmlService.Sanitize(model.Description);

            await this.courseInstancesService
                .EditCourseInstanceAsync(id, this.User, model.Name, model.Description, model.StartDate, model.EndDate);

            // TODO: Redirect to Details
            return this.View(model);
        }
    }
}