using ClassManagementAPI.Controllers;
using ClassManagementAPI.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FakeItEasy;
using Nest;
using AutoMapper;
using ClassManagementAPI.Dto;
using ClassManagementAPI.Dto.ClassDTO;
using ClassManagementAPI.Interface;

namespace ClassManagementAPI.Tests.Controllers
{
    public class ClassControllerTests
    {
        private readonly ILogger<ClassController> _logger;
        private readonly IElasticClient _client;
        private readonly IMapper _mapper;
        private readonly IClassRepository _classRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IClassUserRepository _classUserRepository;

        public ClassControllerTests()
        {
            _logger = A.Fake<ILogger<ClassController>>();
            _client = A.Fake<IElasticClient>();
            _mapper = A.Fake<IMapper>();
            _classRepository = A.Fake<IClassRepository>();
            _locationRepository = A.Fake<ILocationRepository>();
            _userRepository = A.Fake<IUserRepository>();
            _classUserRepository = A.Fake<IClassUserRepository>();
        }

        [Fact]
        public async Task ClassController_Search_ReturnsOk()
        {
            // Arrange
            var controller = new ClassController(_logger, _client, _mapper, _classRepository,
                _locationRepository, _userRepository, _classUserRepository);
            var keyword = "a";

            var expectedResults = new List<Class>();

            var fakeResponse = A.Fake<ISearchResponse<Class>>();
            A.CallTo(() => fakeResponse.Documents).Returns(expectedResults);

            A.CallTo(() => _client.SearchAsync<Class>(A<Func<SearchDescriptor<Class>, ISearchRequest>>._, A<CancellationToken>._))
                .Returns(Task.FromResult(fakeResponse));

            // Act
            var result = await controller.Search(keyword) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            var responseDto = result.Value as ResponseDto;
            responseDto.Should().NotBeNull();
            responseDto.Data.Should().BeEquivalentTo(new { totalCount = expectedResults.Count, items = expectedResults });
        }


        [Fact]
        public async Task GetClassesListByFilter_ReturnsOkResult_WhenFilterDataIsValid()
        {
            // Arrange
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

            var expectedClass = new Class
            {
                ClassId = "0",
                CreatedBy = "Hung",
                CreatedDate = DateTime.Parse("2024-01-29T09:50:57.687"),
                UpdatedBy = "Long",
                ClassStatus = "Plaining",
                ClassCode = "NET_02",
                Duration = 4,
                StartDate = DateOnly.Parse("2021-12-08"),
                EndDate = DateOnly.Parse("2021-12-28"),
                FsuId = "1",
                LocationId = "3",
                AttendeeLevelId = "1",
                TrainingProgramCode = "3",
                SlotTime = "Morning"
            };

            var expectedClasses = new PagedResult<Class>
            {
                TotalCount = 1,
                Items = new List<Class> { expectedClass }
            };

            var expectClassesDto = _mapper.Map<PagedResult<GetClassDto>>(expectedClasses);
            A.CallTo(() => _classRepository.GetClassListByFilter(filterData)).Returns(expectedClasses);

            var controller = new ClassController(_logger, _client, _mapper, _classRepository, 
                _locationRepository, _userRepository, _classUserRepository);

            // Act
            var result = await controller.GetClassesListByFilter(filterData);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;
            var responseDto = okResult.Value as ResponseDto;
            responseDto.Success.Should().BeTrue();
            responseDto.Message.Should().Be("Operation successful");
            responseDto.StatusCode.Should().Be(200);
            responseDto.Data.Should().BeEquivalentTo(expectClassesDto);
        }

    }
}