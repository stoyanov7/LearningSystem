namespace LearningSystem.Services.Student
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Contracts;
    using Data;
    using Models;
    using Repository.Contracts;

    public class StudentPaymentsService : IStudentPaymentsService
    {
        private readonly IRepository<LearningSystemPaymentsContext, Payment> repository;
        private readonly IMapper mapper;

        public StudentPaymentsService(IRepository<LearningSystemPaymentsContext, Payment>
            repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task CreatePaymentAsync<TModel>(TModel model)
        {
            var payment = this.mapper.Map<Payment>(model);
            await this.repository.AddAsync(payment);
        }
    }
}