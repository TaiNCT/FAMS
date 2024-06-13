using EmailInformAPI.Controllers;
using EmailInformAPI.DTO;
using Entities.Context;
using Entities.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Nest;
using System.Linq.Expressions;

namespace EmailInformAPITesting.EmailSendControllerTest
{
    public class EmailSendControllerTest
    {
        //2 test case for getting EmailSend's list
        //2 test case for getting EmailSend by Id
        //3 test case for editing EmailSend
        //3 test case for getting email template detail by TemplateId
        //3 test case for updating status of email template

        [Fact]
        public async Task GetEmailSendList_WithValidList_ReturnsValidList()
        {
            // Arrange
            var _mockDb = new Mock<FamsContext>();
            var _controller = new EmailSendController(_mockDb.Object);

            var builder = new DbContextOptionsBuilder<FamsContext>();

            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<FamsContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase18")
                .UseInternalServiceProvider(serviceProvider)
                .Options;


            using (var context = new FamsContext(options))
            {

                // Add some dummy data for testing
                var testData = new List<EmailSend>
            {
                new EmailSend { EmailSendId = "ES1", Id = 1, TemplateId = "Template1", SenderId = "Sender1", Content = "Content1", SendDate = DateTime.Now, ReceiverType = 1 },
                new EmailSend { EmailSendId = "ES2", Id = 2, TemplateId = "Template2", SenderId = "Sender2", Content = "Content2", SendDate = DateTime.Now, ReceiverType = 2 }
            };

                await context.EmailSends.AddRangeAsync(testData);
                await context.SaveChangesAsync();
            }

            using (var context = new FamsContext(options))
            {
                var controller = new EmailSendController(context);

                // Act
                var result = await controller.GetEmailSendList();

                // Assert
                Assert.NotNull(result);
                var emailSendDTOs = Assert.IsAssignableFrom<IEnumerable<EmailSendDTOs>>(result.Value);
                Assert.Equal(2, emailSendDTOs.Count());
                Assert.Equal("ES1", emailSendDTOs.ElementAt(0).EmailSendId);
                Assert.Equal("ES2", emailSendDTOs.ElementAt(1).EmailSendId);
            }
        }

        [Fact]
        public async Task GetEmailSendList_WithEmptyList_ReturnsEmptyList()
        {
            // Arrange
            var _mockDb = new Mock<FamsContext>();
            var _controller = new EmailSendController(_mockDb.Object);

            var builder = new DbContextOptionsBuilder<FamsContext>();

            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<FamsContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase18")
                .UseInternalServiceProvider(serviceProvider)
                .Options;


            using (var context = new FamsContext(options))
            {

                // Add some dummy data for testing
                var testData = new List<EmailSend>();

                await context.EmailSends.AddRangeAsync(testData);
                await context.SaveChangesAsync();
            }

            using (var context = new FamsContext(options))
            {
                var controller = new EmailSendController(context);

                // Act
                var result = await controller.GetEmailSendList();

                // Assert
                Assert.NotNull(result);
                var emailSendDTOs = Assert.IsAssignableFrom<IEnumerable<EmailSendDTOs>>(result.Value);
                Assert.Empty(emailSendDTOs);
            }
        }

