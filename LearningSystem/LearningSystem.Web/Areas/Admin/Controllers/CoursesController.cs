namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Services.Admin.Contracts;
    using ViewModels.Admin;

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

            await this.coursesService.AddCourseAsync(model);

            // TODO: Redirect to Details
            return this.View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await this.coursesService
                .Details<CourseDetailsViewModel>(id);

            return this.View(model);
        }
    }
}