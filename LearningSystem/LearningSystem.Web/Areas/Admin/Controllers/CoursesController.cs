namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Admin.Contracts;

    public class CoursesController : AdminController
    {
        private readonly ICoursesService coursesService;

        public CoursesController(ICoursesService coursesService)
        {
            this.coursesService = coursesService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = this.coursesService
                .All<AllCoursesViewModel>();

            return this.View(model);
        }

        [HttpGet]
        public IActionResult Create() => this.View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var course = await this.coursesService.AddCourseAsync(model);

            return this.RedirectToAction("Details", new {id = course.Id});
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await this.coursesService
                .DetailsAsync<CourseDetailsViewModel>(id);

            return this.View(model);
        }
    }
}