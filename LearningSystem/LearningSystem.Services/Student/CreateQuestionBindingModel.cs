namespace LearningSystem.Services.Student
{
    using AutoMapper;
    using Models;
    using Utilities.Infrastructure.Contracts;

    public class CreateQuestionBindingModel : IMapFrom<Question>, IHaveCustomMapping
    {
        public string QuestionSlug { get; set; }

        public string QuestionText { get; set; }

        public string Username { get; set; }

        public void ConfigureMapping(Profile mapper) 
            => mapper.CreateMap<CreateQuestionBindingModel, Question>();
    }
}