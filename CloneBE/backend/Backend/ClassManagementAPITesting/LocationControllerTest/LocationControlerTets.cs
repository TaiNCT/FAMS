using ClassManagementAPI.Interface;
using Entities.Models;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ClassManagementAPITesting.LocationControllerTest
{
    public class LocationControlerTets
    {
        [Fact]
        public async Task GetAllLocation_ReturnsLocations()
        {
            // Arrange
            var mockRepository = new Mock<ILocationRepository>();
            var expectedLocations = new List<Location>
            {
                new Location { Id = 1, LocationId = "loc1", Address = "123 Main St", Name = "Location 1" },
                new Location { Id = 2, LocationId = "loc2", Address = "456 Elm St", Name = "Location 2" }
            };
            mockRepository.Setup(repo => repo.GetAllLocation()).ReturnsAsync(expectedLocations);

            // Act
            var result = await mockRepository.Object.GetAllLocation();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedLocations.Count, result.Count);
            // Additional assertions based on the specific logic of GetAllLocation method can be added
        }

        [Fact]
        public async Task GetLocationById_ReturnsLocation()
        {
            // Arrange
            var mockRepository = new Mock<ILocationRepository>();
            var expectedLocation = new Location { Id = 1, LocationId = "loc1", Address = "123 Main St", Name = "Location 1" };
            string locationId = "loc1";
            mockRepository.Setup(repo => repo.GetLocationById(locationId)).ReturnsAsync(expectedLocation);

            // Act
            var result = await mockRepository.Object.GetLocationById(locationId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedLocation.Id, result.Id);
            Assert.Equal(expectedLocation.LocationId, result.LocationId);
            // Additional assertions based on the specific logic of GetLocationById method can be added
        }
    }
}
