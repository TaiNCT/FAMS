using Moq;
using ReservationManagementAPI.Contracts;
using ReservationManagementAPI.Entities.DTOs;
using ReservationManagementAPI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationManagementAPITesting
{
    public class ReclassRepo
    {
        [Fact]
        public async Task InsertReservedClass_ShouldInsertSuccessfully()
        {
            // Arrange
            var repositoryMock = new Mock<IReservedClassRepository>();
            var expectedReservedClass = new ReservedClass
            {
                // Set properties of expected ReservedClass object
                // You may need to adjust these values based on your actual implementation
                StudentId = "exampleStudentId",
                ClassId = "exampleClassId",
                Reason = "exampleReason",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7) // Example end date 7 days from now
            };

            // Configure the mock to return the expected ReservedClass object when InsertReservedClass is called
            repositoryMock.Setup(repo => repo.InsertReservedClass(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>()))
                .ReturnsAsync(expectedReservedClass);

            var repository = repositoryMock.Object;

            // Act
            var result = await repository.InsertReservedClass(
                expectedReservedClass.StudentId,
                expectedReservedClass.ClassId,
                expectedReservedClass.Reason,
                expectedReservedClass.StartDate,
                expectedReservedClass.EndDate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedReservedClass.StudentId, result.StudentId);
            Assert.Equal(expectedReservedClass.ClassId, result.ClassId);
            Assert.Equal(expectedReservedClass.Reason, result.Reason);
            Assert.Equal(expectedReservedClass.StartDate, result.StartDate);
            Assert.Equal(expectedReservedClass.EndDate, result.EndDate);
        }
        [Fact]
        public async Task InsertReservedClass_WithInvalidInput_ShouldThrowException()
        {
            // Arrange
            var repositoryMock = new Mock<IReservedClassRepository>();
            repositoryMock.Setup(repo => repo.InsertReservedClass(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>()))
                .ThrowsAsync(new ArgumentException("Invalid input"));

            var repository = repositoryMock.Object;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await repository.InsertReservedClass("invalidStudentId", "invalidClassId", "invalidReason", DateTime.Now, DateTime.Now);
            });
        }
        [Fact]
        public async Task GetReservedClassByReservedClassId_ShouldReturnReservedClassDTO()
        {
            // Arrange
            var repositoryMock = new Mock<IReservedClassRepository>();
            var expectedReservedClassId = "exampleReservedClassId";
            var expectedReservedClassDTO = new ReservedClassDTO
            {
                // Set properties of expected ReservedClassDTO object
                // You may need to adjust these values based on your actual implementation
                StudentId = "exampleStudentId",
                ClassId = "exampleClassId",
                Reason = "exampleReason",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7) // Example end date 7 days from now
            };

            // Configure the mock to return the expected ReservedClassDTO object when GetReservedClassByReservedClassId is called
            repositoryMock.Setup(repo => repo.GetReservedClassByReservedClassId(expectedReservedClassId))
                .ReturnsAsync(expectedReservedClassDTO);

            var repository = repositoryMock.Object;

            // Act
            var result = await repository.GetReservedClassByReservedClassId(expectedReservedClassId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedReservedClassDTO.StudentId, result.StudentId);
            Assert.Equal(expectedReservedClassDTO.ClassId, result.ClassId);
            Assert.Equal(expectedReservedClassDTO.Reason, result.Reason);
            Assert.Equal(expectedReservedClassDTO.StartDate, result.StartDate);
            Assert.Equal(expectedReservedClassDTO.EndDate, result.EndDate);
        }
        [Fact]
        public async Task GetReservedClassByReservedClassId_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            var repositoryMock = new Mock<IReservedClassRepository>();
            string nonExistentReservedClassId = "nonExistentId";
            ReservedClassDTO nullReservedClassDTO = null;

            // Configure the mock to return null when GetReservedClassByReservedClassId is called with an invalid ID
            repositoryMock.Setup(repo => repo.GetReservedClassByReservedClassId(nonExistentReservedClassId))
                .ReturnsAsync(nullReservedClassDTO);

            var repository = repositoryMock.Object;

            // Act
            var result = await repository.GetReservedClassByReservedClassId(nonExistentReservedClassId);

            // Assert
            Assert.Null(result);
        }
    }
}
