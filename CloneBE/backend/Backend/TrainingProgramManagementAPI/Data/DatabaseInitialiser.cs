using Entities.Context;
using Entities.Models;

namespace TrainingProgramManagementAPI.Data
{
    public static class DatabaseInitialiserExtension
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            // Create IServiceScope to resolve service scope
            using (var scope = app.Services.CreateScope())
            {
                var initialiser = scope.ServiceProvider.GetRequiredService<DatabaseInitialiser>();

                //await initialiser.InitialiseAsync();

                // Try to seeding data
                // await initialiser.SeedAsync();

                await Task.CompletedTask;
            }
        }
    }

    public interface IDatabaseInitialiser
    {
        Task InitialiseAsync();
        Task SeedAsync();
        Task TrySeedAsync();
    }

    public class DatabaseInitialiser : IDatabaseInitialiser
    {
        private readonly FamsContext _context;
        private readonly IWebHostEnvironment _env;

        public DatabaseInitialiser(FamsContext context,
            IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                // Check if database is not exist 
                if (!_context.Database.CanConnect())
                {
                    // Migration Database - Create database 
                    await _context.Database.MigrateAsync();
                }

                // Check if migrations have already been applied 
                var appliedMigrations = await _context.Database.GetAppliedMigrationsAsync();

                if (appliedMigrations.Any())
                {
                    Console.WriteLine("Migrations have already been applied. Skip migratons proccess.");
                    return;
                }

                // Check if data exists in relavent tables 
                if (_context.TrainingPrograms.Any())
                {
                    Console.WriteLine("Data already exist in the database. Skipping migration process.");
                    return;
                }

                Console.WriteLine("Database migrated successfully");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            Console.WriteLine("--> Seeding Data");
            try
            {
                // Data already exist -> Pass func
                // if (_context.TrainingPrograms.Any() && _context.Syllabi.Any())
                // {
                //     Console.WriteLine("--> Data already exist. Skip Seed Process.");
                //     return;
                // }

                // Training program data
                List<TrainingProgram> trainingPrograms = new()
                {
                    new(){Name="C# basic program", Days=7, Hours=14, CreatedBy="Warrior Tran", CreatedDate= new DateTime(2019, 07, 21), Status = nameof(TrainingProgramStatus.Active)},
                    new(){Name=".NET basic program", Days=12, Hours=24, CreatedBy="Warrior Tran", CreatedDate= new DateTime(2021, 10, 07), Status = nameof(TrainingProgramStatus.Active)},
                    new(){Name="DevOps foundation", Days=25, Hours=50, CreatedBy="Mong Quynh", CreatedDate= new DateTime(2021, 11, 10), Status = nameof(TrainingProgramStatus.InActive)},
                    new(){Name="DevOps foundation_2", Days=24, Hours=48, CreatedBy="Warrior Tran", CreatedDate= new DateTime(2022, 05, 20), Status = nameof(TrainingProgramStatus.Active)},
                    new(){Name=".NET basic program_3", Days=13, Hours=26, CreatedBy="John Hubble", CreatedDate= new DateTime(2022, 03, 30), Status = nameof(TrainingProgramStatus.Active)},
                    new(){Name="IT Bussiness Analyst Foundation_4", Days=9, Hours=18, CreatedBy="Lyly Heothi", CreatedDate= new DateTime(2022, 04, 22), Status = nameof(TrainingProgramStatus.Active)},
                    new(){Name="FullStack Java Web Developer_3", Days=20, Hours=40, CreatedBy="Warrior Tran", CreatedDate= new DateTime(2022, 03, 04), Status = nameof(TrainingProgramStatus.Draft)},
                    new(){Name="FullStack Java Web Developer_1", Days=19, Hours=38, CreatedBy="Warrior Tran", CreatedDate= new DateTime(2018, 10, 07), Status = nameof(TrainingProgramStatus.InActive)},
                    new(){Name=".NET basic program_2", Days=14, Hours=28, CreatedBy="Heothi Bally", CreatedDate= new DateTime(2021, 08, 06), Status = nameof(TrainingProgramStatus.Active)},
                    new(){Name="Fullstack .NET Web Developer", Days=12, Hours=24, CreatedBy="Warrior Tran", CreatedDate= new DateTime(2021, 11, 11), Status = nameof(TrainingProgramStatus.Active)},
                    new(){Name="Backend Developer", Days=10, Hours=20, CreatedBy="John Doe", CreatedDate= new DateTime(2021, 12, 1), Status = nameof(TrainingProgramStatus.Active)},
                    new(){Name="Frontend Developer", Days=8, Hours=16, CreatedBy="Jane Smith", CreatedDate= new DateTime(2021, 12, 3), Status = nameof(TrainingProgramStatus.InActive)},
                    new(){Name="Data Scientist", Days=15, Hours=30, CreatedBy="Alice Johnson", CreatedDate= new DateTime(2021, 12, 5), Status = nameof(TrainingProgramStatus.Draft)},
                    new(){Name="Machine Learning Engineer", Days=20, Hours=40, CreatedBy="Bob Williams", CreatedDate= new DateTime(2021, 12, 7), Status = nameof(TrainingProgramStatus.Draft)},
                    new(){Name="Cybersecurity Analyst", Days=18, Hours=36, CreatedBy="Emily Brown", CreatedDate= new DateTime(2021, 12, 9), Status = nameof(TrainingProgramStatus.Draft)},
                    new(){Name="UI/UX Designer", Days=10, Hours=20, CreatedBy="Michael Wilson", CreatedDate= new DateTime(2021, 12, 11), Status = nameof(TrainingProgramStatus.InActive)},
                    new(){Name="DevOps Engineer", Days=15, Hours=30, CreatedBy="Sophia Miller", CreatedDate= new DateTime(2021, 12, 13), Status = nameof(TrainingProgramStatus.Active)},
                    new(){Name="Quality Assurance Engineer", Days=12, Hours=24, CreatedBy="Ethan Davis", CreatedDate= new DateTime(2021, 12, 15), Status = nameof(TrainingProgramStatus.Active)},
                    new(){Name="Database Administrator", Days=12, Hours=24, CreatedBy="Olivia Garcia", CreatedDate= new DateTime(2021, 12, 17), Status = nameof(TrainingProgramStatus.InActive)},
                    new(){Name="Cloud Solutions Architect", Days=20, Hours=40, CreatedBy="Noah Martinez", CreatedDate= new DateTime(2021, 12, 19), Status = nameof(TrainingProgramStatus.Active)}
                };

                var test = await _context.Syllabi.ToListAsync();

                // Syllabus data
                List<Syllabus> syllabi = new()
                {
                    new()
                    {
                        TopicCode = "TEC144",
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
                //     new()
                //     {
                //         TopicCode = "TPC002",
                //         TopicName = "Data Structures and Algorithms",
                //         Version = "2.0",
                //         CreatedBy = "Jane Smith",
                //         CreatedDate = DateTime.Parse("2023-02-15"),
                //         ModifiedBy = null,
                //         ModifiedDate = null,
                //         AttendeeNumber = 25,
                //         Level = "Intermediate",
                //         TechnicalRequirement = "Knowledge of programming languages",
                //         CourseObjective = "Understanding advanced data structures and algorithms",
                //         DeliveryPrinciple = Encoding.UTF8.GetBytes("[\"Lecture series\", \"Coding challenges\"]"),
                //         Days = 7,
                //         Hours = 30.75
                //     },
                //     new()
                //     {
                //         TopicCode = "TPC003",
                //         TopicName = "Web Development",
                //         Version = "2.5",
                //         CreatedBy = "Alice Johnson",
                //         CreatedDate = DateTime.Parse("2023-03-20"),
                //         ModifiedBy = null,
                //         ModifiedDate = null,
                //         AttendeeNumber = 20,
                //         Level = "Intermediate",
                //         TechnicalRequirement = "Familiarity with HTML, CSS, and JavaScript",
                //         CourseObjective = "Building dynamic web applications",
                //         DeliveryPrinciple = Encoding.UTF8.GetBytes("[\"Project-based learning\", \"Code review sessions\"]"),
                //         Days = 10,
                //         Hours = 40.0
                //     },
                //     new()
                //     {
                //         TopicCode = "TC004",
                //         TopicName = "Introduction to Programming",
                //         Version = "1.0",
                //         CreatedBy = "John Doe",
                //         CreatedDate = DateTime.Now.AddDays(-30),
                //         ModifiedBy = "Jane Smith",
                //         ModifiedDate = DateTime.Now.AddDays(-10),
                //         AttendeeNumber = 20,
                //         Level = "Beginner",
                //         TechnicalRequirement = "Basic computer skills",
                //         CourseObjective = "Learn the fundamentals of programming",
                //         DeliveryPrinciple = Encoding.UTF8.GetBytes("Interactive lectures and hands-on exercises"),
                //         Days = 5,
                //         Hours = 20.5
                //     },
                //     new()
                //     {
                //         TopicCode = "TC005",
                //         TopicName = "Data Structures and Algorithms",
                //         Version = "2.0",
                //         CreatedBy = "Alice Johnson",
                //         CreatedDate = DateTime.Now.AddDays(-40),
                //         ModifiedBy = "Bob Williams",
                //         ModifiedDate = DateTime.Now.AddDays(-20),
                //         AttendeeNumber = 15,
                //         Level = "Intermediate",
                //         TechnicalRequirement = "Basic knowledge of programming",
                //         CourseObjective = "Understanding data structures and algorithms",
                //         DeliveryPrinciple = Encoding.UTF8.GetBytes("Classroom lectures and coding challenges"),
                //         Days = 7,
                //         Hours = 30.0
                //     },
                //     new()
                //     {
                //         TopicCode = "TC006",
                //         TopicName = "Linux",
                //         Version = "2.0",
                //         CreatedBy = "Alice Johnson",
                //         CreatedDate = DateTime.Now.AddDays(-40),
                //         ModifiedBy = "Bob Williams",
                //         ModifiedDate = DateTime.Now.AddDays(-20),
                //         AttendeeNumber = 15,
                //         Level = "Intermediate",
                //         TechnicalRequirement = "Basic knowledge of programming",
                //         CourseObjective = "Understanding data structures and algorithms",
                //         DeliveryPrinciple = Encoding.UTF8.GetBytes("Classroom lectures and coding challenges"),
                //         Days = 7,
                //         Hours = 30.0
                //     },
                //     new()
                //     {
                //         TopicCode = "TC007",
                //         TopicName = "Linux 02",
                //         Version = "2.0",
                //         CreatedBy = "Alice Johnson",
                //         CreatedDate = DateTime.Now.AddDays(-40),
                //         ModifiedBy = "Bob Williams",
                //         ModifiedDate = DateTime.Now.AddDays(-20),
                //         AttendeeNumber = 15,
                //         Level = "Intermediate",
                //         TechnicalRequirement = "Basic knowledge of programming",
                //         CourseObjective = "Understanding data structures and algorithms",
                //         DeliveryPrinciple = Encoding.UTF8.GetBytes("Classroom lectures and coding challenges"),
                //         Days = 7,
                //         Hours = 30.0
                //     },
                //     new()
                //     {
                //         TopicCode = "TC008",
                //         TopicName = "Golang NodeJS",
                //         Version = "2.0",
                //         CreatedBy = "Alice Johnson",
                //         CreatedDate = DateTime.Now.AddDays(-40),
                //         ModifiedBy = "Bob Williams",
                //         ModifiedDate = DateTime.Now.AddDays(-20),
                //         AttendeeNumber = 15,
                //         Level = "Intermediate",
                //         TechnicalRequirement = "Basic knowledge of programming",
                //         CourseObjective = "Understanding data structures and algorithms",
                //         DeliveryPrinciple = Encoding.UTF8.GetBytes("Classroom lectures and coding challenges"),
                //         Days = 7,
                //         Hours = 30.0
                //     },
                //     new()
                //     {
                //         TopicCode = "TC009",
                //         TopicName = "C Sharp",
                //         Version = "2.0",
                //         CreatedBy = "Alice Johnson",
                //         CreatedDate = DateTime.Now.AddDays(-40),
                //         ModifiedBy = "Bob Williams",
                //         ModifiedDate = DateTime.Now.AddDays(-20),
                //         AttendeeNumber = 15,
                //         Level = "Intermediate",
                //         TechnicalRequirement = "Basic knowledge of programming",
                //         CourseObjective = "Understanding data structures and algorithms",
                //         DeliveryPrinciple = Encoding.UTF8.GetBytes("Classroom lectures and coding challenges"),
                //         Days = 7,
                //         Hours = 30.0
                //     },
                //     new()
                //     {
                //         TopicCode = "TC0010",
                //         TopicName = "Cyber Security",
                //         Version = "2.0",
                //         CreatedBy = "Alice Johnson",
                //         CreatedDate = DateTime.Now.AddDays(-40),
                //         ModifiedBy = "Bob Williams",
                //         ModifiedDate = DateTime.Now.AddDays(-20),
                //         AttendeeNumber = 15,
                //         Level = "Intermediate",
                //         TechnicalRequirement = "Basic knowledge of programming",
                //         CourseObjective = "Understanding data structures and algorithms",
                //         DeliveryPrinciple = Encoding.UTF8.GetBytes("Classroom lectures and coding challenges"),
                //         Days = 7,
                //         Hours = 30.0
                //     },
                //     new()
                //     {
                //         TopicCode = "TC0011",
                //         TopicName = "DataBase Structure",
                //         Version = "2.0",
                //         CreatedBy = "Alice Johnson",
                //         CreatedDate = DateTime.Now.AddDays(-40),
                //         ModifiedBy = "Bob Williams",
                //         ModifiedDate = DateTime.Now.AddDays(-20),
                //         AttendeeNumber = 15,
                //         Level = "Intermediate",
                //         TechnicalRequirement = "Basic knowledge of programming",
                //         CourseObjective = "Understanding data structures and algorithms",
                //         DeliveryPrinciple = Encoding.UTF8.GetBytes("Classroom lectures and coding challenges"),
                //         Days = 7,
                //         Hours = 30.0
                //     },
                //     new()
                //     {
                //         TopicCode = "TC0012",
                //         TopicName = "Alias",
                //         Version = "2.0",
                //         CreatedBy = "Alice Johnson",
                //         CreatedDate = DateTime.Now.AddDays(-40),
                //         ModifiedBy = "Bob Williams",
                //         ModifiedDate = DateTime.Now.AddDays(-20),
                //         AttendeeNumber = 15,
                //         Level = "Intermediate",
                //         TechnicalRequirement = "Basic knowledge of programming",
                //         CourseObjective = "Understanding data structures and algorithms",
                //         DeliveryPrinciple = Encoding.UTF8.GetBytes("Classroom lectures and coding challenges"),
                //         Days = 7,
                //         Hours = 30.0
                //     }
                };

                // // Delivery type
                List<DeliveryType> deliveryTypes = new(){
                    new ()
                    {
                        Descriptions = "Description 1",
                        Icon = "https://static.thenounproject.com/png/2236613-200.png",
                        Name = "Delivery Type 1"
                    },
                    new ()
                    {
                        Descriptions = "Description 2",
                        Icon = "https://static.thenounproject.com/png/2236613-200.png",
                        Name = "Delivery Type 2"
                    },
                    new ()
                    {
                        Descriptions = "Description 3",
                        Icon = "https://www.freeiconspng.com/uploads/podcast-icon-18.png",
                        Name = "Delivery Type 3"
                    },
                    new ()
                    {
                        Descriptions = "Description 4",
                        Icon = "https://cdn-icons-png.flaticon.com/512/827/827980.png",
                        Name = "Delivery Type 4"
                    }
                };

                // // Output standard
                List<OutputStandard> outputStandards = new(){
                    new ()
                    {
                        Code = "K6SF",
                        Descriptions = "Description 1",
                        Name = "Standard 1"
                    },
                //     new ()
                //     {
                //         Code = "K6SD",
                //         Descriptions = "Description 2",
                //         Name = "Standard 2"
                //     },
                //     new ()
                //     {
                //         Code = "K6SD",
                //         Descriptions = "Description 3",
                //         Name = "Standard 3"
                //     },
                //     new ()
                //     {
                //         Code = "K6SD",
                //         Descriptions = "Description 4",
                //         Name = "Standard 4"
                //     }
                };

                // // Add Range training program
                // await _context.TrainingPrograms.AddRangeAsync(trainingPrograms);
                // // Add Range syllabus
                await _context.Syllabi.AddRangeAsync(syllabi);
                // // Add Range delivery type
                await _context.DeliveryTypes.AddRangeAsync(deliveryTypes);
                // // Add Range output standard
                await _context.OutputStandards.AddRangeAsync(outputStandards);
                // // Save to DB
                await _context.SaveChangesAsync();

                // // Get first syllabus
                var syllabus = await _context.Syllabi.Where(x =>
                    x.TopicCode != null && x.TopicCode.Equals("TEC144") &&
                    x.TopicName.Equals("Introduction to Programming")).FirstOrDefaultAsync();

                // Add syllabus days 
                for (int i = 0; i <= 6; ++i)
                {
                    syllabus!.SyllabusDays.Add(new()
                    {
                        CreatedBy = "John " + i,
                        CreatedDate = DateTime.Now.AddDays(i),
                        IsDeleted = i % 2 == 0,
                        ModifiedBy = "Jane " + i,
                        ModifiedDate = DateTime.Now.AddDays(1).AddDays(i),
                        DayNo = i + 1,
                        SyllabusId = syllabus!.SyllabusId
                    });
                }

                // Save syllabus days to DB
                await _context.SaveChangesAsync();

                // Get syllabus day of first syllabus
                syllabus = await _context.Syllabi
                    .Where(x =>
                        x.TopicCode != null && x.TopicCode.Equals("TEC144") &&
                        x.TopicName.Equals("Introduction to Programming"))
                    .Include(x => x.SyllabusDays)
                    .FirstOrDefaultAsync();

                int unitIndex = 0;
                foreach (var sDay in syllabus!.SyllabusDays)
                {
                    // Add syllabus unit 
                    sDay.SyllabusUnits.Add(new()
                    {
                        CreatedBy = "John",
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                        ModifiedBy = "Jane",
                        ModifiedDate = DateTime.Now.AddDays(1),
                        Duration = 10,
                        Name = "MVC architecture in ASP.NET",
                        UnitNo = ++unitIndex,
                        SyllabusDayId = sDay.SyllabusDayId,
                    });
                    sDay.SyllabusUnits.Add(new()
                    {
                        CreatedBy = "Alice",
                        CreatedDate = DateTime.Now.AddDays(-1),
                        IsDeleted = true,
                        ModifiedBy = "Bob",
                        ModifiedDate = DateTime.Now.AddDays(2),
                        Duration = null,
                        Name = "Routing in MVC",
                        UnitNo = ++unitIndex,
                        SyllabusDayId = sDay.SyllabusDayId,
                    });
                }

                // save changes
                await _context.SaveChangesAsync();

                var syllabusDays = await _context.SyllabusDays
                    .Where(x => x.SyllabusId == syllabus.SyllabusId)
                    .ToListAsync();

                // int unit = 1;
                foreach (var sDay in syllabusDays)
                {
                    // Get first delivery type
                    deliveryTypes = await _context.DeliveryTypes.OrderByDescending(x => x.Id)
                        .ToListAsync();

                    // Get all output standard
                    var standards = await _context.OutputStandards.OrderByDescending(x => x.Id)
                        .ToListAsync();

                    // Generate list of training materials - Replace to real firebase data later...
                    // var trainingMaterials = new List<TrainingMaterial>()
                    // {
                    //     new()
                    //     {
                    //         CreatedBy = "Joseph",
                    //         FileName = ".NET Introduction overview.pdf",
                    //         CreatedDate = DateTime.ParseExact(
                    //             DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    //         Name = ".NET Introduction overview",
                    //         IsFile = true,
                    //         IsDeleted = false
                    //     }
                    // };

                    int chaperIndex = 0;
                    foreach (var unit in sDay.SyllabusUnits)
                    {
                        unit.UnitChapters.Add(new()
                        {
                            CreatedBy = "User1",
                            CreatedDate = DateTime.Now,
                            IsDeleted = false,
                            ModifiedBy = null,
                            ModifiedDate = null,
                            ChapterNo = ++chaperIndex,
                            Duration = 30,
                            IsOnline = true,
                            Name = "MVC architechture parttern overview",
                            DeliveryType = deliveryTypes[0],
                            OutputStandard = standards[0]
                        });

                        unit.UnitChapters.Add(new()
                        {
                            CreatedBy = "User2",
                            CreatedDate = DateTime.Now.AddDays(-1),
                            IsDeleted = false,
                            ModifiedBy = "Admin",
                            ModifiedDate = DateTime.Now,
                            ChapterNo = 2,
                            Duration = 10,
                            IsOnline = false,
                            Name = "ASP.NET MVC Version History",
                            DeliveryType = deliveryTypes[1],
                            OutputStandard = standards[0]
                        });

                        unit.UnitChapters.Add(new()
                        {
                            CreatedBy = "User3",
                            CreatedDate = DateTime.Now.AddDays(-1),
                            IsDeleted = false,
                            ModifiedBy = "Admin",
                            ModifiedDate = DateTime.Now,
                            ChapterNo = 2,
                            Duration = 30,
                            IsOnline = false,
                            Name = "ASP.NET MVC Folder Structure",
                            DeliveryType = deliveryTypes[2],
                            OutputStandard = standards[0]
                        });

                        unit.UnitChapters.Add(new()
                        {
                            CreatedBy = "User3",
                            CreatedDate = DateTime.Now.AddDays(-1),
                            IsDeleted = false,
                            ModifiedBy = "Admin",
                            ModifiedDate = DateTime.Now,
                            ChapterNo = 2,
                            Duration = 30,
                            IsOnline = false,
                            Name = "Controllers in ASP.NET MVC Application",
                            DeliveryType = deliveryTypes[3],
                            OutputStandard = standards[0]
                        });
                    }



                }
                // Save DB
                await _context.SaveChangesAsync();


                // Get first training program
                // var trainingProgram = await _context.TrainingPrograms.OrderBy(x => x.Id).FirstOrDefaultAsync();
                // // Get list of syllabus
                // syllabi = await _context.Syllabi.OrderBy(x => x.Id).ToListAsync();
                // // Add syllabi to training program
                // syllabi.Take(5).ToList().ForEach(x => trainingProgram!.Syllabi.Add(x));
                // Save DB
                // await _context.SaveChangesAsync();


                Console.WriteLine("--> Seeding Data Successfully");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}