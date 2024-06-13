using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailInformAPI.Controllers;
using EmailInformAPI.DTO;
using EmailInformAPI.Repository;
using EmailInformAPI.Scheduler;
using Microsoft.Extensions.Configuration;
using Moq;

namespace EmailInformAPITesting.EmailGetUserControllerTest
{
    public class EmailGetUserControllerTest
    {
        // 2 tests for GetUser
        [Fact]
        public void GetUser_ReturnsNull_IfUserIsNotFound()
        {
            // Arrange
            var repository = new Mock<IEmailRepository>();

            var inMemorySettings = new Dictionary<string, string> {
                {"ConnectionStrings:NoReplyEmail", "fams.fpt.noreply@gmail.com"},
                {"ConnectionStrings:EmailKey", "fpwa vegm zwan gjuf"}
            };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var scheduler = new Mock<IScheduler>();

            var emailGetUserController = new EmailGetUserController(repository.Object, configuration, scheduler.Object);

            // Act
            var result = emailGetUserController.GetUser("nameTest12345");

            // Assert
            Assert.IsAssignableFrom<IEnumerable<UserGetDTO>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetUser_ReturnsCorrectUser_IfUserIsFound()
        {
            // Arrange
            var mockRepository = new Mock<IEmailRepository>();
            var inMemorySettings = new Dictionary<string, string> {
                {"ConnectionStrings:NoReplyEmail", "fams.fpt.noreply@gmail.com"},
                {"ConnectionStrings:EmailKey", "fpwa vegm zwan gjuf"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            var mockScheduler = new Mock<IScheduler>();

            var controller = new EmailGetUserController(
                mockRepository.Object,
                configuration,
                mockScheduler.Object);

            var expectedUser = new UserGetDTO { username = "TestUser", email = "test@example.com" };
            var users = new List<UserGetDTO> { expectedUser };
            mockRepository.Setup(repo => repo.GetUser("TestUser")).Returns(users);

            // Act
            var result = controller.GetUser("TestUser");

            // Assert
            Assert.Collection(result,
                user =>
                {
                    Assert.Equal(expectedUser.username, user.username);
                });
        }
    }
}
