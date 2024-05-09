using ECommerceApp.Services;
using ECommerceApp.Model;
using Moq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using ECommerceAPI.ECommerce.Repositories.NewFolder;

namespace ECommerceApp.Tests
{
    [TestFixture]
    public class UserTest
    {
        private UserService _userService;
        private Mock<IUserRepository> _userRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            var loggerMock = new Mock<ILogger<UserService>>();
            _userService = new UserService(_userRepositoryMock.Object, loggerMock.Object);
        }

        [Test]
        public async Task TestGetUsers()
        {
            // Arrange
            var expectedUsers = new List<Users> { new Users { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com", Password = "hashedPassword" },
                                                   new Users { Id = 2, FirstName = "Jane", LastName = "Doe", Email = "jane@example.com", Password = "hashedPassword" } };
            _userRepositoryMock.Setup(repo => repo.GetUsers()).ReturnsAsync(expectedUsers);

            // Act
            var result = await _userService.GetUsers();

            // Assert
            Assert.AreEqual(expectedUsers.Count, result.Count);
        }

        [Test]
        public async Task TestAddUser()
        {
            // Arrange
            var newUser = new Users { Id = 3, FirstName = "Aditi", LastName = "Ghdage", Email = "aditi@example.com", Password = "password" };

            _userRepositoryMock.Setup(repo => repo.AddUser(It.IsAny<Users>())).ReturnsAsync(1);

            // Act
            var result = await _userService.AddUser(newUser);

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public async Task TestDeleteUser()
        {
            // Arrange
            int userId = 4;

            _userRepositoryMock.Setup(repo => repo.DeleteUser(userId)).ReturnsAsync(true);

            // Act
            var result = await _userService.DeleteUser(userId);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
