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
        private readonly IAdminCourseInstancesService adminCourseInstancesService;
        private readonly IAdminCoursesService adminCoursesService;
        private readonly IAdminLecturersService adminLecturersService;

        public CourseInstancesController(IAdminCourseInstancesService adminCourseInstancesService,
            IAdminCoursesService adminCoursesService, 
            IAdminLecturersService adminLecturersService)
        {
            this.adminCourseInstancesService = adminCourseInstancesService;
            this.adminCoursesService = adminCoursesService;
            this.adminLecturersService = adminLecturersService;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            // TODO: Extract to service
            var course = await this.adminCoursesService
                .FindCourseAsync(id);

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

            var lecturers = await this.adminLecturersService
                .GetAllLecturersAsync<LecturerConsiseViewModel>();

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

            var instance = await this.adminCourseInstancesService
                .CreateCourseIntancesAsync(model);

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
            var model = await this.adminCourseInstancesService
                .CourseInstancesDetailsAsync<DetailsCourseInstanceViewModel>(id);

            return this.View(model);
        }
    }
}