        [Fact]
        public async Task GetEmailSend_WithValidId_ReturnsEmailSendDTO()
        {
            // Arrange
            var emailId = 1;
            var senderId = "sender456";
            var emailSend = new EmailSend
            {
                EmailSendId = "emailSendId",
                Id = 1,
                TemplateId = "1",
                SenderId = senderId,
                Content = "Test content",
                SendDate = DateTime.UtcNow,
                ReceiverType = 1,
            };

            var emailTemplate = new EmailTemplate
            {
                EmailTemplateId = "1",
                Name = "Test Template",
                Description = "Test Description",
                Type = 1,
                IdStatus = 1
            };

            var user = new User
            {
                Id = 1,
                UserId = "sender456",
                FullName = "Sender456",
                Email = "sender1@example.com",
                Dob = new DateTime(1990, 1, 1),
                Address = "address1",
                RoleId = "Role1",
                CreatedBy = "Tai1",
                CreatedDate = DateTime.UtcNow,
                Gender = "Male",
                Password = "12345",
                Phone = "1234567890",
                Username = "user1",
                ModifiedBy = "Tai1",
                ModifiedDate = new DateTime(1990, 5, 12),
                Avatar = "Avatar1",
                Status = true,
            };

            var userList = new List<User> { user };

            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<FamsContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase18")
                .UseInternalServiceProvider(serviceProvider)
                .Options;


            // Initialize context and add data
            using (var context = new FamsContext(options))
            {
                context.EmailTemplates.Add(emailTemplate);
                context.Users.AddRange(userList);
                context.EmailSends.Add(emailSend);
                await context.SaveChangesAsync();
            }

            // Create instance of EmailSendController with the in-memory database context
            using (var context = new FamsContext(options))
            {
                var emailController = new EmailSendController(context);

                // Act
                var result = await emailController.GetEmailSend(emailId);

                // Assert
                var emailSendDTO = Assert.IsType<EmailSendDTOs>(result.Value);

                Assert.Equal(emailSend.Id, emailSendDTO.Id);
                Assert.Equal(emailSend.TemplateId, emailSendDTO.TemplateId);
                Assert.Equal(emailSend.SenderId, emailSendDTO.SenderId);
                Assert.Equal(user.FullName, emailSendDTO.FullName);
                Assert.Equal(emailSend.Content, emailSendDTO.Content);
                Assert.Equal(emailSend.SendDate, emailSendDTO.SendDate);
                Assert.Equal(emailSend.ReceiverType, emailSendDTO.ReceiverType);
            }
        }

        [Fact]
        public async Task GetEmailSend_WithInValidId_ReturnsNotFound()
        {
            // Arrange
            var emailId = 2;
            var senderId = "sender456";
            var emailSend = new EmailSend
            {
                EmailSendId = "emailSendId",
                Id = 1,
                TemplateId = "1",
                SenderId = senderId,
                Content = "Test content",
                SendDate = DateTime.UtcNow,
                ReceiverType = 1,
            };

            var user = new User
            {
                Id = 1,
                UserId = "sender456",
                FullName = "Sender456",
                Email = "sender1@example.com",
                Dob = new DateTime(1990, 1, 1),
                Address = "address1",
                RoleId = "Role1",
                CreatedBy = "Tai1",
                CreatedDate = DateTime.UtcNow,
                Gender = "Male",
                Password = "12345",
                Phone = "1234567890",
                Username = "user1",
                ModifiedBy = "Tai1",
                ModifiedDate = new DateTime(1990, 5, 12),
                Avatar = "Avatar1",
                Status = true,
            };

            var userList = new List<User> { user };

            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<FamsContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase18")
                .UseInternalServiceProvider(serviceProvider)
                .Options;


            // Initialize context and add data
            using (var context = new FamsContext(options))
            {
                context.EmailSends.Add(emailSend);
                context.Users.AddRange(userList);
                await context.SaveChangesAsync();
            }

            // Create instance of EmailSendController with the in-memory database context
            using (var context = new FamsContext(options))
            {
                var emailController = new EmailSendController(context);

                // Act
                var result = await emailController.GetEmailSend(emailId);

                // Assert
                Assert.IsType<BadRequestObjectResult>(result.Result);
            }
        }

        /*[Fact]
        public async Task EditTemplate_WithValidTemplateId_ReturnsEmailTemplateDTO()
        {
            // Arrange
            var expectedTemplateId = "templateId";
            var expectedName = "Test Template";
            var expectedDescription = "Test Description";
            var expectedType = "Reserve";

            var mockDbSet = new Mock<DbSet<EmailTemplate>>();
            mockDbSet.Setup(m => m.FindAsync(expectedTemplateId)).ReturnsAsync(new EmailTemplate
            {
                EmailTemplateId = expectedTemplateId,
                Name = expectedName,
                Description = expectedDescription,
                Type = 1,
                IdStatus = 1
            });

            var mockDbContext = new Mock<FamsContext>();
            mockDbContext.Setup(m => m.EmailTemplates).Returns(mockDbSet.Object);
            var emailController = new EmailSendController(mockDbContext.Object);

            // Act
            var result = await emailController.EditTemplate(expectedTemplateId);

            // Assert
            var okResult = Assert.IsType<ActionResult<EmailTemplateDTO>>(result);
            var emailTemplateDTO = Assert.IsType<EmailTemplateDTO>(okResult.Value);

            Assert.Equal(expectedTemplateId, emailTemplateDTO.TemId);
            Assert.Equal(expectedName, emailTemplateDTO.Name);
            Assert.Equal(expectedDescription, emailTemplateDTO.Description);
            Assert.Equal(1, emailTemplateDTO.IdType);
            Assert.Equal(expectedType, emailTemplateDTO.Type);
            Assert.Equal(1, emailTemplateDTO.Status);
        }*/

