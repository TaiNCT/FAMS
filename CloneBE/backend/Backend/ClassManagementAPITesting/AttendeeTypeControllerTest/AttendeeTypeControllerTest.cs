using AutoMapper;
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

namespace ClassManagementAPITesting.AttendeeTypeControllerTest
{
    public class AttendeeTypeControllerTest
    {
        [Fact]
        public async Task GetAttendeeTypeList_Returns_OkResult_With_AttendeeTypes()
        {
            // Arrange
            var attendeeTypes = new List<AttendeeType>
            {
                new AttendeeType { Id = 1, AttendeeTypeName = "Type 1", Description = "Description for Type 1" },
                new AttendeeType { Id = 2, AttendeeTypeName = "Type 2", Description = "Description for Type 2" }
            };

            var attendeeTypeRepositoryMock = new Mock<IAttendeeTypeRepository>();
            attendeeTypeRepositoryMock.Setup(repo => repo.GetAllAttendeeTypeList()).ReturnsAsync(attendeeTypes);

            var loggerMock = new Mock<ILogger<AttendeeTypeController>>();
            var mapperMock = new Mock<IMapper>();

            // Instantiate 
            var controller = new AttendeeTypeController(loggerMock.Object, null, mapperMock.Object, attendeeTypeRepositoryMock.Object);

            // Act
            var result = await controller.GetAttendeeTypeList();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value; 

            Assert.NotNull(response); 
            var responseProperties = response.GetType().GetProperties();
            var attendeeTypesResult = responseProperties.FirstOrDefault(p => p.PropertyType == typeof(List<AttendeeType>))?.GetValue(response) as List<AttendeeType>;

            Assert.NotNull(attendeeTypesResult); 

            Assert.Equal(attendeeTypes.Count, attendeeTypesResult.Count);
            Assert.Equal(attendeeTypes[0].AttendeeTypeName, attendeeTypesResult[0].AttendeeTypeName);
            Assert.Equal(attendeeTypes[1].AttendeeTypeName, attendeeTypesResult[1].AttendeeTypeName);
        }
    }
}
