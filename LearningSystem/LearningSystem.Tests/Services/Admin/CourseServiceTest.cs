namespace LearningSystem.Tests.Services.Admin
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data;
    using LearningSystem.Services.Admin;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using Models;
    using Moq;
    using Repository.Contracts;

    [TestClass]
    public class CourseServiceTest
    {
        private Mock<IRepository<LearningSystemContext, Course>> courseRepositoryMock;
        private LearningSystemContext context;
        private IMapper mapper;

        [TestInitialize]
        public void TestInitialize()
        {
            this.courseRepositoryMock = new Mock<IRepository<LearningSystemContext, Course>>();
            this.context = LearningSystemContextMock.GetContext();
            this.mapper = AutoMapperMock.GetMapper();
        }

        [TestMethod]
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

            Assert.IsNotNull(courseResult);
            Assert.AreEqual(expectedId, courseResult.Id);
            Assert.AreEqual(expectedName, courseResult.Name);
        }

        [TestMethod]
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
            Assert.IsNull(courseResult);
        }

        [TestMethod]
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
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(AddCourse);
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(AddCourse, "The course is null");
        }

        [TestMethod]
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
            Assert.AreEqual(courseName, course.Name);
            Assert.AreEqual(slugName, course.Slug);
        }

        [TestMethod]
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
            Assert.IsNotNull(courses);
            Assert.AreEqual(3, courses.Count());
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, courses.Select(c => c.Id).ToArray());
        }

        [TestMethod]
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
            Assert.IsNotNull(courses);
            Assert.AreEqual(0, courses.Count());
        }

        [TestMethod]
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
            Assert.AreEqual(1, courses.Id);
        }
    }
}