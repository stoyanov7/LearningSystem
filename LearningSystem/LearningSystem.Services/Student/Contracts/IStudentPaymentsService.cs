namespace LearningSystem.Services.Student.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IStudentPaymentsService
    {
        Payment GetPaymentInfo<TModel>(TModel model);

        Task ProcessPaymentAsync(PaymentBindingModel model, IEnumerable<Models.Payment> payments);

        string CreatePayment(IEnumerable<Models.Payment> payments);

        decimal GetAmountForCourse(int courseId);
    }
}