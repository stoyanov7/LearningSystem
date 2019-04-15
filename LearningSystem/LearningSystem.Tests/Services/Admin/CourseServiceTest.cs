namespace LearningSystem.Tests.Services.Admin
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data;
    using LearningSystem.Services.Admin;
    using Mocks;
    using Models;
    using Moq;
    using Repository.Contracts;
    using Xunit;

    public class CourseServiceTest
    {
        private readonly Mock<IRepository<LearningSystemContext, Course>> courseRepositoryMock;
        private readonly LearningSystemContext context;
        private readonly IMapper mapper;

        public CourseServiceTest()
        {
            this.courseRepositoryMock = new Mock<IRepository<LearningSystemContext, Course>>();
            this.context = LearningSystemContextMock.GetContext();
            this.mapper = AutoMapperMock.GetMapper();
        }

        [Fact]
        public async Task FindAsync_WithOneCourse_ShouldReturnCorrectCourse()
        {
            //Arrange
            const int expectedId = 1;
            const string expectedName = "AAA";

            var course = new Course
            {
                Id = expectedId,
                Name = expectedName
            };

            this.courseRepositoryMock
                .Setup(m => m.FindByIdAsync(expectedId))
                .ReturnsAsync(course)
                .Verifiable();

            var sut = new AdminCoursesService(null, this.courseRepositoryMock.Object);

            //Act
            var courseResult = await sut.FindCourseAsync(expectedId);

            //Assert
            this.courseRepositoryMock.Verify();

            Assert.NotNull(courseResult);
            Assert.Equal(expectedId, courseResult.Id);
            Assert.Equal(expectedName, courseResult.Name);
        }

        [Fact]
        public async Task FindAsync_WithoutCourse_ShouldReturnNull()
        {
            const int expectedId = 1;
            Course course = null;

            this.courseRepositoryMock
                .Setup(m => m.FindByIdAsync(expectedId))
                .ReturnsAsync(course)
                .Verifiable();

            var service = new AdminCoursesService(null, this.courseRepositoryMock.Object);

            //Act
            var courseResult = await service.FindCourseAsync(expectedId);

            // Assert
            Assert.Null(courseResult);
        }

        [Fact]
        public async Task AddCourse_WithNullCourse_ShouldThrowException() 
        {
            // Arrange           
            Course course = null;
            var service = new AdminCoursesService(null, this.courseRepositoryMock.Object);

            this.courseRepositoryMock
                .Setup(m => m.AddAsync(course))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            Task AddCourse() => service.AddCourseAsync(course);

            // Asserts 
            await Assert.ThrowsAsync<ArgumentNullException>(AddCourse);
            //await Assert.ThrowsAsync<ArgumentNullException>(AddCourse, "The course is null");
        }

        [Fact]
        public async Task AddCourse_WithProperCourse_ShouldReturnCorrectCourse()
        {
            // Arrange
            const string courseName = "New course name";
            const string slugName = "new-course-name";

            var courseModel = new Course
            {
                Name = courseName,
                Slug = slugName
            };

            this.courseRepositoryMock
                .Setup(m => m.AddAsync(courseModel))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new AdminCoursesService(this.mapper, this.courseRepositoryMock.Object);

            // Act
            var course = await service.AddCourseAsync(courseModel);
            
            // Asserts 
            Assert.Equal(courseName, course.Name);
            Assert.Equal(slugName, course.Slug);
        }

        [Fact]
        public async Task GetCourses_WithAFewCourses_ShouldReturnAll()
        {
            // Arrange
            this.context.Courses.Add(new Course { Id = 1, Name = "First course" });
            this.context.Courses.Add(new Course { Id = 2, Name = "Second course" });
            this.context.Courses.Add(new Course { Id = 3, Name = "Third course" });
            await this.context.SaveChangesAsync();

            this.courseRepositoryMock
                .Setup(m => m.Details())
                .Returns(this.context.Courses)
                .Verifiable();

            var service = new AdminCoursesService(this.mapper, this.courseRepositoryMock.Object);

            // Act
            var courses = await service.GetCoursesAsync<Course>();

            // Assert
            Assert.NotNull(courses);
            Assert.Equal(3, courses.Count());
            Assert.Equal(new[] { 1, 2, 3 }, courses.Select(c => c.Id).ToArray());
        }

        [Fact]
        public async Task GetCourses_WithNoCourses_ShouldReturnNone()
        {
            // Arrange           
            this.courseRepositoryMock
                .Setup(m => m.Details())
                .Returns(this.context.Courses)
                .Verifiable();

            var service = new AdminCoursesService(this.mapper, this.courseRepositoryMock.Object);

            // Act
            var courses = await service.GetCoursesAsync<Course>();

            // Assert
            Assert.NotNull(courses);
            Assert.Empty(courses);
        }

        [Fact]
        public async Task Details_WithValidCourse_ShouldReturnCorrectDetails()
        {
            // Arrange
            this.context.Courses.Add(new Course { Id = 1, Name = "First course" });
            await this.context.SaveChangesAsync();

            this.courseRepositoryMock
                .Setup(m => m.Details())
                .Returns(this.context.Courses)
                .Verifiable();

            var service = new AdminCoursesService(this.mapper, this.courseRepositoryMock.Object);

            // Act
            var courses = await service.CourseDetailsAsync<Course>(1);

            // Assert
            Assert.Equal(1, courses.Id);
        }
    }
}