using EmailInformAPI.Controllers;
using EmailInformAPI.DTO;
using EmailInformAPI.Repository;
using EmailInformAPI.Scheduler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using Xunit;

namespace EmailInformAPITesting.SendControllerTests
{
    public class SendControllerTests
    {
        // 2 tests for SetSchedulerEmail
        // 2 tests for SendEmail
        [Fact]
        public void SetSchedulerEmail_ReturnsOkResult_IfNoBodyInSchedule()
        {
            // Arrange
            var mockRepository = new Mock<IEmailRepository>();
            var mockScheduler = new Mock<IScheduler>();

            var inMemorySettings = new Dictionary<string, string> {
                {"ConnectionStrings:NoReplyEmail", "fams.fpt.noreply@gmail.com"},
                {"ConnectionStrings:EmailKey", "fpwa vegm zwan gjuf"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var controller = new SendController(mockRepository.Object, configuration, mockScheduler.Object);
            var emailSchedule = new EmailScheduleDTO
            {
                recipient = "test@example.com",
                subject = "Test Subject",
                title = "Test Title",
                body = null,
                date = DateTime.Now.AddMinutes(10) // Set 10 minutes in the future
            };

            // Act
            var result = controller.SetSchedulerEmail(emailSchedule) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void SetSchedulerEmail_ReturnsOkResult_IfAMemberOfBodyIsNull()
        {
            // Arrange
            var mockRepository = new Mock<IEmailRepository>();
            var mockScheduler = new Mock<IScheduler>();

            var inMemorySettings = new Dictionary<string, string> {
                {"ConnectionStrings:NoReplyEmail", "fams.fpt.noreply@gmail.com"},
                {"ConnectionStrings:EmailKey", "fpwa vegm zwan gjuf"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var controller = new SendController(mockRepository.Object, configuration, mockScheduler.Object);
            var emailSchedule = new EmailScheduleDTO
            {
                recipient = "test@example.com",
                subject = "Test Subject",
                title = "Test Title",
                body = new Score
                {
                    html = 10,
                    css = 9.5,
                    quiz3 = 8.5,
                    quiz4 = 9.6,
                    quiz5 = 9.9,
                    quiz6 = 10,
                    quiz_ave = 9.5,

                    // ASM
                    practice1 = 10,
                    practice2 = 9.5,
                    practice3 = 9.5,
                    asm_ave = 9.7,

                    // Extra
                    quizfinal = 10,
                    audit = 10,
                    practicefinal = 10,
                    status = true,

                    // Mock
                    mock = 10,
                    final = 10,
                    gpa = 4,
                    level = null,
                },
                date = DateTime.Now.AddMinutes(10) // Set 10 minutes in the future
            };

            // Act
            var result = controller.SetSchedulerEmail(emailSchedule) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void SendEmail_ReturnsOkResult_IfNoBodyInSchedule()
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

            var controller = new SendController(mockRepository.Object, configuration, mockScheduler.Object);
            var emailData = new EmailDTO
            {
                recipient = "test@example.com",
                subject = "Test Subject",
                title = "Test Title",
            };

            // Act
            var result = controller.SendEmail(emailData) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void SendEmail_ReturnsOkResult_IfAMemberOfBodyIsNull()
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

            var controller = new SendController(mockRepository.Object, configuration, mockScheduler.Object);
            var emailData = new EmailDTO
            {
                recipient = "test@example.com",
                subject = "Test Subject",
                body = new Score
                {
                    html = 10,
                    css = 9.5,
                    quiz3 = 8.5,
                    quiz4 = 9.6,
                    quiz5 = 9.9,
                    quiz6 = 10,
                    quiz_ave = 9.5,

                    // ASM
                    practice1 = 10,
                    practice2 = 9.5,
                    practice3 = 9.5,
                    asm_ave = 9.7,

                    // Extra
                    quizfinal = 10,
                    audit = 10,
                    practicefinal = 10,
                    status = true,

                    // Mock
                    mock = 10,
                    final = 10,
                    gpa = 4,
                    level = null,
                },
                title = "Test Title",
            };

            // Act
            var result = controller.SendEmail(emailData) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
