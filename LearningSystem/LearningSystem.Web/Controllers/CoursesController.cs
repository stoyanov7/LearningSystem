namespace LearningSystem.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models.Course;
    using Models.CourseInstance;
    using Services.Student.Contracts;

    public class CoursesController : Controller
    {
        private readonly IStudentCoursesService studentCoursesService;
        private readonly IStudentCourseInstancesService studentCourseInstancesService;

        public CoursesController(IStudentCoursesService adminCoursesService,
            IStudentCourseInstancesService studentCourseInstancesService)
        {
            this.studentCoursesService = adminCoursesService;
            this.studentCourseInstancesService = studentCourseInstancesService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await this.studentCourseInstancesService
                .GetCourseInstancesAsync<StudentDetailsCourseInstanceViewModel>(id);

            return this.View(model);
        }

        public async Task<IActionResult> All()
        {
            var model = await this.studentCoursesService
                .GetAllCoursesAsync<StudentAllCoursesViewModel>();

            return this.View(model);
        }
    }
}