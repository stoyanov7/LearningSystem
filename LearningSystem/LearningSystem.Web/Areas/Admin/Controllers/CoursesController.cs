namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Models.Course;
    using Services.Admin.Contracts;
    using Services.Html.Contracts;
    using Utilities.Common;
    using Utilities.Constants;
    using Utilities.Helpers.Messages;

    public class CoursesController : AdminController
    {
        private readonly IAdminCoursesService adminCoursesService;
        private readonly ILogger<CoursesController> logger;
        private readonly IHtmlService htmlService;

        public CoursesController(IAdminCoursesService adminCoursesService,
            ILogger<CoursesController> logger,
            IHtmlService htmlService)
        {
            this.adminCoursesService = adminCoursesService;
            this.logger = logger;
            this.htmlService = htmlService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await this.adminCoursesService
                   .GetCoursesAsync<AllCoursesViewModel>();

            return this.View(model);
        }

        [HttpGet]
        public IActionResult Create() => this.View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCourseBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            model.Name = this.htmlService.Sanitize(model.Name);
            model.Slug = this.htmlService.Sanitize(model.Slug);

            var course = await this.adminCoursesService.AddCourseAsync(model);
            this.logger.LogInformation(string.Format(AdminConstants.CreatedSuccessfullyCourse, course.Name));

            this.TempData.Put("__Message", new MessageModel
            {
                Type = MessageType.Success,
                Message = string.Format(AdminConstants.CreatedSuccessfullyCourse, course.Name)
            });

            return this.RedirectToAction("Details", new { id = course.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await this.adminCoursesService
                .CourseDetailsAsync<CourseDetailsViewModel>(id);

            return this.View(model);
        }
    }
}