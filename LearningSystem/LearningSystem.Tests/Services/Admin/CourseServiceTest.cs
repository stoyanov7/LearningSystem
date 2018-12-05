namespace LearningSystem.Tests.Services.Admin
{
    using System;
    using System.Threading.Tasks;
    using LearningSystem.Services.Admin;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using Moq;
    using Repository.Contracts;

    [TestClass]
    public class CourseServiceTest
    {
        private Mock<IRepository<Course>> courseRepositoryMock;

        [TestInitialize]
        public void TestInitialize()
        {
            this.courseRepositoryMock = new Mock<IRepository<Course>>();
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

            var sut = new CoursesService(null, this.courseRepositoryMock.Object);

            //Act
            var courseResult = await sut.FindAsync(expectedId);

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

            var service = new CoursesService(null, this.courseRepositoryMock.Object);

            //Act
            var courseResult = await service.FindAsync(expectedId);

            // Assert
            Assert.IsNull(courseResult);
        }

        [TestMethod]
        public async Task AddCourse_WithNullCourse_ShouldThrowException() 
        {
            // Arrange           
            Course course = null;
            var service = new CoursesService(null, this.courseRepositoryMock.Object);

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

            var mapper = AutoMapperMock.GetMapper();
            var service = new CoursesService(mapper, this.courseRepositoryMock.Object);

            // Act
            var course = await service.AddCourseAsync(courseModel);
            
            // Asserts 
            Assert.AreEqual(courseName, course.Name);
            Assert.AreEqual(slugName, course.Slug);
        }
    }
}