namespace LearningSystem.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Student.Contracts;

    public class PaymentsController : Controller
    {
        private readonly IStudentPaymentsService studentPaymentsService;
        private readonly IStudentCoursesService studentCoursesService;

        public PaymentsController(IStudentPaymentsService studentPaymentsService,
            IStudentCoursesService studentCoursesService)
        {
            this.studentPaymentsService = studentPaymentsService;
            this.studentCoursesService = studentCoursesService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new CreatePaymentBindingModel
            {
                Courses = this.studentCoursesService.GetCoursesForDropdownList()
            };

            return this.View(model);
        }

            return this.View();
        }
    }
}