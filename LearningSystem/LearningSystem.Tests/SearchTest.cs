namespace LearningSystem.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentAssertions;
    using LearningSystem.Services.Student.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Web.Controllers;
    using Web.Models.Search;
    using Xunit;

    public class SearchTest
    {
        [Fact]
        public async Task Search_WithNoCriteria_ShouldReturnNoResults()
        {
            // Arrange
            var controller = new HomeController(null, null, null);

            // Act
            var result = await controller.Search(new SearchFormBindingModel
            {
                SearchInCourses = false,
                SearchInUsers = false
            });

            // Assert
            result.Should().BeOfType<ViewResult>();

            result.As<ViewResult>().Model.Should().BeOfType<SearchViewModel>();

            var searchViewModel = result.As<ViewResult>().Model.As<SearchViewModel>();

            searchViewModel.Courses.Should().BeEmpty();
            searchViewModel.Users.Should().BeEmpty();
            searchViewModel.SearchText.Should().BeNull();
        }

        [Fact]
        public async Task Search_WithValidCourse_ShouldReturnViewWithValidModel()
        {
            // Arrange
            const string searchText = "Text";

            var courseServiceMock = new Mock<IStudentCourseInstancesService>();

            courseServiceMock
                .Setup(c => c.GetCourseInstancesAsync<SearchCourseInstanceViewModel>(It.IsAny<string>()))
                .ReturnsAsync(new List<SearchCourseInstanceViewModel>
                {
                    new SearchCourseInstanceViewModel { Id = 10 }
                });

            var controller = new HomeController(courseServiceMock.Object, null, null);

            // Act
            var result = await controller.Search(new SearchFormBindingModel
            {
                SearchText = searchText,
                SearchInCourses = true,
                SearchInUsers = false
            });

            // Assert
            result.Should().BeOfType<ViewResult>();

            result
                .As<ViewResult>()
                .Model
                .Should()
                .BeOfType<SearchViewModel>();

            var searchViewModel = result.As<ViewResult>().Model.As<SearchViewModel>();

            searchViewModel
                .Courses
                .Should()
                .Match(c => c.As<List<SearchCourseInstanceViewModel>>().Count == 1);

            searchViewModel
                .Courses
                .First()
                .Should()
                .Match(c => c.As<SearchCourseInstanceViewModel>().Id == 10);

            searchViewModel.Users.Should().BeEmpty();
            searchViewModel.SearchText.Should().Be(searchText);
        }

        [Fact]
        public async Task Search_WithValidUser_Should()
        {
            // Arrange
            const string searchText = "User";
            var studentsServiceMock = new Mock<IStudentsService>();

            studentsServiceMock.Setup(x => x.FindAsync<SearchUsersViewModel>(It.IsAny<string>()))
                .ReturnsAsync(new List<SearchUsersViewModel>
                {
                    new SearchUsersViewModel { Username = searchText }
                });

            var controller = new HomeController(null, studentsServiceMock.Object, null);

            // Act
            var result = await controller.Search(new SearchFormBindingModel
            {
                SearchText = searchText,
                SearchInCourses = false,
                SearchInUsers = true
            });

            // Assert
            result.Should().BeOfType<ViewResult>();

            result
                .As<ViewResult>()
                .Model
                .Should()
                .BeOfType<SearchViewModel>();

            var searchViewModel = result.As<ViewResult>().Model.As<SearchViewModel>();

            searchViewModel
                .Users
                .Should()
                .Match(c => c.As<List<SearchUsersViewModel>>().Count == 1);

            searchViewModel
                .Users
                .First()
                .Should()
                .Match(c => c.As<SearchUsersViewModel>().Username == searchText);

            searchViewModel.SearchText.Should().Be(searchText);
            searchViewModel.Courses.Should().BeEmpty();
        }
    }
}