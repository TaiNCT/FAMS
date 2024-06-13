using EmailInformAPI.Controllers;
using EmailInformAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Entities.Context;
using Entities.Models;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmailInformAPITesting.EmailTemplateControllerTest
{
    public class EmailTemplateControllerTest
    {
        // 4 Test Cases for add new email template
        // 14 Test Cases for view email templates
        //      3 Test Cases for testing valid returned result
        //      2 Test Case for testing sort by name
        //      2 Test Case for testing sort by description
        //      2 Test Case for testing sort by type
        //      2 Test Case for filtering by one type
        //      1 Test Case for filtering by three type
        //      1 Test Case for filtering by one status
        //      1 Test Case for filtering by status and type
        // 3 Test Cases for editing an email template
        // 2 Test Case for getting an email template by id

        [Fact]
        public async Task AddNew_ValidInput_WithApplyToStudent_ReturnsCreatedAtActionResult()
        {
            // Arrange
            // Mock DbContext and DbSet
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<FamsContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase18")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            var dbContextMock = new Mock<FamsContext>(options);

            var emailTemplateList = new List<EmailTemplate>(); // Create an empty list to act as DbSet
            var emailTemplateQueryable = emailTemplateList.AsQueryable();
            var emailTemplateMock = new Mock<DbSet<EmailTemplate>>();
            emailTemplateMock.As<IAsyncEnumerable<EmailTemplate>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                                .Returns(new TestAsyncEnumerator<EmailTemplate>(emailTemplateQueryable.GetEnumerator()));
            emailTemplateMock.As<IQueryable<EmailTemplate>>().Setup(m => m.Provider)
                                .Returns(new TestAsyncQueryProvider<EmailTemplate>(emailTemplateQueryable.Provider));
            emailTemplateMock.As<IQueryable<EmailTemplate>>().Setup(m => m.Expression).Returns(emailTemplateQueryable.Expression);
            emailTemplateMock.As<IQueryable<EmailTemplate>>().Setup(m => m.ElementType).Returns(emailTemplateQueryable.ElementType);
            emailTemplateMock.As<IQueryable<EmailTemplate>>().Setup(m => m.GetEnumerator()).Returns(() => emailTemplateQueryable.GetEnumerator());

            var emailSendSetMock = new Mock<DbSet<EmailSend>>();
            var emailSendStudentSetMock = new Mock<DbSet<EmailSendStudent>>();
            var userSetMock = new Mock<DbSet<User>>();


            var studentList = new List<Student>(); // Create an empty list to act as DbSet
            var studentQueryable = studentList.AsQueryable();
            var studentSetMock = new Mock<DbSet<Student>>();
            studentSetMock.As<IAsyncEnumerable<Student>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                                .Returns(new TestAsyncEnumerator<Student>(studentQueryable.GetEnumerator()));
            studentSetMock.As<IQueryable<Student>>().Setup(m => m.Provider)
                                .Returns(new TestAsyncQueryProvider<Student>(studentQueryable.Provider));
            studentSetMock.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(studentQueryable.Expression);
            studentSetMock.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(studentQueryable.ElementType);
            studentSetMock.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(() => studentQueryable.GetEnumerator());

            // Setup behavior of DbContext
            dbContextMock.Setup(m => m.EmailTemplates).Returns(emailTemplateMock.Object);
            dbContextMock.Setup(m => m.EmailSends).Returns(emailSendSetMock.Object);
            dbContextMock.Setup(m => m.EmailSendStudents).Returns(emailSendStudentSetMock.Object);
            dbContextMock.Setup(m => m.Users).Returns(userSetMock.Object);
            dbContextMock.Setup(m => m.Students).Returns(studentSetMock.Object);

            var controller = new EmailTemplateController(dbContextMock.Object);
            var createDto = new AddEmailTemplateDTO
            {
                Name = "Test Email Template",
                Description = "This is a test email template",
                Type = 1,
                ApplyTo = "StUdent"
            };

            // Act
            var result = await controller.AddNew(createDto);

            // Assert
            var viewResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var model = Assert.IsAssignableFrom<AddEmailTemplateDTO>(viewResult.Value);
            Assert.Equal(createDto.ApplyTo.ToLower(), model.ApplyTo);
        }

        [Fact]
        public async Task AddNew_ValidInput_WithApplyToTrainer_ReturnsCreatedAtActionResult()
        {
            // Arrange
            // Mock DbContext and DbSet
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<FamsContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase18")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            var dbContextMock = new Mock<FamsContext>(options);

            var emailTemplateList = new List<EmailTemplate>(); // Create an empty list to act as DbSet
            var emailTemplateQueryable = emailTemplateList.AsQueryable();
            var emailTemplateMock = new Mock<DbSet<EmailTemplate>>();
            emailTemplateMock.As<IAsyncEnumerable<EmailTemplate>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                                .Returns(new TestAsyncEnumerator<EmailTemplate>(emailTemplateQueryable.GetEnumerator()));
            emailTemplateMock.As<IQueryable<EmailTemplate>>().Setup(m => m.Provider)
                                .Returns(new TestAsyncQueryProvider<EmailTemplate>(emailTemplateQueryable.Provider));
            emailTemplateMock.As<IQueryable<EmailTemplate>>().Setup(m => m.Expression).Returns(emailTemplateQueryable.Expression);
            emailTemplateMock.As<IQueryable<EmailTemplate>>().Setup(m => m.ElementType).Returns(emailTemplateQueryable.ElementType);
            emailTemplateMock.As<IQueryable<EmailTemplate>>().Setup(m => m.GetEnumerator()).Returns(() => emailTemplateQueryable.GetEnumerator());

            var emailSendSetMock = new Mock<DbSet<EmailSend>>();
            var emailSendStudentSetMock = new Mock<DbSet<EmailSendStudent>>();


            var userList = new List<User>(); // Create an empty list to act as DbSet
            var userQueryable = userList.AsQueryable();
            var userSetMock = new Mock<DbSet<User>>();
            userSetMock.As<IAsyncEnumerable<User>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                                .Returns(new TestAsyncEnumerator<User>(userQueryable.GetEnumerator()));
            userSetMock.As<IQueryable<User>>().Setup(m => m.Provider)
                                .Returns(new TestAsyncQueryProvider<User>(userQueryable.Provider));
            userSetMock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userQueryable.Expression);
            userSetMock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userQueryable.ElementType);
            userSetMock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => userQueryable.GetEnumerator());


            var studentList = new List<Student>(); // Create an empty list to act as DbSet
            var studentQueryable = studentList.AsQueryable();
            var studentSetMock = new Mock<DbSet<Student>>();
            studentSetMock.As<IAsyncEnumerable<Student>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                                .Returns(new TestAsyncEnumerator<Student>(studentQueryable.GetEnumerator()));
            studentSetMock.As<IQueryable<Student>>().Setup(m => m.Provider)
                                .Returns(new TestAsyncQueryProvider<Student>(studentQueryable.Provider));
            studentSetMock.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(studentQueryable.Expression);
            studentSetMock.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(studentQueryable.ElementType);
            studentSetMock.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(() => studentQueryable.GetEnumerator());

            // Setup behavior of DbContext
            dbContextMock.Setup(m => m.EmailTemplates).Returns(emailTemplateMock.Object);
            dbContextMock.Setup(m => m.EmailSends).Returns(emailSendSetMock.Object);
            dbContextMock.Setup(m => m.EmailSendStudents).Returns(emailSendStudentSetMock.Object);
            dbContextMock.Setup(m => m.Users).Returns(userSetMock.Object);
            dbContextMock.Setup(m => m.Students).Returns(studentSetMock.Object);

            var controller = new EmailTemplateController(dbContextMock.Object);
            var createDto = new AddEmailTemplateDTO
            {
                Name = "Test Email Template",
                Description = "This is a test email template",
                Type = 1,
                ApplyTo = "tRaIner"
            };

            // Act
            var result = await controller.AddNew(createDto);

            // Assert
            var viewResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var model = Assert.IsAssignableFrom<AddEmailTemplateDTO>(viewResult.Value);
            Assert.Equal(createDto.ApplyTo.ToLower(), model.ApplyTo);
        }

        [Fact]
        public async Task AddNew_NullCreation_ReturnsBadRequest()
        {
            // Arrange
            var _mockDb = new Mock<FamsContext>();
            var _controller = new EmailTemplateController(_mockDb.Object);

            AddEmailTemplateDTO nullInput = null;
            // Act
            var result = await _controller.AddNew(nullInput); // Passing null to simulate system error

            // Assert
            // Ensure correct type of ActionResult returned
            var viewResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<string>(viewResult.Value);
            Assert.Equal("Invalid input!", model);

        }

        [Fact]
        public async Task AddNew_InvalidApplyToValue_ReturnsBadRequest()
        {
            // Arrange
            // Mock DbContext and DbSet
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<FamsContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase18")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            var dbContextMock = new Mock<FamsContext>(options);

            var emailTemplateList = new List<EmailTemplate>(); // Create an empty list to act as DbSet
            var emailTemplateQueryable = emailTemplateList.AsQueryable();
            var emailTemplateMock = new Mock<DbSet<EmailTemplate>>();
            emailTemplateMock.As<IAsyncEnumerable<EmailTemplate>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                                .Returns(new TestAsyncEnumerator<EmailTemplate>(emailTemplateQueryable.GetEnumerator()));
            emailTemplateMock.As<IQueryable<EmailTemplate>>().Setup(m => m.Provider)
                                .Returns(new TestAsyncQueryProvider<EmailTemplate>(emailTemplateQueryable.Provider));
            emailTemplateMock.As<IQueryable<EmailTemplate>>().Setup(m => m.Expression).Returns(emailTemplateQueryable.Expression);
            emailTemplateMock.As<IQueryable<EmailTemplate>>().Setup(m => m.ElementType).Returns(emailTemplateQueryable.ElementType);
            emailTemplateMock.As<IQueryable<EmailTemplate>>().Setup(m => m.GetEnumerator()).Returns(() => emailTemplateQueryable.GetEnumerator());

            var emailSendSetMock = new Mock<DbSet<EmailSend>>();
            var emailSendStudentSetMock = new Mock<DbSet<EmailSendStudent>>();
            var userSetMock = new Mock<DbSet<User>>();


            var studentList = new List<Student>(); // Create an empty list to act as DbSet
            var studentQueryable = studentList.AsQueryable();
            var studentSetMock = new Mock<DbSet<Student>>();
            studentSetMock.As<IAsyncEnumerable<Student>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                                .Returns(new TestAsyncEnumerator<Student>(studentQueryable.GetEnumerator()));
            studentSetMock.As<IQueryable<Student>>().Setup(m => m.Provider)
                                .Returns(new TestAsyncQueryProvider<Student>(studentQueryable.Provider));
            studentSetMock.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(studentQueryable.Expression);
            studentSetMock.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(studentQueryable.ElementType);
            studentSetMock.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(() => studentQueryable.GetEnumerator());

            // Setup behavior of DbContext
            dbContextMock.Setup(m => m.EmailTemplates).Returns(emailTemplateMock.Object);
            dbContextMock.Setup(m => m.EmailSends).Returns(emailSendSetMock.Object);
            dbContextMock.Setup(m => m.EmailSendStudents).Returns(emailSendStudentSetMock.Object);
            dbContextMock.Setup(m => m.Users).Returns(userSetMock.Object);
            dbContextMock.Setup(m => m.Students).Returns(studentSetMock.Object);

            var controller = new EmailTemplateController(dbContextMock.Object);
            var createDto = new AddEmailTemplateDTO
            {
                Name = "Test Email Template",
                Description = "This is a test email template",
                Type = 1,
                ApplyTo = "officer"
            };

            // Act
            var result = await controller.AddNew(createDto);

            // Assert
            var viewResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<string>(viewResult.Value);
            Assert.Equal("Invalid ApplyTo value! It must be 'student' or 'trainer'.", model);
        }

        [Fact]
        public async Task GetAll_WithValidInput_ReturnValidListType()
        {
            // Arrange
            var _mockDb = new Mock<FamsContext>();
            var _controller = new EmailTemplateController(_mockDb.Object);

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
                var emailTemplates = new List<EmailTemplate>
                {
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "Template3", Description = "Description1", Type = 1, IdStatus = 1, CreatedDate = DateTime.Now.AddDays(-2), UpdatedDate = DateTime.Now.AddDays(-2) },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "Template2", Description = "Description2", Type = 2, IdStatus = 2, CreatedDate = DateTime.Now.AddDays(-1), UpdatedDate = DateTime.Now.AddDays(-1) },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "Template1", Description = "Description3", Type = 3, IdStatus = 3, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                };

                await context.EmailTemplates.AddRangeAsync(emailTemplates);
                await context.SaveChangesAsync();
            }

            using (var context = new FamsContext(options))
            {
                var controller = new EmailTemplateController(context);

                // Act
                var result = await controller.GetAll("name", "asc");

                // Assert
                // Ensure correct type of ActionResult returned
                var viewResult = Assert.IsType<OkObjectResult>(result.Result);
                var model = Assert.IsAssignableFrom<IEnumerable<EmailTemplateDTO>>(viewResult.Value);
            }
        }

        [Fact]
        public async Task GetAll_WithNoInput_ReturnsEmptyList()
        {
            // Arrange
            var _mockDb = new Mock<FamsContext>();
            var _controller = new EmailTemplateController(_mockDb.Object);

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
                var emailTemplates = new List<EmailTemplate>();

                await context.EmailTemplates.AddRangeAsync(emailTemplates);
                await context.SaveChangesAsync();
            }

            using (var context = new FamsContext(options))
            {
                var controller = new EmailTemplateController(context);

                // Act
                var result = await controller.GetAll("name", "asc");

                // Assert
                var viewResult = Assert.IsType<OkObjectResult>(result.Result);
                var model = Assert.IsAssignableFrom<IEnumerable<EmailTemplateDTO>>(viewResult.Value);

                Assert.Equal(0, model.Count()); // Ensure correct number of records returned

                // Ensure actual list is empty 
                Assert.Empty(model);
            }
        }

        [Fact]
        public async Task GetAll_InputList_ReturnsCorrectInputtedList()
        {
            // Arrange
            var _mockDb = new Mock<FamsContext>();
            var _controller = new EmailTemplateController(_mockDb.Object);

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
                var emailTemplates = new List<EmailTemplate>
                {
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "Template1", Description = "Description1", Type = 1, IdStatus = 1, CreatedDate = DateTime.Now.AddDays(-2), UpdatedDate = DateTime.Now.AddDays(-2) },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "Template2", Description = "Description2", Type = 2, IdStatus = 2, CreatedDate = DateTime.Now.AddDays(-1), UpdatedDate = DateTime.Now.AddDays(-1) },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "Template3", Description = "Description3", Type = 3, IdStatus = 3, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                };

                await context.EmailTemplates.AddRangeAsync(emailTemplates);
                await context.SaveChangesAsync();
            }

            using (var context = new FamsContext(options))
            {
                var controller = new EmailTemplateController(context);

                // Act
                var result = await controller.GetAll("name", "asc");

                // Assert
                var viewResult = Assert.IsType<OkObjectResult>(result.Result);
                var model = Assert.IsAssignableFrom<IEnumerable<EmailTemplateDTO>>(viewResult.Value);

                Assert.Equal(3, model.Count()); // Ensure correct number of records returned

                // Ensure actual list matches expected list
                Assert.Equal("Template1", model.First().Name);
                Assert.Equal("Template2", model.ElementAtOrDefault(1).Name);
                Assert.Equal("Template3", model.Last().Name);
            }
        }

        [Fact]
        public async Task GetAll_SortAscByName_ReturnsCorrectAscSortedList()
        {
            // Arrange
            var _mockDb = new Mock<FamsContext>();
            var _controller = new EmailTemplateController(_mockDb.Object);

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
                var emailTemplates = new List<EmailTemplate>
                {
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "AAA", Description = "Description1", Type = 1, IdStatus = 1, CreatedDate = DateTime.Now.AddDays(-2), UpdatedDate = DateTime.Now.AddDays(-2) },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "EEE", Description = "Description2", Type = 2, IdStatus = 2, CreatedDate = DateTime.Now.AddDays(-1), UpdatedDate = DateTime.Now.AddDays(-1) },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "DDD", Description = "Description3", Type = 3, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "BBB", Description = "Description7", Type = 4, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "CCC", Description = "Description9", Type = 3, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "MMM", Description = "Description8", Type = 4, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "YYY", Description = "Description10", Type = 1, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "WWW", Description = "Description4", Type = 2, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "VVV", Description = "Description5", Type = 4, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "ZZZ", Description = "Description11", Type = 2, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "NNN", Description = "Description6", Type = 1, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                };

                await context.EmailTemplates.AddRangeAsync(emailTemplates);
                await context.SaveChangesAsync();
            }

            using (var context = new FamsContext(options))
            {
                var controller = new EmailTemplateController(context);

                // Act
                var result = await controller.GetAll("name", "asc");

                // Assert
                var viewResult = Assert.IsType<OkObjectResult>(result.Result);
                var model = Assert.IsAssignableFrom<IEnumerable<EmailTemplateDTO>>(viewResult.Value);

                Assert.Equal(11, model.Count()); // Ensure correct number of records returned

                // Ensure sorting ascending by name
                Assert.Equal("AAA", model.First().Name);
                Assert.Equal("DDD", model.ElementAtOrDefault(3).Name);
                Assert.Equal("WWW", model.ElementAtOrDefault(8).Name);
                Assert.Equal("ZZZ", model.Last().Name);
            }
        }

        [Fact]
        public async Task GetAll_SortDescByName_ReturnsCorrectDescSortedList()
        {
            // Arrange
            var _mockDb = new Mock<FamsContext>();
            var _controller = new EmailTemplateController(_mockDb.Object);

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
                var emailTemplates = new List<EmailTemplate>
                {
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "AAA", Description = "Description1", Type = 1, IdStatus = 1, CreatedDate = DateTime.Now.AddDays(-2), UpdatedDate = DateTime.Now.AddDays(-2) },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "EEE", Description = "Description2", Type = 2, IdStatus = 2, CreatedDate = DateTime.Now.AddDays(-1), UpdatedDate = DateTime.Now.AddDays(-1) },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "DDD", Description = "Description3", Type = 3, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "BBB", Description = "Description7", Type = 4, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "CCC", Description = "Description9", Type = 3, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "MMM", Description = "Description8", Type = 4, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "YYY", Description = "Description10", Type = 1, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "WWW", Description = "Description4", Type = 2, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "VVV", Description = "Description5", Type = 4, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "ZZZ", Description = "Description11", Type = 2, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "NNN", Description = "Description6", Type = 1, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                };

                await context.EmailTemplates.AddRangeAsync(emailTemplates);
                await context.SaveChangesAsync();
            }

            using (var context = new FamsContext(options))
            {
                var controller = new EmailTemplateController(context);

                // Act
                var result = await controller.GetAll("name", "desc");

                // Assert
                var viewResult = Assert.IsType<OkObjectResult>(result.Result);
                var model = Assert.IsAssignableFrom<IEnumerable<EmailTemplateDTO>>(viewResult.Value);

                Assert.Equal(11, model.Count()); // Ensure correct number of records returned

                // Ensure sorting descending by name
                Assert.Equal("ZZZ", model.First().Name);
                Assert.Equal("WWW", model.ElementAtOrDefault(2).Name);
                Assert.Equal("CCC", model.ElementAtOrDefault(8).Name);
                Assert.Equal("AAA", model.Last().Name);
            }
        }

        [Fact]
        public async Task GetAll_SortAscByDescription_ReturnsCorrectAscSortedList()
        {
            // Arrange
            var _mockDb = new Mock<FamsContext>();
            var _controller = new EmailTemplateController(_mockDb.Object);

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
                var emailTemplates = new List<EmailTemplate>
                {
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "AAA", Description = "Description1", Type = 1, IdStatus = 1, CreatedDate = DateTime.Now.AddDays(-2), UpdatedDate = DateTime.Now.AddDays(-2) },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "EEE", Description = "Description2", Type = 2, IdStatus = 2, CreatedDate = DateTime.Now.AddDays(-1), UpdatedDate = DateTime.Now.AddDays(-1) },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "DDD", Description = "Description3", Type = 3, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "BBB", Description = "Description7", Type = 4, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "CCC", Description = "Description9", Type = 3, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "MMM", Description = "Description8", Type = 4, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "YYY", Description = "Description10", Type = 1, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "WWW", Description = "Description4", Type = 2, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "VVV", Description = "Description5", Type = 4, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "ZZZ", Description = "Description11", Type = 2, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "NNN", Description = "Description6", Type = 1, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                };

                await context.EmailTemplates.AddRangeAsync(emailTemplates);
                await context.SaveChangesAsync();
            }

            using (var context = new FamsContext(options))
            {
                var controller = new EmailTemplateController(context);

                // Act
                var result = await controller.GetAll("description", "asc");

                // Assert
                var viewResult = Assert.IsType<OkObjectResult>(result.Result);
                var model = Assert.IsAssignableFrom<IEnumerable<EmailTemplateDTO>>(viewResult.Value);

                Assert.Equal(11, model.Count()); // Ensure correct number of records returned

                // Ensure sorting ascending by description
                Assert.Equal("Description1", model.First().Description);
                Assert.Equal("Description11", model.ElementAtOrDefault(2).Description);
                Assert.Equal("Description7", model.ElementAtOrDefault(8).Description);
                Assert.Equal("Description9", model.Last().Description);
            }
        }

        [Fact]
        public async Task GetAll_SortDescByDescription_ReturnsCorrectDescSortedList()
        {
            // Arrange
            var _mockDb = new Mock<FamsContext>();
            var _controller = new EmailTemplateController(_mockDb.Object);

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
                var emailTemplates = new List<EmailTemplate>
                {
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "AAA", Description = "Description1", Type = 1, IdStatus = 1, CreatedDate = DateTime.Now.AddDays(-2), UpdatedDate = DateTime.Now.AddDays(-2) },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "EEE", Description = "Description2", Type = 2, IdStatus = 2, CreatedDate = DateTime.Now.AddDays(-1), UpdatedDate = DateTime.Now.AddDays(-1) },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "DDD", Description = "Description3", Type = 3, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "BBB", Description = "Description7", Type = 4, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "CCC", Description = "Description9", Type = 3, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "MMM", Description = "Description8", Type = 4, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "YYY", Description = "Description10", Type = 1, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "WWW", Description = "Description4", Type = 2, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "VVV", Description = "Description5", Type = 4, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "ZZZ", Description = "Description11", Type = 2, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Name = "NNN", Description = "Description6", Type = 1, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                };

                await context.EmailTemplates.AddRangeAsync(emailTemplates);
                await context.SaveChangesAsync();
            }

            using (var context = new FamsContext(options))
            {
                var controller = new EmailTemplateController(context);

                // Act
                var result = await controller.GetAll("description", "desc");

                // Assert
                var viewResult = Assert.IsType<OkObjectResult>(result.Result);
                var model = Assert.IsAssignableFrom<IEnumerable<EmailTemplateDTO>>(viewResult.Value);

                Assert.Equal(11, model.Count()); // Ensure correct number of records returned

                // Ensure sorting descending by description
                Assert.Equal("Description9", model.First().Description);
                Assert.Equal("Description7", model.ElementAtOrDefault(2).Description);
                Assert.Equal("Description11", model.ElementAtOrDefault(8).Description);
                Assert.Equal("Description1", model.Last().Description);
            }
        }

        [Fact]
        public async Task GetAll_SortAscByType_ReturnsCorrectAscSortedList()
        {
            // Arrange
            var _mockDb = new Mock<FamsContext>();
            var _controller = new EmailTemplateController(_mockDb.Object);

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
                var emailTemplates = new List<EmailTemplate>
                {
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 1, Name = "AAA", Description = "Description1", Type = 1, IdStatus = 1, CreatedDate = DateTime.Now.AddDays(-2), UpdatedDate = DateTime.Now.AddDays(-2) },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 5, Name = "EEE", Description = "Description2", Type = 2, IdStatus = 2, CreatedDate = DateTime.Now.AddDays(-1), UpdatedDate = DateTime.Now.AddDays(-1) },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 7, Name = "DDD", Description = "Description3", Type = 3, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 9, Name = "BBB", Description = "Description7", Type = 4, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 8, Name = "CCC", Description = "Description9", Type = 3, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 11, Name = "MMM", Description = "Description8", Type = 4, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 3, Name = "YYY", Description = "Description10", Type = 1, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 6, Name = "WWW", Description = "Description4", Type = 2, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 10, Name = "VVV", Description = "Description5", Type = 4, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 4, Name = "ZZZ", Description = "Description11", Type = 2, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 2, Name = "NNN", Description = "Description6", Type = 1, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                };

                await context.EmailTemplates.AddRangeAsync(emailTemplates);
                await context.SaveChangesAsync();
            }

            using (var context = new FamsContext(options))
            {
                var controller = new EmailTemplateController(context);

                // Act
                var result = await controller.GetAll("type", "asc");

                // Assert
                var viewResult = Assert.IsType<OkObjectResult>(result.Result);
                var model = Assert.IsAssignableFrom<IEnumerable<EmailTemplateDTO>>(viewResult.Value);

                Assert.Equal(11, model.Count()); // Ensure correct number of records returned

                // Ensure sorting ascending by type
                Assert.Equal("Description1", model.First().Description);
                Assert.Equal("Description6", model.ElementAtOrDefault(2).Description);
                Assert.Equal("Description7", model.ElementAtOrDefault(8).Description);
                Assert.Equal("Description5", model.Last().Description);
            }
        }

        [Fact]
        public async Task GetAll_SortDescByType_ReturnsCorrectDescSortedList()
        {
            // Arrange
            var _mockDb = new Mock<FamsContext>();
            var _controller = new EmailTemplateController(_mockDb.Object);

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
                var emailTemplates = new List<EmailTemplate>
                {
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 1, Name = "AAA", Description = "Description1", Type = 1, IdStatus = 1, CreatedDate = DateTime.Now.AddDays(-2), UpdatedDate = DateTime.Now.AddDays(-2) },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 5, Name = "EEE", Description = "Description2", Type = 2, IdStatus = 2, CreatedDate = DateTime.Now.AddDays(-1), UpdatedDate = DateTime.Now.AddDays(-1) },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 7, Name = "DDD", Description = "Description3", Type = 3, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 9, Name = "BBB", Description = "Description7", Type = 4, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 8, Name = "CCC", Description = "Description9", Type = 3, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 11, Name = "MMM", Description = "Description8", Type = 4, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 3, Name = "YYY", Description = "Description10", Type = 1, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 6, Name = "WWW", Description = "Description4", Type = 2, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 10, Name = "VVV", Description = "Description5", Type = 4, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 4, Name = "ZZZ", Description = "Description11", Type = 2, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 2, Name = "NNN", Description = "Description6", Type = 1, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                };

                await context.EmailTemplates.AddRangeAsync(emailTemplates);
                await context.SaveChangesAsync();
            }

            using (var context = new FamsContext(options))
            {
                var controller = new EmailTemplateController(context);

                // Act
                var result = await controller.GetAll("type", "desc");

                // Assert
                var viewResult = Assert.IsType<OkObjectResult>(result.Result);
                var model = Assert.IsAssignableFrom<IEnumerable<EmailTemplateDTO>>(viewResult.Value);

                Assert.Equal(11, model.Count()); // Ensure correct number of records returned

                // Ensure sorting descending by type
                Assert.Equal("Description7", model.First().Description);
                Assert.Equal("Description5", model.ElementAtOrDefault(2).Description);
                Assert.Equal("Description1", model.ElementAtOrDefault(8).Description);
                Assert.Equal("Description6", model.Last().Description);
            }
        }

        [Fact]
        public async Task GetAll_FilterByOneType_ReturnsCorrectFilteredList()
        {
            // Arrange
            var _mockDb = new Mock<FamsContext>();
            var _controller = new EmailTemplateController(_mockDb.Object);

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
                var emailTemplates = new List<EmailTemplate>
                {
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 1, Name = "AAA", Description = "Description1", Type = 1, IdStatus = 1, CreatedDate = DateTime.Now.AddDays(-2), UpdatedDate = DateTime.Now.AddDays(-2) },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 5, Name = "EEE", Description = "Description2", Type = 2, IdStatus = 2, CreatedDate = DateTime.Now.AddDays(-1), UpdatedDate = DateTime.Now.AddDays(-1) },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 7, Name = "DDD", Description = "Description3", Type = 3, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },

                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 9, Name = "BBB", Description = "Description7", Type = 4, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 8, Name = "CCC", Description = "Description9", Type = 3, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 11, Name = "MMM", Description = "Description8", Type = 4, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },

                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 3, Name = "YYY", Description = "Description10", Type = 1, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 6, Name = "WWW", Description = "Description4", Type = 2, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 10, Name = "VVV", Description = "Description5", Type = 4, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },

                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 4, Name = "ZZZ", Description = "Description11", Type = 2, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 2, Name = "NNN", Description = "Description6", Type = 1, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                };

                await context.EmailTemplates.AddRangeAsync(emailTemplates);
                await context.SaveChangesAsync();
            }

            using (var context = new FamsContext(options))
            {
                var controller = new EmailTemplateController(context);

                // Act
                var result = await controller.GetAll("type", "asc", null, "1");

                // Assert
                var viewResult = Assert.IsType<OkObjectResult>(result.Result);
                var model = Assert.IsAssignableFrom<IEnumerable<EmailTemplateDTO>>(viewResult.Value);

                Assert.Equal(3, model.Count()); // Ensure correct number of records returned

                // Ensure filtered list returned
                Assert.Equal("Description1", model.First().Description);
                Assert.Equal("Description10", model.ElementAtOrDefault(1).Description);
                Assert.Equal("Description6", model.Last().Description);
            }
        }

        [Fact]
        public async Task GetAll_ReturnsEmptyListWhenFilter_IfNotFound()
        {
            // Arrange
            var _mockDb = new Mock<FamsContext>();
            var _controller = new EmailTemplateController(_mockDb.Object);

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
                var emailTemplates = new List<EmailTemplate>
                {
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 1, Name = "AAA", Description = "Description1", Type = 3, IdStatus = 1, CreatedDate = DateTime.Now.AddDays(-2), UpdatedDate = DateTime.Now.AddDays(-2) },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 5, Name = "EEE", Description = "Description2", Type = 2, IdStatus = 2, CreatedDate = DateTime.Now.AddDays(-1), UpdatedDate = DateTime.Now.AddDays(-1) },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 7, Name = "DDD", Description = "Description3", Type = 3, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },

                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 9, Name = "BBB", Description = "Description7", Type = 4, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 8, Name = "CCC", Description = "Description9", Type = 3, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 11, Name = "MMM", Description = "Description8", Type = 4, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },

                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 3, Name = "YYY", Description = "Description10", Type = 4, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 6, Name = "WWW", Description = "Description4", Type = 2, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 10, Name = "VVV", Description = "Description5", Type = 4, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },

                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 4, Name = "ZZZ", Description = "Description11", Type = 2, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 2, Name = "NNN", Description = "Description6", Type = 2, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                };

                await context.EmailTemplates.AddRangeAsync(emailTemplates);
                await context.SaveChangesAsync();
            }

            using (var context = new FamsContext(options))
            {
                var controller = new EmailTemplateController(context);

                // Act
                var result = await controller.GetAll("type", "asc", null, "1");

                // Assert
                var viewResult = Assert.IsType<OkObjectResult>(result.Result);
                var model = Assert.IsAssignableFrom<IEnumerable<EmailTemplateDTO>>(viewResult.Value);

                Assert.Equal(0, model.Count()); // Ensure correct number of records returned

                // Ensure empty list returned
                Assert.Empty(model);
            }
        }

        [Fact]
        public async Task GetAll_FilterByThreeType_ReturnsCorrectFilteredList()
        {
            // Arrange
            var _mockDb = new Mock<FamsContext>();
            var _controller = new EmailTemplateController(_mockDb.Object);

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
                var emailTemplates = new List<EmailTemplate>
                {
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 1, Name = "AAA", Description = "Description1", Type = 1, IdStatus = 1, CreatedDate = DateTime.Now.AddDays(-2), UpdatedDate = DateTime.Now.AddDays(-2) },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 5, Name = "EEE", Description = "Description2", Type = 2, IdStatus = 2, CreatedDate = DateTime.Now.AddDays(-1), UpdatedDate = DateTime.Now.AddDays(-1) },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 7, Name = "DDD", Description = "Description3", Type = 3, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },

                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 9, Name = "BBB", Description = "Description7", Type = 4, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 8, Name = "CCC", Description = "Description9", Type = 3, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 11, Name = "MMM", Description = "Description8", Type = 4, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },

                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 3, Name = "YYY", Description = "Description10", Type = 1, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 6, Name = "WWW", Description = "Description4", Type = 2, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 10, Name = "VVV", Description = "Description5", Type = 4, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },

                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 4, Name = "ZZZ", Description = "Description11", Type = 2, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 2, Name = "NNN", Description = "Description6", Type = 1, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                };

                await context.EmailTemplates.AddRangeAsync(emailTemplates);
                await context.SaveChangesAsync();
            }

            using (var context = new FamsContext(options))
            {
                var controller = new EmailTemplateController(context);

                // Act
                var result = await controller.GetAll("type", "asc", null, "1,2,4");

                // Assert
                var viewResult = Assert.IsType<OkObjectResult>(result.Result);
                var model = Assert.IsAssignableFrom<IEnumerable<EmailTemplateDTO>>(viewResult.Value);

                Assert.Equal(9, model.Count()); // Ensure correct number of records returned

                // Ensure filtered list returned
                Assert.True(model.All(x => x.IdType == 1 || x.IdType == 2 || x.IdType == 4));
            }
        }

        [Fact]
        public async Task GetAll_FilterByOneStatus_ReturnsCorrectFilteredList()
        {
            // Arrange
            var _mockDb = new Mock<FamsContext>();
            var _controller = new EmailTemplateController(_mockDb.Object);

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
                var emailTemplates = new List<EmailTemplate>
                {
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 1, Name = "AAA", Description = "Description1", Type = 1, IdStatus = 1, CreatedDate = DateTime.Now.AddDays(-2), UpdatedDate = DateTime.Now.AddDays(-2) },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 5, Name = "EEE", Description = "Description2", Type = 2, IdStatus = 2, CreatedDate = DateTime.Now.AddDays(-1), UpdatedDate = DateTime.Now.AddDays(-1) },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 7, Name = "DDD", Description = "Description3", Type = 3, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },

                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 9, Name = "BBB", Description = "Description7", Type = 4, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 8, Name = "CCC", Description = "Description9", Type = 3, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 11, Name = "MMM", Description = "Description8", Type = 4, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },

                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 3, Name = "YYY", Description = "Description10", Type = 1, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 6, Name = "WWW", Description = "Description4", Type = 2, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 10, Name = "VVV", Description = "Description5", Type = 4, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },

                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 4, Name = "ZZZ", Description = "Description11", Type = 2, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 2, Name = "NNN", Description = "Description6", Type = 1, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                };

                await context.EmailTemplates.AddRangeAsync(emailTemplates);
                await context.SaveChangesAsync();
            }

            using (var context = new FamsContext(options))
            {
                var controller = new EmailTemplateController(context);

                // Act
                var result = await controller.GetAll("status", "asc", "1");

                // Assert
                var viewResult = Assert.IsType<OkObjectResult>(result.Result);
                var model = Assert.IsAssignableFrom<IEnumerable<EmailTemplateDTO>>(viewResult.Value);

                Assert.Equal(6, model.Count()); // Ensure correct number of records returned

                // Ensure filtered list returned
                Assert.True(model.All(x => x.Status == 1));
            }
        }

        [Fact]
        public async Task GetAll_FilterByStatusAndType_ReturnsCorrectFilteredList()
        {
            // Arrange
            var _mockDb = new Mock<FamsContext>();
            var _controller = new EmailTemplateController(_mockDb.Object);

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
                var emailTemplates = new List<EmailTemplate>
                {
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 1, Name = "AAA", Description = "Description1", Type = 1, IdStatus = 1, CreatedDate = DateTime.Now.AddDays(-2), UpdatedDate = DateTime.Now.AddDays(-2) },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 5, Name = "EEE", Description = "Description2", Type = 2, IdStatus = 2, CreatedDate = DateTime.Now.AddDays(-1), UpdatedDate = DateTime.Now.AddDays(-1) },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 7, Name = "DDD", Description = "Description3", Type = 3, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },

                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 9, Name = "BBB", Description = "Description7", Type = 4, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 8, Name = "CCC", Description = "Description9", Type = 3, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 11, Name = "MMM", Description = "Description8", Type = 4, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },

                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 3, Name = "YYY", Description = "Description10", Type = 1, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 6, Name = "WWW", Description = "Description4", Type = 2, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 10, Name = "VVV", Description = "Description5", Type = 4, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },

                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 4, Name = "ZZZ", Description = "Description11", Type = 2, IdStatus = 2, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new EmailTemplate { EmailTemplateId = Guid.NewGuid().ToString(), Id = 2, Name = "NNN", Description = "Description6", Type = 1, IdStatus = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                };

                await context.EmailTemplates.AddRangeAsync(emailTemplates);
                await context.SaveChangesAsync();
            }

            using (var context = new FamsContext(options))
            {
                var controller = new EmailTemplateController(context);

                // Act
                var result = await controller.GetAll("name", "asc", "1", "1,4");

                // Assert
                var viewResult = Assert.IsType<OkObjectResult>(result.Result);
                var model = Assert.IsAssignableFrom<IEnumerable<EmailTemplateDTO>>(viewResult.Value);

                Assert.Equal(4, model.Count()); // Ensure correct number of records returned

                // Ensure filtered list returned
                Assert.True(model.All(x => x.Status == 1 && (x.IdType == 1 || x.IdType == 4)));
            }
        }

        [Fact]
        public async Task Edit_ValidId_ReturnsOkResult()
        {
            // Arrange
            var dbContextMock = new Mock<FamsContext>(new DbContextOptions<FamsContext>());
            var controller = new EmailTemplateController(dbContextMock.Object);

            var id = 1;
            var editDto = new EmailTemplateDTO
            {
                Name = "Updated Name",
                Description = "Updated Description",
                IdType = 2,
                Status = 1
            };

            var existingEntity = new EmailTemplate
            {
                Id = id,
                Name = "Original Name",
                Description = "Original Description",
                Type = 1,
                IdStatus = 2
            };

            dbContextMock.Setup(m => m.EmailTemplates.FindAsync(id)).ReturnsAsync(existingEntity);

            // Act
            var result = await controller.Edit(id, editDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var updatedDto = Assert.IsType<EmailTemplateDTO>(okResult.Value);

            Assert.Equal(editDto.Name, updatedDto.Name);
            Assert.Equal(editDto.Description, updatedDto.Description);
            Assert.Equal(editDto.Type, updatedDto.Type);
            Assert.Equal(editDto.Status, updatedDto.Status);
        }

        [Fact]
        public async Task Edit_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var dbContextMock = new Mock<FamsContext>(new DbContextOptions<FamsContext>());
            var controller = new EmailTemplateController(dbContextMock.Object);

            var id = 0;
            var editDto = new EmailTemplateDTO();

            // Act
            var result = await controller.Edit(id, editDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task Edit_EntityNotFound_ReturnsNotFound()
        {
            // Arrange
            var dbContextMock = new Mock<FamsContext>(new DbContextOptions<FamsContext>());
            var controller = new EmailTemplateController(dbContextMock.Object);

            var id = 1;
            var editDto = new EmailTemplateDTO();

            dbContextMock.Setup(m => m.EmailTemplates.FindAsync(id)).ReturnsAsync((EmailTemplate)null);

            // Act
            var result = await controller.Edit(id, editDto);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void GetById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            int id = 1;
            var mockDbContext = new Mock<FamsContext>();
            var mockEntity = new Mock<EmailTemplate>();

            mockDbContext.Setup(db => db.EmailTemplates.Find(id)).Returns(mockEntity.Object);

            var controller = new EmailTemplateController(mockDbContext.Object);

            // Act
            var result = controller.GetById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult);

            var dto = Assert.IsType<EmailTemplateDTO>(okResult.Value);
            Assert.NotNull(dto);
        }

        [Fact]
        public void GetById_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            int id = 1;
            var mockDbContext = new Mock<FamsContext>();

            mockDbContext.Setup(db => db.EmailTemplates.Find(id)).Returns((EmailTemplate)null);

            var controller = new EmailTemplateController(mockDbContext.Object);

            // Act
            var result = controller.GetById(id);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
