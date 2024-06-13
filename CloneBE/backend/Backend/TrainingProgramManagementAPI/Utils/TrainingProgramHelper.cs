using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http.HttpResults;
using TrainingProgramManagementAPI.Common.Enums;
using Entities.Context;
using Entities.Models;

namespace TrainingProgramManagementAPI.Utils
{
    public class TrainingProgramHelper
    {

        public static async Task<(List<int>, List<TrainingProgram>)> FindDuplicatesFileAsync(FamsContext _context, IEnumerable<CsvRecord> records, string scanning)
        {
            var duplicates = new List<TrainingProgram>();
            var indexs = new List<int>();
            var numbers = 3;
            foreach (var record in records)
            {
                // Check duplicate base on scanning
                TrainingProgram? existingProgram = scanning switch
                {
                    nameof(DuplicateScanningProgram.PROGRAMNAME) => await _context.TrainingPrograms.FirstOrDefaultAsync(tp => tp.Name.Equals(record.Name)),
                    nameof(DuplicateScanningProgram.PROGRAMID) => await _context.TrainingPrograms.FirstOrDefaultAsync(tp => tp.TrainingProgramCode.Equals(record.Id)),
                    _ => await _context.TrainingPrograms.FirstOrDefaultAsync(tp => tp.TrainingProgramCode.Equals(record.Id) && tp.Name.Equals(record.Name)),
                };

                if (existingProgram != null)
                {
                    duplicates.Add(existingProgram);
                    indexs.Add(numbers);
                }
                numbers++;
            }
            return (indexs, duplicates);
        }

        public static async Task<(bool, IDictionary<string, string[]>)> ProcessRecords(FamsContext _context, IMapper _mapper, IEnumerable<CsvRecord> recordsList, String createdBy)
        {
            // Initiate errors handler
            IDictionary<string, string[]> errors = new Dictionary<string, string[]>();

            foreach (var record in recordsList)
            {
                /// NOTE: Entity running in memory data context is different from SqlServer
                /// Id, Code is required cause it not auto generate
                /// --> new instance of entity to make use of its custom constructor for testing 
                var trainingProgramEntity = new TrainingProgram()
                {
                    // Create by
                    CreatedBy = createdBy,
                    // Training program name
                    Name = record.Name,
                    // Default status 
                    Status = nameof(TrainingProgramStatus.Draft),
                    CreatedDate = DateTime.ParseExact( // Create Date is today
                                                       // Format with particular pattern
                        DateTime.Now.ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    // More properties here...
                    // Start-time is required while import
                };

                // Create trainingProgramEntity 
                // var trainingProgramEntity = _mapper.Map<TrainingProgram>(new TrainingProgramDto());

                // trainingProgramEntity.CreatedBy = createdBy; 
                // trainingProgramEntity.Name = record.Name; 

                // Split ListSyllabus of file CSV to add by loop
                var pattern = "[;.\\t :]";
                var syllabusIds = Regex.Replace(record.ListSyllabus, pattern, ",")
                                       .Split(',', StringSplitOptions.RemoveEmptyEntries)
                                       .Select(s => s.Trim())
                                       .ToList();

                // Generate training program syllabi
                var trainingProgramSyllabi = new List<TrainingProgramSyllabus>();

                foreach (var id in syllabusIds)
                {
                    // Get list syllabus from id 
                    var syllabus = await _context.Syllabi.SingleOrDefaultAsync(s => s.SyllabusId.Equals(id));
                    if (syllabus != null)
                    {
                        // Adding hours
                        trainingProgramEntity.Hours = syllabus.Hours.HasValue
                            // If it does has value -> ceiling to integer
                            ? (int)Math.Ceiling((double)syllabus.Hours.Value)
                            // Default 0 
                            : 0;

                        // Adding days
                        trainingProgramEntity.Days = syllabus.Days.HasValue
                            // If it does has value -> ceiling to integer
                            ? (int)Math.Ceiling((double)syllabus.Days.Value)
                            // Default 0 
                            : 0;
                    }
                    else // Not found any syllabus match id provided
                    {
                        // Check not exist key
                        if (!errors.TryGetValue("Ids", out var key))
                        {
                            // Add key new pair
                            errors.Add(new KeyValuePair<string, string[]>("Ids", new[] { $"{id}" }));
                        }
                        else // Exist key 
                        {
                            // Concat old with new one
                            errors["Ids"] = errors["Ids"].Concat(new[] { $"{id}" }).ToArray();
                        }
                    }
                }

                if (!errors.Any()) // Check if causing error or not
                {
                    // Save change Training Program
                    await _context.TrainingPrograms.AddAsync(trainingProgramEntity);
                    var result = await _context.SaveChangesAsync() > 0;

                    var handleAddSyllabiTask = syllabusIds.Select(async x =>
                    {
                        if (result) // Save change training program entity successfully
                        {
                            // Generate training program syllabus
                            var trainingProgramSyllabus = new TrainingProgramSyllabus
                            {
                                SyllabusId = x,
                                TrainingProgramCode = trainingProgramEntity.TrainingProgramCode
                            };

                            await _context.TrainingProgramSyllabi.AddAsync(trainingProgramSyllabus);
                        }
                    });

                    // To ensure that add task complete before move to next task
                    await Task.WhenAll(handleAddSyllabiTask);
                }
            }

            // Causing errors
            if (errors.Any()) return (false, errors);

            return (true, null!);
        }
    }

    public static class TrainingProgramHelperExtension
    {
        public static IActionResult HandleErrorResponse(this (bool, IDictionary<string, string[]>) error)
        {
            // Handle error
            return new BadRequestObjectResult(new BaseResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Not found syllabus match file given",
                // Give error data for FE show all syllabus not exist
                Errors = error.Item2
            });
        }
    }
}
