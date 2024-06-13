using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Nest;
using StudentInfoManagementAPI.DTO;
using StudentInfoManagementAPI.Service;
using Xunit;
using Entities.Models;

namespace StudentInfoManagementAPITesting
{
    public class ExportFileTesting
    {

        private readonly Mock<IElasticClient> _elasticClientMock;

        public ExportFileTesting()
        {
            _elasticClientMock = new Mock<IElasticClient>();
        }

        [Fact]
        public void ExportStudentInclass_ReturnsDataTable_WhenNothingProvided()
        {
            // Arrange
            var classId = "class123";

            List<StudentDTO> documents = GetActiveStudents(GetSampleStudents(), null, classId, null, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }

            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            var exportFunction = new StudentService_LamNS(null, null, mockElasticClient.Object);

            // Act
            var result = exportFunction.exportStudentInclass(classId, null, null, null, null, "id", "asc");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DataTable>(result);
            Assert.Equal(documents.Count, result.Rows.Count);
            Assert.Equal("John Doe", result.Rows[0]["Full Name"]);
        }
        
        [Fact]
        public void ExportStudentInclass_ReturnsDataTable_WhenInValidDoBProvided()
        {
            // Arrange
            var classId = "class123";

            DateTime myDateTime = new DateTime(2003, 8, 9);

            List<StudentDTO> documents = GetActiveStudents(GetSampleStudents(), null, classId, myDateTime, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }

            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            var exportFunction = new StudentService_LamNS(null, null, mockElasticClient.Object);

            // Act
            var result = exportFunction.exportStudentInclass(classId, null, myDateTime, null, null, "id", "asc");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(documents.Count, result.Rows.Count);
        }
        
        [Fact]
        public void ExportStudentInclass_ReturnsDataTable_WhenValidDoBProvided()
        {
            // Arrange
            var classId = "class123";

            DateTime myDateTime = new DateTime(1990,01,01);

            List<StudentDTO> documents = GetActiveStudents(GetSampleStudents(), null, classId, myDateTime, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }

            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            var exportFunction = new StudentService_LamNS(null, null, mockElasticClient.Object);

            // Act
            var result = exportFunction.exportStudentInclass(classId, null, myDateTime, null, null, "id", "asc");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(documents.Count, result.Rows.Count);
        }
        
        [Fact]
        public void ExportStudentInclass_ReturnsDataTable_WhenGenderProvided()
        {
            // Arrange
            var classId = "class123";

            List<StudentDTO> documents = GetActiveStudents(GetSampleStudents(), null, classId, null, "Male", null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }

            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            var exportFunction = new StudentService_LamNS(null, null, mockElasticClient.Object);

            // Act
            var result = exportFunction.exportStudentInclass(classId, null, null, "Male", null, "id", "asc");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DataTable>(result);
            Assert.Equal(documents.Count, result.Rows.Count);
            Assert.Equal("John Doe", result.Rows[0]["Full Name"]);
        }
        
        [Fact]
        public void ExportStudentInclass_ReturnsDataTable_WhenStatusProvided()
        {
            // Arrange
            var classId = "class123";

            List<StudentDTO> documents = GetActiveStudents(GetSampleStudents(), null, classId, null, null, "Attending");

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }

            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            var exportFunction = new StudentService_LamNS(null, null, mockElasticClient.Object);

            // Act
            var result = exportFunction.exportStudentInclass(classId, null, null, null, "Attending", "id", "asc");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DataTable>(result);
            Assert.Equal(documents.Count, result.Rows.Count);
            Assert.Equal("Alice Johnson", result.Rows[0]["Full Name"]);
        }
        
        [Fact]
        public void ExportStudentInclass_ReturnsDataTable_WhenKeywordNameProvided()
        {
            // Arrange
            var classId = "class123";

            List<StudentDTO> documents = GetActiveStudents(GetSampleStudents(), "John", classId, null, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }

            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            var exportFunction = new StudentService_LamNS(null, null, mockElasticClient.Object);

            // Act
            var result = exportFunction.exportStudentInclass(classId, null, null, null, null, "id", "asc");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DataTable>(result);
            Assert.Equal(documents.Count, result.Rows.Count);
            Assert.Equal("John Doe", result.Rows[0]["Full Name"]);
            Assert.Equal("Alice Johnson", result.Rows[1]["Full Name"]);
        }
        
