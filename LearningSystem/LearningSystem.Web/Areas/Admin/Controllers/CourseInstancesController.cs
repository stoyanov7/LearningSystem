namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Admin.Contracts;

    public class CourseInstancesController : AdminController
    {
        private readonly ICourseInstancesService courseInstancesService;
        private readonly ICoursesService coursesService;

        public CourseInstancesController(ICourseInstancesService courseInstancesService,
            ICoursesService coursesService)
        {
            this.courseInstancesService = courseInstancesService;
            this.coursesService = coursesService;
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

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseInstancesBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.courseInstancesService.Create(model);

            // TODO: Redirect to details
            return this.View();
        }
    }
}