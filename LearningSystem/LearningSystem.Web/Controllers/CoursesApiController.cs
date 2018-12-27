namespace LearningSystem.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Areas.Admin.Models.Course;
    using Microsoft.AspNetCore.Mvc;
    using Services.Student.Contracts;

    [Route("api/courses")]
    [ApiController]
    public class CoursesApiController : ControllerBase
    {
        private readonly IStudentCoursesService studentCoursesService;

        public CoursesApiController(IStudentCoursesService studentCoursesService)
        {
            this.studentCoursesService = studentCoursesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await this.studentCoursesService
                .GetAllCoursesAsync<IEnumerable<CourseDetailsViewModel>>();

            return this.Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await this.studentCoursesService
                .GetCourseAsync<CourseDetailsViewModel>(id);

            if (course == null)
            {
                return this.NotFound(new
                {
                    Message = "The course does not exist!",
                    Code = "ERR_COURSE_NOT_FOUND"
                });
            }

            return this.Ok(course);
        }
    }
}