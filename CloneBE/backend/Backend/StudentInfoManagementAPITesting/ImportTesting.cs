using AutoMapper;
using Entities.Context;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Nest;
using StudentInfoManagementAPI.Controllers;
using StudentInfoManagementAPI.DTO;
using StudentInfoManagementAPI.Service;
using StudentInfoManagementAPITesting.Data;

namespace StudentManagementAPITesting;

public class ImportTesting
{
    private readonly IStudentService_LamNS _studentService;
    private readonly IUploadFileDL _uploadFileDL;
    private readonly IStudentService_QuyNDC _studentServiceQuyNDC;
    private readonly IEditStatusStudentInBatch _editStatusService;
    private readonly IAddStudentClassService _addStudentClassService;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IElasticClient> _elasticClientMock;

    public ImportTesting()
    {
        _mapperMock = new Mock<IMapper>();
        _elasticClientMock = new Mock<IElasticClient>();
    }
    
    [Fact]
    public async Task UploadXMLFile_ValidRequest_Success()
    {
        // Arrange
        var studentsData = new List<Student>();
        var studentClassesData = new List<StudentClass>();

        var mockContext = new Mock<FamsContext>();
        mockContext.Setup(c => c.Students).Returns(MockDbSet(studentsData));
        mockContext.Setup(c => c.StudentClasses).Returns(MockDbSet(studentClassesData));
        
        var elasticClientMock = new Mock<IElasticClient>();
        //_elasticClientMock.Setup(e => e.Indices.Delete("students")).Returns(new AcknowledgedResponse(true)); // Assuming Delete method returns AcknowledgedResponse


        var configurationMock = new Mock<IConfiguration>();

        var uploadService = new UploadFileDL(configurationMock.Object, mockContext.Object, _elasticClientMock.Object, _mapperMock.Object);

        var fileStream = new FileStream("/Users/apple/Desktop/testing.xlsx", FileMode.Open);

        var request = new UploadExcelFileRequest
        {
            File = new FormFile(fileStream, 0, fileStream.Length, "testing.xlsx", "testing.xlsx"),
            duplicateOption = "Replace",
            classId = "class123"
        };

        var path = "/Users/apple/Desktop/testing.xlsx";

        // Act
        var response = await uploadService.UploadXMLFile(request, path);

        // Assert
        Assert.True(response.IsSuccess);
        Assert.Equal("Successful", response.Message);
    }

    
     [Fact]
    public async Task UploadXMLFile_InValidRequest_Success()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<FamsContext>()
            .UseInMemoryDatabase(databaseName: "ImportTesting")
            .Options;