        /*[Fact]
        public async Task EditTemplate_WithInValidTemplateId_ReturnsEmailTemplateDTO()
        {
            // Arrange
            var expectedTemplateId = "templateId";
            var expectedName = "Test Template";
            var expectedDescription = "Test Description";
            var expectedType = "Reserve";

            var mockDbSet = new Mock<DbSet<EmailTemplate>>();
            mockDbSet.Setup(m => m.FindAsync(expectedTemplateId)).ReturnsAsync((EmailTemplate)null);

            var mockDbContext = new Mock<FamsContext>();
            mockDbContext.Setup(m => m.EmailTemplates).Returns(mockDbSet.Object);
            var emailController = new EmailSendController(mockDbContext.Object);

            // Act
            var result = await emailController.EditTemplate(expectedTemplateId);

            // Assert
            var okResult = Assert.IsType<NotFoundResult>(result.Result);
        }*/

        [Fact]
        public async Task EditEmailSend_ReturnsEmailSendNotFound_IfEmailSendNotFound()
        {
            // Arrange
            var senderId = "sender456";
            var emailSend = new EmailSend
            {
                EmailSendId = "emailSendId",
                Id = 1,
                TemplateId = "1",
                SenderId = senderId,
                Content = "Test content",
                SendDate = DateTime.UtcNow,
                ReceiverType = 1,
            };

            var updatedEmailSendDTO = new EditEmailSendDTO
            {
                EmailTemplateId = "1",
                Content = "Updated content",
                SendDate = DateTime.UtcNow,
                ReceiverType = 1,
                SenderId = "sender123"
            };

            var emailTemplate = new EmailTemplate
            {
                EmailTemplateId = "1",
                Name = "Test Template",
                Description = "Test Description",
                Type = 1,
                IdStatus = 1
            };

            var user = new User
            {
                Id = 1,
                UserId = "sender456",
                FullName = "Sender456",
                Email = "sender1@example.com",
                Dob = new DateTime(1990, 1, 1),
                Address = "address1",
                RoleId = "Role1",
                CreatedBy = "Tai1",
                CreatedDate = DateTime.UtcNow,
                Gender = "Male",
                Password = "12345",
                Phone = "1234567890",
                Username = "user1",
                ModifiedBy = "Tai1",
                ModifiedDate = new DateTime(1990, 5, 12),
                Avatar = "Avatar1",
                Status = true,
            };

            var userList = new List<User> { user };

            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<FamsContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase18")
                .UseInternalServiceProvider(serviceProvider)
                .Options;


            // Initialize context and add data
            using (var context = new FamsContext(options))
            {
                context.EmailTemplates.Add(emailTemplate);
                context.Users.AddRange(userList);
                context.EmailSends.Add(emailSend);
                await context.SaveChangesAsync();
            }

            // Create instance of EmailSendController with the in-memory database context
            using (var context = new FamsContext(options))
            {
                var emailController = new EmailSendController(context);

                // Act
                var result = await emailController.EditEmailSend("2", updatedEmailSendDTO);

                // Assert
                var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
                Assert.Equal("Email send not found!", notFoundResult.Value);
            }
        }

