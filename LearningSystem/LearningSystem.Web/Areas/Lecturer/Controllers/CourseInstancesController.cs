namespace LearningSystem.Web.Areas.Lecturer.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Lecturer.Contracts;

    public class CourseInstancesController : LecturerController
    {
        private readonly ILecturerCourseInstancesService courseInstancesService;

        public CourseInstancesController(ILecturerCourseInstancesService courseInstancesService)
        {
            this.courseInstancesService = courseInstancesService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await this.courseInstancesService
                .DetailsAsync<DetailsCourseInstanceViewModel>(id);

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await this.courseInstancesService
                .PrepareInstanceForEditingAsync<EditCourseInstanceBindingModel>(id);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditCourseInstanceBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.courseInstancesService
                .EditAsync(id, this.User, model.Name, model.Description, model.StartDate, model.EndDate);

            // TODO: Redirect to Details
            return this.View(model);
        }
    }
}