        using (var context = new FamsContext(options))
        {

            var fileStream = new FileStream("/Users/apple/Documents/Labs/Lab1- WritingBlackBoxTestCase/Lab1-.xlsxfasda.docx", FileMode.Open);

            var request = new UploadExcelFileRequest
            {
                File = new FormFile(fileStream, 0, fileStream.Length, "Lab1-.xlsxfasda.docx", "Lab1-.xlsxfasda.docx")
            };
            
            var mockDbSet = new Mock<DbSet<Student>>();
            mockDbSet.Setup(m => m.Add(It.IsAny<Student>()))
                .Callback((Student student) =>
                {
                    student.StudentId = Guid.NewGuid().ToString();
                })
                .Returns((Student student) =>
                {
                    context.Students.Add(student);
                    return context.Entry(student);
                });
            var mockDbSetStudentClass = new Mock<DbSet<StudentClass>>();
            mockDbSetStudentClass.Setup(m => m.Add(It.IsAny<StudentClass>()))
                .Callback((StudentClass StudentClass) =>
                {
                    StudentClass.StudentClassId = Guid.NewGuid().ToString();
                })
                .Returns((StudentClass StudentClass) =>
                {
                    context.StudentClasses.Add(StudentClass);
                    return context.Entry(StudentClass);
                });
            
            var configurationMock = new Mock<IConfiguration>();
            
            var mockContext = new Mock<FamsContext>(options);
            mockContext.Setup(c => c.Students).Returns(mockDbSet.Object);
            mockContext.Setup(c => c.StudentClasses).Returns(mockDbSetStudentClass.Object);

            
            var uploadService = new UploadFileDL(configurationMock.Object, mockContext.Object, _elasticClientMock.Object, _mapperMock.Object);
            var path = "/Users/apple/Documents/Labs/Lab1- WritingBlackBoxTestCase/Lab1-.xlsxfasda.docx";
            
            

            // Act
            var response = await uploadService.UploadXMLFile(request, path);
            
            // Assert
            Assert.False(response.IsSuccess);
        }
    }
    
       [Fact]
    public async Task UploadXMLFile_InValidFileFormat_Success()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<FamsContext>()
            .UseInMemoryDatabase(databaseName: "ImportTesting")
            .Options;

        using (var context = new FamsContext(options))
        {

            var fileStream = new FileStream("/Users/apple/Desktop/testing.docx", FileMode.Open);

            var request = new UploadExcelFileRequest
            {
                File = new FormFile(fileStream, 0, fileStream.Length, "testing.docx", "testing.docx")
            };
            
            var mockDbSet = new Mock<DbSet<Student>>();
            mockDbSet.Setup(m => m.Add(It.IsAny<Student>()))
                .Callback((Student student) =>
                {
                    student.StudentId = Guid.NewGuid().ToString();
                })
                .Returns((Student student) =>
                {
                    context.Students.Add(student);
                    return context.Entry(student);
                });
            var mockDbSetStudentClass = new Mock<DbSet<StudentClass>>();
            mockDbSetStudentClass.Setup(m => m.Add(It.IsAny<StudentClass>()))
                .Callback((StudentClass StudentClass) =>
                {
                    StudentClass.StudentClassId = Guid.NewGuid().ToString();
                })
                .Returns((StudentClass StudentClass) =>
                {
                    context.StudentClasses.Add(StudentClass);
                    return context.Entry(StudentClass);
                });
            
            var configurationMock = new Mock<IConfiguration>();
            
            var mockContext = new Mock<FamsContext>(options);
            mockContext.Setup(c => c.Students).Returns(mockDbSet.Object);
            mockContext.Setup(c => c.StudentClasses).Returns(mockDbSetStudentClass.Object);

            
            var uploadService = new UploadFileDL(configurationMock.Object, mockContext.Object, _elasticClientMock.Object, _mapperMock.Object);
            var path = "/Users/apple/Desktop/testing.docx";
            
            

            // Act
            var response = await uploadService.UploadXMLFile(request, path);
            
            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Invalid file format", response.Message);
        }
    }

    [Fact]
    public async Task UploadXMLFile_ValidRequest_Replace_Success()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<FamsContext>()
            .UseInMemoryDatabase(databaseName: "ImportTesting")
            .Options;

        using (var context = new FamsContext(options))
        {
            var data = new ViewData();
            var students = data.GetTestStudents();

            var studentClasses = data.getStudentClass();
            var mockDbSet = new Mock<DbSet<Student>>();
            mockDbSet.As<IAsyncEnumerable<Student>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<Student>(students.AsQueryable().GetEnumerator()));
            mockDbSet.As<IQueryable<Student>>().Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Student>(students.AsQueryable().Provider));
            mockDbSet.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(students.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(students.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(students.AsQueryable().GetEnumerator());
            
            var mockDbSetStudentClass = new Mock<DbSet<StudentClass>>();
            mockDbSetStudentClass.As<IAsyncEnumerable<StudentClass>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<StudentClass>(studentClasses.AsQueryable().GetEnumerator()));
            mockDbSetStudentClass.As<IQueryable<StudentClass>>().Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<StudentClass>(studentClasses.AsQueryable().Provider));
            mockDbSetStudentClass.As<IQueryable<StudentClass>>().Setup(m => m.Expression).Returns(studentClasses.AsQueryable().Expression);
            mockDbSetStudentClass.As<IQueryable<StudentClass>>().Setup(m => m.ElementType).Returns(studentClasses.AsQueryable().ElementType);
            mockDbSetStudentClass.As<IQueryable<StudentClass>>().Setup(m => m.GetEnumerator()).Returns(studentClasses.AsQueryable().GetEnumerator());


            var fileStream = new FileStream("/Users/apple/Desktop/testing.xlsx", FileMode.Open);

            var request = new UploadExcelFileRequest
            {
                File = new FormFile(fileStream, 0, fileStream.Length, "testing.xlsx", "testing.xlsx"),
                duplicateOption = "Replace",
                classId = "class123"
            };
            
            mockDbSet.Setup(m => m.Add(It.IsAny<Student>()))
                .Callback((Student student) =>
                {
                    student.StudentId = Guid.NewGuid().ToString();
                })
                .Returns((Student student) =>
                {
                    context.Students.Add(student);
                    return context.Entry(student);
                });
            
            mockDbSetStudentClass.Setup(m => m.Add(It.IsAny<StudentClass>()))
                .Callback((StudentClass StudentClass) =>
                {
                    StudentClass.StudentClassId = Guid.NewGuid().ToString();
                })
                .Returns((StudentClass StudentClass) =>
                {
                    context.StudentClasses.Add(StudentClass);
                    return context.Entry(StudentClass);
                });
            
            var configurationMock = new Mock<IConfiguration>();
            
            var mockContext = new Mock<FamsContext>(options);
            mockContext.Setup(c => c.Students).Returns(mockDbSet.Object);
            mockContext.Setup(c => c.StudentClasses).Returns(mockDbSetStudentClass.Object);

            var uploadService = new UploadFileDL(configurationMock.Object, mockContext.Object, _elasticClientMock.Object, _mapperMock.Object);

            var path = "/Users/apple/Desktop/testing.xlsx";
            
            

            // Act
            var isDuplicate = await uploadService.HasDuplicates(request.File);
            
            await uploadService.UploadOption(request, path);
            await uploadService.HandleReplaceOption(request.classId);

            // Assert

        }
    }
    
    private static DbSet<T> MockDbSet<T>(List<T> list) where T : class
    {
        var queryable = list.AsQueryable();
        var dbSetMock = new Mock<DbSet<T>>();
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
        dbSetMock.Setup(m => m.Add(It.IsAny<T>())).Callback((T entity) =>
        {
            // Set unique identifier for StudentId and StudentClassId
            var student = entity as Student;
            if (student != null)
            {
                student.StudentId = Guid.NewGuid().ToString();
            }

            var studentClass = entity as StudentClass;
            if (studentClass != null)
            {
                studentClass.StudentClassId = Guid.NewGuid().ToString();
            }

            list.Add(entity);
        });
        return dbSetMock.Object;
    }

}
