namespace LearningSystem.Tests.Services.Admin
{
    using System.Threading.Tasks;
    using Data;
    using LearningSystem.Services.Admin;
    using Models.Identity;
    using Moq;
    using Repository.Contracts;
    using Xunit;

    public class UsersServiceTest
    {
        private readonly Mock<IRepository<LearningSystemContext, ApplicationUser>> adminUsersRepositoryMock;

        public UsersServiceTest()
        {
            this.adminUsersRepositoryMock = new Mock<IRepository<LearningSystemContext, ApplicationUser>>();
        }

        [Fact]
        public async Task FindUserAsync_WithValidUser_ShouldReturnCorrectUser()
        {
            //Arrange
            const string expectedId = "1";
            const string expectedName = "AAA";

            var user = new ApplicationUser
            {
                Id = expectedId,
                UserName = expectedName
            };

            this.adminUsersRepositoryMock
                .Setup(m => m.FindByIdAsync(expectedId))
                .ReturnsAsync(user)
                .Verifiable();

            var sut = new AdminUsersService(this.adminUsersRepositoryMock.Object, null, null);

            //Act
            var result = await sut.FindUserAsync(expectedId);

            //Assert
            this.adminUsersRepositoryMock.Verify();

            Assert.NotNull(result);
            Assert.Equal(expectedId, result.Id);
            Assert.Equal(expectedName, result.UserName);
        }

        [Fact]
        public async Task FindUserAsync_WithoutUser_ShouldReturnNull()
        {
            const string expectedId = "1";
            ApplicationUser user = null;

            this.adminUsersRepositoryMock
                .Setup(m => m.FindByIdAsync(expectedId))
                .ReturnsAsync(user)
                .Verifiable();

            var service = new AdminUsersService(this.adminUsersRepositoryMock.Object, null, null);

            //Act
            var courseResult = await service.FindUserAsync(expectedId);

            // Assert
            Assert.Null(courseResult);
        }
    }
}