using TrainingProgramManagementAPI.Controllers;
using Entities.Models;
using TrainingProgramManagementAPI.Utils;
using TrainingProgramManagementAPITests.Extensions;
using TrainingProgramManagementAPITests.Repositories;
using Xunit.Abstractions;
using Moq;
using Microsoft.AspNetCore.Mvc;
using TrainingProgramManagementAPI.Payloads.Responses;
using AutoMapper;
using Microsoft.Extensions.Options;
using TrainingProgramManagementAPI.Services;
using TrainingProgramManagementAPI.DTOs;
using TrainingProgramManagementAPITests.Utils;
using System.Globalization;
using TrainingProgramManagementAPI.Payloads.Requests;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using TrainingProgramManagementAPI.Common.Enums;
// using NUnit.Framework;

using FileHelper = TrainingProgramManagementAPITests.Utils.FileHelper;

namespace TrainingProgramManagementAPITests.Controllers
{
    public class TrainingProgramControllerTests
    {
        private readonly FamsContext _context;
        private readonly IRepository<TrainingProgram> _repository;
        private readonly TrainingProgramController _controller;
        private readonly IMapper _mapper;
        private readonly IFirebaseService _firebaseService;

        private readonly Mock<IOptionsMonitor<FirebaseCredentials>> _mockFirebaseCredentials = new Mock<IOptionsMonitor<FirebaseCredentials>>();
        private readonly Mock<IOptionsMonitor<AppSettings>> _mockAppSettings = new Mock<IOptionsMonitor<AppSettings>>();

        // Custom properties
        public string TrainingProgramCode { get; set; } = string.Empty;

        // Optional - Maybe Null
        private readonly Mock<IS3Service> _mockS3Service = new Mock<IS3Service>();

        public TrainingProgramControllerTests(ITestOutputHelper output)
        {
            // Config Services
            ConfigurationExtension.ConfigureServices();

            // Config DbContext
            _context = ConfigurationExtension.ConfigureDbContext();

            // Test Repository
            _repository = new TrainingProgramRepository(output, _context);

            // Config AutoMapper
            _mapper = ConfigurationExtension.ConfigureAutoMapper();

            // Config Appsettings    
            _mockAppSettings.ConfigureAppSettings();

            // Config firebase credential
            _mockFirebaseCredentials.ConfigureFirebaseCredential();

            // Config firebase Service
            _firebaseService = ConfigurationExtension.ConfigureFirebaseService();

            _controller = new TrainingProgramController(
                _context,
                _mapper,
                _mockAppSettings.Object,
                _mockFirebaseCredentials.Object,
                _mockS3Service.Object,
                _firebaseService
            );
        }

        // Summary:
        //      Testing get all training programs with paging 
        [Theory]
        [InlineData(1, "5")]
        [InlineData(1, "10")]
        [InlineData(2, "10")]
        public async Task ReturnAViewResult_WithPagingAListOfTrainingProgram(
            int _page = 1, string _perPage = "")
        {
            try
            {
                // Arrange
                // Act 
                var result = await _controller.GetAllAsync(_page, _perPage);

                // Assert
                var objectResult = Assert.IsType<OkObjectResult>(result);
                var baseResponse = Assert.IsType<BaseResponse>(objectResult.Value);
                var entityPage = Assert.IsType<EntitiesByPage<TrainingProgramDto>>(baseResponse.Data);
                // Success Status Code
                Assert.Equal(200, baseResponse.StatusCode);

                // Compare data list 
                var expectedData = await _repository.GetAllAsync();

                // Map expected result to List<TraininingProgramDto>
                var expectedDataList = Assert.IsType<List<TrainingProgramDto>>(
                    _mapper.Map<List<TrainingProgramDto>>(expectedData.ToList()));
                // Map actual result to List<TraininingProgramDto>
                var actualDataList = Assert.IsType<List<TrainingProgramDto>>(
                    _mapper.Map<List<TrainingProgramDto>>(entityPage.List));

                // Paging
                var pagingExpectedDataList = PaginationHelper.PaginationAsync(
                    // Page index
                    _page,
                    // List of training program
                    expectedDataList,
                    // Total item in a page
                    Convert.ToInt32(_perPage)
                ).List.ToList(); // Convert result to List<TrainingProgramDto>


                // Compare properties
                for (int i = 0; i < pagingExpectedDataList.Count; i++)
                {
                    Assert.Equal(pagingExpectedDataList[i].Id, actualDataList[i].Id);
                    Assert.Equal(pagingExpectedDataList[i].Name, actualDataList[i].Name);
                    Assert.Equal(pagingExpectedDataList[i].TrainingProgramCode, actualDataList[i].TrainingProgramCode);
                    Assert.Equal(pagingExpectedDataList[i].Hours, actualDataList[i].Hours);
                    Assert.Equal(pagingExpectedDataList[i].StartTime, actualDataList[i].StartTime);
                    Assert.Equal(pagingExpectedDataList[i].CreatedBy, actualDataList[i].CreatedBy);
                    Assert.Equal(pagingExpectedDataList[i].CreatedDate, actualDataList[i].CreatedDate);
                    Assert.Equal(pagingExpectedDataList[i].UpdatedBy, actualDataList[i].UpdatedBy);
                    Assert.Equal(pagingExpectedDataList[i].UpdatedDate, actualDataList[i].UpdatedDate);
                }

                if (_page == 1)
                {
                    // Check total training program response
                    Assert.Equal(_perPage, pagingExpectedDataList.Count.ToString());
                }
                else if (_page == 2)
                {
                    // Check total training program response
                    Assert.Equal("1", pagingExpectedDataList.Count.ToString());
                }

                // Check total page
                var pageSize = Convert.ToInt32(_perPage);
                var totalItems = expectedDataList.Count;

                // Must be correct total page
                Assert.Equal(entityPage.TotalPage,
                    // No page generate for remaining item
                    totalItems % pageSize == 0
                        // Total page must be integer
                        ? totalItems / pageSize
                        // Real number (double, float...)
                        : (int)Math.Ceiling((double)totalItems / pageSize)); // Ceiling and convert to Int
            }
            catch (Exception ex)
            {
                Assert.Contains(typeof(NotFoundObjectResult).ToString(), ex.Message);
                Console.WriteLine($"An Unexpected Error Occured: {ex.Message}");
            }
            finally
            {
                await Task.CompletedTask;
            }
        }


