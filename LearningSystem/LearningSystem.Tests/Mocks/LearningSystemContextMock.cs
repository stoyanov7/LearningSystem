namespace LearningSystem.Tests.Mocks
{
    using System;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class LearningSystemContextMock
    {
        public static LearningSystemContext GetContext()
        {
            var options = new DbContextOptionsBuilder<LearningSystemContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new LearningSystemContext(options);
        }
    }
}