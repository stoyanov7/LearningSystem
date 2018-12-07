namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Lecturer.Models;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Admin.Contracts;
    using Utilities.Common;
    using Utilities.Constants;
    using Utilities.Helpers.Messages;

    public class CourseInstancesController : AdminController
    {
        private readonly ICourseInstancesService courseInstancesService;
        private readonly ICoursesService coursesService;
        private readonly ILecturersService lecturersService;

        public CourseInstancesController(ICourseInstancesService courseInstancesService,
            ICoursesService coursesService, 
            ILecturersService lecturersService)
        {
            this.courseInstancesService = courseInstancesService;
            this.coursesService = coursesService;
            this.lecturersService = lecturersService;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            // TODO: Extract to service
            var course = await this.coursesService.FindAsync(id);

            if (course == null)
            {
                return this.NotFound();
            }

            var model = new CreateCourseInstancesBindingModel
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1),
                CourseId = course.Id,
                Name = course.Name,
                Slug = course.Slug
            };

            var lecturers = await this.lecturersService
                .GetAllLecturers<LecturerConsiseViewModel>();

            this.ViewData["Lecturers"] = lecturers;

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCourseInstancesBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var instance = await this.courseInstancesService.Create(model);

            this.TempData.Put("__Message", new MessageModel
            {
                Type = MessageType.Success,
                Message = string.Format(AdminConstants.CreatedSuccessfullyCourse, model.Name)
            });

            return this.RedirectToAction("Details", "CourseInstances", new { id = instance});
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await this.courseInstancesService
                .DetailsAsync<DetailsCourseInstanceViewModel>(id);

            return this.View(model);
        }
    }
}