        // Summary:
        //      Get all authors
        [Fact]
        public async Task ReturnAViewResult_ListOfAuthor()
        {
            try
            {
                // Arrange

                // Act
                // Get all authors
                var result = await _controller.GetAllAuthorsAsync();

                // Assert 
                var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
                var baseResponse = Assert.IsType<BaseResponse>(objectResult.Value);
                var actualResult = Assert.IsType<List<string>>(baseResponse.Data);

                // Success Status Code
                Assert.Equal(200, baseResponse.StatusCode);

                // Get all testing authors
                var trainingPrograms = await _repository.GetAllAsync();
                var expectedResult = trainingPrograms // From list program 
                    .Select(x => x.CreatedBy) // Select all training program creator
                    .Distinct() // Must be unique
                    .ToList(); // Covert to List<string>
                // Check exist
                Assert.NotNull(expectedResult);

                // Compare Data List
                Assert.Equal(expectedResult!, actualResult);
            }
            catch (Exception ex)
            {
                Assert.Contains(typeof(NotFoundObjectResult).ToString(), ex.Message);
                Console.WriteLine($"An Unexpected Error occured: ${ex.Message}");
            }
            finally
            {
                await Task.CompletedTask;
            }
        }


        // Summary:
        //      Get Training Program By Code
        [Theory]
        [InlineData("")] // Must assert not found 
        [InlineData("f6a9bff5-9e84-47de-9a4c-78fc0dc76bd4")]
        [InlineData("766ee0f2-7817-4f86-a70c-1d72c76c2ab3")]
        public async Task ReturnAViewResult_GetByCode(string code)
        {
            try
            {
                // Arrange

                // Act
                var result = await _controller.GetByCodeAsync(code);

                // Assert
                var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
                var baseResponse = Assert.IsType<BaseResponse>(objectResult.Value);
                var actualValue = Assert.IsType<TrainingProgramDto>(baseResponse.Data);

                // Success Status Code
                Assert.Equal(200, baseResponse.StatusCode);

                // Testing get by code
                var expectedResult = await _repository.GetByIdAsync(code);
                // Mapping expectedResult to TrainingProgramDto
                var expectedResultDto = _mapper.Map<TrainingProgramDto>(expectedResult);
                // Check exist
                Assert.NotNull(expectedResultDto);

                // Compare data list
                expectedResultDto.AssertObjEquals(actualValue);
            }
            catch (Exception ex)
            {
                Assert.Contains(typeof(NotFoundObjectResult).ToString(), ex.Message);
                Console.WriteLine($"An Unexpected Error Occurred: {ex.Message}");
            }
            finally
            {
                await Task.CompletedTask;
            }
        }


        // Summary:
        //      Get Training Program By Syllabus Code 
        [Theory]
        [InlineData("")] // Must assert not found 
        [InlineData("31968dc0-0400-4fa3-9303-f8f19a427f86")]
        public async Task ReturnAViewResult_GetProgramSyllabusByCode(string code)
        {
            /// NOTE: This a redundant method cause it same as GetByCode - Please delete it 
            try
            {
                // Arrange
                // Act
                var result = await _controller.GetProgramSyllabusByCodeAsync(code);

                // Assert
                var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
                var baseResponse = Assert.IsType<BaseResponse>(objectResult.Value);
                var actualValue = Assert.IsType<TrainingProgramDto>(baseResponse.Data);

                // Success Status Code
                Assert.Equal(200, baseResponse.StatusCode);

                // Testing get by code
                var expectedResult = await _repository.GetByIdAsync(code);
                // Mapping expectedResult to TrainingProgramDto
                var expectedResultDto = _mapper.Map<TrainingProgramDto>(expectedResult);
                // Check exist
                Assert.NotNull(expectedResultDto);

                // Compare data list
                expectedResultDto.AssertObjEquals(actualValue);
            }
            catch (Exception ex)
            {
                Assert.Contains(typeof(NotFoundObjectResult).ToString(), ex.Message);
                Console.WriteLine($"An Unexpected Error Occured: {ex.Message}");
            }
            finally
            {
                await Task.CompletedTask;
            }
        }


