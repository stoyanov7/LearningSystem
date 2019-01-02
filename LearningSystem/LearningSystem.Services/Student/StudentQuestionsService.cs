namespace LearningSystem.Services.Student
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using MongoDB.Driver;
    using Repository.Contracts;

    public class StudentQuestionsService : IStudentQuestionsService
    {
        private readonly IRepository<LearningSystemContext, CourseInstance> repository;
        private readonly IMapper mapper;
        private readonly LearningSystemQuestionsContext questionsContext;

        public StudentQuestionsService(IRepository<LearningSystemContext, CourseInstance> repository,
            IMapper mapper, 
            LearningSystemQuestionsContext questionsContext)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.questionsContext = questionsContext;
        }

        public async Task<TModel> GetCourseInstanceAsync<TModel>(string questionSlug)
        {
            var course = await this.repository
                .Details()
                .FirstOrDefaultAsync(x => x.Slug == questionSlug);

            if (course == null)
            {
                return default(TModel);
            }

            var model = this.mapper.Map<TModel>(course);

            return model;
        }

        public void AddQuestion(CreateQuestionBindingModel model)
        {
            var questions = this.questionsContext.Database.GetCollection<QuestionPage>("questions");

            questions.InsertOne(new QuestionPage
            {
                QuestionSlug = "c#-web",
                Questions = new List<Question>
                {
                    new Question { Username = "firstUsername", QuestionText = "text question" },
                    new Question { Username = "secondUsername", QuestionText = "text question" }
                }
            });

            var result = questions.FindSync(FilterDefinition<QuestionPage>.Empty).ToList();
        }
    }
}