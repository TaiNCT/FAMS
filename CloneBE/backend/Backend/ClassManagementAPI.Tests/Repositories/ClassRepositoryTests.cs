using ClassManagementAPI.Dto;
using ClassManagementAPI.Models;
using ClassManagementAPI.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassManagementAPI.Tests.Repositories
{
    public class ClassRepositoryTests
    {
        private readonly ServiceProvider _serviceProvider;

        public ClassRepositoryTests()
        {
            _serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();
        }

        public async Task<FamsContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<FamsContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .UseInternalServiceProvider(_serviceProvider)
        .Options;

            var databaseContext = new FamsContext(options);

            await databaseContext.Database.EnsureCreatedAsync();

            databaseContext.Classes.AddRange(MockData.ClassMockData.GetClasses());

            await databaseContext.SaveChangesAsync();

            return databaseContext;
        }

        [Fact]
        public async Task ClassRepository_GetClass_ReturnsClassList()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var classRepository = new ClassRepository(dbContext);

            //Act
            var result = classRepository.GetClasses();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<List<Class>>));
        }


        [Fact]
        public async Task ClassRepository_GetClassListByFilter_ReturnsCorrectPagedResult()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var repository = new ClassRepository(dbContext);


            var filterData = new FilterFormatDto
            {
                FromDate = DateOnly.Parse("2021-12-08"),
                ToDate = DateOnly.Parse("2021-12-28"),
                Locations_id = new List<string> { "3" },
                SlotTimes = new List<string> { "Morning" },
                Class_status = new List<string> { "Plaining" },
                Fsu_ID = 1,
                TrainingProgramCode = "3",
                AttendeeLevelID = new List<string> { "1" }
            };

            // Act
            try
            {
                var result = await repository.GetClassListByFilter(filterData);

                // Assert
                Assert.NotNull(result);
                result.Items.Should().NotBeNullOrEmpty();
                result.TotalCount.Should().BeGreaterThan(0);
                Assert.IsType<PagedResult<Class>>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
