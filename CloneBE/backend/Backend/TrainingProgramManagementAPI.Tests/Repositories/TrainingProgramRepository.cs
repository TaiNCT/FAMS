using System.Data.Common;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using Entities.Models;
using TrainingProgramManagementAPI.Enums;
using Xunit.Abstractions;

namespace TrainingProgramManagementAPITests.Repositories;


public interface IRepository<T>
{

    // Summary:
    //      Get all item     
    // IEnumerable<T> GetAll();
    Task<IEnumerable<T>> GetAllAsync();

    //  Summary:
    //      Get all item with condition
    Task<IEnumerable<T>> GetAllWithConditionAsync(
        Expression<Func<TrainingProgram, bool>> predicate);

    // Summary:
    //      Get all item with related Entities
    Task<IEnumerable<T>> GetAllWithRelatedEntitiesAsync(
        params Expression<Func<T, object>>[] includeProperties);

    // Summary:
    //      Get all item with related entities and condition
    Task<IEnumerable<T>> GetAllWithRelatedEntitiesAndConditionAsync(
        Expression<Func<TrainingProgram, bool>> predicate,
        params Expression<Func<T, object>>[] includeProperties);

    // Summary:
    //      Get item by id 
    // T GetById(string id);
    Task<T?> GetByIdAsync(string id);

    // Summary:
    //      Add new item 
    // void Add(T entity);
    Task CreateAsync(T entity);


    // Summary:
    //      Update item
    // void Update(T entity);
    Task UpdateAsync(T entity);

    // Summary:
    //      Delete item
    // void Delete(T entity);
    Task DeleteAsync(T entity);

    // Summary:
    //      Delete by id
    // void DeleteById(string id);
    // Task DeleteByIdAsync(string id); 
    IQueryable<T> GetAllAsQueryable();
}

public class TrainingProgramRepository : IRepository<TrainingProgram>
{
    private readonly Serilog.ILogger _logger;
    private readonly FamsContext _context;


