using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Castle.Components.DictionaryAdapter.Xml;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.EntityFrameworkCore;
using Moq;
using OJTStudentManagement;
using StudentInfoManagementAPI.DTO;
using Entities.Models;
using StudentInfoManagementAPI.Service;
using StudentInfoManagementAPITesting.Data;
using Xunit;
using Entities.Context;
using Microsoft.IdentityModel.Tokens;
using Nest;

namespace StudentInfoManagementAPITesting
{
    public class ViewFunctionTesting
    {

        private readonly Mock<FamsContext> _dbContextMock;
        private readonly Mock<IElasticClient> _elasticClientMock;
        private readonly IMapper _mapper;

        public ViewFunctionTesting()
        {
            _dbContextMock = new Mock<FamsContext>();
            _mapper = ConfigMapper.ConfigureAutoMapper();
            _elasticClientMock = new Mock<IElasticClient>();
        }

        [Fact]
        public void GetListStudentInClass_FilterByGender_ReturnsFilteredStudentListDTO()
        {
            // Arrange
            var data = new GetListStudentData();
            var testStudents = data.studentDTOs();
            string filter = "gender";
            string field = "Male";

            List<StudentDTO> documents = GetExpectedStudents(data.studentDTOs(), null ,null ,field , null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }


            var mockDbSet = new Mock<DbSet<Student>>();
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Provider).Returns(testStudents.AsQueryable().Provider);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Expression).Returns(testStudents.AsQueryable().Expression);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.ElementType).Returns(testStudents.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.GetEnumerator()).Returns(testStudents.AsQueryable().GetEnumerator());
            _dbContextMock.Setup(m => m.Students).Returns(mockDbSet.Object);


            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            // Act
            var service = new StudentService_QuyNDC(_dbContextMock.Object, mockElasticClient.Object);

            var result = service.GetListStudent(null, null, field, null, "fullname", null,1, 10);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Result.IsSuccess);
            Assert.NotNull(result.Result);
            PaginatedStudentListDTO r = result.Result.Result as PaginatedStudentListDTO;
            List<string> actualStudents = r.Students.Select(s => s.StudentInfoDTO.StudentId).ToList();
            List<string> expectedStudents = documents.Select(s => s.StudentInfoDTO.StudentId).ToList();

            int count = 0;
            foreach (var e in expectedStudents)
            {
                foreach (var a in actualStudents) if (a.Equals(e)) count++;
            }

            Assert.Equal(2, count);
            
        }

        [Fact]
        public void GetListStudentInClass_FilterByStatus_ReturnsFilteredStudentListDTO()
        {
            // Arrange
            var data = new GetListStudentData();
            var testStudents = data.studentDTOs();
            string filter = "status";
            string field = "Inactive";

            List<StudentDTO> documents = GetExpectedStudents(data.studentDTOs(), null ,null ,null , field);


            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }


            var mockDbSet = new Mock<DbSet<Student>>();
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Provider).Returns(testStudents.AsQueryable().Provider);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Expression).Returns(testStudents.AsQueryable().Expression);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.ElementType).Returns(testStudents.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.GetEnumerator()).Returns(testStudents.AsQueryable().GetEnumerator());
            _dbContextMock.Setup(m => m.Students).Returns(mockDbSet.Object);


            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            // Act
            var service = new StudentService_QuyNDC(_dbContextMock.Object, mockElasticClient.Object);
            var result = service.GetListStudent(null, null, null, field, "fullname", null, 1, 10);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Result.IsSuccess);
            Assert.NotNull(result.Result.Result);
            PaginatedStudentListDTO r = result.Result.Result as PaginatedStudentListDTO;
            
            List<string> actualStudents = r.Students.Select(s => s.StudentInfoDTO.StudentId).ToList();
            List<string> expectedStudents = documents.Select(s => s.StudentInfoDTO.StudentId).ToList();

            int count = 0;
            foreach (var e in expectedStudents)
            {
                foreach (var a in actualStudents) if (a.Equals(e)) count++;
            }

            Assert.Equal(1, count);
        }


        [Fact]
        public void GetListStudentInClass_FilterByDob_ReturnsFilteredStudentListDTO()
        {
            // Arrange
            var data = new GetListStudentData();
            var testStudents = data.studentDTOs();
            string filter = "dob";
            DateTime field = new DateTime(1990,1,1);
            string s = "1990-1-1";

            List<StudentDTO> documents = GetExpectedStudents(data.studentDTOs(), null ,field ,null , null);


            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }


            var mockDbSet = new Mock<DbSet<Student>>();
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Provider).Returns(testStudents.AsQueryable().Provider);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Expression).Returns(testStudents.AsQueryable().Expression);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.ElementType).Returns(testStudents.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.GetEnumerator()).Returns(testStudents.AsQueryable().GetEnumerator());
            _dbContextMock.Setup(m => m.Students).Returns(mockDbSet.Object);


            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            // Act
            var service = new StudentService_QuyNDC(_dbContextMock.Object, mockElasticClient.Object);
            var result = service.GetListStudent(null, s, null, null, "fullname", null,1, 10);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Result.IsSuccess);
            Assert.NotNull(result.Result.Result);
            PaginatedStudentListDTO r = result.Result.Result as PaginatedStudentListDTO;
            
            List<string> actualStudents = r.Students.Select(s => s.StudentInfoDTO.StudentId).ToList();
            List<string> expectedStudents = documents.Select(s => s.StudentInfoDTO.StudentId).ToList();

            int count = 0;
            foreach (var e in expectedStudents)
            {
                foreach (var a in actualStudents) if (a.Equals(e)) count++;
            }

            Assert.Equal(1, count);
        }

        [Fact]
        public void GetListStudentInClass_FilterByKeywordName_ReturnsSortedStudentListDTO()
        {
            // Arrange
            var data = new GetListStudentData();
            var testStudents = data.studentDTOs();
            string filter = "keyword";
            string field = "John";

            List<StudentDTO> documents = GetExpectedStudents(data.studentDTOs(), field, null, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }


            var mockDbSet = new Mock<DbSet<Student>>();
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Provider).Returns(testStudents.AsQueryable().Provider);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Expression).Returns(testStudents.AsQueryable().Expression);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.ElementType).Returns(testStudents.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.GetEnumerator()).Returns(testStudents.AsQueryable().GetEnumerator());
            _dbContextMock.Setup(m => m.Students).Returns(mockDbSet.Object);


            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            // Act
            var service = new StudentService_QuyNDC(_dbContextMock.Object, mockElasticClient.Object);
            var result = service.GetListStudent(field, null, null, null, "fullname", null,1, 10);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Result.IsSuccess);
            Assert.NotNull(result.Result.Result);
            PaginatedStudentListDTO r = result.Result.Result as PaginatedStudentListDTO;

            List<string> actualStudent = r.Students.Select(s => s.StudentInfoDTO.FullName).ToList();
            Assert.Equal(2, r.TotalCount);
            Assert.Equal("Michael Johnson", actualStudent[0]);
            Assert.Equal("John Doe", actualStudent[1]);
        }

        [Fact]
        public void GetListStudentInClass_FilterByKeywordEmail_ReturnsSortedStudentListDTO()
        {
            // Arrange
            var data = new GetListStudentData();
            var testStudents = data.studentDTOs();
            string filter = "keyword";
            string field = "@";

            List<StudentDTO> documents = GetExpectedStudents(data.studentDTOs(), field, null, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }


            var mockDbSet = new Mock<DbSet<Student>>();
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Provider).Returns(testStudents.AsQueryable().Provider);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Expression).Returns(testStudents.AsQueryable().Expression);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.ElementType).Returns(testStudents.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.GetEnumerator()).Returns(testStudents.AsQueryable().GetEnumerator());
            _dbContextMock.Setup(m => m.Students).Returns(mockDbSet.Object);


            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            // Act
            var service = new StudentService_QuyNDC(_dbContextMock.Object, mockElasticClient.Object);
            var result = service.GetListStudent(field, null, null, null, "fullname", null,1, 10);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Result.IsSuccess);
            Assert.NotNull(result.Result.Result);
            PaginatedStudentListDTO r = result.Result.Result as PaginatedStudentListDTO;

            List<string> actualStudent = r.Students.Select(s => s.StudentInfoDTO.FullName).ToList();
            Assert.Equal(3, r.TotalCount);
            Assert.Equal("Michael Johnson", actualStudent[0]);
            Assert.Equal("John Doe", actualStudent[1]);
            Assert.Equal("Jane Smith", actualStudent[2]);
        }
        
        [Fact]
        public void GetListStudentInClass_SortByFullName_ReturnsSortedStudentListDTO()
        {
            // Arrange
            var data = new GetListStudentData();
            var testStudents = data.studentDTOs();
            string filter = "keyword";
            string field = "";

            List<StudentDTO> documents = GetExpectedStudents(data.studentDTOs(), field, null, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }


            var mockDbSet = new Mock<DbSet<Student>>();
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Provider).Returns(testStudents.AsQueryable().Provider);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Expression).Returns(testStudents.AsQueryable().Expression);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.ElementType).Returns(testStudents.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.GetEnumerator()).Returns(testStudents.AsQueryable().GetEnumerator());
            _dbContextMock.Setup(m => m.Students).Returns(mockDbSet.Object);


            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            // Act
            var service = new StudentService_QuyNDC(_dbContextMock.Object, mockElasticClient.Object);
            var result = service.GetListStudent(field, null, null, null, "fullname", null, 1, 10);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Result.IsSuccess);
            Assert.NotNull(result.Result.Result);
            PaginatedStudentListDTO r = result.Result.Result as PaginatedStudentListDTO;

            List<string> actualStudent = r.Students.Select(s => s.StudentInfoDTO.FullName).ToList();
            Assert.Equal(3, r.TotalCount);
            Assert.Equal("Michael Johnson", actualStudent[0]);
            Assert.Equal("John Doe", actualStudent[1]);
            Assert.Equal("Jane Smith", actualStudent[2]);
        }

        [Fact]
        public void GetListStudentInClass_SortByGender_ReturnsSortedStudentListDTO()
        {
            // Arrange
            var data = new GetListStudentData();
            var testStudents = data.studentDTOs();
            string filter = "keyword";
            string field = "";

            List<StudentDTO> documents = GetExpectedStudents(data.studentDTOs(), field, null, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }


            var mockDbSet = new Mock<DbSet<Student>>();
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Provider).Returns(testStudents.AsQueryable().Provider);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Expression).Returns(testStudents.AsQueryable().Expression);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.ElementType).Returns(testStudents.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.GetEnumerator()).Returns(testStudents.AsQueryable().GetEnumerator());
            _dbContextMock.Setup(m => m.Students).Returns(mockDbSet.Object);


            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            // Act
            var service = new StudentService_QuyNDC(_dbContextMock.Object, mockElasticClient.Object);
            var result = service.GetListStudent(field, null, null, null, "gender", null, 1, 10);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Result.IsSuccess);
            Assert.NotNull(result.Result.Result);
            PaginatedStudentListDTO r = result.Result.Result as PaginatedStudentListDTO;

            List<string> actualStudent = r.Students.Select(s => s.StudentInfoDTO.FullName).ToList();
            Assert.Equal(3, r.TotalCount);
            Assert.Equal("John Doe", actualStudent[0]);
            Assert.Equal("Michael Johnson", actualStudent[1]);
            Assert.Equal("Jane Smith", actualStudent[2]);
        }

        [Fact]
        public void GetListStudentInClass_SortByPhone_ReturnsSortedPaginatedStudentListDTO()
        {
            // Arrange
            var data = new GetListStudentData();
            var testStudents = data.studentDTOs();
            string filter = "keyword";
            string field = "";

            List<StudentDTO> documents = GetExpectedStudents(data.studentDTOs(), field, null, null, null);


            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }


            var mockDbSet = new Mock<DbSet<Student>>();
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Provider).Returns(testStudents.AsQueryable().Provider);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Expression).Returns(testStudents.AsQueryable().Expression);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.ElementType).Returns(testStudents.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.GetEnumerator()).Returns(testStudents.AsQueryable().GetEnumerator());
            _dbContextMock.Setup(m => m.Students).Returns(mockDbSet.Object);


            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            // Act
            var service = new StudentService_QuyNDC(_dbContextMock.Object, mockElasticClient.Object);
            var result = service.GetListStudent(field, null, null, null, "phone", null, 1, 10);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Result.IsSuccess);
            Assert.NotNull(result.Result.Result);
            PaginatedStudentListDTO r = result.Result.Result as PaginatedStudentListDTO;

            List<string> actualStudent = r.Students.Select(s => s.StudentInfoDTO.FullName).ToList();
            Assert.Equal(3, r.TotalCount);
            Assert.Equal("Jane Smith", actualStudent[0]);
            Assert.Equal("Michael Johnson", actualStudent[1]);
            Assert.Equal("John Doe", actualStudent[2]);
            
        }

        [Fact]
        public void GetListStudentInClass_SortByEmail_ReturnsSortedPaginatedStudentListDTO()
        {
            // Arrange
            var data = new GetListStudentData();
            var testStudents = data.studentDTOs();
            string filter = "keyword";
            string field = "";

            List<StudentDTO> documents = GetExpectedStudents(data.studentDTOs(), field, null, null, null);


            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }


            var mockDbSet = new Mock<DbSet<Student>>();
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Provider).Returns(testStudents.AsQueryable().Provider);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Expression).Returns(testStudents.AsQueryable().Expression);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.ElementType).Returns(testStudents.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.GetEnumerator()).Returns(testStudents.AsQueryable().GetEnumerator());
            _dbContextMock.Setup(m => m.Students).Returns(mockDbSet.Object);


            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            // Act
            var service = new StudentService_QuyNDC(_dbContextMock.Object, mockElasticClient.Object);
            var result = service.GetListStudent(field, null, null, field, "email", null, 1, 10);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Result.IsSuccess);
            Assert.NotNull(result.Result.Result);
            PaginatedStudentListDTO r = result.Result.Result as PaginatedStudentListDTO;

            List<string> actualStudent = r.Students.Select(s => s.StudentInfoDTO.FullName).ToList();
            Assert.Equal(3, r.TotalCount);
            Assert.Equal("Michael Johnson", actualStudent[0]);
            Assert.Equal("John Doe", actualStudent[1]);
            Assert.Equal("Jane Smith", actualStudent[2]);
            
        }

        [Fact]
        public void GetListStudentInClass_SortByUniversity_ReturnsSortedPaginatedStudentListDTO()
        {
            // Arrange
            var data = new GetListStudentData();
            var testStudents = data.studentDTOs();
            string filter = "keyword";
            string field = "";

            List<StudentDTO> documents = GetExpectedStudents(data.studentDTOs(), field, null, null, null);


            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }


            var mockDbSet = new Mock<DbSet<Student>>();
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Provider).Returns(testStudents.AsQueryable().Provider);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Expression).Returns(testStudents.AsQueryable().Expression);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.ElementType).Returns(testStudents.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.GetEnumerator()).Returns(testStudents.AsQueryable().GetEnumerator());
            _dbContextMock.Setup(m => m.Students).Returns(mockDbSet.Object);


            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            // Act
            var service = new StudentService_QuyNDC(_dbContextMock.Object, mockElasticClient.Object);
            var result = service.GetListStudent(field, null, null, null, "university", null, 1, 10);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Result.IsSuccess);
            Assert.NotNull(result.Result.Result);
            PaginatedStudentListDTO r = result.Result.Result as PaginatedStudentListDTO;

            List<string> actualStudent = r.Students.Select(s => s.StudentInfoDTO.FullName).ToList();
            Assert.Equal(3, r.TotalCount);
            Assert.Equal("Jane Smith", actualStudent[0]);
            Assert.Equal("Michael Johnson", actualStudent[1]);
            Assert.Equal("John Doe", actualStudent[2]);
            
        }

        [Fact]
        public void GetListStudentInClass_SortByMajor_ReturnsSortedPaginatedStudentListDTO()
        {
            // Arrange
            var data = new GetListStudentData();
            var testStudents = data.studentDTOs();
            string filter = "keyword";
            string field = "";

            List<StudentDTO> documents = GetExpectedStudents(data.studentDTOs(), field, null, null, null);


            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }


            var mockDbSet = new Mock<DbSet<Student>>();
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Provider).Returns(testStudents.AsQueryable().Provider);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Expression).Returns(testStudents.AsQueryable().Expression);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.ElementType).Returns(testStudents.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.GetEnumerator()).Returns(testStudents.AsQueryable().GetEnumerator());
            _dbContextMock.Setup(m => m.Students).Returns(mockDbSet.Object);


            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            // Act
            var service = new StudentService_QuyNDC(_dbContextMock.Object, mockElasticClient.Object);
            var result = service.GetListStudent(field, null, null, null, "university", null, 1, 10);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Result.IsSuccess);
            Assert.NotNull(result.Result.Result);
            PaginatedStudentListDTO r = result.Result.Result as PaginatedStudentListDTO;

            List<string> actualStudent = r.Students.Select(s => s.StudentInfoDTO.FullName).ToList();
            Assert.Equal(3, r.TotalCount);
            Assert.Equal("Jane Smith", actualStudent[0]);
            Assert.Equal("Michael Johnson", actualStudent[1]);
            Assert.Equal("John Doe", actualStudent[2]);
            
        }

        [Fact]
        public void GetListStudentInClass_SortByGraduationTime_ReturnsSortedPaginatedStudentListDTO()
        {
            // Arrange
            var data = new GetListStudentData();
            var testStudents = data.studentDTOs();
            string filter = "keyword";
            string field = "";

            List<StudentDTO> documents = GetExpectedStudents(data.studentDTOs(), field, null, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }


            var mockDbSet = new Mock<DbSet<Student>>();
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Provider).Returns(testStudents.AsQueryable().Provider);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Expression).Returns(testStudents.AsQueryable().Expression);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.ElementType).Returns(testStudents.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.GetEnumerator()).Returns(testStudents.AsQueryable().GetEnumerator());
            _dbContextMock.Setup(m => m.Students).Returns(mockDbSet.Object);


            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            // Act
            var service = new StudentService_QuyNDC(_dbContextMock.Object, mockElasticClient.Object);
            var result = service.GetListStudent(field, null, null, null, "graduationtime", null, 1, 10);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Result.IsSuccess);
            Assert.NotNull(result.Result.Result);
            PaginatedStudentListDTO r = result.Result.Result as PaginatedStudentListDTO;

            List<string> actualStudent = r.Students.Select(s => s.StudentInfoDTO.FullName).ToList();
            Assert.Equal(3, r.TotalCount);
            Assert.Equal("Jane Smith", actualStudent[0]);
            Assert.Equal("John Doe", actualStudent[1]);
            Assert.Equal("Michael Johnson", actualStudent[2]);
            
        }

        [Fact]
        public void GetListStudentInClass_SortByGPA_ReturnsSortedPaginatedStudentListDTO()
        {
            // Arrange
            var data = new GetListStudentData();
            var testStudents = data.studentDTOs();
            string filter = "keyword";
            string field = "";

            List<StudentDTO> documents = GetExpectedStudents(data.studentDTOs(), field, null, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }


            var mockDbSet = new Mock<DbSet<Student>>();
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Provider).Returns(testStudents.AsQueryable().Provider);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Expression).Returns(testStudents.AsQueryable().Expression);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.ElementType).Returns(testStudents.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.GetEnumerator()).Returns(testStudents.AsQueryable().GetEnumerator());
            _dbContextMock.Setup(m => m.Students).Returns(mockDbSet.Object);


            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            // Act
            var service = new StudentService_QuyNDC(_dbContextMock.Object, mockElasticClient.Object);
            var result = service.GetListStudent(field, null, null, null, "gpa", null, 1, 10);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Result.IsSuccess);
            Assert.NotNull(result.Result.Result);
            PaginatedStudentListDTO r = result.Result.Result as PaginatedStudentListDTO;

            List<string> actualStudent = r.Students.Select(s => s.StudentInfoDTO.FullName).ToList();
            Assert.Equal(3, r.TotalCount);
            Assert.Equal("Jane Smith", actualStudent[0]);
            Assert.Equal("John Doe", actualStudent[1]);
            Assert.Equal("Michael Johnson", actualStudent[2]);
            
        }

        [Fact]
        public void GetListStudentInClass_SortByAddress_ReturnsSortedPaginatedStudentListDTO()
        {
            // Arrange
            var data = new GetListStudentData();
            var testStudents = data.studentDTOs();
            string filter = "keyword";
            string field = "";

            List<StudentDTO> documents = GetExpectedStudents(data.studentDTOs(), field, null, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }


            var mockDbSet = new Mock<DbSet<Student>>();
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Provider).Returns(testStudents.AsQueryable().Provider);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Expression).Returns(testStudents.AsQueryable().Expression);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.ElementType).Returns(testStudents.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.GetEnumerator()).Returns(testStudents.AsQueryable().GetEnumerator());
            _dbContextMock.Setup(m => m.Students).Returns(mockDbSet.Object);


            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            // Act
            var service = new StudentService_QuyNDC(_dbContextMock.Object, mockElasticClient.Object);
            var result = service.GetListStudent(field, null, null, null, "address", null, 1, 10);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Result.IsSuccess);
            Assert.NotNull(result.Result.Result);
            PaginatedStudentListDTO r = result.Result.Result as PaginatedStudentListDTO;

            List<string> actualStudent = r.Students.Select(s => s.StudentInfoDTO.FullName).ToList();
            Assert.Equal(3, r.TotalCount);
            Assert.Equal("Michael Johnson", actualStudent[0]);
            Assert.Equal("Jane Smith", actualStudent[1]);
            Assert.Equal("John Doe", actualStudent[2]);
            
        }

        [Fact]
        public void GetListStudentInClass_SortByRecer_ReturnsSortedPaginatedStudentListDTO()
        {
            // Arrange
            var data = new GetListStudentData();
            var testStudents = data.studentDTOs();
            string filter = "keyword";
            string field = "";

            List<StudentDTO> documents = GetExpectedStudents(data.studentDTOs(), field, null, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }


            var mockDbSet = new Mock<DbSet<Student>>();
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Provider).Returns(testStudents.AsQueryable().Provider);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Expression).Returns(testStudents.AsQueryable().Expression);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.ElementType).Returns(testStudents.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.GetEnumerator()).Returns(testStudents.AsQueryable().GetEnumerator());
            _dbContextMock.Setup(m => m.Students).Returns(mockDbSet.Object);


            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            // Act
            var service = new StudentService_QuyNDC(_dbContextMock.Object, mockElasticClient.Object);
            var result = service.GetListStudent(field, null, null, null, "recer", null, 1, 10);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Result.IsSuccess);
            Assert.NotNull(result.Result.Result);
            PaginatedStudentListDTO r = result.Result.Result as PaginatedStudentListDTO;

            List<string> actualStudent = r.Students.Select(s => s.StudentInfoDTO.FullName).ToList();
            Assert.Equal(3, r.TotalCount);
            Assert.Equal("Jane Smith", actualStudent[0]);
            Assert.Equal("John Doe", actualStudent[1]);
            Assert.Equal("Michael Johnson", actualStudent[2]);
            
        }

        [Fact]
        public void GetListStudentInClass_SortByStatus_ReturnsSortedPaginatedStudentListDTO()
        {
            // Arrange
            var data = new GetListStudentData();
            var testStudents = data.studentDTOs();
            string filter = "keyword";
            string field = "";

            List<StudentDTO> documents = GetExpectedStudents(data.studentDTOs(), field, null, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }


            var mockDbSet = new Mock<DbSet<Student>>();
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Provider).Returns(testStudents.AsQueryable().Provider);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.Expression).Returns(testStudents.AsQueryable().Expression);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.ElementType).Returns(testStudents.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<StudentDTO>>().Setup(m => m.GetEnumerator()).Returns(testStudents.AsQueryable().GetEnumerator());
            _dbContextMock.Setup(m => m.Students).Returns(mockDbSet.Object);


            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            // Act
            var service = new StudentService_QuyNDC(_dbContextMock.Object, mockElasticClient.Object);
            var result = service.GetListStudent("", null, null, null, "status", null, 1, 10);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Result.IsSuccess);
            Assert.NotNull(result.Result.Result);
            PaginatedStudentListDTO r = result.Result.Result as PaginatedStudentListDTO;

            List<string> actualStudent = r.Students.Select(s => s.StudentInfoDTO.FullName).ToList();
            Assert.Equal(3, r.TotalCount);
            Assert.Equal("Michael Johnson", actualStudent[0]);
            Assert.Equal("John Doe", actualStudent[1]);
            Assert.Equal("Jane Smith", actualStudent[2]);
            
        }
        
        public static List<StudentDTO> GetExpectedStudents(List<StudentDTO> students, string? keyword, DateTime? dob, string? gender, string? status)
        {

            List<StudentDTO> studentList = students;

            if (!status.IsNullOrEmpty())
            {
                studentList = students
                    .Where(student => student.StudentInfoDTO.Status.Equals(status))
                    .ToList();
            }

            if (!gender.IsNullOrEmpty())
            {
                studentList = students
                    .Where(student => student.StudentInfoDTO.Gender.Equals(gender))
                    .ToList();
            }

            if (dob != null)
            {
                
                studentList = students
                    .Where(student => student.StudentInfoDTO.Dob.Equals(dob))
                    .ToList();
            }

            if (!keyword.IsNullOrEmpty())
            {
                    studentList = students.Where(student =>
                        (student.StudentInfoDTO.FullName.Contains(keyword) ||
                         student.StudentInfoDTO.Address.Contains(keyword) ||
                         student.StudentInfoDTO.Email.Contains(keyword))
                    ).Distinct().ToList();
                    
            }
            
            return studentList;
        }
        
        
    }

}


