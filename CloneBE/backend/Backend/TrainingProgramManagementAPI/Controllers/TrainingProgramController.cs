using static TrainingProgramManagementAPI.Payloads.Requests.UploadFile;
using CsvHelper;
using CsvHelper.Configuration;
using TrainingProgramManagementAPI.Common.Enums;
using TrainingProgramManagementAPI.Services;
using Entities.Context;
using Entities.Models;

namespace TrainingProgramManagementAPI.Controllers
{
    [ApiController]
    public class TrainingProgramController : ControllerBase
    {
        private readonly FamsContext _context;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IS3Service _s3Service;
        private readonly IFirebaseService _firebaseService;
        private readonly FirebaseCredentials _fbCredentials;

        public TrainingProgramController(
            FamsContext context,
            IMapper mapper,
            IOptionsMonitor<AppSettings> monitor,
            IOptionsMonitor<FirebaseCredentials> monitor1,
            IS3Service s3Service,
            IFirebaseService firebaseService)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = monitor.CurrentValue;
            _fbCredentials = monitor1.CurrentValue;
            _s3Service = s3Service;
            _firebaseService = firebaseService;
        }



        /// <summary>
        /// Get All training program
        /// </summary>
        /// <Param name="_page">Current page selected</Param>
        /// <param name="_perPage">Number row of page</param>
        /// <returns></returns>
        [HttpGet(APIRoutes.TrainingProgram.GetAll, Name = nameof(GetAllAsync))] // API Endpoints
        public async Task<IActionResult> GetAllAsync([FromQuery] int _page = 1, [FromQuery] string _perPage = "")
        {
            // Get all training program entities
            var trainingProgramDtos =
                // Mapping to IEnumerable<TrainingProgramDto>
                _mapper.Map<IEnumerable<TrainingProgramDto>>(
                    await _context.TrainingPrograms.OrderByDescending(x => x.Id).ToListAsync());
            // Try Parse _perPage into pageSize
            int pageSize;
            //  Get the list based on pagination
            var paginationResult = PaginationHelper.PaginationAsync(_page, trainingProgramDtos, Int32.TryParse(_perPage, out pageSize) ? pageSize : _appSettings.PageSize);
            return !paginationResult.List.Any() // Not found any training program
                                                // response NOT FOUND with base obj detail
                ? NotFound(new BaseResponse()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Not found any training program",
                    IsSuccess = false
                })
                // response OK with base obj detail
                : Ok(new BaseResponse()
                {
                    StatusCode = StatusCodes.Status200OK,
                    Data = paginationResult,
                    IsSuccess = true
                });
        }



        /// <summary>
        /// Get Training program by code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet(APIRoutes.TrainingProgram.GetByCode, Name = nameof(GetByCodeAsync))] // API Endpoints
        public async Task<IActionResult> GetByCodeAsync([FromRoute] string code)
        {
            // Get training program by code 
            // Mapping to TrainingProgramDto
            var trainingProgramDto = _mapper.Map<TrainingProgramDto>(
                await _context.TrainingPrograms
                    // .Include(x => x.Syllabi)
                    .FirstOrDefaultAsync(x => x.TrainingProgramCode.Equals(code))
            );

            // Not exist 
            if (trainingProgramDto is null)
            {
                return NotFound(new BaseResponse
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Not found any training program match code {code}",
                    IsSuccess = false
                });
            }

            // Attach syllabi (if any)
            var syllabiIds = await _context
                // Select from TrainingProgramSyllabi (intermediate table)
                .TrainingProgramSyllabi
                // With condition by: {TrainingProgramCode}
                .Where(x => x.TrainingProgramCode.Equals(code))
                // Select Syllabus id
                .Select(ts => ts.SyllabusId)
                // Get list of syllabus id
                .ToListAsync();

            // Check exist syllabus 
            if (syllabiIds.Any())
            {
                // Load syllabi with all related entities using eager loading
                var syllabi = await _context.Syllabi
                .Where(x => syllabiIds.Contains(x.SyllabusId))
                // Building query with relational table
                .Include(x => x.SyllabusDays) // Include all existing syllabus days 
                                              // Then include all exist units from days
                    .ThenInclude(x => x.SyllabusUnits)
                        // Then inlcude all exist chapters from unit
                        .ThenInclude(x => x.UnitChapters)
                            // Then include all exist training materials 
                            // from unit chapters
                            .ThenInclude(x => x.TrainingMaterials)
                // Get list of syllabus
                .ToListAsync();

                // Load output standards and delivery types in batches
                var outputStandardIds = syllabi
                    .SelectMany(syllabus => syllabus.SyllabusDays)
                    .SelectMany(day => day.SyllabusUnits)
                    .SelectMany(unit => unit.UnitChapters)
                    .Select(chapter => chapter.OutputStandardId)
                    .Distinct()
                    .ToList();

                var deliveryTypeIds = syllabi
                    .SelectMany(syllabus => syllabus.SyllabusDays)
                    .SelectMany(day => day.SyllabusUnits)
                    .SelectMany(unit => unit.UnitChapters)
                    .Select(chapter => chapter.DeliveryTypeId)
                    .Distinct()
                    .ToList();

                // output standards and delivery types
                var outputStandards = await _context.OutputStandards
                    .Where(x => outputStandardIds.Contains(x.OutputStandardId))
                    .ToDictionaryAsync(x => x.OutputStandardId);

                var deliveryTypes = await _context.DeliveryTypes
                    .Where(x => deliveryTypeIds.Contains(x.DeliveryTypeId))
                    .ToDictionaryAsync(x => x.DeliveryTypeId);

                // Assign output standards and delivery types to unit chapters
                foreach (var syllabus in syllabi)
                {
                    foreach (var day in syllabus.SyllabusDays)
                    {
                        foreach (var unit in day.SyllabusUnits)
                        {
                            foreach (var chapter in unit.UnitChapters)
                            {
                                if (outputStandards.TryGetValue(chapter.OutputStandardId!, out var outputStandard))
                                {
                                    chapter.OutputStandard = outputStandard;
                                }

                                if (deliveryTypes.TryGetValue(chapter.DeliveryTypeId!, out var deliveryType))
                                {
                                    chapter.DeliveryType = deliveryType;
                                }
                            }
                        }
                    }
                }

                // Map and assign to training program dto 
                trainingProgramDto.Syllabi = _mapper.Map<List<SyllabusDto>>(syllabi);
            }


            return Ok(new BaseResponse()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = trainingProgramDto,
                IsSuccess = true
            });
        }



        [HttpPatch(APIRoutes.TrainingProgram.GetByCode, Name = nameof(UpdateStatusAsync))]
        public async Task<IActionResult> UpdateStatusAsync([FromRoute] string code)
        {
            // Get training program by code 
            // Mapping to TrainingProgramDto
            var trainingProgram = await _context.TrainingPrograms
                    // .Include(x => x.Syllabi)
                    .FirstOrDefaultAsync(x => x.TrainingProgramCode.Equals(code));
            if (trainingProgram != null)
            {
                if (trainingProgram.Status.Equals(nameof(TrainingProgramStatus.Active))) trainingProgram.Status = nameof(TrainingProgramStatus.InActive);
                else if (trainingProgram.Status.Equals(nameof(TrainingProgramStatus.InActive))) trainingProgram.Status = nameof(TrainingProgramStatus.Active);
                else
                {
                    return BadRequest(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = $"Training program with code {code}, invalid status value. Only 'active' or 'inactive' are accepted.",
                        IsSuccess = false
                    });
                }
                await _context.SaveChangesAsync();
                return Ok(new BaseResponse()
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Change Status Succesfully.",
                    IsSuccess = true
                });
            }
            return NotFound(new BaseResponse
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = $"Training program with code {code} not found.",
                IsSuccess = false
            });
        }



        /// <summary>
        /// Search Training Program
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        [HttpGet(APIRoutes.TrainingProgram.Search, Name = nameof(SearchTrainingProgramAsync))] // API Endpoints
        public async Task<IActionResult> SearchTrainingProgramAsync([FromQuery] string? searchValue = "", [FromQuery] int _page = 1, [FromQuery] string _perPage = "", [FromQuery] string? sort = null)
        {
            // IQueryable<out T>: Get all training programs but not executed yet  
            var trainingPrograms = _context.TrainingPrograms.AsQueryable();

            // execute query
            var result = await SearchHelper.SearchByPatternAsync(trainingPrograms, searchValue, _appSettings);
            if (!string.IsNullOrEmpty(sort))
            {
                result = SortingHelper.SortByColumn(result, sort).ToList();
            }

            // Try Parse _perPage into pageSize
            int pageSize;
            //  Get the list based on pagination
            var paginationResult = PaginationHelper.PaginationAsync(_page, result, Int32.TryParse(_perPage, out pageSize) ? pageSize : _appSettings.PageSize);

            // response
            return paginationResult.List.Any()
                // return result with mapping to IEnumerable<TrainingProgramDto>
                ? Ok(new BaseResponse() { StatusCode = StatusCodes.Status200OK, Data = paginationResult })
                // Not found
                : NotFound(new BaseResponse() { StatusCode = StatusCodes.Status404NotFound, Message = "Not found any result" });
        }



        /// <summary>
        /// Get All training program with optional sorting
        /// </summary>
        /// <param name="sort">Sort order (asc or desc) and Field to sort by</param>
        /// <param name="currentPage">Current page selected</param>
        /// <param name="pageSize">Number row of page</param>
        /// <returns></returns>
        [HttpGet(APIRoutes.TrainingProgram.Sorting, Name = nameof(SortingAsync))] // API Endpoints
        public async Task<IActionResult> SortingAsync([FromQuery] string sort, [FromQuery] int _page = 1, [FromQuery] string _perPage = "")
        {
            // Get all training program entities from the database
            var trainingProgramsQuery = _context.TrainingPrograms.AsQueryable();
            // Project to IQueryable<TrainingProgramDto> using AutoMapper's ProjectTo
            //var trainingProgramsDtoQuery = _mapper.ProjectTo<TrainingProgramDto>(trainingProgramsQuery);
            if (string.IsNullOrEmpty(sort))
            {
                return BadRequest(new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "The sort field is required.",
                    IsSuccess = false
                });
            }
            var result = await trainingProgramsQuery.ToListAsync();
            //Sort
            var sortList = SortingHelper.SortByColumn(result, sort);
            // Try Parse _perPage into pageSize
            int pageSize;

            // Perform pagination
            var paginatedTrainingPrograms = PaginationHelper.PaginationAsync(_page, sortList, Int32.TryParse(_perPage, out pageSize) ? pageSize : _appSettings.PageSize);

            return !paginatedTrainingPrograms.List.Any() // Not found any training program
            ? NotFound(new BaseResponse
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = "Some thing went wrong when sorting!",
                IsSuccess = false
            })
            : Ok(new BaseResponse
            {
                StatusCode = StatusCodes.Status200OK,
                Data = paginatedTrainingPrograms,
                IsSuccess = true
            });
        }



        /// <summary>
        /// Filter training program
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="ProgramTimeFrameFrom"></param>
        /// <param name="ProgramTimeFrameTo"></param>
        /// <param name="_page"></param>
        /// <param name="_perPage"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        [HttpGet(APIRoutes.TrainingProgram.Filter, Name = nameof(FilterAsync))] // API Endpoints
        public async Task<IActionResult> FilterAsync(
             [FromQuery(Name = "Status")] string[]? Status = null,
             [FromQuery(Name = "CreatedBy")] string CreatedBy = "",
             [FromQuery(Name = "ProgramTimeFrameFrom")] DateTime? ProgramTimeFrameFrom = null,
             [FromQuery(Name = "ProgramTimeFrameTo")] DateTime? ProgramTimeFrameTo = null,
             [FromQuery(Name = "_page")] int _page = 1,
             [FromQuery(Name = "_perPage")] string _perPage = "",
             [FromQuery] string? sort = null)
        {
            //  check if the time from -> to is null
            if (ProgramTimeFrameFrom.HasValue && ProgramTimeFrameTo.HasValue)
            {
                //check if the training program time frame from larger than training program time frame to 

                if (ProgramTimeFrameFrom.Value.CompareTo(ProgramTimeFrameTo.Value) > 0)
                    return BadRequest(new BaseResponse() // BadRequest 
                                                         // response BAD REQUEST with base obj detail
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = " Date 'To' must greater than Date 'From' "
                    });
            }

            var filterListQueryable = _context.TrainingPrograms.AsQueryable();
            var filterList = await FilterHelper.FilterByPattern(filterListQueryable,
                    Status, CreatedBy, ProgramTimeFrameFrom,
                    ProgramTimeFrameTo, _page, _perPage);

            if (!string.IsNullOrEmpty(sort))
            {
                filterList = SortingHelper.SortByColumn(filterList, sort).ToList();
            }

            //  Get the list based on pagination
            int pageSize;
            var paginationResult = PaginationHelper.PaginationAsync(_page, filterList, Int32.TryParse(_perPage, out pageSize) ? pageSize : _appSettings.PageSize);
            return !paginationResult.List.Any()// Not found any training program
                                               // response NOT FOUND with base obj detail
                    ? NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = $"Not found any training program match with filter",
                        IsSuccess = false
                    })
                    // response OK with base obj detail
                    : Ok(new BaseResponse()
                    {
                        Data = paginationResult,
                        IsSuccess = true,
                        StatusCode = StatusCodes.Status200OK
                    });
        }



        /// <summary>
        /// Create Training Program
        /// </summary>
        /// <param name="reqObj"></param>
        /// <returns></returns>
        [HttpPost(APIRoutes.TrainingProgram.Create, Name = nameof(CreateAsync))]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTrainingProgramRequest reqObj)
        {
            // Map request to TrainingProgramDto
            var trainingProgramDto = reqObj.ToTrainingProgramDto();
            // Validation
            var validationResult = await trainingProgramDto.ValidateAsync();
            if (validationResult is not null && !validationResult.IsValid)
            {
                return BadRequest(new BaseResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    IsSuccess = false,
                    Message = "Some thing went wrong",
                    Errors = validationResult.ToProblemDetails().Errors
                });
            }

            // Create new training program
            var trainingProgramEntity = new TrainingProgram
            {
                Name = trainingProgramDto.Name,
                CreatedBy = trainingProgramDto.CreatedBy,
                //CreatedBy = "Warrior Tran",
                CreatedDate = trainingProgramDto.CreatedDate,
                Days = trainingProgramDto.Days,
                Hours = trainingProgramDto.Hours,
                StartTime = trainingProgramDto.StartTime,
                Status = trainingProgramDto.Status
            };

            // Save change training program
            await _context.TrainingPrograms.AddAsync(trainingProgramEntity);
            var result = await _context.SaveChangesAsync() > 0;

            // Get created training program
            trainingProgramEntity = await _context.TrainingPrograms
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            //Add intermidate table (TrainingProgram - Syllabus)
            //Get syllabus by id
            var syllabi = new List<SyllabusDto>();
            foreach (var id in reqObj.SyllabiIDs)
            {
                var syllabus = await _context.Syllabi.Where(x =>
                    x.SyllabusId.Equals(id)).FirstOrDefaultAsync();

                //Call API GATEWAY to SyllabusManagementAPI
                // trainingProgramEntity.Syllabi.Add(syllabus!);
                if (syllabus is not null)
                {
                    var trainingProgramSyllabus = new TrainingProgramSyllabus
                    {
                        TrainingProgramCode = trainingProgramEntity!.TrainingProgramCode,
                        SyllabusId = syllabus.SyllabusId
                    };

                    await _context.TrainingProgramSyllabi.AddAsync(trainingProgramSyllabus);
                    // Save change add syllabi (if any)
                    result = await _context.SaveChangesAsync() > 0;
                }
            }

            // Check row effected
            if (result)
            {
                // get created training program
                trainingProgramEntity = await _context.TrainingPrograms
                            .Where(x => x.Name.Equals(trainingProgramEntity!.Name))
                            .OrderByDescending(x => x.CreatedDate)
                            .FirstOrDefaultAsync();
                // Remove all training program in each syllabus 
                // trainingProgramEntity!.Syllabi.Select(x => x.TrainingProgramCodes = null!);
            }

            return result // Create successfully
                          // Created and call back to get API
                ? CreatedAtRoute(nameof(GetByCodeAsync),
                    new { Code = trainingProgramEntity?.TrainingProgramCode },
                    _mapper.Map<TrainingProgramDto>(trainingProgramEntity))
                // 500 Error
                : StatusCode(StatusCodes.Status500InternalServerError);
        }



        /// <summary>
        /// Get all the training program created
        /// </summary>
        /// <returns></returns>
        [HttpGet(APIRoutes.TrainingProgram.GetALlAuthors, Name = nameof(GetAllAuthorsAsync))]
        public async Task<IActionResult> GetAllAuthorsAsync()
        {
            var authorsList = await _context.TrainingPrograms
            .Select(x => x.CreatedBy)
            .Distinct()
            .ToListAsync();
            return !authorsList.Any() // Not found any training program authors
                                      // response NOT FOUND with base obj detail
                   ? NotFound(new BaseResponse()
                   {
                       StatusCode = StatusCodes.Status404NotFound,
                       Message = "Not found any training program",
                       IsSuccess = false
                   })
                   // response OK with base obj detail
                   : Ok(new BaseResponse()
                   {
                       StatusCode = StatusCodes.Status200OK,
                       Data = authorsList,
                       IsSuccess = true
                   });
        }



        /// <summary>
        /// Delete the training program
        /// <param name="code">code of training program</param>
        /// </summary>
        /// <returns></returns>
        [HttpDelete(APIRoutes.TrainingProgram.Delete, Name = nameof(DeleteTrainingProgramByCode))]
        public async Task<IActionResult> DeleteTrainingProgramByCode([FromRoute] string code)
        {
            // get training program by code
            var record = await _context.TrainingPrograms.FirstOrDefaultAsync(x => x.TrainingProgramCode.Equals(code));
            // Delete the record
            // Check if the record is null
            if (record is null)
            {
                return // Not found training program
                       // response NOT FOUND with base obj detail
                    NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = $"Not found any training program code match {code}",
                        IsSuccess = false
                    });
            }
            // Remove training program syllabus 
            var trainingProgramSyllabi = await _context
                // From traininig program syllabus table
                .TrainingProgramSyllabi
                // Query with condition by: {TrainingProgramCode} 
                .Where(x => x.TrainingProgramCode.Equals(code))
                // Get list of training program syllabus
                .ToListAsync();

            // Remove all existing training program
            _context.TrainingProgramSyllabi.RemoveRange(trainingProgramSyllabi);
            // Remove training program
            _context.TrainingPrograms.Remove(record);

            return await _context.SaveChangesAsync() < 0
                ? BadRequest(new BaseResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = $"Problem deleting the training program with code match {code}",
                    IsSuccess = false
                })
                : Ok(new BaseResponse()
                {
                    StatusCode = StatusCodes.Status200OK,
                    IsSuccess = true
                });
        }



        /// <summary>
        /// Duplicate the training program
        /// <param name="code"> Code of training program</param>
        /// </summary>
        /// <returns></returns>
        [HttpPost(APIRoutes.TrainingProgram.Duplicate, Name = nameof(DuplicateProgramAsync))]
        public async Task<IActionResult> DuplicateProgramAsync([FromBody] DuplicateTrainingProgramRequest reqObj)
        {
            //check if code is empty
            if (string.IsNullOrEmpty(reqObj.Code))
            {
                return BadRequest(new BaseResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "The code of training program is empty",
                    IsSuccess = false
                });
            }

            // get training program based on code
            var originalProgram = await _context.TrainingPrograms
                        .FirstOrDefaultAsync(x => x.TrainingProgramCode.Equals(reqObj.Code));

            if (originalProgram is null)
            {
                return NotFound(new BaseResponse
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Not found any training program code match {reqObj.Code}"
                });
            }

            // Get all syllabus existing in training program
            var trainingProgramSyllabi = await _context.TrainingProgramSyllabi
                .Where(x => x.TrainingProgramCode.Equals(originalProgram.TrainingProgramCode))
                .ToListAsync();

            // Map to Create training program request to facilitate extension method
            var duplicateProgramDto = _mapper.Map<CreateTrainingProgramRequest>(originalProgram).ToTrainingProgramDto();

            // Create new instance instead of using mapper
            // To make use of Training Program constructors from testing or develop
            var duplicateProgram = new TrainingProgram
            {
                // Update properties
                Name = duplicateProgramDto.Name,
                CreatedBy = duplicateProgramDto.CreatedBy,
                CreatedDate = duplicateProgramDto.CreatedDate,
                Days = duplicateProgramDto.Days,
                Hours = duplicateProgramDto.Hours,
                StartTime = duplicateProgramDto.StartTime,
                Status = duplicateProgramDto.Status
            };

            // Check duplicateProgram is not null
            var result = false; // Flag that no query / change has been occured
            if (duplicateProgram is not null)
            {
                // create duplicate training program
                duplicateProgram.CreatedBy = reqObj.CreatedBy;
                duplicateProgram.CreatedDate = DateTime.UtcNow;
                duplicateProgram.UpdatedBy = null;
                duplicateProgram.UpdatedDate = null;
                duplicateProgram.Status = TrainingProgramStatus.Draft.ToString();
                await _context.TrainingPrograms.AddAsync(duplicateProgram);
                // Save change new training program
                result = await _context.SaveChangesAsync() > 0;
            }

            if (result) // Save change training program successfully
            {
                // get the duplicate program
                duplicateProgram = await _context.TrainingPrograms
                    .Where(x => x.Name.Equals(duplicateProgram!.Name))
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefaultAsync();

                // Adding list of syllabus (if any)
                var newTrainingProgramSyllabi = trainingProgramSyllabi.Select(x => new TrainingProgramSyllabus
                {
                    SyllabusId = x.SyllabusId,
                    TrainingProgramCode = duplicateProgram!.TrainingProgramCode
                });

                if (newTrainingProgramSyllabi.Any()) // If any entity be created
                {
                    // Add to DB 
                    await _context.TrainingProgramSyllabi.AddRangeAsync(newTrainingProgramSyllabi);
                    // Save change
                    result = await _context.SaveChangesAsync() > 0;
                }
            }

            return result // Create successfully
                          // Created and call back to get API
              ? RedirectToAction(nameof(GetByCodeAsync), new { Code = duplicateProgram!.TrainingProgramCode })
              // 500 Error
              : StatusCode(StatusCodes.Status500InternalServerError);
        }



        [HttpGet("api/trainingprograms/syllabi")]
        public async Task<IActionResult> Test()
        {
            return Ok(await _context.Syllabi.ToListAsync());
        }



        /// <summary>
        /// Update training program
        /// </summary>
        /// <param name="reqObj"></param>
        /// <returns></returns>
        [HttpPut(APIRoutes.TrainingProgram.Update, Name = nameof(UpdateProgramAsync))]
        public async Task<IActionResult> UpdateProgramAsync([FromBody] UpdateTrainingProgramRequest reqObj)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Map request to TrainingProgramDto
                var trainingProgramDto = reqObj.ToTrainingProgramDto();
                var SyllabusIds = reqObj.SyllabiIDs;
                // Validation
                var validationResult = await trainingProgramDto.ValidateAsync();
                if (validationResult is not null && !validationResult.IsValid)
                {
                    return BadRequest(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        IsSuccess = false,
                        Message = "Some thing went wrong",
                        Errors = validationResult.ToProblemDetails().Errors
                    });
                }

                // map dto to entity
                // var trainingProgramEntity = _mapper.Map<TrainingProgram>(trainingProgramDto);
                var trainingProgramEntity = await _context.TrainingPrograms.FirstOrDefaultAsync(x =>
                    x.TrainingProgramCode == reqObj.TrainingProgramCode);

                if (trainingProgramEntity is null)
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = $"Not found any training program match {reqObj.TrainingProgramCode}"
                    });
                }

                //Update intermidate table (TrainingProgram - TrainingProgramSyllabus)
                //update TrainingProgram
                //_context.TrainingPrograms.Attach(trainingProgramEntity);
                //_context.Entry(trainingProgramEntity).Property(x => x.UpdatedBy).IsModified = true;
                //_context.Entry(trainingProgramEntity).Property(x => x.UpdatedDate).IsModified = true;
                //_context.Entry(trainingProgramEntity).Property(x => x.Days).IsModified = true;
                //_context.Entry(trainingProgramEntity).Property(x => x.Name).IsModified = true;
                //_context.Entry(trainingProgramEntity).Property(x => x.Status).IsModified = true;
                trainingProgramEntity.UpdatedBy = reqObj.UpdatedBy;
                trainingProgramEntity.UpdatedDate = DateTime.UtcNow;
                trainingProgramEntity.Days = reqObj.Days;
                trainingProgramEntity.Name = reqObj.Name;
                trainingProgramEntity.Status = reqObj.Status;


                // Get all training program syllabus by code
                var trainingProgramSyllabiIds = await _context
                    // From training program syllabus
                    .TrainingProgramSyllabi
                    // Query with condition by : {TrainingProgramCode}
                    .Where(x => x.TrainingProgramCode.Equals(reqObj.TrainingProgramCode))
                    // Select syllabus id
                    .Select(x => x.SyllabusId)
                    // Convert to list of id
                    .ToListAsync();


                // Get all syllabus not existing in TrainingProgramSyllabus
                var newSyllabusIds = SyllabusIds.Where(x => !trainingProgramSyllabiIds.Contains(x)).ToList();
                // Get all syllabus remove from SyllabusIds
                var removeSyllabusIds = trainingProgramSyllabiIds.Where(x => !SyllabusIds.Contains(x)).ToList();

                if (newSyllabusIds.Any()) // Handle update new syllabus
                {
                    // Loop each id 
                    var addNewTask = newSyllabusIds.Select(async x =>
                    {
                        // Handle add new 
                        var trainingProgramSyllabus = new TrainingProgramSyllabus
                        {
                            // Assign new syllabus id
                            SyllabusId = x,
                            // Assign existing training program code
                            TrainingProgramCode = trainingProgramEntity.TrainingProgramCode
                        };
                        await _context.AddAsync(trainingProgramSyllabus);
                    });

                    // Add to handling task
                    await Task.WhenAll(addNewTask);
                }

                var removeSyllabusList = new List<TrainingProgramSyllabus>();
                if (removeSyllabusIds.Any()) // Handle remove syllabus
                {
                    foreach (var syllabusId in removeSyllabusIds)
                    {
                        // Get training program syllabus by SyllabusId
                        var trainingProgramSyllabus =
                            await _context.TrainingProgramSyllabi
                                .Where(tps => tps.SyllabusId.Equals(syllabusId))
                                .FirstOrDefaultAsync();

                        if (trainingProgramSyllabus != null)
                        {
                            // Set relational object to null
                            trainingProgramSyllabus.TrainingProgramCodeNavigation = null!;
                            trainingProgramSyllabus.Syllabus = null!;

                            // Handle add delete 
                            // _context.Remove(trainingProgramSyllabus);
                            removeSyllabusList.Add(trainingProgramSyllabus);
                        }
                    }

                    if (removeSyllabusList.Any())
                    {
                        _context.TrainingProgramSyllabi.RemoveRange(removeSyllabusList);
                    }
                }

                var result = await _context.SaveChangesAsync() > 0;
                if (result)
                {
                    await transaction.CommitAsync();

                    // Redirect to GetByCodeAsync func
                    // return RedirectToAction(nameof(GetByCodeAsync),new { Code = trainingProgramEntity.TrainingProgramCode });
                    return await GetByCodeAsync(trainingProgramEntity.TrainingProgramCode);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Message = "Failed to update the training program.",
                        IsSuccess = false
                    });
                }
            }
            catch (Exception)
            {
                // Log error
                await transaction.RollbackAsync();
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred while updating the training program.",
                    IsSuccess = false
                });
            }

        }



        [HttpGet(APIRoutes.TrainingProgram.GetProgramSyllabusByCode, Name = nameof(GetProgramSyllabusByCodeAsync))]
        public async Task<IActionResult> GetProgramSyllabusByCodeAsync([FromRoute] string code)
        {
            // Get list of syllabi
            var syllabiIds = await _context
                // From training program syllabus
                .TrainingProgramSyllabi
                // Query with condition by : {TrainingProgramCode}
                .Where(x => x.TrainingProgramCode.Equals(code))
                // Select syllabus
                .Select(x => x.SyllabusId)
                // Convert to list of syllabus id
                .ToListAsync();

            // Get training program by code 
            var trainingProgramDto = _mapper.Map<TrainingProgramDto>(
                await _context.TrainingPrograms.FirstOrDefaultAsync(x => x.TrainingProgramCode.Equals(code))
            );

            // Check exist syllabus 
            if (syllabiIds.Any())
            {
                var syllabi = await _context.Syllabi
                    .Where(x => syllabiIds.Contains(x.SyllabusId))
                    // Get list of syllabus
                    .ToListAsync();

                // Map and assign to training program dto 
                trainingProgramDto.Syllabi = _mapper.Map<List<SyllabusDto>>(syllabi);
            }

            return trainingProgramDto is null // Not found training program
                                              // response NOT FOUND with base obj detail
                ? NotFound(new BaseResponse()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Not found any training program code match {code}",
                    IsSuccess = false
                })
                // response OK with base obj detail
                : Ok(new BaseResponse()
                {
                    StatusCode = StatusCodes.Status200OK,
                    Data = trainingProgramDto,
                    IsSuccess = true
                });
        }



        [HttpPost(APIRoutes.TrainingProgram.ImportFile, Name = nameof(UploadFile))]
        public async Task<IActionResult> UploadFile([FromForm] UploadFileRequest reqObj)
        {
            var file = reqObj.File;
            //Check file is not empty
            if (file == null || file.Length == 0)
            {
                return BadRequest(new BaseResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    IsSuccess = false,
                    Message = "No file was uploaded."
                });
            }

            /// NOTE: Those line of code cause error from unit testing 
            //Check file is a csv
            // var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName?.Trim('"');
            // if (Path.GetExtension(fileName)?.ToLowerInvariant() != ".csv")
            // {
            //     return BadRequest(new BaseResponse()
            //     {
            //         StatusCode = StatusCodes.Status400BadRequest,
            //         IsSuccess = false,
            //         Message = "File is not a CSV file."
            //     });
            // }

            /// REPLACE: 
            var validator = new ExcelFileValidator();
            var validationResult = await validator.ValidateAsync(file);
            if (!validationResult.IsValid)
            {
                return BadRequest(new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    IsSuccess = false,
                    Errors = validationResult.ToProblemDetails().Errors
                });
            }


            // Check EncodingType to read file csv
            Encoding encodingType = reqObj.EncodingType?.ToUpper() switch
            {
                "ASCII" => Encoding.ASCII,
                "UTF8" => Encoding.UTF8,
                _ => Encoding.Default
            };

            var recordsList = new List<CsvRecord>();
            // config file csv
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = reqObj.ColumnSeperator,
                HeaderValidated = null,
                MissingFieldFound = null
            };

            try
            {
                using (var reader = new StreamReader(file.OpenReadStream(), encodingType))
                using (var csv = new CsvReader(reader, config))
                {
                    //Read the first line and skip
                    csv.Read();
                    // Read the second line and read header
                    csv.Read();
                    csv.ReadHeader();
                    //Read the other line and read data to list
                    while (csv.Read())
                    {
                        var record = csv.GetRecord<CsvRecord>();
                        recordsList.Add(record);
                    }
                }

                //Check scanning is duplicate
                string scanning = reqObj.Scanning!.ToUpper().Replace(" ", "");
                // Identify duplicate records
                var (lines, duplicatesItem) = await TrainingProgramHelper.FindDuplicatesFileAsync(_context, recordsList, scanning);

                // Initiate process result 
                (bool, IDictionary<string, string[]>) result = (true, null!); // By default not causing any error
                if (duplicatesItem.Any())
                {
                    var index = 0;
                    // Handle when there are duplicates
                    if (reqObj.DuplicateHandle == null)
                    {
                        // Create duplicate notifications
                        var duplicateItemsString = scanning switch
                        {
                            nameof(DuplicateScanningProgram.PROGRAMNAME) => string.Join("\n", duplicatesItem.Select(d =>
                            {
                                ++index;
                                return $"Name: {d.Name}, At line: {lines[index - 1]}.";
                            })),
                            nameof(DuplicateScanningProgram.PROGRAMID) => string.Join("\n", duplicatesItem.Select(d =>
                            {
                                ++index;
                                return $"ID: {d.TrainingProgramCode}, At line: {lines[index - 1]}.";
                            })),
                            _ => string.Join("\n", duplicatesItem.Select(d =>
                            {
                                ++index;
                                return $"ID: {d.TrainingProgramCode} - Name: {d.Name} At line: {lines[index - 1]}.";
                            })),
                        };

                        return StatusCode(StatusCodes.Status409Conflict, new BaseResponse
                        {
                            StatusCode = StatusCodes.Status409Conflict,
                            Message = $"The following records are duplicated: \n{duplicateItemsString}",
                            Data = duplicatesItem
                        });
                    }
                    else
                    {
                        // Handling based on DuplicateHandle
                        switch (reqObj.DuplicateHandle)
                        {
                            case DuplicateHandleProgram.Allow:
                                // Allow duplicates, process all records
                                result = await TrainingProgramHelper.ProcessRecords(_context, _mapper, recordsList, reqObj.CreatedBy);
                                if (!result.Item1) return result.HandleErrorResponse();
                                break;
                            case DuplicateHandleProgram.Replace:
                                // Delete duplicate records and process all records
                                _context.TrainingPrograms.RemoveRange(duplicatesItem);
                                result = await TrainingProgramHelper.ProcessRecords(_context, _mapper, recordsList, reqObj.CreatedBy);
                                if (!result.Item1) return result.HandleErrorResponse();
                                break;
                            case DuplicateHandleProgram.Skip:
                                // Ignore duplicate records and only process non-duplicate records
                                var duplicateIdentifiers = scanning == nameof(DuplicateScanningProgram.PROGRAMNAME)
                                    ? duplicatesItem.Select(d => d.Name)
                                    : duplicatesItem.Select(d => d.TrainingProgramCode);
                                var filteredRecords = recordsList.Where(record => !duplicateIdentifiers.Contains(scanning == nameof(DuplicateScanningProgram.PROGRAMNAME) ? record.Name : record.Id)).ToList();
                                result = await TrainingProgramHelper.ProcessRecords(_context, _mapper, filteredRecords, reqObj.CreatedBy);
                                if (!result.Item1) return result.HandleErrorResponse();
                                break;
                        }
                    }
                }
                else
                {
                    // No duplicates, process all records
                    result = await TrainingProgramHelper.ProcessRecords(_context, _mapper, recordsList, reqObj.CreatedBy);
                    if (!result.Item1) return result.HandleErrorResponse();
                }

                if (!_context.ChangeTracker.HasChanges())
                {
                    // No changes in DbContext
                    return Ok(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status200OK,
                        IsSuccess = true,
                        Message = "No records were processed due to skipping all.",
                    });
                }
                else
                {
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        // return data after read
                        return Ok(new BaseResponse
                        {
                            StatusCode = StatusCodes.Status200OK,
                            IsSuccess = true,
                            Message = "File has been processed successfully.",
                        });
                    }
                }


                // return data after read fail
                return BadRequest(new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    IsSuccess = false,
                    Message = "File has been processed fail.",
                    Data = recordsList
                });

            }
            catch (CsvHelperException)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = $"Error reading CSV file: Column Seperator is not correct.",
                    IsSuccess = false
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Internal server error: {ex}",
                    IsSuccess = false
                });
            }
        }


        /// <summary>
        ///     Export training program to excel
        /// </summary>
        /// <param name="reqObj"></param>
        /// <returns></returns>
        [HttpPost(APIRoutes.TrainingProgram.ExportExcel)]
        public async Task<IActionResult> ExportTrainingProgram([FromBody] ExportTrainingProgramRequest reqObj)
        {
            //  check if the time from -> to is null
            if (reqObj.ProgramTimeFrameFrom.HasValue && reqObj.ProgramTimeFrameTo.HasValue)
            {
                //check if the training program time frame from larger than training program time frame to 

                if (reqObj.ProgramTimeFrameFrom.Value.CompareTo(reqObj.ProgramTimeFrameTo.Value) > 0)
                    return BadRequest(new BaseResponse() // BadRequest 
                                                         // response BAD REQUEST with base obj detail
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = " Date 'To' must greater than Date 'From' "
                    });
            }

            var filterListQueryable = _context.TrainingPrograms // From Training Program table
                .Include(x => x.TrainingProgramSyllabi) // Include relational table 
                                                        // Then include syllabus
                    .ThenInclude(x => x.Syllabus)
                // Building query
                .AsQueryable();

            // Filter training program list as pattern
            var filterList = await FilterHelper.FilterByPattern(filterListQueryable,
                    reqObj.Status, reqObj.CreatedBy ?? null!, reqObj.ProgramTimeFrameFrom,
                    reqObj.ProgramTimeFrameTo, Convert.ToInt32(reqObj._Page), reqObj._PerPage ?? null!);

            // Sort by pattern (if any)
            if (!string.IsNullOrEmpty(reqObj.Sort))
            {
                filterList = SortingHelper.SortByColumn(filterList, reqObj.Sort).ToList();
            }

            //  Get the list based on pagination
            // int pageSize;
            // var paginationResult = PaginationHelper.PaginationAsync(
            //     reqObj._Page,
            //     filterList,
            //     Int32.TryParse(reqObj._PerPage, out pageSize) ? pageSize : _appSettings.PageSize);

            // Get list training program
            var trainingPrograms = filterList.ToList();

            if (!trainingPrograms.Any()) // not found any training program
            {
                return NotFound(new BaseResponse
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Not found any training program"
                });
            }

            // start exporting to excel
            // create file stream
            var stream = new MemoryStream();

            // create excel package
            using (var xlPackage = new OfficeOpenXml.ExcelPackage(stream))
            {
                // define a worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("TrainingPrograms");
                // first row
                var startRow = 3;
                // worksheet details
                worksheet.Cells["A1"].Value = "List of training program";
                using (var r = worksheet.Cells["A1:C1"])
                {
                    r.Merge = true;
                }

                // table header
                worksheet.Cells["A2"].Value = "Id";
                worksheet.Cells["B2"].Value = "Created By";
                worksheet.Cells["C2"].Value = "Create Date";
                worksheet.Cells["D2"].Value = "Updated By";
                worksheet.Cells["E2"].Value = "Updated Date";
                worksheet.Cells["F2"].Value = "Days";
                worksheet.Cells["G2"].Value = "Hours";
                worksheet.Cells["H2"].Value = "StartTime";
                worksheet.Cells["I2"].Value = "Name";
                worksheet.Cells["J2"].Value = "Status";
                worksheet.Cells["K2"].Value = "Syllabi";

                // table row
                var row = startRow;
                foreach (var trainingProgram in trainingPrograms)
                {
                    // get all syllabus (if any)
                    if (trainingProgram.TrainingProgramSyllabi.Any())
                    {
                        var syllabusNames = trainingProgram.TrainingProgramSyllabi
                            .Where(x => x.Syllabus is not null)
                            .Select(x => x.Syllabus.TopicName).ToList();

                        foreach (var s in syllabusNames)
                        {
                            if (s.Equals(syllabusNames.First())) worksheet.Cells[row, 11].Value = s;
                            else worksheet.Cells[row, 11].Value += "," + s;
                        }
                    }
                    else // not found any syllabus
                    {
                        // print out no syllabus found
                        worksheet.Cells[row, 11].Value = "No data";
                    }

                    // set row record
                    worksheet.Cells[row, 1].Value = trainingProgram.Id.ToString();
                    worksheet.Cells[row, 2].Value = trainingProgram.CreatedBy;
                    worksheet.Cells[row, 3].Value = Convert.ToDateTime(trainingProgram.CreatedDate).ToString("dd/MM/yyyy");
                    worksheet.Cells[row, 4].Value = trainingProgram.UpdatedBy;
                    worksheet.Cells[row, 6].Value = trainingProgram.Days;
                    worksheet.Cells[row, 7].Value = trainingProgram.Hours;
                    worksheet.Cells[row, 8].Value = trainingProgram.StartTime;
                    worksheet.Cells[row, 9].Value = trainingProgram.Name;
                    worksheet.Cells[row, 10].Value = trainingProgram.Status;

                    if (trainingProgram.UpdatedDate is not null)
                    {
                        worksheet.Cells[row, 5].Value = Convert.ToDateTime(trainingProgram.UpdatedDate).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        worksheet.Cells[row, 5].Value = "";
                    }

                    // next row
                    ++row;

                }
                // properties
                xlPackage.Workbook.Properties.Title = "List of Training Program";
                // xlPackage.Workbook.Properties.Author = "Admin";
                await xlPackage.SaveAsync();

            }
            // read from position
            stream.Position = 0;

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "trainingPrograms.xlsx");
        }

        /// <summary>
        /// Download all material of a training program including 
        /// Day - Unit - Unit Chaper of each syllabus
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet(APIRoutes.TrainingProgram.DownloadMaterial, Name = nameof(DownloadMaterialAsync))]
        public async Task<IActionResult> DownloadMaterialAsync([FromRoute] Guid code)
        {
            // Get training program by code
            var trainingProgram = await _context.TrainingPrograms
                // Query with condition by : {TrainingProgramCode}
                .FirstOrDefaultAsync(x => x.TrainingProgramCode.Equals(code.ToString()));

            // Get training program syllabus
            var syllabi = await _context
                // From training program syllabus
                .TrainingProgramSyllabi
                // Query with condition by : {TrainingProgramCode}
                .Where(x => x.TrainingProgramCode.Equals(code.ToString()))
                // Select syllabus
                .Select(x => x.Syllabus)
                // Convert to list of syllabus
                .ToListAsync();

            // Check exist training programs and its syllabus list
            if (trainingProgram is null || !syllabi.Any())
            {
                return NotFound(new BaseResponse
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Not found any training program match {code} or any syllabus inside"
                });
            }

            // Initiate zip file repsonse 
            var zipFileName = $"{trainingProgram.Name}-materials.zip";
            // Retrieve all files, subdirectories related from path pattern
            var fileStream = await _firebaseService.RetrieveItemZipWithListPatternAsync(syllabi.Select(x =>
                $"{x.TopicCode}-{x.TopicName}"));

            // return typoof Zip File response
            return File(fileStream, "application/zip", zipFileName);
        }



        /// <summary>
        /// Download training material by id from firebase cloud
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(APIRoutes.TrainingProgram.DownloadSingleMaterial, Name = nameof(DownloadSingleMaterialAsync))]
        public async Task<IActionResult> DownloadSingleMaterialAsync([FromRoute] string id)
        {
            // Check exist training material
            var trainingMaterial = await _context.TrainingMaterials
                // Get first element match expression
                .FirstOrDefaultAsync(x =>
                    // Query with condition - by id
                    x.TrainingMaterialId.Equals(id));

            if (trainingMaterial is null)
            {
                return NotFound(new BaseResponse
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Not found any training material match"
                });
            }
            else
            {
                if (trainingMaterial.Url is null)
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "This training program not exist on storage, please check again"
                    });
                }
            }

            // Proccess download single material
            var firstIndexOfPattern = trainingMaterial.Url.IndexOf("/");
            var baseUrl = trainingMaterial.Url.Substring(firstIndexOfPattern + 1);
            var fileStream = await _firebaseService.RetrieveItemAsync(baseUrl);

            // return typoof Zip File response
            return File(fileStream, "application/octet-stream", trainingMaterial.FileName);
        }

        /// <summary>
        ///     Upload material for training program
        /// </summary>
        /// <param name="reqObj"></param>
        /// <returns></returns>
        [HttpPost(APIRoutes.TrainingProgram.UploadMaterial, Name = nameof(UploadMaterialAsync))]
        public async Task<IActionResult> UploadMaterialAsync([FromForm] UploadMaterialRequest reqObj)
        {
            try
            {
                // Validation
                var validationResult = await reqObj.ValidateAsync();
                if (validationResult is not null) // Error invoked
                {
                    return BadRequest(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Something went wrong",
                        IsSuccess = false,
                        Errors = validationResult.Errors
                    });
                }

                // // Get training program by syllabus id
                // var trainingProgram = await _context.TrainingPrograms
                //     // Building query with one-to-many relationship
                //     .Include(x => x.Syllabi.Where(x =>
                //             // Get related syllabus 
                //             x.SyllabusId.Equals(reqObj.SyllabusId.ToString())))
                //             // Include one-to-many relation of syllabus - syllabusDay
                //             .ThenInclude(x => x.SyllabusDays
                //                 // Query with Day no condition
                //                 .Where(x => x.DayNo == reqObj.DayNo))
                //     // Get first element in List
                //     .FirstOrDefaultAsync();
                // // Not exist in any training program
                // if (trainingProgram is null)
                // {
                //     return NotFound(new BaseResponse
                //     {
                //         StatusCode = StatusCodes.Status404NotFound,
                //         Message = $"Not found syllabus {reqObj.SyllabusId} in any training program",
                //         IsSuccess = false
                //     });
                // }

                /// Those get query will cause repeating code. However, it prevent us from "NullPointerException"

                // Get appropriate syllabus to upload
                var syllabus = await _context.Syllabi
                    // Get related syllabus day
                    .Include(x =>
                        // Query with Day no condition
                        x.SyllabusDays.Where(x => x.DayNo == reqObj.DayNo))
                    .FirstOrDefaultAsync(x =>
                        x.SyllabusId.Equals(reqObj.SyllabusId.ToString()));
                // Check exist syllabus 
                if (syllabus is null)
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = $"Not found any syllabus match {reqObj.SyllabusId}"
                    });
                }

                // Get Syllabus Day
                var syllabusDay = syllabus.SyllabusDays.FirstOrDefault();
                // Check exist Syllabus Day
                if (syllabusDay is null)
                {
                    return BadRequest(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Syllabus Day is required"
                    });
                }

                // Get SyllabusUnit
                var syllabusUnit = await _context.SyllabusUnits
                    // Match syllabus day id and unit no
                    .Where(x => x.SyllabusDayId == syllabusDay.SyllabusDayId
                        && x.UnitNo == reqObj.UnitNo)
                    // Include unit chapters relation
                    .Include(x => x.UnitChapters
                        // With condition: Chaper Number
                        .Where(x => x.ChapterNo == reqObj.ChapterNo))
                    // Get first element 
                    .FirstOrDefaultAsync();
                // Check exist syllabus Unit 
                if (syllabusUnit is null || !syllabusUnit.UnitChapters.Any())
                {
                    return BadRequest(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Syllabus Unit is required"
                    });
                }

                // Urls 
                var baseUrl = $"{syllabus.TopicCode}-{syllabus.TopicName}/Day-{reqObj.DayNo}/Unit-{reqObj.UnitNo}/Chapter-{reqObj.ChapterNo}";

                // Generate material model
                var trainingMaterial = reqObj.ToTrainingMaterial(_fbCredentials.BucketName, baseUrl,
                    syllabusUnit!.UnitChapters!.FirstOrDefault()!.UnitChapterId);

                // Add new training program detail
                await _context.AddAsync(trainingMaterial);
                var result = await _context.SaveChangesAsync() > 0;

                if (result)
                {
                    // Generate directory path and upload to firebase
                    await _firebaseService.UploadItemAsync(
                        reqObj.File,
                        baseUrl
                    );

                    return Ok(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Upload material successfully"
                    });
                }

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            // Cause exception abt GoogleApiException -> Check Auth credentials
            catch (Exception ex) when (ex is Google.GoogleApiException)
            {
                // ObjectResult response
                return new ObjectResult(new BaseResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An Error Occured: " + ex.Message
                })
                { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }



        /// <summary>
        ///     Delete Material
        /// </summary>
        /// <param name="reqObj"></param>
        /// <returns></returns>
        [HttpDelete(APIRoutes.TrainingProgram.DeleteMaterial, Name = nameof(DeleteMaterialAsync))]
        public async Task<IActionResult> DeleteMaterialAsync([FromQuery] DeleteMaterialRequest reqObj)
        {
            // Get syllabus by id
            var syllabus = await _context.Syllabi
                .FirstOrDefaultAsync(x =>
                    x.SyllabusId.Equals(reqObj.SyllabusId.ToString()));

            // Check exist syllabus
            if (syllabus is null)
            {
                return NotFound(new BaseResponse
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Not found syllabus match id {reqObj.SyllabusId}"
                });
            }

            // Urls 
            var baseUrl = $"{syllabus.TopicCode}-{syllabus.TopicName}"
                + $"/Day-{reqObj.DayNo}/Unit-{reqObj.UnitNo}/Chapter-{reqObj.ChapterNo}";

            // Delete training material 
            var trainingMaterial = await _context.TrainingMaterials.Where(x => x.IsFile
                    && x.FileName.Equals(reqObj.FileName)
                    && x.Url != null && x.Url.Contains($"{_fbCredentials.BucketName}/{baseUrl}"))
                .FirstOrDefaultAsync();
            // Check exist training material 
            if (trainingMaterial is null)
            {
                return NotFound(new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Not found any training material"
                });
            }

            // Remove training material
            _context.Remove(trainingMaterial!);
            // Execute query
            var dbResult = await _context.SaveChangesAsync() > 0;

            // Delete item 
            var result = await _firebaseService.DeleteItemAsync(
                baseUrl, reqObj.FileName);

            // Response 
            return (result && dbResult)
                ? Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Delete material successfully",
                    IsSuccess = true
                })
                : StatusCode(StatusCodes.Status500InternalServerError);
        }



        /// <summary>
        /// Update training material
        /// </summary>
        /// <param name="reqObj"></param>
        /// <returns></returns>
        [HttpPut(APIRoutes.TrainingProgram.UpdateMaterial, Name = nameof(UpdateMaterialAsync))]
        public async Task<IActionResult> UpdateMaterialAsync([FromForm] UpdateMaterialRequest reqObj)
        {
            // Get Syllabus by id 
            var syllabus = await _context.Syllabi.FirstOrDefaultAsync(x =>
                // With condition: SyllabusId
                x.SyllabusId.Equals(reqObj.SyllabusId));

            // Check exist syllabus
            if (syllabus is null)
            {
                // Response NotFound obj
                return NotFound(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = $"Not found any syllabus match id {reqObj.SyllabusId}"
                });
            }


            /// More code here....

            // Base URL
            var baseUrl = FileHelper.CombineWithRegex(
                new[] { @"\\" }, "/",
                $"{syllabus.TopicCode}-{syllabus.TopicName}",
                $"Day-{reqObj.DayNo}",
                $"Unit-{reqObj.UnitNo}",
                $"Chapter-{reqObj.ChapterNo}"
            );

            // Get training material by id
            var trainingMaterial = await _context.TrainingMaterials.FirstOrDefaultAsync(x =>
               x.TrainingMaterialId == reqObj.TrainingMaterialId // Query by id
            && x.IsFile // Is PDF, Word, image file 
            && x.Url != null && x.Url.Contains(baseUrl) // Contain URL require
            );
            // Not exist training material 
            if (trainingMaterial is null)
            {
                return NotFound(new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = $"Not found any training material match id {reqObj.TrainingMaterialId}"
                });
            }

            // Prev file name
            var prevFileName = trainingMaterial.FileName;

            // Do Update to entity
            trainingMaterial.Name = FileHelper.GetFileNameWithoutExtension(reqObj.FileName);
            trainingMaterial.Url = FileHelper.Combine(_fbCredentials.BucketName, baseUrl, reqObj.FileName);
            trainingMaterial.FileName = reqObj.FileName;
            trainingMaterial.ModifiedBy = reqObj.ModifiedBy;
            trainingMaterial.ModifiedDate = DateTime.ParseExact(
                DateTime.Now.ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture);

            // Save DB
            await _context.SaveChangesAsync();

            // Process update file
            var updateResult = await _firebaseService.UpdateItemFileNameAsync(baseUrl, prevFileName, reqObj.FileName, reqObj.File);

            return updateResult
                ? Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Update training material successfully"
                })
                : BadRequest(new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Not found path directory"
                });
        }

        [HttpGet(APIRoutes.TrainingProgram.GetMaterial, Name = nameof(GetAllTrainingMaterialAsync))]
        public async Task<IActionResult> GetAllTrainingMaterialAsync()
        {
            var trainingMaterialsDto =
                _mapper.Map<IEnumerable<TrainingMaterialDto>>(
                    await _context.TrainingMaterials.ToListAsync());
            if (trainingMaterialsDto.Any())
            {
                return Ok(new BaseResponse()
                {
                    Message = "Get all training material successfully",
                    StatusCode = StatusCodes.Status200OK,
                    Data = trainingMaterialsDto,
                    IsSuccess = true
                });
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, new BaseResponse()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Not found any training material",
                    IsSuccess = false
                });
            }
        }
    }
}