        [Fact]
        public async Task EditEmailSend_ReturnsNoContentObjectResult_IfEditingSuccessfully()
        {
            // Arrange
            var senderId = "sender456";
            var emailSend = new EmailSend
            {
                EmailSendId = "emailSendId",
                Id = 1,
                TemplateId = "1",
                SenderId = senderId,
                Content = "Test content",
                SendDate = DateTime.UtcNow,
                ReceiverType = 1,
            };

            var updatedEmailSendDTO = new EditEmailSendDTO
            {
                EmailTemplateId = "1",
                Content = "Updated content",
                SendDate = DateTime.UtcNow,
                ReceiverType = 1,
                SenderId = "sender123"
            };

            var emailTemplate = new EmailTemplate
            {
                EmailTemplateId = "1",
                Name = "Test Template",
                Description = "Test Description",
                Type = 1,
                IdStatus = 1
            };

            var user = new User
            {
                Id = 1,
                UserId = "sender456",
                FullName = "Sender456",
                Email = "sender1@example.com",
                Dob = new DateTime(1990, 1, 1),
                Address = "address1",
                RoleId = "Role1",
                CreatedBy = "Tai1",
                CreatedDate = DateTime.UtcNow,
                Gender = "Male",
                Password = "12345",
                Phone = "1234567890",
                Username = "user1",
                ModifiedBy = "Tai1",
                ModifiedDate = new DateTime(1990, 5, 12),
                Avatar = "Avatar1",
                Status = true,
            };

            var userList = new List<User> { user };

            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<FamsContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase18")
                .UseInternalServiceProvider(serviceProvider)
                .Options;


            // Initialize context and add data
            using (var context = new FamsContext(options))
            {
                context.EmailTemplates.Add(emailTemplate);
                context.Users.AddRange(userList);
                context.EmailSends.Add(emailSend);
                await context.SaveChangesAsync();
            }

            // Create instance of EmailSendController with the in-memory database context
            using (var context = new FamsContext(options))
            {
                var emailController = new EmailSendController(context);

                // Act
                var result = await emailController.EditEmailSend("1", updatedEmailSendDTO);

                // Assert
                var emailSendDTO = Assert.IsType<NoContentResult>(result);
            }
        }

        [Fact]
        public async Task EditEmailSend_ReturnsEmailTemplateNotFound()
        {
            // Arrange
            var senderId = "sender456";
            var emailSend = new EmailSend
            {
                EmailSendId = "emailSendId",
                Id = 1,
                TemplateId = "1",
                SenderId = senderId,
                Content = "Test content",
                SendDate = DateTime.UtcNow,
                ReceiverType = 1,
            };

            var updatedEmailSendDTO = new EditEmailSendDTO
            {
                EmailTemplateId = "1",
                Content = "Updated content",
                SendDate = DateTime.UtcNow,
                ReceiverType = 1,
                SenderId = "sender123"
            };

            var emailTemplate = new EmailTemplate
            {
                EmailTemplateId = "2",
                Name = "Test Template",
                Description = "Test Description",
                Type = 1,
                IdStatus = 1
            };

            var user = new User
            {
                Id = 1,
                UserId = "sender456",
                FullName = "Sender456",
                Email = "sender1@example.com",
                Dob = new DateTime(1990, 1, 1),
                Address = "address1",
                RoleId = "Role1",
                CreatedBy = "Tai1",
                CreatedDate = DateTime.UtcNow,
                Gender = "Male",
                Password = "12345",
                Phone = "1234567890",
                Username = "user1",
                ModifiedBy = "Tai1",
                ModifiedDate = new DateTime(1990, 5, 12),
                Avatar = "Avatar1",
                Status = true,
            };

            var userList = new List<User> { user };

            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<FamsContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase18")
                .UseInternalServiceProvider(serviceProvider)
                .Options;


            // Initialize context and add data
            using (var context = new FamsContext(options))
            {
                context.EmailTemplates.Add(emailTemplate);
                context.Users.AddRange(userList);
                context.EmailSends.Add(emailSend);
                await context.SaveChangesAsync();
            }

            // Create instance of EmailSendController with the in-memory database context
            using (var context = new FamsContext(options))
            {
                var emailController = new EmailSendController(context);

                // Act
                var result = await emailController.EditEmailSend("1", updatedEmailSendDTO);

                // Assert
                var notFoundResult = Assert.IsType<BadRequestObjectResult>(result);
                Assert.Equal("Email template not found!", notFoundResult.Value);
            }
        }