        // Summary:
        //     Search Training Program
        [Theory]
        [InlineData("", 1, "5", "-programname")] // Get all 
        [InlineData("abcdef", 1, "10", "+createdon")] // Invoke Not found Exception 
        [InlineData("Developer", 1, "10", "+duration")] // Get by pattern
        public async Task ReturnAViewResult_SearchTrainingProgramWithSortingAndPagingAsync(
            string searchValue, // Search term value
            int _page, // Page index
            string _perPage, // Page size 
            string sort) // Sort pattern
        {
            try
            {
                // Arrange
                // Act
                var result = await _controller.SearchTrainingProgramAsync(
                    searchValue, _page, _perPage, sort);

                // Assert
                var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
                var baseResponse = Assert.IsType<BaseResponse>(objectResult.Value);
                var entitiesByPage = Assert.IsType<EntitiesByPage<TrainingProgram>>(baseResponse.Data);

                // Success Status Code
                Assert.Equal(200, baseResponse.StatusCode);

                // Get all training programs 
                var expectedData = _repository.GetAllAsQueryable();

                // Search by pattern
                var searchListResult = await SearchHelper.SearchByPatternAsync(
                    expectedData, // Building Query data
                    searchValue, // Value search
                    _mockAppSettings.Object.CurrentValue); // Appsettings

                // Sorting 
                if (!string.IsNullOrEmpty(sort))
                {
                    searchListResult = SortingHelper.SortByColumn(searchListResult, sort).ToList();
                }

                // Map expected result to List<TraininingProgramDto>
                var expectedDataList = Assert.IsType<List<TrainingProgramDto>>(
                    _mapper.Map<List<TrainingProgramDto>>(searchListResult.ToList()));
                // Map actual result to List<TraininingProgramDto>
                var actualDataList = Assert.IsType<List<TrainingProgramDto>>(
                    _mapper.Map<List<TrainingProgramDto>>(entitiesByPage.List));

                // Paging 
                /// NOTE: Conflict method - if your method is not a asynchronous method
                /// Please do not naming its with Async at the end
                var pagingExpectedData = PaginationHelper.PaginationAsync(
                    // Page index
                    _page,
                    // List of training program
                    expectedDataList,
                    // Total item in a page
                    Convert.ToInt32(_perPage)
                ); // Convert result to List<TrainingProgramDto>

                // Check total page
                var pageSize = Convert.ToInt32(_perPage);
                var totalItems = expectedDataList.Count;

                // Must be correct total page
                Assert.Equal(pagingExpectedData.TotalPage,
                    // No page generate for remaining item
                    totalItems % pageSize == 0
                        // Total page must be integer
                        ? totalItems / pageSize
                        // Real number (double, float...)
                        : (int)Math.Ceiling((double)totalItems / pageSize)); // Ceiling and convert to Int


                // Check sorting
                // List of pagined data
                var listPaginedData = pagingExpectedData.List.ToList();
                if (sort is "-programname") // Check valid sorting by descending program name
                {
                    // Assert True
                    Assert.True(SortingValidHelper.IsDescending(
                        listPaginedData.Select(x => x.Name).ToList()));
                }
                else if (sort is "+createon") // Check valid sorting by ascending create date
                {
                    // Assert True
                    Assert.True(SortingValidHelper.IsAscending(
                        listPaginedData
                            .Where(x => x.CreatedDate.HasValue)
                            .Select(x => x.CreatedDate!.Value).ToList()));
                }
                else if (sort is "+duration") // Check valid sorting by ascending days
                {
                    // Assert True
                    Assert.True(SortingValidHelper.IsAscending(
                        listPaginedData
                            .Where(x => x.Days.HasValue)
                            .Select(x => x.Days!.Value).ToList()));
                }

            }
            catch (Exception ex)
            {
                Assert.Contains(typeof(NotFoundObjectResult).ToString(), ex.Message);
                Console.WriteLine($"An Unexpected Error Occured: {ex.Message}");
            }
            finally
            {
                await Task.CompletedTask;
            }
        }


