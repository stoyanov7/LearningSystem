namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using Models;
    using Services.Admin.Contracts;
    using Utilities.Constants;
    using Utilities.Helpers.Messages;

    public class CoursesController : AdminController
    {
        private readonly ICoursesService coursesService;
        private readonly ILogger<CoursesController> logger;
        private readonly IStringLocalizer<CoursesController> stringLocalizer;

        public CoursesController(ICoursesService coursesService,
            ILogger<CoursesController> logger,
            IStringLocalizer<CoursesController> stringLocalizer)
        {
            this.coursesService = coursesService;
            this.logger = logger;
            this.stringLocalizer = stringLocalizer;
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
            this.logger.LogInformation(string.Format(AdminConstants.CreatedSuccessfullyCourse, course.Name));

            this.TempData["__Message"] = new MessageModel
            {
                Type = MessageType.Success,
                Message = string.Format(AdminConstants.CreatedSuccessfullyCourse, course.Name)
            };

            return this.RedirectToAction("Details", new { id = course.Id });
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