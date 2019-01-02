namespace LearningSystem.Data
{
    using MongoDB.Driver;

    public class LearningSystemQuestionsContext
    {
        public LearningSystemQuestionsContext()
        {
            this.Client = new MongoClient("mongodb://localhost:27017");
        }

        public MongoClient Client { get; set; }
    }
}