        [Fact]
        public void ExportStudentInclass_ReturnsDataTable_WhenKeywordEmailProvided()
        {
            // Arrange
            var classId = "class123";

            List<StudentDTO> documents = GetActiveStudents(GetSampleStudents(), "@", classId, null, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }

            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            var exportFunction = new StudentService_LamNS(null, null, mockElasticClient.Object);

            // Act
            var result = exportFunction.exportStudentInclass(classId, null, null, null, null, "id", "asc");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DataTable>(result);
            Assert.Equal(documents.Count, result.Rows.Count);
            Assert.Equal("John Doe", result.Rows[0]["Full Name"]);
            Assert.Equal("Alice Johnson", result.Rows[1]["Full Name"]);
        }
        
        [Fact]
        public void ExportStudentInclass_ReturnsDataTable_WhenKeywordMajorProvided()
        {
            // Arrange
            var classId = "class123";

            List<StudentDTO> documents = GetActiveStudents(GetSampleStudents(), "Physic", classId, null, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }

            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            var exportFunction = new StudentService_LamNS(null, null, mockElasticClient.Object);

            // Act
            var result = exportFunction.exportStudentInclass(classId, null, null, null, null, "id", "asc");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DataTable>(result);
            Assert.Equal(documents.Count, result.Rows.Count);
            Assert.Equal("Michael Smith", result.Rows[0]["Full Name"]);
        }

        [Fact]
        public void ExportStudentInclass_ReturnsDataTable_WhenKeywordAddressProvided()
        {
            // Arrange
            var classId = "class123";

            List<StudentDTO> documents = GetActiveStudents(GetSampleStudents(), "1", classId, null, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }

            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            var exportFunction = new StudentService_LamNS(null, null, mockElasticClient.Object);

            // Act
            var result = exportFunction.exportStudentInclass(classId, null, null, null, null, "id", "asc");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DataTable>(result);
            Assert.Equal(documents.Count, result.Rows.Count);
             Assert.Equal("John Doe", result.Rows[0]["Full Name"]);
            
        }

        [Fact]
        public void ExportStudentInclass_ReturnsDataTable_WhenStatusAndGenderProvided()
        {
            // Arrange
            var classId = "class123";

            List<StudentDTO> documents = GetActiveStudents(GetSampleStudents(), null, classId, null, "Male", "Die");

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }

            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            var exportFunction = new StudentService_LamNS(null, null, mockElasticClient.Object);

