namespace LearningSystem.Services.Student
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Contracts;
    using Data;
    using Models;
    using PayPal.Api;
    using Repository.Contracts;
    using Payment = PayPal.Api.Payment;

    public class StudentPaymentsService : IStudentPaymentsService
    {
        private const string ApprovalUrlRel = "approval_url";
        private const string PaymentMethod = "paypal";
        private const string Currency = "EUR";
        private const string Intent = "sale";
        private const string ReturnUrl = "https://localhost:44319/Payments/Process";
        private const string CancelUrl = "https://localhost:44319/Payments/Cancel";

        private readonly IRepository<LearningSystemPaymentsContext, Models.Payment> repository;
        private readonly IRepository<LearningSystemContext, StudentsInCourses> studentsInCourseRepository;
        private readonly IMapper mapper;
        private readonly APIContext apiContext;

        public StudentPaymentsService(IRepository<LearningSystemPaymentsContext, Models.Payment> repository,
            IMapper mapper, IRepository<LearningSystemContext, StudentsInCourses> studentsInCourseRepository)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.studentsInCourseRepository = studentsInCourseRepository;

            var apiToken = new OAuthTokenCredential("AaHyyJSKlaI7dNUZVmSKjXzAnhZExaI_wZkrWzhLAbsthXw5iWZ6ycHujLNSb4SnjSvGz9QbRj_WnLtK",
                    "EM48aVUbccMGdntebIPgvqy5PtXySgL6DBsVF30dzdMVRvp7DXQ1dppMooanyGS_Qc_b1d42_A76Eu0K")
                .GetAccessToken();

            this.apiContext = new APIContext(apiToken);
        }

        public Models.Payment GetPaymentInfo<TModel>(TModel model)
        {
            var payment = this.mapper.Map<Models.Payment>(model);
            payment.Amount = this.GetAmountForCourse(payment.CourseId);

            return payment;
        }

        public string CreatePayment(IEnumerable<Models.Payment> payments)
        {
            var total = payments
                .Sum(p => this.GetAmountForCourse(p.CourseId));

            var link = this.GetPayPalLink(total);

            return link;
        }

        public async Task ProcessPaymentAsync(PaymentBindingModel model, IEnumerable<Models.Payment> payments)
        {
            var payment = new Payment
            {
                id = model.PaymentId,
                token = model.Token
            };

            var executed = payment
                .Execute(this.apiContext, new PaymentExecution {payer_id = model.PayerId});

            foreach (var payment1 in payments)
            {
                payment1.PayPalPaymentId = model.PaymentId;
                payment1.Username = model.Username;
                payment1.StudentId = model.StudentId;
            }

            await this.repository.AddRangeAsync(payments);

            StudentsInCourses studentsInCourses = null;
            foreach (var payment2 in payments)
            {
                studentsInCourses = new StudentsInCourses
                {
                    CourseId = payment2.CourseId,
                    StudentId = payment2.StudentId
                };
            }

            await this.studentsInCourseRepository.AddAsync(studentsInCourses);
        }

        public decimal GetAmountForCourse(int courseId)
        {
            // for testing purpose only
            return 20m;
        }

        private string GetPayPalLink(decimal total)
        {
            var payer = new Payer
            {
                payment_method = PaymentMethod
            };

            var amount = new Amount
            {
                currency = Currency,
                total = total.ToString()
            };

            var transaction = new Transaction
            {
                amount = amount
            };

            var payment = new Payment
            {
                payer = payer,
                transactions = new[] {transaction}.ToList(),
                intent = Intent,
                redirect_urls = new RedirectUrls
                {
                    return_url = ReturnUrl,
                    cancel_url = CancelUrl
                }
            };

            var created = payment.Create(this.apiContext);
            var links = created.links.ToList();
            var approvalLink = links.FirstOrDefault(l => l.rel == ApprovalUrlRel);

            return approvalLink.href;
        }
    }
}