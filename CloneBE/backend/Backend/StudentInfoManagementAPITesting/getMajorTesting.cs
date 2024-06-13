using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Context;
using Moq;
using Xunit;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using StudentInfoManagementAPI.Service;
using Nest;
using StudentInfoManagementAPI.DTO;

namespace StudentInfoMangementAPITesting
{
    public class getMajorTesting
    {
        private readonly Mock<FamsContext> _dbContext;

        private readonly Mock<IElasticClient> _elasticClientMock;

        public getMajorTesting()
        {
            _dbContext = new Mock<FamsContext>();
            _elasticClientMock = new Mock<IElasticClient>();
        }



        [Fact]
        public void GetMajor_WhenMajorExists_ShouldReturnSuccessResponse()
        {
            // Arrange
            var mockDbSet = new Mock<DbSet<Major>>();
            var majorId = "M001";
            var expectedMajor = new Major { MajorId = majorId, Name = "Physical" };

            var data = new List<Major> { expectedMajor }.AsQueryable();

            mockDbSet.As<IQueryable<Major>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Major>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Major>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Major>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _dbContext.Setup(c => c.Majors).Returns(mockDbSet.Object);

            _dbContext.Setup(c => c.Majors)
                         .Returns(mockDbSet.Object);

            var service = new StudentService_QuyNDC(_dbContext.Object, _elasticClientMock.Object);
            var expectedResponse = new ResponseDTO
            {
                Result = expectedMajor,
                IsSuccess = true
            };

            // Act
            var response = service.GetMajor(majorId);

            var major = response.Result as IEnumerable<Major>;

            Major test = new Major();

            if (major != null)
            {
                test = major.ToList().First();
            }

            // Assert
            Assert.Equal(expectedResponse.Result, test);
            Assert.Equal(expectedResponse.IsSuccess, response.IsSuccess);
            Assert.Equal(expectedResponse.Message, response.Message);

        }


        [Fact]
        public void GetMajor_WhenExceptionThrown_ShouldReturnFailureResponseWithErrorMessage()
        {
            // Arrange
            var mockDbSet = new Mock<DbSet<Major>>();
            var majorId = "M001";
            var expectedMajor = new Major { MajorId = majorId, Name = "Physical" };

            _dbContext.Setup(c => c.Majors)
                         .Throws(new Exception("Database error"));

            var service = new StudentService_QuyNDC(_dbContext.Object, _elasticClientMock.Object);
            var expectedResponse = new ResponseDTO
            {
                Result = null,
                IsSuccess = false,
                Message = "Database error"
            };

            // Act
            var response = service.GetMajor(majorId);

            // Assert
            Assert.Equal(expectedResponse.Result, response.Result);
            Assert.Equal(expectedResponse.IsSuccess, response.IsSuccess);
            Assert.Equal(expectedResponse.Message, response.Message);
        }

        [Fact]
        public void GetMajor_WhenMajorNonExists_ShouldReturnEmptyResponse()
        {
            // Arrange
            var mockDbSet = new Mock<DbSet<Major>>();
            var majorId = "M001";
            var expectedMajor = new Major { MajorId = majorId, Name = "Physical" };

            var data = new List<Major> { expectedMajor }.AsQueryable();

            mockDbSet.As<IQueryable<Major>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Major>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Major>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Major>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _dbContext.Setup(c => c.Majors).Returns(mockDbSet.Object);

            _dbContext.Setup(c => c.Majors)
                         .Returns(mockDbSet.Object);

            var service = new StudentService_QuyNDC(_dbContext.Object, _elasticClientMock.Object);
            // Act
            var response = service.GetMajor("M002");

            // Assert
            Assert.Equal("", response.Result);
            Assert.True(response.IsSuccess);

        }
    }

}


