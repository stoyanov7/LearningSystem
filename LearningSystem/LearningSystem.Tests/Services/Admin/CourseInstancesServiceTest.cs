namespace LearningSystem.Tests.Services.Admin
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Data;
    using LearningSystem.Services.Admin;
    using Mocks;
    using Models;
    using Moq;
    using Repository.Contracts;
    using Web.Areas.Lecturer.Models;
    using Xunit;

    public class CourseInstancesServiceTest
    {
        private readonly IMapper mapper;
        private readonly Mock<IRepository<LearningSystemContext, CourseInstance>> courseInstancesRepositoryMock;
        private readonly LearningSystemContext context;

        public CourseInstancesServiceTest()
        {
            this.mapper = AutoMapperMock.GetMapper();
            this.courseInstancesRepositoryMock = new Mock<IRepository<LearningSystemContext, CourseInstance>>();
            this.context = LearningSystemContextMock.GetContext();
        }

        [Fact]
        public async Task CreateCourseInstanceAsync_WithValidCourseInstance_ShouldAddCourseInstance()
        {
            // Arrange
            const string name = "Course test";
            const string slug = "course-test";

            var courseInstance = new CourseInstance
            {
                Name = name,
                Slug = slug
            };
            
            this.courseInstancesRepositoryMock
                .Setup(x => x.AddAsync(courseInstance))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new AdminCourseInstancesService(this.courseInstancesRepositoryMock.Object, this.mapper);

            // Act
            var result = await service.CreateCourseIntancesAsync(courseInstance);

            // Assert
            Assert.Equal(name, courseInstance.Name);
        }

        [Fact]
        public async Task CourseInstancesDetailsAsync_WithValidCourseInstance_ShouldReturnCorrectDetails()
        {
            // Arrange
            const int expectedId = 1;
            const string expecterName = "First course";

            var courseInstance = new CourseInstance
            {
                Id = expectedId,
                Name = expecterName
            };

            this.context.CourseInstances.Add(courseInstance);
            await this.context.SaveChangesAsync();

            this.courseInstancesRepositoryMock
                .Setup(m => m.Details())
                .Returns(this.context.CourseInstances)
                .Verifiable();

            var service = new AdminCourseInstancesService(this.courseInstancesRepositoryMock.Object, this.mapper);

            // Act
            var result = await service.CourseInstancesDetailsAsync<DetailsCourseInstanceViewModel>(expectedId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedId, result.Id);
            Assert.Equal(expecterName, result.Name);
        }
    }
}