        // Summary:
        //      Filter training program
        [Theory]
        [InlineData(new[] { "Active", "InActive" }, "", "2017-02-01", "2022-02-01", 1, "10", "-programname")]
        [InlineData(new[] { "Draft" }, "", "2017-02-01", "2023-02-01", 1, "5", "+createon")]
        public async Task ReturnAViewResult_FilterTrainingProgramAsync(
            // There are exceed than 4 params in a methods - Please create a class for those 
            // All parameters must be follow Camel Case
            string[]? status = null, // Training program status 
            string createdBy = "", // Create by
            string programTimeFrameFrom = "", // From Datetime  
            string programTimeFrameTo = "", // To Datetime
            int _page = 1, // Current page, 
            string _perPage = "", // Page size
            string? sort = null // Sorting 
        )
        {
            try
            {
                // Arrange

                // Act
                var dateFrom = DateTime.ParseExact(programTimeFrameFrom, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                var dateTo = DateTime.ParseExact(programTimeFrameTo, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                var result = await _controller.FilterAsync(
                    status, createdBy, dateFrom,
                    dateTo, _page, _perPage, sort);

                // Verify object derived from OkObjectResult 
                // If not -> Catch exception
                var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
                var baseResponse = Assert.IsType<BaseResponse>(objectResult.Value);
                var entitiesByPage = Assert.IsType<EntitiesByPage<TrainingProgram>>(baseResponse.Data);

                // Success Status Code
                Assert.Equal(200, baseResponse.StatusCode);

                // Get all training programs 
                var expectedData = _repository.GetAllAsQueryable();

                // Filter by pattern
                var filterListResult =
                    await TrainingProgramManagementAPI.Utils.FilterHelper.FilterByPattern(
                        expectedData, status, createdBy,
                        dateFrom, dateTo, _page, _perPage
                    );

                // Check filter result
                if (status != null && !status.Contains("Draft"))
                {
                    // There are 8 data match filter
                    Assert.Equal(8, filterListResult.Count());
                }
                else
                {
                    // Just exist 1 result
                    Assert.Single(filterListResult);
                }


                // Sorting 
                if (!string.IsNullOrEmpty(sort))
                {
                    filterListResult = SortingHelper.SortByColumn(filterListResult, sort).ToList();
                }

                // Map expected result to List<TraininingProgramDto>
                var expectedDataList = Assert.IsType<List<TrainingProgramDto>>(
                    _mapper.Map<List<TrainingProgramDto>>(filterListResult.ToList()));
                // Map actual result to List<TraininingProgramDto>
                var actualDataList = Assert.IsType<List<TrainingProgramDto>>(
                    _mapper.Map<List<TrainingProgramDto>>(entitiesByPage.List));

                // Paging 
                /// NOTE: Conflict method - if your method is not a asynchronous method
                /// Please do not naming its with Async at the end
                var pagingExpectedData = PaginationHelper.PaginationAsync(
                    // Page index
                    _page,
                    // List of training program
                    expectedDataList,
                    // Total item in a page
                    Convert.ToInt32(_perPage)
                ); // Convert result to List<TrainingProgramDto>

                // Check total page
                var pageSize = Convert.ToInt32(_perPage);
                var totalItems = expectedDataList.Count;

                // Must be correct total page
                Assert.Equal(pagingExpectedData.TotalPage,
                    // No page generate for remaining item
                    totalItems % pageSize == 0
                        // Total page must be integer
                        ? totalItems / pageSize
                        // Real number (double, float...)
                        : (int)Math.Ceiling((double)totalItems / pageSize)); // Ceiling and convert to Int


                // Check sorting
                // List of pagined data
                var listPaginedData = pagingExpectedData.List.ToList();
                if (sort is "-programname") // Check valid sorting by descending program name
                {
                    // Assert True
                    Assert.True(SortingValidHelper.IsDescending(
                        listPaginedData.Select(x => x.Name).ToList()));
                }
                else if (sort is "+createon") // Check valid sorting by ascending create date
                {
                    // Assert True
                    Assert.True(SortingValidHelper.IsAscending(
                        listPaginedData
                            .Where(x => x.CreatedDate.HasValue)
                            .Select(x => x.CreatedDate!.Value).ToList()));
                }
                else if (sort is "+duration") // Check valid sorting by ascending days
                {
                    // Assert True
                    Assert.True(SortingValidHelper.IsAscending(
                        listPaginedData
                            .Where(x => x.Days.HasValue)
                            .Select(x => x.Days!.Value).ToList()));
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(nameof(NotFoundObjectResult)))
                {
                    Assert.Contains(typeof(NotFoundObjectResult).ToString(), ex.Message);
                }
                else if (ex.Message.Contains(nameof(BadRequestObjectResult)))
                {
                    Assert.Contains(typeof(BadRequestObjectResult).ToString(), ex.Message);
                }

                Assert.True(false, $"An Unexpected Error Occured: {ex.Message}");
                Console.WriteLine($"An Unexpected Error Occured: {ex.Message}");
            }
            finally
            {
                await Task.CompletedTask;
            }
        }


        // Summary:
        //      Sort training program
        [Theory]
        [InlineData("-programname", 1, "10")]
        [InlineData("+programname", 1, "10")]
        [InlineData("-createdon", 1, "10")]
        [InlineData("+createdon", 1, "10")]
        [InlineData("-createdby", 1, "10")]
        [InlineData("+createdby", 1, "10")]
        [InlineData("-status", 1, "10")]
        [InlineData("+status", 1, "10")]
        [InlineData("-duration", 1, "10")]
        [InlineData("+duration", 1, "10")]
        public async Task ReturnAViewResult_SortTrainingProgramAsync(
            string sort, int _page = 1, string _perPage = "")
        {
            var result = await _controller.SortingAsync(sort, _page, _perPage);

            // Assert
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var baseResponse = Assert.IsType<BaseResponse>(objectResult.Value);
            var entitiesByPage = Assert.IsType<EntitiesByPage<TrainingProgram>>(baseResponse.Data);

            // Success Status Code
            Assert.Equal(200, baseResponse.StatusCode);

            // Get all training programs 
            var expectedData = await _repository.GetAllAsync();

            // Sorting 
            if (!string.IsNullOrEmpty(sort))
            {
                expectedData = SortingHelper.SortByColumn(expectedData, sort).ToList();
            }

            // Map expected result to List<TraininingProgramDto>
            var expectedDataList = Assert.IsType<List<TrainingProgramDto>>(
                _mapper.Map<List<TrainingProgramDto>>(expectedData.ToList()));
            // Map actual result to List<TraininingProgramDto>
            var actualDataList = Assert.IsType<List<TrainingProgramDto>>(
                _mapper.Map<List<TrainingProgramDto>>(entitiesByPage.List));

            // Paging 
            /// NOTE: Conflict method - if your method is not a asynchronous method
            /// Please do not naming its with Async at the end
            var pagingExpectedData = PaginationHelper.PaginationAsync(
                // Page index
                _page,
                // List of training program
                expectedDataList,
                // Total item in a page
                Convert.ToInt32(_perPage)
            ); // Convert result to List<TrainingProgramDto>

            // Check total page
            var pageSize = Convert.ToInt32(_perPage);
            var totalItems = expectedDataList.Count;

            // Must be correct total page
            Assert.Equal(pagingExpectedData.TotalPage,
                // No page generate for remaining item
                totalItems % pageSize == 0
                    // Total page must be integer
                    ? totalItems / pageSize
                    // Real number (double, float...)
                    : (int)Math.Ceiling((double)totalItems / pageSize)); // Ceiling and convert to Int


            // Check sorting
            // List of pagined data
            var listPaginedData = pagingExpectedData.List.ToList();
            if (sort is "+programname") // Check valid sorting by descending program name
            {
                // Assert True
                Assert.True(SortingValidHelper.IsAscending(
                    listPaginedData.Select(x => x.Name).ToList()));
            }
            else if (sort is "-programname")
            {
                // Assert True
                Assert.True(SortingValidHelper.IsDescending(
                    listPaginedData.Select(x => x.Name).ToList()));
            }
            else if (sort is "+createon") // Check valid sorting by ascending create date
            {
                // Assert True
                Assert.True(SortingValidHelper.IsAscending(
                    listPaginedData
                        .Where(x => x.CreatedDate.HasValue)
                        .Select(x => x.CreatedDate!.Value).ToList()));
            }
            else if (sort is "-createon") // Check valid sorting by ascending create date
            {
                // Assert True
                Assert.True(SortingValidHelper.IsDescending(
                    listPaginedData
                        .Where(x => x.CreatedDate.HasValue)
                        .Select(x => x.CreatedDate!.Value).ToList()));
            }
            else if (sort is "+createdby") // Check valid sorting by ascending create date
            {
                // Assert True
                Assert.True(SortingValidHelper.IsAscending(
                    listPaginedData
                        .Where(x => x.CreatedBy != null)
                        .Select(x => x.CreatedBy!).ToList()));
            }
            else if (sort is "-createdby") // Check valid sorting by ascending create date
            {
                // Assert True
                Assert.True(SortingValidHelper.IsDescending(
                    listPaginedData
                        .Where(x => x.CreatedBy != null)
                        .Select(x => x.CreatedBy!).ToList()));
            }
            else if (sort is "+duration") // Check valid sorting by ascending days
            {
                // Assert True
                Assert.True(SortingValidHelper.IsAscending(
                    listPaginedData
                        .Where(x => x.Days.HasValue)
                        .Select(x => x.Days!.Value).ToList()));
            }
            else if (sort is "-duration") // Check valid sorting by ascending days
            {
                // Assert True
                Assert.True(SortingValidHelper.IsDescending(
                    listPaginedData
                        .Where(x => x.Days.HasValue)
                        .Select(x => x.Days!.Value).ToList()));
            }
            else if (sort is "+status") // Check valid sorting by ascending days
            {
                // Assert True
                Assert.True(SortingValidHelper.IsAscending(
                    listPaginedData
                        .Where(x => x.Status != null)
                        .Select(x => x.Status).ToList()));
            }
            else if (sort is "-status") // Check valid sorting by ascending days
            {
                // Assert True
                Assert.True(SortingValidHelper.IsDescending(
                    listPaginedData
                        .Where(x => x.Status != null)
                        .Select(x => x.Status).ToList()));
            }
        }



        // Summary:
        //      Create training program
        /// NOTE: This class can't be initiated from utils class, because MemberData just recieve method name
        public static TheoryData<CreateTrainingProgramRequest> CreateInitialRequestData()
        {
            return new TheoryData<CreateTrainingProgramRequest>
            {
                new CreateTrainingProgramRequest
                {
                    CreatedBy = "Warrior Tran",
                    CreatedDate = DateTime.Parse("2023-03-18"),
                    UpdatedBy = "",
                    UpdatedDate = null,
                    Days = 5,
                    Hours = 20,
                    StartTime = TimeOnly.Parse("09:00:00"),
                    Name = "Sample Training Program",
                    Status = "Active",
                },
            };
        }

        public static TheoryData<CreateTrainingProgramRequest> CreateInitialValidationRequestData()
        {
            return new TheoryData<CreateTrainingProgramRequest>
                {
                    new CreateTrainingProgramRequest
                    {
                        CreatedBy = "Warrior Tran",
                        // Not valid date time
                        CreatedDate = DateTime.Parse("2024-02-01"),
                        UpdatedBy = "",
                        UpdatedDate = null,
                        Days = 5,
                        Hours = 20,
                        // Not valid Time
                        StartTime = TimeOnly.Parse("12:00:00"),
                        // Program name not exist 
                        Name = "",
                        // Wrong Status
                        Status = "abc",
                    },
                };
        }

        [Theory]
        [MemberData(nameof(CreateInitialRequestData))]
        [MemberData(nameof(CreateInitialValidationRequestData))]
        public async Task ReturnAViewResult_CreateTraininingProgramAsync(
            CreateTrainingProgramRequest reqObj)
        {
            try
            {
                // Arrange
                // Act
                var result = await _controller.CreateAsync(reqObj);

                // Assert
                var badResult = result as BadRequestObjectResult;

                if (badResult != null && badResult.StatusCode == 400)
                {
                    var badRequestObjectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(result);
                    var failResponse = Assert.IsType<BaseResponse>(badRequestObjectResult.Value);
                    var errors = Assert.IsAssignableFrom<IDictionary<string, string[]>>(failResponse.Errors);

                    // Check validation
                    if (errors.TryGetValue("CreatedDate", out var errorMsg))
                    {
                        // Catch error success
                        Assert.True(true);
                    }
                    else if (errors.TryGetValue("StartTime", out errorMsg))
                    {
                        // Catch error success
                        Assert.True(true);
                    }
                    else if (errors.TryGetValue("Name", out errorMsg))
                    {
                        // Catch error success
                        Assert.True(true);
                    }

                    await Task.CompletedTask;
                    return;
                }
                else
                {

                    var objectResult = Assert.IsAssignableFrom<CreatedAtRouteResult>(result);
                    var actualData = Assert.IsType<TrainingProgramDto>(objectResult.Value);

                    // Map to training program dto
                    var trainingProgramDto = reqObj.ToTrainingProgramDto();
                    // Create instance of TrainingProgram to Testing Unique Generator
                    var trainingProgramEntity = new TrainingProgram
                    {
                        Name = trainingProgramDto.Name,
                        CreatedBy = trainingProgramDto.CreatedBy,
                        CreatedDate = trainingProgramDto.CreatedDate,
                        Days = trainingProgramDto.Days,
                        Hours = trainingProgramDto.Hours,
                        StartTime = trainingProgramDto.StartTime,
                        Status = trainingProgramDto.Status
                    };
                    // Assign to custom props
                    TrainingProgramCode = trainingProgramEntity.TrainingProgramCode;

                    // Create new memory data
                    await _repository.CreateAsync(trainingProgramEntity);

                    // Get lastest expected training program
                    var trainingPrograms = await _repository.GetAllAsync();
                    var expectedData = trainingPrograms
                        .OrderByDescending(x => x.Id)
                        .FirstOrDefault();

                    // Map expected result to List<TraininingProgramDto>
                    var expectedDataList = Assert.IsType<TrainingProgramDto>(
                        _mapper.Map<TrainingProgramDto>(expectedData));
                    // Map actual result to List<TraininingProgramDto>
                    var actualDataList = Assert.IsType<TrainingProgramDto>(
                        _mapper.Map<TrainingProgramDto>(actualData));

                    // Compare data
                    expectedDataList.AssertObjEquals(actualDataList);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(nameof(NotFoundObjectResult)))
                {
                    Assert.Contains(nameof(NotFoundObjectResult), ex.Message);
                }

                Assert.True(false, $"An Unexpected Error Occured: {ex.Message}");
                Console.WriteLine($"An Unexpected Error Occured: {ex.Message}");
            }
            finally
            {
                await Task.CompletedTask;
            }
        }


        // Summary:
        //      Duplicate Training Program
        public static TheoryData<DuplicateTrainingProgramRequest> DuplicateInitialRequestData()
        {
            return new TheoryData<DuplicateTrainingProgramRequest>
            {
                new DuplicateTrainingProgramRequest
                {
                    Code = "f6a9bff5-9e84-47de-9a4c-78fc0dc76bd4",
                    CreatedBy = "Warrior Tran"
                }
            };
        }

        [Theory]
        [MemberData(nameof(DuplicateInitialRequestData))]
        public async Task ReturnAViewResult_DuplicateTraningProgramAsync(DuplicateTrainingProgramRequest reqObj)
        {
            try
            {
                // Arrange
                // Act
                var result = await _controller.DuplicateProgramAsync(reqObj);

                // Assert
                var objectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
                // var baseResponse = Assert.IsType<BaseResponse>(objectResult.Value);

                // Assert.Equal(200, objectResult.);

                // Assump that data change 
                // Current data is 10, if duplicate successfully - 11
                var trainingPrograms = await _repository.GetAllAsync();
                Assert.Equal(12, trainingPrograms.Count());

                // Duplicate logic
                // Providing that successfully, its relationship also duplicate or not 
                var expectedDataList = await _repository.GetAllWithRelatedEntitiesAsync(
                    tp => tp.TrainingProgramSyllabi
                    // Other include here...
                );

                // Take order
                var expectedData = expectedDataList.OrderBy(x => x.Id).FirstOrDefault();

                // Get object being duplicated
                var actualDataList = await _repository.GetAllWithRelatedEntitiesAndConditionAsync(
                    // Condition
                    x => x.TrainingProgramCode == reqObj.Code,
                    // Include syllabi
                    x => x.TrainingProgramSyllabi
                );
                var actualData = actualDataList.FirstOrDefault();

                // Compare data list
                Assert.NotNull(expectedData);
                Assert.NotNull(actualData);
                Assert.Equal(expectedData.TrainingProgramSyllabi.Count(), actualData.TrainingProgramSyllabi.Count());


                // Get expected syllabi
                var expectedSyllabi = expectedData.TrainingProgramSyllabi.Select(x => x.Syllabus).ToList();
                // Get actual syllabi
                var actualSyllabi = actualData.TrainingProgramSyllabi.Select(x => x.Syllabus).ToList();

                // Map expected result to List<SyllabusDto>
                var expectedDataListDto = Assert.IsType<List<SyllabusDto>>(
                    _mapper.Map<List<SyllabusDto>>(expectedSyllabi));
                // Map actual result to List<SyllabusDto>
                var actualDataListDto = Assert.IsType<List<SyllabusDto>>(
                    _mapper.Map<List<SyllabusDto>>(actualSyllabi));

                // Compare data list
                for (int i = 0; i < expectedDataListDto.Count; ++i)
                {
                    expectedDataListDto[i].AssertObjEquals(actualDataListDto[i]);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(nameof(NotFoundObjectResult)))
                {
                    Assert.Contains(nameof(NotFoundObjectResult), ex.Message);
                }
                else if (ex.Message.Contains(nameof(BadRequestObjectResult)))
                {
                    Assert.Contains(nameof(BadRequestObjectResult), ex.Message);
                }

                Assert.True(false, $"An Unexpected Error Occured: {ex.Message}");
                Console.WriteLine($"An Unexpected Error Occured: {ex.Message}");
            }
        }


        //  Summary:
        //      Delete Training Program
        [Theory]
        [InlineData("31968dc0-0400-4fa3-9303-f8f19a427f86")] // Delete existing element
        public async Task ReturnAViewResult_DeleteTrainingProgramAsync(string code)
        {
            try
            {
                // Arrange 
                // Act
                var result = await _controller.DeleteTrainingProgramByCode(code);
                // Delete Created Training Program (if any)
                if (!string.IsNullOrEmpty(TrainingProgramCode))
                {
                    var deleteCreatedResult = await _controller.DeleteTrainingProgramByCode(TrainingProgramCode);

                    var statusCodeResult = deleteCreatedResult as OkObjectResult;
                    Assert.True(statusCodeResult != null && statusCodeResult.StatusCode == 200);
                }

                // Assert
                var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
                Assert.True(true); // Mark that no exception cause when assign result to OkObjectResult - 200


                // Assump current total data
                var trainingPrograms = await _repository.GetAllAsync();
                trainingPrograms = await _repository.GetAllAsync();
                Assert.Equal(10, trainingPrograms.Count());
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(nameof(NotFoundObjectResult)))
                {
                    Assert.Contains(nameof(NotFoundObjectResult), ex.Message);
                }
                else if (ex.Message.Contains(nameof(BadRequestObjectResult)))
                {
                    Assert.Contains(nameof(BadRequestObjectResult), ex.Message);
                }

                Assert.True(false, $"An Unexpected Error Occured: {ex.Message}");
                Console.WriteLine($"An Unexpected Error Occured: {ex.Message}");
            }
        }

        // Summary:
        //      Update Training Program
        public static TheoryData<UpdateTrainingProgramRequest> UpdateInitialRequestData()
        {
            return new TheoryData<UpdateTrainingProgramRequest>
            {
                new UpdateTrainingProgramRequest
                {
                    Days = 10,
                    // Id = 1,
                    Name = "It's gonna updated successfully",
                    Status = "Draft",
                    UpdatedBy = "Le Xuan Phuoc",
                    TrainingProgramCode = "f6a9bff5-9e84-47de-9a4c-78fc0dc76bd4",
                    // Delete 1 syllabus from trainnig program code 
                    SyllabiIDs = new(){"eb39c944-d924-4304-8d61-c06729b5de3e", "e470aa85-c2d5-49e6-9741-bf093d923856"}
                }
            };
        }

        // Summary:
        //      Update Training Program
        [Theory]
        [MemberData(nameof(UpdateInitialRequestData))]
        public async Task ReturnAView_UpdateTrainingProgramAsync(UpdateTrainingProgramRequest reqObj)
        {
            /// NOTE: Handle Transaction ignore in DbContext Configuration
            try
            {
                // Arrange
                // Act
                var result = await _controller.UpdateProgramAsync(reqObj);

                // Assert
                var badResult = result as BadRequestObjectResult;

                if (badResult != null && badResult.StatusCode == 400)
                {
                    var badRequestObjectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(result);
                    var failResponse = Assert.IsType<BaseResponse>(badRequestObjectResult.Value);
                    var errors = Assert.IsAssignableFrom<IDictionary<string, string[]>>(failResponse.Errors);

                    // Check validation
                    if (errors.TryGetValue("CreatedDate", out var errorMsg))
                    {
                        // Catch error success
                        Assert.True(true);
                    }
                    else if (errors.TryGetValue("StartTime", out errorMsg))
                    {
                        // Catch error success
                        Assert.True(true);
                    }
                    else if (errors.TryGetValue("Name", out errorMsg))
                    {
                        // Catch error success
                        Assert.True(true);
                    }

                    await Task.CompletedTask;
                    return;
                }
                else
                {
                    // Proccess testing update 
                    var redirectObjectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
                    // Assert.Equal("GetByCodeAsync", redirectObjectResult.ActionName);
                    // Assert.Equal("GetByCodeAsync", redirectObjectResult.ControllerName);
                    // var baseResponse = Assert.IsType<BaseResponse>(redirectObjectResult.RouteValues);
                    // Assert.Equal(200, baseResponse.StatusCode);

                    // Check total syllabi
                    var getByCodeResult = await _controller.GetByCodeAsync(reqObj.TrainingProgramCode!);
                    var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(getByCodeResult);
                    var baseResponse = Assert.IsType<BaseResponse>(okObjectResult.Value);

                    // Convert to TrainingProgramDto
                    var trainingProgramDto = Assert.IsType<TrainingProgramDto>(baseResponse.Data);

                    // Assump total syllabi - mark that update syllabi successfully
                    Assert.Equal(2, trainingProgramDto.Syllabi.Count);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(nameof(NotFoundObjectResult)))
                {
                    Assert.Contains(nameof(NotFoundObjectResult), ex.Message);
                }
                else if (ex.Message.Contains(nameof(BadRequestObjectResult)))
                {
                    Assert.Contains(nameof(BadRequestObjectResult), ex.Message);
                }

                Assert.True(false, $"An Unexpected Error Occured: {ex.Message}");
                Console.WriteLine($"An Unexpected Error Occured: {ex.Message}");
            }
        }


        // Summary:
        //      Update status training program
        [Theory]
        [InlineData("f6a9bff5-9e84-47de-9a4c-78fc0dc76bd4")]
        public async Task ReturnAViewResult_UpdateStatusTraininingProgramAsync(string code)
        {
            try
            {
                // Arrange
                // Get by code 
                var trainingProgram = await _repository.GetByIdAsync(code);
                // Act
                var result = await _controller.UpdateStatusAsync(code);

                // Assert
                var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
                var baseResponse = Assert.IsType<BaseResponse>(objectResult.Value);

                Assert.Equal(200, baseResponse.StatusCode);

                // Get actual training program
                var actualTrainingProgram = _repository.GetByIdAsync(code);

                // Check status change
                Assert.True(!trainingProgram!.Status.Equals(actualTrainingProgram.Status));
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(nameof(NotFoundObjectResult)))
                {
                    Assert.Contains(nameof(NotFoundObjectResult), ex.Message);
                }
                else if (ex.Message.Contains(nameof(BadRequestObjectResult)))
                {
                    Assert.Contains(nameof(BadRequestObjectResult), ex.Message);
                }

                Assert.True(false, $"An Unexpected Error Occured: {ex.Message}");
                Console.WriteLine($"An Unexpected Error Occured: {ex.Message}");
            }
        }



