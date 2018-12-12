namespace LearningSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LearningSystem.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Student;
    using Services.Student.Contracts;
    using Utilities.Common;

    [Authorize]
    public class PaymentsController : Controller
    {
        private const string PaymentKey = "payments";

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
                Courses = this.studentCoursesService
                    .GetCoursesForDropdownList()
            };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Index(CreatePaymentBindingModel model)
        {
            var payment = this.studentPaymentsService
                .GetPaymentInfo(model);

            var payments = this.HttpContext
                               .Session
                               .Get<List<Payment>>(PaymentKey) ?? new List<Payment>();

            payments.Add(payment);

            this.HttpContext
                .Session
                .Put(PaymentKey, payments);

            return this.RedirectToAction("Cart");
        }

        [HttpGet]
        public IActionResult Cart()
        {
            var payments = this.HttpContext
                .Session
                .Get<List<Payment>>(PaymentKey);

            return this.View(payments);
        }

        [HttpPost]
        public IActionResult Checkout()
        {
            var payments = this.HttpContext
                .Session
                .Get<List<Payment>>(PaymentKey);

            var payPalLink =  this.studentPaymentsService.CreatePayment(payments);

            if (payPalLink == null)
            {
                return this.BadRequest();
            }

            return this.Redirect(payPalLink);
        }

        [HttpGet]
        public async Task<IActionResult> Process(PaymentBindingModel model)
        {
            var payments = this.HttpContext
                .Session
                .Get<List<Payment>>(PaymentKey);

            if (payments == null)
            {
                return this.NotFound();
            }

            model.Username = this.User.Identity.Name;

            await this.studentPaymentsService.ProcessPaymentAsync(model, payments);

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Cancel()
        {
            return this.View("Index");
        }
    }
}