            // Act
            var result = exportFunction.exportStudentInclass(classId, null, null, "Female", "Attending", "id", "asc");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DataTable>(result);
            Assert.Equal(documents.Count, result.Rows.Count);
            Assert.Equal("John Doe", result.Rows[0]["Full Name"]);
        }
        
        [Fact]
        public void ExportStudentInclass_ReturnsDataTable_WhenNothingProvided_SortByFullName()
        {
            // Arrange
            var classId = "class123";

            List<StudentDTO> documents = GetActiveStudents(GetSampleStudents(), null, classId, null, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }

            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            var exportFunction = new StudentService_LamNS(null, null, mockElasticClient.Object);

            // Act
            var result = exportFunction.exportStudentInclass(classId, null, null, null, null, "fullname", "asc");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DataTable>(result);
            Assert.Equal(documents.Count, result.Rows.Count);
            Assert.Equal("Alice Johnson", result.Rows[0]["Full Name"]);
            Assert.Equal("Emily Brown", result.Rows[1]["Full Name"]);
            Assert.Equal("John Doe", result.Rows[2]["Full Name"]);
            Assert.Equal("Michael Smith", result.Rows[3]["Full Name"]);
        }
        
        [Fact]
        public void ExportStudentInclass_ReturnsDataTable_WhenNothingProvided_SortByBirthDay()
        {
            // Arrange
            var classId = "class123";

            List<StudentDTO> documents = GetActiveStudents(GetSampleStudents(), null, classId, null, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }

            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            var exportFunction = new StudentService_LamNS(null, null, mockElasticClient.Object);

            // Act
            var result = exportFunction.exportStudentInclass(classId, null, null, null, null, "birthday", "asc");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DataTable>(result);
            Assert.Equal(documents.Count, result.Rows.Count);
            Assert.Equal("John Doe", result.Rows[0]["Full Name"]);
            Assert.Equal("Michael Smith", result.Rows[1]["Full Name"]);
            Assert.Equal("Alice Johnson", result.Rows[2]["Full Name"]);
            Assert.Equal("Emily Brown", result.Rows[3]["Full Name"]);
        }
        
        [Fact]
        public void ExportStudentInclass_ReturnsDataTable_WhenNothingProvided_SortByGender()
        {
            // Arrange
            var classId = "class123";

            List<StudentDTO> documents = GetActiveStudents(GetSampleStudents(), null, classId, null, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }

            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            var exportFunction = new StudentService_LamNS(null, null, mockElasticClient.Object);

            // Act
            var result = exportFunction.exportStudentInclass(classId, null, null, null, null, "gender", "asc");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DataTable>(result);
            Assert.Equal(documents.Count, result.Rows.Count);
            Assert.Equal("Emily Brown", result.Rows[0]["Full Name"]);
        }
        
        [Fact]
        public void ExportStudentInclass_ReturnsDataTable_WhenNothingProvided_SortByPhone()
        {
            // Arrange
            var classId = "class123";

            List<StudentDTO> documents = GetActiveStudents(GetSampleStudents(), null, classId, null, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }

            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            var exportFunction = new StudentService_LamNS(null, null, mockElasticClient.Object);

            // Act
            var result = exportFunction.exportStudentInclass(classId, null, null, null, null, "phone", "asc");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DataTable>(result);
            Assert.Equal(documents.Count, result.Rows.Count);
            Assert.Equal("Michael Smith", result.Rows[0]["Full Name"]);
            Assert.Equal("John Doe", result.Rows[1]["Full Name"]);
            Assert.Equal("Emily Brown", result.Rows[2]["Full Name"]);
            Assert.Equal("Alice Johnson", result.Rows[3]["Full Name"]);
        }
        
        [Fact]
        public void ExportStudentInclass_ReturnsDataTable_WhenNothingProvided_SortByEmail()
        {
            // Arrange
            var classId = "class123";

            List<StudentDTO> documents = GetActiveStudents(GetSampleStudents(), null, classId, null, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }

            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            var exportFunction = new StudentService_LamNS(null, null, mockElasticClient.Object);

            // Act
            var result = exportFunction.exportStudentInclass(classId, null, null, null, null, "email", "asc");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DataTable>(result);
            Assert.Equal(documents.Count, result.Rows.Count);
            Assert.Equal("Alice Johnson", result.Rows[0]["Full Name"]);
            Assert.Equal("Emily Brown", result.Rows[1]["Full Name"]);
            Assert.Equal("John Doe", result.Rows[2]["Full Name"]);
            Assert.Equal("Michael Smith", result.Rows[3]["Full Name"]);
        }
        
        [Fact]
        public void ExportStudentInclass_ReturnsDataTable_WhenNothingProvided_SortByUniversity()
        {
            // Arrange
            var classId = "class123";

            List<StudentDTO> documents = GetActiveStudents(GetSampleStudents(), null, classId, null, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }

            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            var exportFunction = new StudentService_LamNS(null, null, mockElasticClient.Object);

            // Act
            var result = exportFunction.exportStudentInclass(classId, null, null, null, null, "university", "asc");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DataTable>(result);
            Assert.Equal(documents.Count, result.Rows.Count);
            Assert.Equal("Alice Johnson", result.Rows[0]["Full Name"]);
            Assert.Equal("Emily Brown", result.Rows[1]["Full Name"]);
            Assert.Equal("John Doe", result.Rows[2]["Full Name"]);
            Assert.Equal("Michael Smith", result.Rows[3]["Full Name"]);
        }
        
        [Fact]
        public void ExportStudentInclass_ReturnsDataTable_WhenNothingProvided_SortByMajor()
        {
            // Arrange
            var classId = "class123";

            List<StudentDTO> documents = GetActiveStudents(GetSampleStudents(), null, classId, null, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }

            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            var exportFunction = new StudentService_LamNS(null, null, mockElasticClient.Object);

            // Act
            var result = exportFunction.exportStudentInclass(classId, null, null, null, null, "major", "asc");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DataTable>(result);
            Assert.Equal(documents.Count, result.Rows.Count);
            Assert.Equal("Emily Brown", result.Rows[0]["Full Name"]);
            Assert.Equal("John Doe", result.Rows[1]["Full Name"]);
            Assert.Equal("Alice Johnson", result.Rows[2]["Full Name"]);
            Assert.Equal("Michael Smith", result.Rows[3]["Full Name"]);
        }
        
        [Fact]
        public void ExportStudentInclass_ReturnsDataTable_WhenNothingProvided_SortByGrateduateTime()
        {
            // Arrange
            var classId = "class123";

            List<StudentDTO> documents = GetActiveStudents(GetSampleStudents(), null, classId, null, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }

            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            var exportFunction = new StudentService_LamNS(null, null, mockElasticClient.Object);

            // Act
            var result = exportFunction.exportStudentInclass(classId, null, null, null, null, "graduatetime", "asc");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DataTable>(result);
            Assert.Equal(documents.Count, result.Rows.Count);
            Assert.Equal("John Doe", result.Rows[0]["Full Name"]);
            Assert.Equal("Alice Johnson", result.Rows[1]["Full Name"]);
            Assert.Equal("Michael Smith", result.Rows[2]["Full Name"]);
            Assert.Equal("Emily Brown", result.Rows[3]["Full Name"]);
        }
        
        [Fact]
        public void ExportStudentInclass_ReturnsDataTable_WhenNothingProvided_SortByGPA()
        {
            // Arrange
            var classId = "class123";

            List<StudentDTO> documents = GetActiveStudents(GetSampleStudents(), null, classId, null, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }

            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            var exportFunction = new StudentService_LamNS(null, null, mockElasticClient.Object);

            // Act
            var result = exportFunction.exportStudentInclass(classId, null, null, null, null, "gpa", "asc");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DataTable>(result);
            Assert.Equal(documents.Count, result.Rows.Count);
            Assert.Equal("John Doe", result.Rows[0]["Full Name"]);
            Assert.Equal("Emily Brown", result.Rows[1]["Full Name"]);
            Assert.Equal("Michael Smith", result.Rows[2]["Full Name"]);
            Assert.Equal("Alice Johnson", result.Rows[3]["Full Name"]);
        }
        
        [Fact]
        public void ExportStudentInclass_ReturnsDataTable_WhenNothingProvided_SortByAddress()
        {
            // Arrange
            var classId = "class123";

            List<StudentDTO> documents = GetActiveStudents(GetSampleStudents(), null, classId, null, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }

            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            var exportFunction = new StudentService_LamNS(null, null, mockElasticClient.Object);

            // Act
            var result = exportFunction.exportStudentInclass(classId, null, null, null, null, "address", "asc");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DataTable>(result);
            Assert.Equal(documents.Count, result.Rows.Count);
            Assert.Equal("John Doe", result.Rows[0]["Full Name"]);
            Assert.Equal("Michael Smith", result.Rows[1]["Full Name"]);
            Assert.Equal("Alice Johnson", result.Rows[2]["Full Name"]);
            Assert.Equal("Emily Brown", result.Rows[3]["Full Name"]);
        }
        
        [Fact]
        public void ExportStudentInclass_ReturnsDataTable_WhenNothingProvided_SortByRecer()
        {
            // Arrange
            var classId = "class123";

            List<StudentDTO> documents = GetActiveStudents(GetSampleStudents(), null, classId, null, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }

            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            var exportFunction = new StudentService_LamNS(null, null, mockElasticClient.Object);

            // Act
            var result = exportFunction.exportStudentInclass(classId, null, null, null, null, "recer", "asc");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DataTable>(result);
            Assert.Equal(documents.Count, result.Rows.Count);
            Assert.Equal("Alice Johnson", result.Rows[0]["Full Name"]);
            Assert.Equal("John Doe", result.Rows[1]["Full Name"]);
            Assert.Equal("Emily Brown", result.Rows[2]["Full Name"]);
            Assert.Equal("Michael Smith", result.Rows[3]["Full Name"]);
        }
        
        [Fact]
        public void ExportStudentInclass_ReturnsDataTable_WhenNothingProvided_SortByStatus()
        {
            // Arrange
            var classId = "class123";

            List<StudentDTO> documents = GetActiveStudents(GetSampleStudents(), null, classId, null, null, null);

            var hits = new List<IHit<StudentDTO>>();
            foreach (var student in documents)
            {
                hits.Add(new Mock<IHit<StudentDTO>>().Object);
            }

            var mockElasticClient = new Mock<IElasticClient>();
            var mockSearchResponse = new Mock<ISearchResponse<StudentDTO>>();
            mockSearchResponse.SetupGet(r => r.Documents).Returns(documents);
            mockSearchResponse.SetupGet(r => r.Hits).Returns(hits);
            mockElasticClient.Setup(c => c.Search<StudentDTO>(It.IsAny<Func<SearchDescriptor<StudentDTO>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            var exportFunction = new StudentService_LamNS(null, null, mockElasticClient.Object);

            // Act
            var result = exportFunction.exportStudentInclass(classId, null, null, null, null, "status", "asc");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DataTable>(result);
            Assert.Equal(documents.Count, result.Rows.Count);
            Assert.Equal("John Doe", result.Rows[3]["Full Name"]);
        }

        public static List<StudentDTO> GetActiveStudents(IEnumerable<StudentDTO> students, string? keyword, string classId, DateTime? dob, string? gender, string? status)
        {
            var filteredStudents = students.Where(student => student.StudentClassDTOs.Any(sc => sc.ClassId.Equals(classId)));
            
            if (dob.HasValue)
            {
                filteredStudents = filteredStudents.Where(student => student.StudentInfoDTO.Dob == dob);
            }
            if (!string.IsNullOrEmpty(gender))
            {
                filteredStudents = filteredStudents.Where(student => student.StudentInfoDTO.Gender.Equals(gender));
            }
            if (!string.IsNullOrEmpty(status))
            {
                filteredStudents = filteredStudents.Where(student => student.StudentClassDTOs.First().AttendingStatus.Equals(status));
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                filteredStudents = students.Where(student =>
                    (student.StudentInfoDTO.FullName.Contains(keyword) ||
                     student.StudentInfoDTO.Address.Contains(keyword) ||
                     student.StudentInfoDTO.Email.Contains(keyword)||
                     student.MajorDTO.Name.Contains(keyword))
                ).Distinct().ToList();
            }
            return filteredStudents.ToList();
        }
        

        static IEnumerable<StudentDTO> GetSampleStudents()
        {
            return new List<StudentDTO>
            {
                new StudentDTO
                {
                    StudentInfoDTO = new StudentInfoDTO
                    {
                        FullName = "John Doe",
                        Dob = DateTime.Parse("1990-01-01"),
                        Gender = "Male",
                        Phone = "123456789",
                        Email = "john.doe@example.com",
                        University = "FPT",
                        MajorId = "exampleMajorId",
                        GraduatedDate = DateTime.Parse("2015-01-01"),
                        Gpa = 3.5m,
                        Address = "123 Example St",
                        Recer = "Example Recer",
                        StudentId = "exampleStudentId"
                    },
                    MajorDTO = new MajorDTO
                    {
                        MajorId = "exampleMajorId",
                        Id = "exampleMajorId",
                        Name = "Computer Science"
                    },
                    StudentClassDTOs = new List<StudentClassDTO>
                    {
                        new StudentClassDTO
                        {
                            StudentClassId = "exampleStudentClassId",
                            Id = 1,
                            StudentId = "exampleStudentId",
                            ClassId = "class123",
                            AttendingStatus = "Die",
                            Result = 90,
                            FinalScore = 95,
                            Gpalevel = 4,
                            CertificationStatus = 1,
                            CertificationDate = DateTime.Parse("2023-01-01"),
                            Method = 1,
                            Class = null
                        }
                    },

                },new StudentDTO
    {
        StudentInfoDTO = new StudentInfoDTO
        {
            FullName = "Alice Johnson",
            Dob = new DateTime(1992, 8, 20),
            Gender = "Male",
            Phone = "9876543210",
            Email = "alice.johnson@example.com",
            University = "Another University",
            MajorId = "anotherMajorId",
            GraduatedDate = new DateTime(2016, 6, 30),
            Gpa = 3.9m,
            Address = "789 Another Ave",
            Recer = "Another Recer",
            StudentId = "anotherStudentId"
        },
        MajorDTO = new MajorDTO
        {
            MajorId = "anotherMajorId",
            Id = "anotherMajorId",
            Name = "Mathematics"
        },
        StudentClassDTOs = new List<StudentClassDTO>
        {
            new StudentClassDTO
            {
                StudentClassId = "anotherStudentClassId1",
                Id = 2,
                StudentId = "anotherStudentId",
                ClassId = "class123",
                AttendingStatus = "Attending",
                Result = 88,
                FinalScore = 91,
                Gpalevel = 4,
                CertificationStatus = 1,
                CertificationDate = new DateTime(2023, 6, 15),
                Method = 1,
                Class = null
            }
        }
    },

    // Student 3
    new StudentDTO
    {
        StudentInfoDTO = new StudentInfoDTO
        {
            FullName = "Michael Smith",
            Dob = new DateTime(1991, 3, 10),
            Gender = "Male",
            Phone = "1231231234",
            Email = "michael.smith@example.com",
            University = "Yet Another University",
            MajorId = "yetAnotherMajorId",
            GraduatedDate = new DateTime(2017, 5, 15),
            Gpa = 3.7m,
            Address = "456 Yet Another St",
            Recer = "Yet Another Recer",
            StudentId = "yetAnotherStudentId"
        },
        MajorDTO = new MajorDTO
        {
            MajorId = "yetAnotherMajorId",
            Id = "yetAnotherMajorId",
            Name = "Physics"
        },
        StudentClassDTOs = new List<StudentClassDTO>
        {
            new StudentClassDTO
            {
                StudentClassId = "yetAnotherStudentClassId1",
                Id = 3,
                StudentId = "yetAnotherStudentId",
                ClassId = "class123",
                AttendingStatus = "Attending",
                Result = 95,
                FinalScore = 98,
                Gpalevel = 4,
                CertificationStatus = 1,
                CertificationDate = new DateTime(2023, 12, 20),
                Method = 1,
                Class = null
            }
        }
    }
    ,

    // Student 4
    new StudentDTO
    {
        StudentInfoDTO = new StudentInfoDTO
        {
            FullName = "Emily Brown",
            Dob = new DateTime(1993, 11, 5),
            Gender = "Female",
            Phone = "5555555555",
            Email = "emily.brown@example.com",
            University = "Final University",
            MajorId = "finalMajorId",
            GraduatedDate = new DateTime(2018, 8, 20),
            Gpa = 3.6m,
            Address = "789 Final Blvd",
            Recer = "Final Recer",
            StudentId = "finalStudentId"
        },
        MajorDTO = new MajorDTO
        {
            MajorId = "finalMajorId",
            Id = "finalMajorId",
            Name = "Chemistry"
        },
        StudentClassDTOs = new List<StudentClassDTO>
        {
            new StudentClassDTO
            {
                StudentClassId = "finalStudentClassId1",
                Id = 4,
                StudentId = "finalStudentId",
                ClassId = "class123",
                AttendingStatus = "Attending",
                Result = 90,
                FinalScore = 92,
                Gpalevel = 4,
                CertificationStatus = 1,
                CertificationDate = new DateTime(2023, 9, 30),
                Method = 1,
                Class = null
            }
        }
    }
            };
        }


    }
}