        // Summary:
        //      Import valid file
        [Fact]
        public async Task ReturnAViewResult_ImportValidFileAsync()
        {
            try
            {
                // Arrange
                var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var filePath = Path.Combine(assemblyDirectory!, "import-trainingprogram.csv");
                var file = FileHelper.ConvertToIFormFile(filePath);
                var test = await _repository.GetAllAsync();
                // Act
                var result = await _controller.UploadFile(new UploadFile.UploadFileRequest
                {
                    File = file,
                    EncodingType = "UTF8",
                    ColumnSeperator = ",",
                    Scanning = "programname",
                    CreatedBy = "Warior Tran",
                    DuplicateHandle = DuplicateHandleProgram.Allow
                });

                // Assert
                var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
                var baseResponse = Assert.IsType<BaseResponse>(objectResult.Value);

                Assert.Equal(200, baseResponse.StatusCode);

                // Assump total current record
                var trainingPrograms = await _repository.GetAllWithRelatedEntitiesAsync(
                    x => x.TrainingProgramSyllabi
                );

                // Previous data exist 11 record - Import more 4 record -> current: 15
                Assert.Equal(15, trainingPrograms.Count());

                // Check syllabus added
                // Get 3 latest import training program
                trainingPrograms = trainingPrograms
                    // Order Id descending
                    .OrderByDescending(x => x.Id)
                    // Take 3 from all
                    .Take(3)
                    // Convert to List<TrainingProgram>
                    .ToList();

                // Each of them must offer at least 2 syllabus 
                trainingPrograms.Select(x =>
                {
                    Assert.True(x.TrainingProgramSyllabi.Count() == 2);
                    return true;
                });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(nameof(NotFoundObjectResult)))
                {
                    Assert.Contains(nameof(NotFoundObjectResult), ex.Message);
                }
                else if (ex.Message.Contains(nameof(BadRequestObjectResult)))
                {
                    Assert.Contains(nameof(BadRequestObjectResult), ex.Message);
                }
                else if (ex.Message.Contains(nameof(StatusCodeResult)))
                {
                    Assert.Contains(nameof(StatusCodeResult), ex.Message);
                }

                Assert.True(false, $"An Unexpected Error Occured: {ex.Message}");
                Console.WriteLine($"An Unexpected Error Occured: {ex.Message}");
            }
        }


