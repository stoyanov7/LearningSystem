namespace LearningSystem.Tests.Services.Students
{
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data;
    using LearningSystem.Services.Student;
    using Mocks;
    using Models;
    using Moq;
    using Repository.Contracts;
    using Web.Areas.Admin.Models;
    using Web.Models;
    using Xunit;

    public class StudentCoursesServiceTest
    {
        private readonly LearningSystemContext context;
        private readonly Mock<IRepository<LearningSystemContext, Course>> repositoryMock;
        private readonly IMapper mapper;

        public StudentCoursesServiceTest()
        {
            this.context = LearningSystemContextMock.GetContext();
            this.repositoryMock = new Mock<IRepository<LearningSystemContext, Course>>();
            this.mapper = AutoMapperMock.GetMapper();
        }

        [Fact]
        public async Task GetCourseAsync_WithOneCourse_ShouldReturnCorrectDetails()
        {
            // Arrange
            this.context.Courses.Add(new Course { Id = 1, Name = "First course" });
            await this.context.SaveChangesAsync();

            this.repositoryMock
                .Setup(x => x.Details())
                .Returns(this.context.Courses)
                .Verifiable();

            var service = new StudentCoursesService(this.repositoryMock.Object, this.mapper);

            // Act
            var result = service.GetCourseAsync<CourseDetailsViewModel>(1);

            // Assert
            Assert.NotNull(result);
            await Assert.IsAssignableFrom<Task<CourseDetailsViewModel>>(result);
            Assert.Equal(1, result.Result.Id);
            Assert.Equal("First course", result.Result.Name);
        }

        [Fact]
        public async Task GetAllCoursesAsync_WithFewCourses_ShouldReturnAllCourses()
        {
            // Arrange
            this.context.Courses.Add(new Course { Id = 1, Name = "First course" });
            this.context.Courses.Add(new Course { Id = 2, Name = "Second course" });
            this.context.Courses.Add(new Course { Id = 3, Name = "Third course" });
            await this.context.SaveChangesAsync();

            this.repositoryMock
                .Setup(m => m.Details())
                .Returns(this.context.Courses)
                .Verifiable();

            var service = new StudentCoursesService(this.repositoryMock.Object, this.mapper);

            // Act
            var result = service.GetAllCoursesAsync<StudentAllCoursesViewModel>();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Result.Count());
            Assert.Equal(new[] { 1, 2, 3 }, result.Result.Select(c => c.Id).ToArray());
        }
    }
}