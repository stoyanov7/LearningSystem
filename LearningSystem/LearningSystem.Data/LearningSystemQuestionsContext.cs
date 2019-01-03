namespace LearningSystem.Data
{
    using MongoDB.Driver;

    public class LearningSystemQuestionsContext
    {
        public LearningSystemQuestionsContext()
        {
            this.Client = new MongoClient("mongodb://localhost:27017");
            this.Database = this.Client.GetDatabase("LearningSystemQuestions");
        }

        public MongoClient Client { get; set; }

        public IMongoDatabase Database { get; set; }
    }
}