using ClassManagementAPI.Controllers;
using ClassManagementAPI.Interface;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassManagementAPITesting.FSUContrrollerTest
{
    public class FSUControllerTest
    {
        [Fact]
        public async Task GetFSUList_ReturnsOkObjectResult_WithListOfFsu()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<FSUController>>();
            var fsuRepositoryMock = new Mock<IFSURepository>();
            var expectedFsuList = new List<Fsu>
        {
            new Fsu { Id = 1, FsuId = "FSU1", Name = "John Doe" },
            new Fsu { Id = 2, FsuId = "FSU2", Name = "Jane Doe" }
        };
            fsuRepositoryMock.Setup(repo => repo.GetAllFSU()).ReturnsAsync(expectedFsuList);
            var controller = new FSUController(loggerMock.Object, null, null, fsuRepositoryMock.Object);

            // Act
            var result = await controller.GetFSUList();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value;
            var itemsProperty = response.GetType().GetProperty("items");
            Assert.NotNull(itemsProperty);
            var items = itemsProperty.GetValue(response) as List<Fsu>;
            Assert.NotNull(items);

            Assert.Equal(expectedFsuList, items);
        }
    }
}