        [Fact]
        public async Task GetEmailbyTempId_ReturnsEmailDetail_IfItExists()
        {
            // Arrange
            var tempId = "your_temp_id"; // Provide a valid template ID for testing

            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<FamsContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase18")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            // Populate the in-memory database with test data
            using (var context = new FamsContext(options))
            {
                context.EmailTemplates.Add(new EmailTemplate
                {
                    EmailTemplateId = tempId,
                    Name = "Test Template",
                    Description = "Test description",
                    Type = 1,
                    CreatedDate = DateTime.Now
                });
                context.Users.Add(new User
                {
                    Id = 1,
                    UserId = "sender456",
                    FullName = "Sender456",
                    Email = "sender1@example.com",
                    Dob = new DateTime(1990, 1, 1),
                    Address = "address1",
                    RoleId = "Role1",
                    CreatedBy = "Tai1",
                    CreatedDate = DateTime.UtcNow,
                    Gender = "Male",
                    Password = "12345",
                    Phone = "1234567890",
                    Username = "user1",
                    ModifiedBy = "Tai1",
                    ModifiedDate = new DateTime(1990, 5, 12),
                    Avatar = "Avatar1",
                    Status = true,
                });
                context.EmailSends.Add(new EmailSend
                {
                    EmailSendId = "ES1",
                    Id = 1,
                    TemplateId = tempId,
                    SenderId = "sender456",
                    Content = "Test content",
                    SendDate = DateTime.Now,
                    ReceiverType = 1
                });
                await context.SaveChangesAsync();
            }

            using (var context = new FamsContext(options))
            {
                var controller = new EmailSendController(context);

                // Act
                var result = await controller.GetEmailbyTempId(tempId);

                // Assert
                Assert.NotNull(result);
                Assert.IsType<ActionResult<EmailSendDTOs>>(result);

                var emailSendDTO = Assert.IsType<EmailSendDTOs>(result.Value);
                Assert.Equal("Test Template", emailSendDTO.EmailName);
                Assert.Equal("Test description", emailSendDTO.Description);
                Assert.Equal("ES1", emailSendDTO.EmailSendId);
                Assert.Equal("Sender456", emailSendDTO.FullName);
            }
        }

        [Fact]
        public async Task GetEmailbyTempId_ReturnsNotFound_IfEmailSendCanNotBeFound()
        {
            // Arrange
            var tempId = "your_temp_id"; // Provide a valid template ID for testing

            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<FamsContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase18")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            // Populate the in-memory database with test data
            using (var context = new FamsContext(options))
            {
                context.EmailTemplates.Add(new EmailTemplate
                {
                    EmailTemplateId = tempId,
                    Name = "Test Template",
                    Description = "Test description",
                    Type = 1,
                    CreatedDate = DateTime.Now
                });
                context.Users.Add(new User
                {
                    Id = 1,
                    UserId = "sender456",
                    FullName = "Sender456",
                    Email = "sender1@example.com",
                    Dob = new DateTime(1990, 1, 1),
                    Address = "address1",
                    RoleId = "Role1",
                    CreatedBy = "Tai1",
                    CreatedDate = DateTime.UtcNow,
                    Gender = "Male",
                    Password = "12345",
                    Phone = "1234567890",
                    Username = "user1",
                    ModifiedBy = "Tai1",
                    ModifiedDate = new DateTime(1990, 5, 12),
                    Avatar = "Avatar1",
                    Status = true,
                });
                context.EmailSends.Add(new EmailSend
                {
                    EmailSendId = "ES1",
                    Id = 1,
                    TemplateId = tempId + "/",
                    SenderId = "sender456",
                    Content = "Test content",
                    SendDate = DateTime.Now,
                    ReceiverType = 1
                });
                await context.SaveChangesAsync();
            }

            using (var context = new FamsContext(options))
            {
                var controller = new EmailSendController(context);

                // Act
                var result = await controller.GetEmailbyTempId(tempId);

                // Assert
                var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);

                Assert.Equal("Not Found!", badRequest.Value);
            }
        }

