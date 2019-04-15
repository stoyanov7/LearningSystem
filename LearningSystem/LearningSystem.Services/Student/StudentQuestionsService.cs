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
        private const string Database = "questions";

        private readonly IRepository<LearningSystemContext, CourseInstance> repository;
        private readonly IMapper mapper;
        private readonly LearningSystemQuestionsContext questionsContext;
        private readonly IMongoCollection<QuestionPage> questions;

        public StudentQuestionsService(IRepository<LearningSystemContext, CourseInstance> repository,
            IMapper mapper, 
            LearningSystemQuestionsContext questionsContext)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.questionsContext = questionsContext;

            this.questions = this.questionsContext
                .Database
                .GetCollection<QuestionPage>(Database);
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
            var questionPage = this.GetQuestionPage(model.QuestionSlug);

            if (questionPage == null)
            {
                questionPage = new QuestionPage
                {
                    QuestionSlug = model.QuestionSlug,
                    Questions = new List<Question>()
                };

                this.questions.InsertOne(questionPage);
                questionPage = this.GetQuestionPage(model.QuestionSlug);
            }

            var oldQuestions = questionPage.Questions;
            oldQuestions.Add(this.mapper.Map<Question>(model));

            this.questions.UpdateOne(
                p => p.QuestionSlug == questionPage.QuestionSlug,
                Builders<QuestionPage>.Update.Set(p => p.Questions, oldQuestions));

            var newQuestions = this.questions
                .FindSync(p => p.QuestionSlug == model.QuestionSlug)
                .First();
        }

        public QuestionPage GetQuestionPage(string questionSlug)
            => this.questions
                .FindSync(p => p.QuestionSlug == questionSlug)
                .FirstOrDefault();
    }
}