    public TrainingProgramRepository(ITestOutputHelper output,
        FamsContext context)
    {
        // Custom Serilog
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss}] {Level} {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
            .WriteTo.TestOutput(output)
            .CreateLogger();
        _logger = Log.Logger;

        _context = context;

        try
        {
            // Ensures that the database for the context exists
            _context.Database.EnsureCreated();
            // Seed the in-memory database with test data   
            _logger.Information("--> Migrating memory database");

            // Data already exist -> Pass func
            if (_context.TrainingPrograms.Any() && _context.Syllabi.Any())
            {
                _logger.Information("--> Data already exist. Skip Seed Process.");
                return;
            }

            // Seeding data
            _logger.Information("--> Seeding testing data");

            // Training program data
            var codes = new[]
            {
                "f6a9bff5-9e84-47de-9a4c-78fc0dc76bd4",
                "766ee0f2-7817-4f86-a70c-1d72c76c2ab3",
                "17051b46-4a48-4b71-b373-4cb4b9faa5b2",
                "9bfc5c08-c41f-47c9-81ad-aa658c3856da",
                "08e35a92-5815-4e3c-98d8-5576d491a001",
                "51098f7f-0f43-4164-9d7f-a74a2d103b00",
                "53d0bcfc-7703-43be-a5e1-6d758a9ed530",
                "dac70907-29d2-4dbf-98ff-183ba6983d9f",
                "f8eac28b-c3cf-4193-a7d0-93be19d70ced",
                "ca589648-d866-4c44-b29f-fac6f84290e1",
                "31968dc0-0400-4fa3-9303-f8f19a427f86"
            };
            List<TrainingProgram> trainingPrograms = new()
                {
                    new(){Id=1, TrainingProgramCode = codes[0], Name="C# basic program", Days=7, Hours=14, CreatedBy="Warrior Tran", CreatedDate= new DateTime(2019, 07, 21), Status = nameof(TrainingProgramStatus.Active)},
                    new(){Id=2, TrainingProgramCode = codes[1], Name=".NET basic program", Days=12, Hours=24, CreatedBy="Warrior Tran", CreatedDate= new DateTime(2021, 10, 07), Status = nameof(TrainingProgramStatus.Active)},
                    new(){Id=3, TrainingProgramCode = codes[2], Name="DevOps foundation", Days=25, Hours=50, CreatedBy="Mong Quynh", CreatedDate= new DateTime(2021, 11, 10), Status = nameof(TrainingProgramStatus.InActive)},
                    new(){Id=4, TrainingProgramCode = codes[3], Name="DevOps foundation_2", Days=24, Hours=48, CreatedBy="Warrior Tran", CreatedDate= new DateTime(2022, 05, 20), Status = nameof(TrainingProgramStatus.Active)},
                    new(){Id=5, TrainingProgramCode = codes[4], Name="IT Bussiness Analyst Foundation_4", Days=9, Hours=18, CreatedBy="Lyly Heothi", CreatedDate= new DateTime(2022, 04, 22), Status = nameof(TrainingProgramStatus.Active)},
                    new(){Id=6, TrainingProgramCode = codes[5], Name="FullStack Java Web Developer_3", Days=20, Hours=40, CreatedBy="Warrior Tran", CreatedDate= new DateTime(2022, 03, 04), Status = nameof(TrainingProgramStatus.Draft)},
                    new(){Id=7, TrainingProgramCode = codes[6], Name="FullStack Java Web Developer_1", Days=19, Hours=38, CreatedBy="Warrior Tran", CreatedDate= new DateTime(2018, 10, 07), Status = nameof(TrainingProgramStatus.InActive)},
                    new(){Id=8, TrainingProgramCode = codes[7], Name=".NET basic program_2", Days=14, Hours=28, CreatedBy="Heothi Bally", CreatedDate= new DateTime(2021, 08, 06), Status = nameof(TrainingProgramStatus.Active)},
                    new(){Id=9, TrainingProgramCode = codes[8], Name="Fullstack .NET Web Developer", Days=12, Hours=24, CreatedBy="Warrior Tran", CreatedDate= new DateTime(2021, 11, 11), Status = nameof(TrainingProgramStatus.Active)},
                    new(){Id=10, TrainingProgramCode = codes[9], Name="Backend Developer", Days=10, Hours=20, CreatedBy="John Doe", CreatedDate= new DateTime(2021, 12, 1), Status = nameof(TrainingProgramStatus.Active)},
                    new(){Id=11, TrainingProgramCode = codes[10], Name="Frontend Developer", Days=8, Hours=16, CreatedBy="Jane Smith", CreatedDate= new DateTime(2021, 12, 3), Status = nameof(TrainingProgramStatus.InActive)},
                };

            // Syllabus data
            var syllabiIds = new[]
            {
                "eb39c944-d924-4304-8d61-c06729b5de3e",
                "e470aa85-c2d5-49e6-9741-bf093d923856",
                "28cb11dd-d54b-44d4-9cc1-14bb2048aef5"
            };
            List<Syllabus> syllabi = new()
                {
                    new()
                    {
                        Id = 1,
                        SyllabusId = syllabiIds[0],
                        TopicCode = "TPC001",
                        TopicName = "Introduction to Programming",
                        Version = "1.0",
                        CreatedBy = "John Doe",
                        CreatedDate = DateTime.Parse("2023-01-10"),
                        ModifiedBy = null,
                        ModifiedDate = null,
                        AttendeeNumber = 30,
                        Level = "Beginner",
                        TechnicalRequirement = "Basic computer knowledge",
                        CourseObjective = "Understanding programming fundamentals",
                        DeliveryPrinciple = "Interactive lectures",
                        Days = 5,
                        Hours = 20.5,
                        Status = "Active"
                    },
                    new()
                    {
                        Id = 2,
                        SyllabusId = syllabiIds[1],
                        TopicCode = "TPC002",
                        TopicName = "Data Structures and Algorithms",
                        Version = "2.0",
                        CreatedBy = "Jane Smith",
                        CreatedDate = DateTime.Parse("2023-02-15"),
                        ModifiedBy = null,
                        ModifiedDate = null,
                        AttendeeNumber = 25,
                        Level = "Intermediate",
                        TechnicalRequirement = "Knowledge of programming languages",
                        CourseObjective = "Understanding advanced data structures and algorithms",
                        DeliveryPrinciple = "Interactive lectures",
                        Days = 7,
                        Hours = 30.75,
                        Status = "Active"
                    },
                    new()
                    {
                        Id = 3,
                        SyllabusId = syllabiIds[2],
                        TopicCode = "TPC003",
                        TopicName = "Web Development",
                        Version = "2.5",
                        CreatedBy = "Alice Johnson",
                        CreatedDate = DateTime.Parse("2023-03-20"),
                        ModifiedBy = null,
                        ModifiedDate = null,
                        AttendeeNumber = 20,
                        Level = "Intermediate",
                        TechnicalRequirement = "Familiarity with HTML, CSS, and JavaScript",
                        CourseObjective = "Building dynamic web applications",
                        DeliveryPrinciple = "Interactive lectures",
                        Days = 10,
                        Hours = 40.0,
                        Status = "Active"
                    },
                };

            // Delivery type
            List<DeliveryType> deliveryTypes = new(){
                    new ()
                    {
                        Id = 1,
                        DeliveryTypeId = "247871fd-ad47-4846-9c71-3004de24741c",
                        Descriptions = "Description 1",
                        Icon = "https://static.thenounproject.com/png/2236613-200.png",
                        Name = "Delivery Type 1"
                    }
                };

            // Output standard
            List<OutputStandard> outputStandards = new()
            {
                new ()
                {
                    Id = 2,
                    OutputStandardId = "6c406c77-0e9f-4da6-aa83-6285cb6b1d3d",
                    Code = "K6SD",
                    Descriptions = "Description 1",
                    Name = "Standard 1"
                }
            };

            // Add Range training program
            _context.TrainingPrograms.RemoveRange(_context.TrainingPrograms);
            _context.TrainingPrograms.AddRange(trainingPrograms);
            // // Add Range syllabus
            _context.Syllabi.AddRange(syllabi);
            // Add Range delivery type
            _context.DeliveryTypes.AddRange(deliveryTypes);
            // Add Range output standard
            _context.OutputStandards.AddRange(outputStandards);
            // Save to DB
            _context.SaveChanges();

            // Add Training Program Syllabus
            List<TrainingProgramSyllabus> trainingProgramSyllabi = new()
            {
                new ()
                {
                    SyllabusId = syllabiIds[0],
                    TrainingProgramCode = "f6a9bff5-9e84-47de-9a4c-78fc0dc76bd4"
                },
                new ()
                {
                    SyllabusId = syllabiIds[1],
                    TrainingProgramCode = "f6a9bff5-9e84-47de-9a4c-78fc0dc76bd4"
                },
                new ()
                {
                    SyllabusId = syllabiIds[2],
                    TrainingProgramCode = "f6a9bff5-9e84-47de-9a4c-78fc0dc76bd4"
                }
            };
            _context.TrainingProgramSyllabi.AddRange(trainingProgramSyllabi);
            _context.SaveChanges();
            

            _logger.Information("--> Seeding testing data successfully");
            // }
        }
        catch (Exception ex) when (ex is DbException)
        {
            _logger.Error("An error occured: " + ex.Message);
        }
    }

    public async Task CreateAsync(TrainingProgram entity)
    {
        await _context.TrainingPrograms.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TrainingProgram entity)
    {
        _context.TrainingPrograms.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TrainingProgram entity)
    {
        _context.TrainingPrograms.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TrainingProgram>> GetAllAsync()
    {
        return await _context.TrainingPrograms.ToListAsync();
    }

    public async Task<IEnumerable<TrainingProgram>> GetAllWithConditionAsync(Expression<Func<TrainingProgram, bool>> predicate)
    {
        return await _context.TrainingPrograms.Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<TrainingProgram>> GetAllWithRelatedEntitiesAsync(params Expression<Func<TrainingProgram, object>>[] includeProperties)
    {
        try
        {
            // Base Query
            IQueryable<TrainingProgram> query = _context.Set<TrainingProgram>();

            // Include related entities
            foreach (var includeProp in includeProperties)
            {
                query = query.Include(includeProp);
            }

            // Execute the query
            return await query.ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An Unexpected Error Occured When fetching data: {ex.Message}");
        }

        return null!;
    }

    public async Task<IEnumerable<TrainingProgram>> GetAllWithRelatedEntitiesAndConditionAsync(
        Expression<Func<TrainingProgram, bool>> predicate,
        params Expression<Func<TrainingProgram, object>>[] includeProperties)
    {
        try
        {
            // Base Query
            IQueryable<TrainingProgram> query = _context.Set<TrainingProgram>();

            // Include related entities
            foreach (var includeProp in includeProperties)
            {
                query = query.Include(includeProp);
            }

            // Execute the query
            return await query.Where(predicate).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An Unexpected Error Occured When fetching data: {ex.Message}");
        }

        return null!;
    }

    public async Task<TrainingProgram?> GetByIdAsync(string id)
    {
        return await _context.TrainingPrograms.FirstOrDefaultAsync(x => x.TrainingProgramCode == id);
    }

    public IQueryable<TrainingProgram> GetAllAsQueryable()
    {
        return _context.TrainingPrograms.AsQueryable();
    }


}