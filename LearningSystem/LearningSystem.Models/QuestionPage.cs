namespace LearningSystem.Models
{
    using System.Collections.Generic;
    using MongoDB.Bson;

    public class QuestionPage
    {
        public ObjectId Id { get; set; }

        public string QuestionSlug { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}