namespace LearningSystem.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Student.Contracts;

    public class PaymentsController : Controller
    {
        private readonly IStudentPaymentsService studentPaymentsService;

        public PaymentsController(IStudentPaymentsService studentPaymentsService)
        {
            this.studentPaymentsService = studentPaymentsService;
        }

        public async Task<IActionResult> Index()
        {
            await this.studentPaymentsService.CreatePaymentAsync(new CreatePaymentBindingModel
            {
                Username = this.User.Identity.Name,
                Amount = 30m
            });

            return this.View();
        }
    }
}