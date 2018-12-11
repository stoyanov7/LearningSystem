namespace LearningSystem.Services.Student.Contracts
{
    using System.Threading.Tasks;

    public interface IStudentPaymentsService
    {
        Task CreatePaymentAsync<TModel>(TModel model);
    }
}