        [Fact]
        public async Task GetEmailbyTempId_ReturnsTemplateDetailHasNullFullName_IfSenderCanNotBeFound()
        {
            // Arrange
            var tempId = "your_temp_id"; // Provide a valid template ID for testing

            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<FamsContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase18")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            // Populate the in-memory database with test data
            using (var context = new FamsContext(options))
            {
                context.EmailTemplates.Add(new EmailTemplate
                {
                    EmailTemplateId = tempId,
                    Name = "Test Template",
                    Description = "Test description",
                    Type = 1,
                    CreatedDate = DateTime.Now
                });
                context.Users.Add(new User
                {
                    Id = 1,
                    UserId = "sender456",
                    FullName = "Sender456",
                    Email = "sender1@example.com",
                    Dob = new DateTime(1990, 1, 1),
                    Address = "address1",
                    RoleId = "Role1",
                    CreatedBy = "Tai1",
                    CreatedDate = DateTime.UtcNow,
                    Gender = "Male",
                    Password = "12345",
                    Phone = "1234567890",
                    Username = "user1",
                    ModifiedBy = "Tai1",
                    ModifiedDate = new DateTime(1990, 5, 12),
                    Avatar = "Avatar1",
                    Status = true,
                });
                context.EmailSends.Add(new EmailSend
                {
                    EmailSendId = "ES1",
                    Id = 1,
                    TemplateId = tempId,
                    SenderId = "sender458",
                    Content = "Test content",
                    SendDate = DateTime.Now,
                    ReceiverType = 1
                });
                await context.SaveChangesAsync();
            }

            using (var context = new FamsContext(options))
            {
                var controller = new EmailSendController(context);

                // Act
                var result = await controller.GetEmailbyTempId(tempId);

                // Assert
                Assert.NotNull(result);
                Assert.IsType<ActionResult<EmailSendDTOs>>(result);

                var emailSendDTO = Assert.IsType<EmailSendDTOs>(result.Value);
                Assert.Null(emailSendDTO.FullName);
            }
        }

        [Fact]
        public void UpdateStatus_ReturnsStatusUpdatedSuccessfully_WhenUpdatingSuccessfully()
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<FamsContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase18")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            using (var context = new FamsContext(options))
            {
                context.EmailTemplates.Add(new EmailTemplate
                {
                    EmailTemplateId = "testTemplateId",
                    IdStatus = 1,
                    Description = "Test description",
                    Name = "Test name",
                });

                context.SaveChanges();
            }

            using (var context = new FamsContext(options))
            {
                var controller = new EmailSendController(context);
                var statusDto = new StatusDTO { EmailTemplateId = "testTemplateId", Status = 2 };

                // Act
                var result = controller.UpdateStatus(statusDto) as IActionResult;

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var value = Assert.IsType<String>(okResult.Value);
                Assert.Equal("Status updated successfully.", value);
            }
        }

        [Fact]
        public void UpdateStatus_ReturnsStatusMustBeEither1Or2_WhenStatusNeedUpdatingIsNotCorrected()
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<FamsContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase18")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            using (var context = new FamsContext(options))
            {
                context.EmailTemplates.Add(new EmailTemplate
                {
                    EmailTemplateId = "testTemplateId",
                    IdStatus = 1,
                    Description = "Test description",
                    Name = "Test name",
                });

                context.SaveChanges();
            }

            using (var context = new FamsContext(options))
            {
                var controller = new EmailSendController(context);
                var statusDto = new StatusDTO { EmailTemplateId = "testTemplateId", Status = 3 };

                // Act
                var result = controller.UpdateStatus(statusDto) as IActionResult;

                // Assert
                var okResult = Assert.IsType<BadRequestObjectResult>(result);
                var value = Assert.IsType<String>(okResult.Value);
                Assert.Equal("Status must be either 1 or 2.", value);
            }
        }

        [Fact]
        public void UpdateStatus_ReturnsEmailTemplateNotFound_IfEmailTemplateIdCanNotBeFound()
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<FamsContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase18")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            using (var context = new FamsContext(options))
            {
                context.EmailTemplates.Add(new EmailTemplate
                {
                    EmailTemplateId = "testTemplateId",
                    IdStatus = 1,
                    Description = "Test description",
                    Name = "Test name",
                });

                context.SaveChanges();
            }

            using (var context = new FamsContext(options))
            {
                var controller = new EmailSendController(context);
                var statusDto = new StatusDTO { EmailTemplateId = "testTemplateI", Status = 2 };

                // Act
                var result = controller.UpdateStatus(statusDto) as IActionResult;

                // Assert
                var okResult = Assert.IsType<NotFoundObjectResult>(result);
                var value = Assert.IsType<String>(okResult.Value);
                Assert.Equal("Email template not found.", value);
            }
        }
    }
}