        // Summary:
        //      Import invalid file
        public static TheoryData<UploadFile.UploadFileRequest> InValidUploadFileInitialRequestData()
        {
            return new TheoryData<UploadFile.UploadFileRequest>
            {
                new UploadFile.UploadFileRequest
                {
                    Scanning = "programname,programid",
                    EncodingType = "UTF8",
                    ColumnSeperator = ",",
                    CreatedBy = "Warior Tran"
                }
            };
        }

        [Theory]
        [MemberData(nameof(InValidUploadFileInitialRequestData))]
        public async Task ReturnAViewResult_ImportInvalidFileAsync(UploadFile.UploadFileRequest reqObj)
        {
            try
            {
                // Arrange
                var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var filePath = Path.Combine(assemblyDirectory!, "import-trainingprogram.csv");
                var file = FileHelper.ConvertToIFormFile(filePath);
                // Act
                reqObj.File = file;
                var result = await _controller.UploadFile(reqObj);

                // Assert
                var badResult = result as BadRequestObjectResult;
                var statusCodeResult = result as StatusCodeResult;
                var objectResult = result as ObjectResult;

                if (badResult != null && badResult.StatusCode == 400)
                {
                    var badRequestObjectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(result);
                    var failResponse = Assert.IsType<BaseResponse>(badRequestObjectResult.Value);

                    Console.WriteLine($"Fail message: {failResponse.Message}");
                    return;
                }
                else if (statusCodeResult != null && statusCodeResult.StatusCode == 409)
                {
                    var conflictResult = Assert.IsAssignableFrom<StatusCodeResult>(result);
                    Assert.Equal(StatusCodes.Status409Conflict, conflictResult.StatusCode);
                }
                else if (objectResult != null)
                {
                    var baseResponse = Assert.IsType<BaseResponse>(objectResult.Value);
                    Assert.Contains("duplicated", baseResponse.Message);
                    Assert.Contains("31968dc0-0400-4fa3-9303-f8f19a427f86", baseResponse.Message);
                    Assert.Equal(StatusCodes.Status409Conflict, baseResponse.StatusCode);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(nameof(NotFoundObjectResult)))
                {
                    Assert.Contains(nameof(NotFoundObjectResult), ex.Message);
                }
                else if (ex.Message.Contains(nameof(BadRequestObjectResult)))
                {
                    Assert.Contains(nameof(BadRequestObjectResult), ex.Message);
                }
                else if (ex.Message.Contains(nameof(StatusCodeResult)))
                {
                    Assert.Contains(nameof(StatusCodeResult), ex.Message);
                }

                Assert.True(false, $"An Unexpected Error Occured: {ex.Message}");
                Console.WriteLine($"An Unexpected Error Occured: {ex.Message}");
            }
        }

    }
}