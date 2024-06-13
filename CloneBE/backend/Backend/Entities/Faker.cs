using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Newtonsoft.Json;


namespace Entities
{
    public class Faker
    {

        public void fake(ModelBuilder modelBuilder)
        {

            // Fake "Role" data
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = "4463c0cd-ff13-441c-8cd5-5e68961ec70b", Id = 1, CreatedBy = "1", ModifiedBy = "1", CreatedDate = DateTime.Now, RoleName = "Administrator", Title = "Admin", ModifiedDate = DateTime.Now },
                new Role { RoleId = "81e3b0e8-02f1-4f51-bb68-5b6e465a45b2", Id = 2, CreatedBy = "2", ModifiedBy = "3", CreatedDate = DateTime.Now, RoleName = "Standard", Title = "User", ModifiedDate = DateTime.Now },
                new Role { RoleId = "e2353151-51ec-41f7-907a-fc1d60f15a6d", Id = 3, CreatedBy = "3", ModifiedBy = "3", CreatedDate = DateTime.Now, RoleName = "Manager", Title = "Manager", ModifiedDate = DateTime.Now }
            );


            // Fake "User" data
            modelBuilder.Entity<User>().HasData(

                // Administrator
                new User { Id = 1, FullName = "Dinh The Vinh", UserId = "4463c0cdff13441c8cd55e68961ec70b", Username = "dtvin123", Gender = "Female", ModifiedDate = DateTime.Now, Address = "Something", Avatar = "avatar.png", CreatedDate = DateTime.Now, Dob = DateTime.Now, CreatedBy = "1", Password = "password", ModifiedBy = "2", Phone = "111-111-1111", RoleId = "4463c0cd-ff13-441c-8cd5-5e68961ec70b", Email = "email@gmail.com", Status = false },
                new User { Id = 2, FullName = "Nguyen Tan Phat", UserId = "81e3b0e802f14f51bb685b6e465a45b2", Username = "phatn111", Gender = "Male", ModifiedDate = DateTime.Now, Address = "Something", Avatar = "avatar.png", CreatedDate = DateTime.Now, Dob = DateTime.Now, CreatedBy = "1", Password = "password", ModifiedBy = "2", Phone = "111-111-1111", RoleId = "4463c0cd-ff13-441c-8cd5-5e68961ec70b", Email = "email12@gmail.com", Status = false },

                // Standard
                new User { Id = 3, FullName = "Hoang DUng", UserId = "e235315151ec41f7907afc1d60f15a6d", Username = "hd2k211", Gender = "Male", ModifiedDate = DateTime.Now, Address = "Something", Avatar = "avatar.png", CreatedDate = DateTime.Now, Dob = DateTime.Now, CreatedBy = "1", Password = "password", ModifiedBy = "2", Phone = "111-111-1111", RoleId = "81e3b0e8-02f1-4f51-bb68-5b6e465a45b2", Email = "emai22l@gmail.com", Status = false },
                new User { Id = 4, FullName = "Tan Phat", UserId = "d915d11d38de42a2b6b39ffebbc5bc1d", Username = "tanphatdd", Gender = "Male", ModifiedDate = DateTime.Now, Address = "Something", Avatar = "avatar.png", CreatedDate = DateTime.Now, Dob = DateTime.Now, CreatedBy = "1", Password = "password", ModifiedBy = "2", Phone = "111-111-1111", RoleId = "81e3b0e8-02f1-4f51-bb68-5b6e465a45b2", Email = "emai23l@gmail.com", Status = false },

                // Manager
                new User { Id = 5, FullName = "Truong Pham", UserId = "99b2bc17dbec476e9a99b34a95dd5b26", Username = "Tpham111", Gender = "Female", ModifiedDate = DateTime.Now, Address = "Something", Avatar = "avatar.png", CreatedDate = DateTime.Now, Dob = DateTime.Now, CreatedBy = "1", Password = "password", ModifiedBy = "2", Phone = "111-111-1111", RoleId = "e2353151-51ec-41f7-907a-fc1d60f15a6d", Email = "emai111l@gmail.com", Status = false },
                new User { Id = 6, FullName = "Hoang Tri", UserId = "a3db3e4e0f25445282b2e61a32c582a4", Username = "tien2k2", Gender = "Female", ModifiedDate = DateTime.Now, Address = "Something", Avatar = "avatar.png", CreatedDate = DateTime.Now, Dob = DateTime.Now, CreatedBy = "1", Password = "password", ModifiedBy = "2", Phone = "111-111-1111", RoleId = "e2353151-51ec-41f7-907a-fc1d60f15a6d", Email = "em444ail@gmail.com", Status = false }

                );

            // Fake "Major" data
            modelBuilder.Entity<Major>().HasData(
                new Major { Id = 1, MajorId = "IT101", Name = "Information Technology" },
                new Major { Id = 2, MajorId = "AS101", Name = "Applied Science" },
                new Major { Id = 3, MajorId = "TC101", Name = "Telecommunication" },
                new Major { Id = 4, MajorId = "ER101", Name = "Economy research" }
            );


            // Fake "Student" data
            modelBuilder.Entity<Student>().HasData(
                new Student { StudentId = "cd9fe541-1c6f-4e9e-a94b-1ff748186975",MutatableStudentId="STD1001", CertificationStatus = false, Id = 1, Area = "Ho Chi Minh", MajorId = "IT101", University = "Greenwich", Type = 11, Status = "In Class", Address = JsonConvert.SerializeObject(new Dictionary<string, string> { { "permanent_res", "Vietnam, Ho Chi Minh city" }, { "location", "Ho Chi Minh city" } }), Phone = "111-232-1312", Dob = DateTime.Now, Email = "vinh@fpt.edu.vn", Faaccount = "DTVinh12223", FullName = "Đinh Thế Vinh", Gender = "Male", Gpa = 88, GraduatedDate = DateTime.Now, JoinedDate = DateTime.Now, Recer = "Ho Hai Quang" },
                new Student { StudentId = "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50",MutatableStudentId="STD1002",CertificationStatus = false, Id = 2, Area = "Ho Chi Minh city", MajorId = "AS101", University = "FPT", Type = 23, Status = "In Class", Address = JsonConvert.SerializeObject(new Dictionary<string, string> { { "permanent_res", "Vietnam, Ho Chi Minh city" }, { "location", "Ho Chi Minh city" } }), Phone = "111-111-123", Dob = DateTime.Now, Email = "v122@fpt.edu.vn", Faaccount = "HHSon2k1", FullName = "Hoàng Hải Sơn", Gender = "Male", Gpa = 22, GraduatedDate = DateTime.Now, JoinedDate = DateTime.Now, Recer = "Tran Tat Nghia" },
                new Student { StudentId = "ea21b42d-89a2-4e9e-a6b6-d6a9c7b52480",MutatableStudentId="STD1003",CertificationStatus = false, Id = 3, Area = "Da Nang", MajorId = "TC101", University = "RMIT", Type = 41, Status = "In Class", Address = JsonConvert.SerializeObject(new Dictionary<string, string> { { "permanent_res", "Vietnam, Ha Noi" }, { "location", "Ha Noi" } }), Phone = "111-111-33", Dob = DateTime.Now, Email = "add112@fpt.edu.vn", Faaccount = "DungNQ111", FullName = "Nguyễn Quang Dũng", Gender = "Male", Gpa = 12, GraduatedDate = DateTime.Now, JoinedDate = DateTime.Now, Recer = "Nguyen Quang Dung" },
                new Student { StudentId = "3dc8f1f1-0a7a-4db4-91c3-98b93ec1b2a3",MutatableStudentId="STD1004",CertificationStatus = false, Id = 4, Area = "Da Nang", MajorId = "TC101", University = "Hutech", Type = 2, Status = "Inactive", Address = JsonConvert.SerializeObject(new Dictionary<string, string> { { "permanent_res", "United States" }, { "location", "New York" } }), Phone = "111-111-2411", Dob = DateTime.Now, Email = "camecam@fpt.edu.vn", Faaccount = "CarmOD1k9", FullName = "Carmila Odesn", Gender = "Male", Gpa = 32, GraduatedDate = DateTime.Now, JoinedDate = DateTime.Now, Recer = "Ho Hai Quang" },
                new Student { StudentId = "97d39a95-2e4b-437f-a032-35e6357f06aa",MutatableStudentId="STD1005",CertificationStatus = false, Id = 5, Area = "Hue", MajorId = "AS101", University = "Tampere", Type = 22, Status = "In Class", Address = JsonConvert.SerializeObject(new Dictionary<string, string> { { "permanent_res", "Finland" }, { "location", "Tampere" } }), Phone = "111-111-2411", Dob = DateTime.Now, Email = "hestamp@fpt.edu.vn", Faaccount = "HelT112", FullName = "Helsinji Tampe", Gender = "Male", Gpa = 88, GraduatedDate = DateTime.Now, JoinedDate = DateTime.Now, Recer = "Tran Tat Nghia" },
                new Student { StudentId = "35f2f906-2a79-442e-b7d8-2f3f59c7c89c",MutatableStudentId="STD1006",CertificationStatus = false, Id = 6, Area = "Helsinki", MajorId = "AS101", University = "Helsinki", Type = 18, Status = "Inactive", Address = JsonConvert.SerializeObject(new Dictionary<string, string> { { "permanent_res", "Finland" }, { "location", "Helsinki" } }), Phone = "123-456-7890", Dob = DateTime.Parse("2000-05-15"), Email = "example1@example.com", Faaccount = "example1", FullName = "John Doe", Gender = "Female", Gpa = 75, GraduatedDate = DateTime.Parse("2023-06-30"), JoinedDate = DateTime.Parse("2020-09-01"), Recer = "Alice Johnson" },
                new Student { StudentId = "d269b62c-c9dc-4d8f-8b3c-602d88e72438",MutatableStudentId="STD1007",CertificationStatus = false, Id = 7, Area = "Turku", MajorId = "ER101", University = "Turku", Type = 20, Status = "In Class", Address = JsonConvert.SerializeObject(new Dictionary<string, string> { { "permanent_res", "Finland" }, { "location", "Turku" } }), Phone = "987-654-3210", Dob = DateTime.Parse("2001-08-20"), Email = "example2@example.com", Faaccount = "example2", FullName = "Jane Smith", Gender = "Male", Gpa = 92, GraduatedDate = DateTime.Parse("2024-05-31"), JoinedDate = DateTime.Parse("2021-08-15"), Recer = "Bob Anderson" },
                new Student { StudentId = "f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6",MutatableStudentId="STD1008",CertificationStatus = false, Id = 8, Area = "Oulu", MajorId = "AS101", University = "Oulu", Type = 21, Status = "Inactive", Address = JsonConvert.SerializeObject(new Dictionary<string, string> { { "permanent_res", "Finland" }, { "location", "Oulu" } }), Phone = "456-789-0123", Dob = DateTime.Parse("1999-02-10"), Email = "example3@example.com", Faaccount = "example3", FullName = "Emma Johnson", Gender = "Female", Gpa = 85, GraduatedDate = DateTime.Parse("2022-12-31"), JoinedDate = DateTime.Parse("2019-09-01"), Recer = "David Williams" },
                new Student { StudentId = "d5d5c72c-6b4a-4299-bc57-29cb8120e118",MutatableStudentId="STD1009",CertificationStatus = false, Id = 9, Area = "Vaasa", MajorId = "ER101", University = "Vaasa", Type = 19, Status = "In Class", Address = JsonConvert.SerializeObject(new Dictionary<string, string> { { "permanent_res", "Finland" }, { "location", "Vaasa" } }), Phone = "789-012-3456", Dob = DateTime.Parse("2002-11-25"), Email = "example4@example.com", Faaccount = "example4", FullName = "Michael Brown", Gender = "Male", Gpa = 68, GraduatedDate = DateTime.Parse("2025-06-30"), JoinedDate = DateTime.Parse("2022-08-15"), Recer = "Emily Garcia" },
                new Student { StudentId = "e8011711-9367-404e-b0a1-3cfa0e54f015",MutatableStudentId="STD1010",CertificationStatus = false, Id = 10, Area = "Tampere", MajorId = "TC101", University = "Tampere", Type = 22, Status = "In Class", Address = JsonConvert.SerializeObject(new Dictionary<string, string> { { "permanent_res", "Finland" }, { "location", "Tampere" } }), Phone = "111-111-2411", Dob = DateTime.Now, Email = "hestamp@fpt.edu.vn", Faaccount = "HelT112", FullName = "Helsinji Tampe", Gender = "Male", Gpa = 88, GraduatedDate = DateTime.Now, JoinedDate = DateTime.Now, Recer = "Tran Tat Nghia" },
                new Student { StudentId = "a4f15d45-0b76-4c1b-97b5-6a6b27229aae",MutatableStudentId="STD1011",CertificationStatus = false, Id = 11, Area = "Espoo", MajorId = "AS101", University = "Espoo", Type = 23, Status = "Inactive", Address = JsonConvert.SerializeObject(new Dictionary<string, string> { { "permanent_res", "Finland" }, { "location", "Espoo" } }), Phone = "555-555-5555", Dob = DateTime.Parse("2003-04-17"), Email = "example6@example.com", Faaccount = "example6", FullName = "Olivia Miller", Gender = "Female", Gpa = 81, GraduatedDate = DateTime.Parse("2026-06-30"), JoinedDate = DateTime.Parse("2023-09-01"), Recer = "Daniel Moore" },
                new Student { StudentId = "815dd6f4-2d41-4c6c-a032-5e78a1cf065b",MutatableStudentId="STD1012",CertificationStatus = false, Id = 12, Area = "Jyväskylä", MajorId = "TC101", University = "Jyväskylä", Type = 24, Status = "In Class", Address = JsonConvert.SerializeObject(new Dictionary<string, string> { { "permanent_res", "Finland" }, { "location", "Jyväskylä" } }), Phone = "222-222-2222", Dob = DateTime.Parse("2004-07-05"), Email = "example7@example.com", Faaccount = "example7", FullName = "William Wilson", Gender = "Male", Gpa = 95, GraduatedDate = DateTime.Parse("2027-05-31"), JoinedDate = DateTime.Parse("2024-08-15"), Recer = "Sophia Taylor" },
                new Student { StudentId = "573e5801-f47e-4a82-bcb5-90fc722e4d4f",MutatableStudentId= "STD1013", CertificationStatus = false, Id = 13, Area = "Kuopio", MajorId = "IT101", University = "Kuopio", Type = 25, Status = "Inactive", Address = JsonConvert.SerializeObject(new Dictionary<string, string> { { "permanent_res", "Finland" }, { "location", "Kuopio" } }), Phone = "333-333-3333", Dob = DateTime.Parse("2005-10-12"), Email = "example8@example.com", Faaccount = "example8", FullName = "Liam Brown", Gender = "Male", Gpa = 72, GraduatedDate = DateTime.Parse("2028-12-31"), JoinedDate = DateTime.Parse("2025-09-01"), Recer = "Mia Clark" },
                new Student { StudentId = "39ffcf76-0db7-4f95-b1ae-3f527c8fe5a7", MutatableStudentId = "STD1014", CertificationStatus = false, Id = 14, Area = "Rovaniemi", MajorId = "IT101", University = "Rovaniemi", Type = 26, Status = "In Class", Address = JsonConvert.SerializeObject(new Dictionary<string, string> { { "permanent_res", "Finland" }, { "location", "Rovaniemi" } }), Phone = "444-444-4444", Dob = DateTime.Parse("2006-01-30"), Email = "example9@example.com", Faaccount = "example9", FullName = "Noah Anderson", Gender = "Male", Gpa = 88, GraduatedDate = DateTime.Parse("2029-06-30"), JoinedDate = DateTime.Parse("2026-08-15"), Recer = "Ethan Adams" },
                new Student { StudentId = "b739b16a-24eb-49bb-b57a-86f8c3c9c2fc", MutatableStudentId = "STD1015", CertificationStatus = false, Id = 15, Area = "Lahti", MajorId = "TC101", University = "Lahti", Type = 27, Status = "Inactive", Address = JsonConvert.SerializeObject(new Dictionary<string, string> { { "permanent_res", "Finland" }, { "location", "Lahti" } }), Phone = "666-666-6666", Dob = DateTime.Parse("2007-12-20"), Email = "example10@example.com", Faaccount = "example10", FullName = "Sophia Wilson", Gender = "Female", Gpa = 79, GraduatedDate = DateTime.Parse("2030-05-31"), JoinedDate = DateTime.Parse("2027-09-01"), Recer = "Logan Baker" }
                );





            // Fake "TechnicalProgram" data
            modelBuilder.Entity<TechnicalGroup>().HasData(
                new TechnicalGroup { Id = 1, TechnicalGroupId = "e792ad2b-9d75-46f3-a1f0-eb3184372f92", Description = "Group1", TechnicalGroupName = "GRTECH1" },
                new TechnicalGroup { Id = 2, TechnicalGroupId = "3fc26468-3fd0-49ff-bde4-0b2484aa3c3f", Description = "Group2", TechnicalGroupName = "GRTECH2" },
                new TechnicalGroup { Id = 3, TechnicalGroupId = "b9358a53-d1c1-4d3d-8f22-1e75a8a9a6d8", Description = "Group3", TechnicalGroupName = "GRTECH3" }
                );

            // Fake "TechnicalGroup" data
            modelBuilder.Entity<TechnicalCode>().HasData(
                new TechnicalCode { Id = 1, TechnicalCodeId = "7a9b5e84-1a8c-4e57-bfc7-23dcb6d78e92", Description = "Technical 1", TechnicalCodeName = "TECH1" },
                new TechnicalCode { Id = 2, TechnicalCodeId = "4f58ebf5-79d8-4fb9-bd7b-12119bfa3171", Description = "Technical 2", TechnicalCodeName = "TECH2" },
                new TechnicalCode { Id = 3, TechnicalCodeId = "ac2a35a0-c9d7-4a19-b1d4-0f0e6c1f14c7", Description = "Technical 3", TechnicalCodeName = "TECH3" }
            );


            // Fake "TrainingProgram" data
            modelBuilder.Entity<TrainingProgram>().HasData(
                new TrainingProgram { TechnicalCodeId = "7a9b5e84-1a8c-4e57-bfc7-23dcb6d78e92", TechnicalGroupId = "e792ad2b-9d75-46f3-a1f0-eb3184372f92", UserId = "4463c0cdff13441c8cd55e68961ec70b", TrainingProgramCode = "b26cfe3f-6d6c-4d24-afe0-12e21715b042", Id = 1, Name = "Introduction to SQL", Status = "Done", CreatedBy = "1", Days = 10, Hours = 10, StartTime = new TimeOnly(10, 30, 15), CreatedDate = DateTime.Now, UpdatedBy = "0", UpdatedDate = DateTime.Now },
                new TrainingProgram { TechnicalCodeId = "4f58ebf5-79d8-4fb9-bd7b-12119bfa3171", TechnicalGroupId = "3fc26468-3fd0-49ff-bde4-0b2484aa3c3f", UserId = "e235315151ec41f7907afc1d60f15a6d", TrainingProgramCode = "8c06310b-4d44-4e0e-a7a0-b84d09ccfc3a", Id = 2, Name = "Advanced Data Analysis", Status = "Not yet", CreatedBy = "0", StartTime = new TimeOnly(12, 17, 5), CreatedDate = DateTime.Now, UpdatedBy = "0", UpdatedDate = DateTime.Now },
                new TrainingProgram { TechnicalCodeId = "ac2a35a0-c9d7-4a19-b1d4-0f0e6c1f14c7", TechnicalGroupId = "b9358a53-d1c1-4d3d-8f22-1e75a8a9a6d8", UserId = "a3db3e4e0f25445282b2e61a32c582a4", TrainingProgramCode = "1f749079-2a92-4f32-aa0b-8391a028cfe4", Id = 3, Name = "Python Fundamentals", Status = "Done", CreatedBy = "0", StartTime = new TimeOnly(6, 20, 25), CreatedDate = DateTime.Now, UpdatedBy = "0", UpdatedDate = DateTime.Now }
            );


            // Fake "Module" data
            modelBuilder.Entity<Module>().HasData(
                new Module { Id = 1, ModuleId = "6e68f0c2-d9b2-4ab7-8b3b-8fbf81a8dd40", ModuleName = "Python programming", CreatedDate = DateTime.Now.AddDays(-20), UpdatedDate = DateTime.Now, UpdatedBy = "", CreatedBy = "Dinh The Vinh" },
                new Module { Id = 2, ModuleId = "b684c89f-7b7e-4145-b345-9347a67673a3", ModuleName = "C# programming", CreatedDate = DateTime.Now.AddDays(-22), UpdatedDate = DateTime.Now, UpdatedBy = "", CreatedBy = "Dinh The Vinh" },
                new Module { Id = 3, ModuleId = "f74e3a7a-312f-4f80-86cb-25f7d52735bc", ModuleName = "OOP Module", CreatedDate = DateTime.Now.AddDays(-23), UpdatedDate = DateTime.Now, UpdatedBy = "", CreatedBy = "Dinh The Vinh" },
                new Module { Id = 4, ModuleId = "9bb0a5cb-6bf6-418b-b549-4b3d8abbebb7", ModuleName = "Cyber security", CreatedDate = DateTime.Now.AddDays(-24), UpdatedDate = DateTime.Now, UpdatedBy = "", CreatedBy = "Dinh The Vinh" },
                new Module { Id = 5, ModuleId = "315fd315-6764-4514-a289-569c07b91894", ModuleName = "Fundamental programmng", CreatedDate = DateTime.Now.AddDays(-25), UpdatedDate = DateTime.Now, UpdatedBy = "", CreatedBy = "Dinh The Vinh" },
                new Module { Id = 6, ModuleId = "c485fd06-7b81-470d-a4d1-2e66e8fbd11d", ModuleName = "Lua programming", CreatedDate = DateTime.Now.AddDays(-27), UpdatedDate = DateTime.Now, UpdatedBy = "", CreatedBy = "Dinh The Vinh" },
                new Module { Id = 7, ModuleId = "09cf6935-9c54-49c5-8a48-202268ad4f55", ModuleName = "C++ programming", CreatedDate = DateTime.Now.AddDays(-22), UpdatedDate = DateTime.Now, UpdatedBy = "", CreatedBy = "Dinh The Vinh" },
                new Module { Id = 8, ModuleId = "5488d35d-0a1e-4634-898e-92dbde019029", ModuleName = "Project Management", CreatedDate = DateTime.Now.AddDays(-29), UpdatedDate = DateTime.Now, UpdatedBy = "", CreatedBy = "Dinh The Vinh" },
                new Module { Id = 9, ModuleId = "d067d9d4-2c4e-4dcf-85aa-30edc14263e2", ModuleName = "DevOps practices", CreatedDate = DateTime.Now.AddDays(-30), UpdatedDate = DateTime.Now, UpdatedBy = "", CreatedBy = "Dinh The Vinh" },
                new Module { Id = 10, ModuleId = "c8a4b3cc-78c5-4f20-804e-d617c35aa769", ModuleName = "ASP.NET Fundamentals", CreatedDate = DateTime.Now.AddDays(-31), UpdatedDate = DateTime.Now, UpdatedBy = "", CreatedBy = "Dinh The Vinh" }
            );

            // Fake "Class" data
            modelBuilder.Entity<Class>().HasData(
                new Class { Id = 1, ClassId = "176d899b-1c24-49fc-baf1-8755ef89f1b3", ClassStatus = "Finished", ClassCode = "CL1", ClassName = "Web Development Fundamentals", CreatedBy = "0", CreatedDate = DateTime.Now, UpdatedBy = "0", UpdatedDate = DateTime.Now, Duration = 30, EndDate = new DateOnly(2022, 5, 3), StartDate = new DateOnly(2022, 1, 1), ApprovedBy = "1", ApprovedDate = DateTime.Now, StartTime = new TimeOnly(6, 20, 25), ActualAttendee = 5, AcceptedAttendee = 2, ReviewBy = "0", ReviewDate = DateTime.Now, EndTime = new TimeOnly(10, 24, 22), PlannedAttendee = 10, SlotTime = "11" },
                new Class { Id = 2, ClassId = "4fb5a126-7a1a-4dc5-ae48-689aa5f26464", ClassStatus = "In class", ClassCode = "CL2", ClassName = "Advanced Web Development", CreatedBy = "0", CreatedDate = DateTime.Now, UpdatedBy = "0", UpdatedDate = DateTime.Now, Duration = 45, EndDate = new DateOnly(2022, 12, 2), StartDate = new DateOnly(2022, 2, 28), ApprovedBy = "1", ApprovedDate = DateTime.Now, StartTime = new TimeOnly(6, 20, 25), ActualAttendee = 5, AcceptedAttendee = 2, ReviewBy = "0", ReviewDate = DateTime.Now, EndTime = new TimeOnly(10, 24, 22), PlannedAttendee = 10, SlotTime = "11" },
                new Class { Id = 3, ClassId = "a5c34e23-2b1f-4ef6-80b9-78ac176d091e", ClassStatus = "In class", ClassCode = "CL3", ClassName = "Mobile App Development Basics", CreatedBy = "0", CreatedDate = DateTime.Now, UpdatedBy = "0", UpdatedDate = DateTime.Now, Duration = 23, EndDate = new DateOnly(2023, 6, 1), StartDate = new DateOnly(2023, 3, 17), ApprovedBy = "1", ApprovedDate = DateTime.Now, StartTime = new TimeOnly(6, 20, 25), ActualAttendee = 5, AcceptedAttendee = 2, ReviewBy = "0", ReviewDate = DateTime.Now, EndTime = new TimeOnly(10, 24, 22), PlannedAttendee = 10, SlotTime = "11" },
                new Class { Id = 4, ClassId = "6bfb28e9-10c2-4a02-9755-c1fcb9a01d14", ClassStatus = "Finished", ClassCode = "CL4", ClassName = "Data Science Essentials", CreatedBy = "0", CreatedDate = DateTime.Now, UpdatedBy = "0", UpdatedDate = DateTime.Now, Duration = 15, EndDate = new DateOnly(2022, 11, 10), StartDate = new DateOnly(2022, 5, 27), ApprovedBy = "1", ApprovedDate = DateTime.Now, StartTime = new TimeOnly(6, 20, 25), ActualAttendee = 5, AcceptedAttendee = 2, ReviewBy = "0", ReviewDate = DateTime.Now, EndTime = new TimeOnly(10, 24, 22), PlannedAttendee = 10, SlotTime = "11" },
                new Class { Id = 5, ClassId = "e4e338a8-4413-4fb6-8652-990e20c40526", ClassStatus = "In class", ClassCode = "CL5", ClassName = "Cybersecurity Fundamentals", CreatedBy = "0", CreatedDate = DateTime.Now, UpdatedBy = "0", UpdatedDate = DateTime.Now, Duration = 30, EndDate = new DateOnly(2022, 2, 28), StartDate = new DateOnly(2022, 1, 2), ApprovedBy = "1", ApprovedDate = DateTime.Now, StartTime = new TimeOnly(6, 20, 25), ActualAttendee = 5, AcceptedAttendee = 2, ReviewBy = "0", ReviewDate = DateTime.Now, EndTime = new TimeOnly(10, 24, 22), PlannedAttendee = 10, SlotTime = "11" },
                new Class { Id = 6, ClassId = "04b2851a-5d19-4aa2-aa14-f8d68d0c90b9", ClassStatus = "Finished", ClassCode = "CL6", ClassName = "Artificial Intelligence Basics", CreatedBy = "0", CreatedDate = DateTime.Now, UpdatedBy = "0", UpdatedDate = DateTime.Now, Duration = 9, EndDate = new DateOnly(2012, 8, 23), StartDate = new DateOnly(2022, 6, 23), ApprovedBy = "1", ApprovedDate = DateTime.Now, StartTime = new TimeOnly(6, 20, 25), ActualAttendee = 5, AcceptedAttendee = 2, ReviewBy = "0", ReviewDate = DateTime.Now, EndTime = new TimeOnly(10, 24, 22), PlannedAttendee = 10, SlotTime = "11" }
            );


            // Fake "StudentClass" data
            modelBuilder.Entity<StudentClass>().HasData(
                new StudentClass { Id = 1, Method = 3, StudentClassId = "2f1e6d1d-7a0a-4b42-b4d1-25f0e7b4a4f1", Result = 10, StudentId = "815dd6f4-2d41-4c6c-a032-5e78a1cf065b", ClassId = "04b2851a-5d19-4aa2-aa14-f8d68d0c90b9", AttendingStatus = "Active", FinalScore = 55, Gpalevel = 3 },
               new StudentClass { Id = 2, Method = 1, StudentClassId = "d32f4d16-78d6-4fe1-89d2-9653c8b12c6d", Result = 92, StudentId = "35f2f906-2a79-442e-b7d8-2f3f59c7c89c", ClassId = "e4e338a8-4413-4fb6-8652-990e20c40526", AttendingStatus = "Active",  FinalScore = 35, Gpalevel = 3 },
               new StudentClass { Id = 3, Method = 2, StudentClassId = "9ae7ec99-1ee5-4a61-b2ff-308b14f2bb38", Result = 80, StudentId = "815dd6f4-2d41-4c6c-a032-5e78a1cf065b", ClassId = "04b2851a-5d19-4aa2-aa14-f8d68d0c90b9", AttendingStatus = "Active", FinalScore = 78, Gpalevel = 1 },
               new StudentClass { Id = 4, Method = 2, StudentClassId = "c7e1f7c5-615e-4328-8e4f-77a70c57fd52", Result = 23, StudentId = "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", ClassId = "6bfb28e9-10c2-4a02-9755-c1fcb9a01d14", AttendingStatus = "Active", FinalScore = 85, Gpalevel = 2 },
               new StudentClass { Id = 5, Method = 1, StudentClassId = "b23a6a23-b8eb-4ec6-bd34-c3e11b446441", Result = 5, StudentId = "f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", ClassId = "6bfb28e9-10c2-4a02-9755-c1fcb9a01d14", AttendingStatus = "Active", FinalScore = 92, Gpalevel = 3 },
               new StudentClass { Id = 6, Method = 2, StudentClassId = "5a9e7fd7-d49e-4c5b-946b-dac02ac8565d", Result = 18, StudentId = "cd9fe541-1c6f-4e9e-a94b-1ff748186975", ClassId = "176d899b-1c24-49fc-baf1-8755ef89f1b3", AttendingStatus = "Active", FinalScore = 70, Gpalevel = 1 },
               new StudentClass { Id = 7, Method = 3, StudentClassId = "87acbe6a-7bcb-4c59-ba27-7f62421709f9", Result = 63, StudentId = "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", ClassId = "176d899b-1c24-49fc-baf1-8755ef89f1b3", AttendingStatus = "Active", FinalScore = 63, Gpalevel = 2 }
                );





            // Fake "Assignment" data
            modelBuilder.Entity<Assignment>().HasData(
                new Assignment { Id = 1, ModuleId  = "6e68f0c2-d9b2-4ab7-8b3b-8fbf81a8dd40" , AssignmentId = "7e18a9ef-3723-472b-afd5-6786c4545d54", AssignmentName = "Practice 1", Description = "Assignment 1 About", DueDate = DateTime.Now.AddDays(4), AssignmentType = 1, CreatedBy = "Dinh The Vinh", CreatedDate = DateTime.Now.AddDays(-20), UpdatedBy = "", UpdatedDate = DateTime.Now },
                new Assignment { Id = 2, ModuleId  = "b684c89f-7b7e-4145-b345-9347a67673a3" , AssignmentId = "a11e94d1-ebec-48d5-84e6-7c8589a1851f", AssignmentName = "Practice 2", Description = "Assignment 2 About", DueDate = DateTime.Now.AddDays(5), AssignmentType = 2, CreatedBy = "Dinh The Vinh", CreatedDate = DateTime.Now.AddDays(-20), UpdatedBy = "", UpdatedDate = DateTime.Now },
                new Assignment { Id = 3, ModuleId  = "c485fd06-7b81-470d-a4d1-2e66e8fbd11d", AssignmentId = "e921a7f7-78e8-4f13-82d5-2d2b2c35ab44", AssignmentName = "Practice 3", Description = "Assignment 3 About", DueDate = DateTime.Now.AddDays(6), AssignmentType = 3, CreatedBy = "Dinh The Vinh", CreatedDate = DateTime.Now.AddDays(-20), UpdatedBy = "", UpdatedDate = DateTime.Now },
                new Assignment { Id = 4, ModuleId = "c8a4b3cc-78c5-4f20-804e-d617c35aa769", AssignmentId   = "3f257a47-0c8b-4b06-aa0d-487d03e83db7", AssignmentName = "Practice Final", Description = "Practice Final About", DueDate = DateTime.Now.AddDays(6), AssignmentType = 3, CreatedBy = "Dinh The Vinh", CreatedDate = DateTime.Now.AddDays(-20), UpdatedBy = "", UpdatedDate = DateTime.Now }
                );

            // Fake "Quiz" data
            modelBuilder.Entity<Quiz>().HasData(
                new Quiz { Id = 1, ModuleId=  "c485fd06-7b81-470d-a4d1-2e66e8fbd11d", QuizName = "HTML", QuizId   = "a4b6eb12-22df-4eeb-bc98-7d3f2b27b67a", CreateDate = DateTime.Now.AddDays(-20), CreatedBy= "Dinh The Vinh", UpdatedBy="", UpdatedDate= DateTime.Now },
                new Quiz { Id = 2, ModuleId = "5488d35d-0a1e-4634-898e-92dbde019029", QuizName = "CSS", QuizId    = "e1af1567-8259-4b10-91bf-0f9ff57a2a42", CreateDate = DateTime.Now.AddDays(-21), CreatedBy = "Dinh The Vinh", UpdatedBy = "", UpdatedDate = DateTime.Now },
                new Quiz { Id = 3, ModuleId = "c8a4b3cc-78c5-4f20-804e-d617c35aa769", QuizName = "Quiz 3", QuizId = "3cfc58d0-eb6f-4725-aa63-44f065d1dbb8", CreateDate = DateTime.Now.AddDays(-22), CreatedBy = "Dinh The Vinh", UpdatedBy = "", UpdatedDate = DateTime.Now },
                new Quiz { Id = 4, ModuleId = "c485fd06-7b81-470d-a4d1-2e66e8fbd11d", QuizName = "Quiz 4", QuizId = "f5b34985-2322-4884-9d29-3d550b2cf0a4", CreateDate = DateTime.Now.AddDays(-23), CreatedBy = "Dinh The Vinh", UpdatedBy = "", UpdatedDate = DateTime.Now },
                new Quiz { Id = 5, ModuleId = "c8a4b3cc-78c5-4f20-804e-d617c35aa769", QuizName = "Quiz 5", QuizId = "71ac61b5-5991-4977-a9e1-38235d18b7c5", CreateDate = DateTime.Now.AddDays(-24), CreatedBy = "Dinh The Vinh", UpdatedBy = "", UpdatedDate = DateTime.Now },
                new Quiz { Id = 6, ModuleId = "d067d9d4-2c4e-4dcf-85aa-30edc14263e2", QuizName = "Quiz 6", QuizId = "88e6f2b6-3f82-45c7-a5b9-3768b2cc2d85", CreateDate = DateTime.Now.AddDays(-25), CreatedBy = "Dinh The Vinh", UpdatedBy = "", UpdatedDate = DateTime.Now },
                new Quiz { Id = 7, ModuleId = "09cf6935-9c54-49c5-8a48-202268ad4f55", QuizName = "Quiz Final", QuizId = "8f3d5fe2-9c61-4fd5-92b2-e3c06b7c51e7", CreateDate = DateTime.Now.AddDays(-19), CreatedBy = "Dinh The Vinh", UpdatedBy = "", UpdatedDate = DateTime.Now }
                );

/*
            c485fd06-7b81-470d-a4d1-2e66e8fbd11d
            5488d35d-0a1e-4634-898e-92dbde019029
            c8a4b3cc-78c5-4f20-804e-d617c35aa769
            c485fd06-7b81-470d-a4d1-2e66e8fbd11d
            c8a4b3cc-78c5-4f20-804e-d617c35aa769
            d067d9d4-2c4e-4dcf-85aa-30edc14263e2*/


            // Fake "QuizStudent" data
            modelBuilder.Entity<QuizStudent>().HasData(
                new QuizStudent { Id = 1 , QuizId = "a4b6eb12-22df-4eeb-bc98-7d3f2b27b67a", QuizStudentId = "aab179ec-9467-4db9-a17e-2c7ef6405d3d", StudentId = "573e5801-f47e-4a82-bcb5-90fc722e4d4f", Score = 23, SubmissionDate = DateTime.Now.AddDays(6) },
                new QuizStudent { Id = 2 , QuizId = "e1af1567-8259-4b10-91bf-0f9ff57a2a42", QuizStudentId = "60ac2dcf-7a67-4fa5-bca3-c4e59d101fb7", StudentId = "97d39a95-2e4b-437f-a032-35e6357f06aa", Score = 56, SubmissionDate = DateTime.Now.AddDays(5) },
                new QuizStudent { Id = 3 , QuizId = "3cfc58d0-eb6f-4725-aa63-44f065d1dbb8", QuizStudentId = "a86cb2d2-0e32-4326-aa9d-5d417a96cd25", StudentId = "573e5801-f47e-4a82-bcb5-90fc722e4d4f", Score = 82, SubmissionDate = DateTime.Now.AddDays(10) },
                new QuizStudent { Id = 4 , QuizId = "f5b34985-2322-4884-9d29-3d550b2cf0a4", QuizStudentId = "71e684c2-ef11-45ad-89cb-832fd28928b3", StudentId = "97d39a95-2e4b-437f-a032-35e6357f06aa", Score = 77, SubmissionDate = DateTime.Now.AddDays(5) },
                new QuizStudent { Id = 5 , QuizId = "71ac61b5-5991-4977-a9e1-38235d18b7c5", QuizStudentId = "eba6827d-f367-41b1-88bf-414bd56fe53c", StudentId = "e8011711-9367-404e-b0a1-3cfa0e54f015", Score = 52, SubmissionDate = DateTime.Now.AddDays(4) },
                new QuizStudent { Id = 6 , QuizId = "88e6f2b6-3f82-45c7-a5b9-3768b2cc2d85", QuizStudentId = "14b7f4a6-d13e-4c87-b344-37613c46e91d", StudentId = "e8011711-9367-404e-b0a1-3cfa0e54f015", Score = 63, SubmissionDate = DateTime.Now.AddDays(5) },
                new QuizStudent { Id = 7 , QuizId = "a4b6eb12-22df-4eeb-bc98-7d3f2b27b67a", QuizStudentId = "c3b869df-1b82-4318-95f6-7b31780c107e", StudentId = "815dd6f4-2d41-4c6c-a032-5e78a1cf065b", Score = 100, SubmissionDate = DateTime.Now.AddDays(13) },
                new QuizStudent { Id = 8 , QuizId = "e1af1567-8259-4b10-91bf-0f9ff57a2a42", QuizStudentId = "6fbc7b7a-308b-4a49-bd52-99ff76a07094", StudentId = "39ffcf76-0db7-4f95-b1ae-3f527c8fe5a7", Score = 98, SubmissionDate = DateTime.Now.AddDays(7) },
                new QuizStudent { Id = 9 , QuizId = "3cfc58d0-eb6f-4725-aa63-44f065d1dbb8", QuizStudentId = "93bfb29e-4f1f-4f69-858e-92f4f6ed8ef0", StudentId = "815dd6f4-2d41-4c6c-a032-5e78a1cf065b", Score = 86, SubmissionDate = DateTime.Now.AddDays(8) },
                new QuizStudent { Id = 10, QuizId = "f5b34985-2322-4884-9d29-3d550b2cf0a4", QuizStudentId = "314ff57a-1a7f-4dd5-9ff0-2bdcf163fc5f", StudentId = "39ffcf76-0db7-4f95-b1ae-3f527c8fe5a7", Score = 23, SubmissionDate = DateTime.Now.AddDays(20) },
                new QuizStudent { Id = 11, QuizId = "71ac61b5-5991-4977-a9e1-38235d18b7c5", QuizStudentId = "75b01c5b-363b-4d39-b2d2-93bf41e95a4a", StudentId = "f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", Score = 67, SubmissionDate = DateTime.Now.AddDays(9) },
                new QuizStudent { Id = 12, QuizId = "88e6f2b6-3f82-45c7-a5b9-3768b2cc2d85", QuizStudentId = "e0c6dc55-6c79-4e2b-a1e3-38f70062d1f9", StudentId = "a4f15d45-0b76-4c1b-97b5-6a6b27229aae", Score = 34, SubmissionDate = DateTime.Now.AddDays(2) },
                new QuizStudent { Id = 13, QuizId = "a4b6eb12-22df-4eeb-bc98-7d3f2b27b67a", QuizStudentId = "7f85e30e-2e5b-4c51-b91e-2213b0246709", StudentId = "d5d5c72c-6b4a-4299-bc57-29cb8120e118", Score = 75, SubmissionDate = DateTime.Now.AddDays(5) },
                new QuizStudent { Id = 14, QuizId = "e1af1567-8259-4b10-91bf-0f9ff57a2a42", QuizStudentId = "2eab2e34-3454-4315-818a-c2284462b4c6", StudentId = "d5d5c72c-6b4a-4299-bc57-29cb8120e118", Score = 99, SubmissionDate = DateTime.Now.AddDays(10) },
                new QuizStudent { Id = 15, QuizId = "3cfc58d0-eb6f-4725-aa63-44f065d1dbb8", QuizStudentId = "a2d3d344-7e50-4cb2-9ae6-d6020df012fb", StudentId = "f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", Score = 89, SubmissionDate = DateTime.Now.AddDays(20) },
                new QuizStudent { Id = 16, QuizId = "f5b34985-2322-4884-9d29-3d550b2cf0a4", QuizStudentId = "53944115-bbb0-4eb8-b437-5eaf20b0fd1e", StudentId = "d269b62c-c9dc-4d8f-8b3c-602d88e72438", Score = 49, SubmissionDate = DateTime.Now.AddDays(8) },
                new QuizStudent { Id = 17, QuizId = "71ac61b5-5991-4977-a9e1-38235d18b7c5", QuizStudentId = "5a39a8fc-24d3-46e0-94ed-5e8f8a3b17b7", StudentId = "f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", Score = 76, SubmissionDate = DateTime.Now.AddDays(9) },
                new QuizStudent { Id = 18, QuizId = "88e6f2b6-3f82-45c7-a5b9-3768b2cc2d85", QuizStudentId = "18686b3f-042b-4b13-a3c5-67b3f9a7eab0", StudentId = "d269b62c-c9dc-4d8f-8b3c-602d88e72438", Score = 87, SubmissionDate = DateTime.Now.AddDays(8) },

                new QuizStudent { Id = 19, QuizId = "e1af1567-8259-4b10-91bf-0f9ff57a2a42", QuizStudentId = "3064c949-2940-4e61-aa94-c0e1a3927754", StudentId = "35f2f906-2a79-442e-b7d8-2f3f59c7c89c", Score = 67, SubmissionDate = DateTime.Now.AddDays(23) },
                new QuizStudent { Id = 20, QuizId = "3cfc58d0-eb6f-4725-aa63-44f065d1dbb8", QuizStudentId = "d6b0b963-4d06-4908-b479-69c097e9295a", StudentId = "d269b62c-c9dc-4d8f-8b3c-602d88e72438", Score = 87, SubmissionDate = DateTime.Now.AddDays(17) },
                new QuizStudent { Id = 21, QuizId = "f5b34985-2322-4884-9d29-3d550b2cf0a4", QuizStudentId = "0b504d2e-3681-4e76-9f3d-4f4a02eb98f7", StudentId = "35f2f906-2a79-442e-b7d8-2f3f59c7c89c", Score = 67, SubmissionDate = DateTime.Now.AddDays(27) },
                new QuizStudent { Id = 22, QuizId = "71ac61b5-5991-4977-a9e1-38235d18b7c5", QuizStudentId = "4e5dd609-2a63-4cc6-aee3-ec2ff9a2c04b", StudentId = "a4f15d45-0b76-4c1b-97b5-6a6b27229aae", Score = 87, SubmissionDate = DateTime.Now.AddDays(10) },
                new QuizStudent { Id = 23, QuizId = "88e6f2b6-3f82-45c7-a5b9-3768b2cc2d85", QuizStudentId = "e41dc6d5-8e94-4d27-8e32-409c9e09e623", StudentId = "573e5801-f47e-4a82-bcb5-90fc722e4d4f", Score = 100, SubmissionDate = DateTime.Now.AddDays(12) },

                new QuizStudent { Id = 24, QuizId = "a4b6eb12-22df-4eeb-bc98-7d3f2b27b67a", QuizStudentId = "1c8ff533-4b5a-4870-8941-70c2fc62b63c", StudentId = "cd9fe541-1c6f-4e9e-a94b-1ff748186975", Score = 23, SubmissionDate = DateTime.Now.AddDays(12) },
                new QuizStudent { Id = 25, QuizId = "e1af1567-8259-4b10-91bf-0f9ff57a2a42", QuizStudentId = "6942c349-e742-40d1-8409-8629a9272b89", StudentId = "cd9fe541-1c6f-4e9e-a94b-1ff748186975", Score = 53, SubmissionDate = DateTime.Now.AddDays(3) },
                new QuizStudent { Id = 26, QuizId = "3cfc58d0-eb6f-4725-aa63-44f065d1dbb8", QuizStudentId = "d28a8a30-6be0-4dd1-a699-49d8a88317d2", StudentId = "cd9fe541-1c6f-4e9e-a94b-1ff748186975", Score = 63, SubmissionDate = DateTime.Now.AddDays(7) },
                new QuizStudent { Id = 27, QuizId = "f5b34985-2322-4884-9d29-3d550b2cf0a4", QuizStudentId = "8b51cb96-9917-4453-a499-f87dd63f5c81", StudentId = "cd9fe541-1c6f-4e9e-a94b-1ff748186975", Score = 23, SubmissionDate = DateTime.Now.AddDays(5) },
                new QuizStudent { Id = 28, QuizId = "71ac61b5-5991-4977-a9e1-38235d18b7c5", QuizStudentId = "f9c2aa52-50b7-4e7e-a75c-2a9f7b7479f4", StudentId = "cd9fe541-1c6f-4e9e-a94b-1ff748186975", Score = 29, SubmissionDate = DateTime.Now.AddDays(10) },
                new QuizStudent { Id = 29, QuizId = "88e6f2b6-3f82-45c7-a5b9-3768b2cc2d85", QuizStudentId = "593d26cd-1e62-4f07-9c4d-f7b7c73b109d", StudentId = "cd9fe541-1c6f-4e9e-a94b-1ff748186975", Score = 25, SubmissionDate = DateTime.Now.AddDays(12) },
               new QuizStudent { Id =  42, QuizId = "8f3d5fe2-9c61-4fd5-92b2-e3c06b7c51e7", QuizStudentId = "00042f62-ee98-4e01-b99f-f5d89aadee8c", StudentId = "cd9fe541-1c6f-4e9e-a94b-1ff748186975", Score = 12, SubmissionDate = DateTime.Now.AddDays(12) },

                new QuizStudent { Id = 30, QuizId = "a4b6eb12-22df-4eeb-bc98-7d3f2b27b67a", QuizStudentId = "4a9e7e7f-7b94-4bbd-bd7d-f95b69e4f3c5", StudentId = "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", Score = 97, SubmissionDate = DateTime.Now.AddDays(21) },
                new QuizStudent { Id = 31, QuizId = "e1af1567-8259-4b10-91bf-0f9ff57a2a42", QuizStudentId = "b3b8d5a9-b303-4c88-8c04-77f3a03bb87b", StudentId = "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", Score = 75, SubmissionDate = DateTime.Now.AddDays(8) },
                new QuizStudent { Id = 32, QuizId = "3cfc58d0-eb6f-4725-aa63-44f065d1dbb8", QuizStudentId = "0f62e8fc-2956-4d16-a6e1-4b4d51d86780", StudentId = "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", Score = 54, SubmissionDate = DateTime.Now.AddDays(6) },
                new QuizStudent { Id = 33, QuizId = "f5b34985-2322-4884-9d29-3d550b2cf0a4", QuizStudentId = "e79f979c-8a08-478b-a05b-4418db6f5467", StudentId = "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", Score = 65, SubmissionDate = DateTime.Now.AddDays(9) },
                new QuizStudent { Id = 34, QuizId = "71ac61b5-5991-4977-a9e1-38235d18b7c5", QuizStudentId = "bcc05624-9145-4b4a-a29e-c51a02efc0f4", StudentId = "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", Score = 24, SubmissionDate = DateTime.Now.AddDays(6) },
                new QuizStudent { Id = 35, QuizId = "88e6f2b6-3f82-45c7-a5b9-3768b2cc2d85", QuizStudentId = "94a42f62-ee98-4e01-b99f-f5d89aadee8c", StudentId = "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", Score = 12, SubmissionDate = DateTime.Now.AddDays(12) },
                new QuizStudent { Id = 43, QuizId = "8f3d5fe2-9c61-4fd5-92b2-e3c06b7c51e7", QuizStudentId = "94a2af62-ee98-4e01-b99f-f5d89aadee8c", StudentId = "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", Score = 12, SubmissionDate = DateTime.Now.AddDays(12) },

                new QuizStudent { Id = 36, QuizId = "a4b6eb12-22df-4eeb-bc98-7d3f2b27b67a", QuizStudentId = "2ee2d5cb-bc4f-4f79-9a8d-d98df9bb98b4", StudentId = "ea21b42d-89a2-4e9e-a6b6-d6a9c7b52480", Score = 86, SubmissionDate = DateTime.Now.AddDays(20) },
                new QuizStudent { Id = 37, QuizId = "e1af1567-8259-4b10-91bf-0f9ff57a2a42", QuizStudentId = "3d53c66f-89b3-4b92-ba41-d84d91707349", StudentId = "ea21b42d-89a2-4e9e-a6b6-d6a9c7b52480", Score = 10, SubmissionDate = DateTime.Now.AddDays(30) },
                new QuizStudent { Id = 38, QuizId = "3cfc58d0-eb6f-4725-aa63-44f065d1dbb8", QuizStudentId = "19a7d14c-0ba0-4b85-835d-9c8a454c9c6c", StudentId = "ea21b42d-89a2-4e9e-a6b6-d6a9c7b52480", Score = 33, SubmissionDate = DateTime.Now.AddDays(3) },
                new QuizStudent { Id = 39, QuizId = "f5b34985-2322-4884-9d29-3d550b2cf0a4", QuizStudentId = "10cc3b78-0e50-4e63-8367-f3fd2c61d7d1", StudentId = "ea21b42d-89a2-4e9e-a6b6-d6a9c7b52480", Score = 23, SubmissionDate = DateTime.Now.AddDays(8) },
                new QuizStudent { Id = 40, QuizId = "71ac61b5-5991-4977-a9e1-38235d18b7c5", QuizStudentId = "b1421bc3-9d47-41c7-9f4b-785d65c5867a", StudentId = "ea21b42d-89a2-4e9e-a6b6-d6a9c7b52480", Score = 87, SubmissionDate = DateTime.Now.AddDays(5) },
                new QuizStudent { Id = 41, QuizId = "88e6f2b6-3f82-45c7-a5b9-3768b2cc2d85", QuizStudentId = "c894c187-8375-4578-bb11-4b4899ff52ab", StudentId = "ea21b42d-89a2-4e9e-a6b6-d6a9c7b52480", Score = 53, SubmissionDate = DateTime.Now.AddDays(16) },
                new QuizStudent { Id = 44, QuizId = "8f3d5fe2-9c61-4fd5-92b2-e3c06b7c51e7", QuizStudentId = "04a42f62-ee98-4e01-b99f-f5d89aadee8c", StudentId = "ea21b42d-89a2-4e9e-a6b6-d6a9c7b52480", Score = 12, SubmissionDate = DateTime.Now.AddDays(12) }


                );

            // Fake "StudentModule" data
            modelBuilder.Entity<StudentModule>().HasData(
                new StudentModule { Id = 1, StudentModuleId  = "977aef17-9c24-48e8-83a4-d55e5fc0e319", StudentId = "35f2f906-2a79-442e-b7d8-2f3f59c7c89c", ModuleId = "6e68f0c2-d9b2-4ab7-8b3b-8fbf81a8dd40", ModuleLevel = 1, ModuleScore = 22},
                new StudentModule { Id = 2, StudentModuleId  = "e426c7e7-05e0-4798-8cf9-8ec2bc18d2d4", StudentId = "d5d5c72c-6b4a-4299-bc57-29cb8120e118", ModuleId = "b684c89f-7b7e-4145-b345-9347a67673a3", ModuleLevel = 2, ModuleScore = 42},
                new StudentModule { Id = 3, StudentModuleId  = "8b2a69f2-625b-4749-afe5-5a2a60f3cb53", StudentId = "39ffcf76-0db7-4f95-b1ae-3f527c8fe5a7", ModuleId = "6e68f0c2-d9b2-4ab7-8b3b-8fbf81a8dd40", ModuleLevel = 1, ModuleScore = 52},
                new StudentModule { Id = 4, StudentModuleId  = "b1446b4c-3b57-4dbf-a94f-6f29e2d33a9e", StudentId = "d5d5c72c-6b4a-4299-bc57-29cb8120e118", ModuleId = "b684c89f-7b7e-4145-b345-9347a67673a3", ModuleLevel = 3, ModuleScore = 22},
                new StudentModule { Id = 5, StudentModuleId  = "9ae1e7c4-459b-4155-9a28-c5ad8fbdb1f6", StudentId = "39ffcf76-0db7-4f95-b1ae-3f527c8fe5a7", ModuleId = "f74e3a7a-312f-4f80-86cb-25f7d52735bc", ModuleLevel = 1, ModuleScore = 24},
                new StudentModule { Id = 6, StudentModuleId  = "70d26e2f-b330-4e97-b2d2-d3be9ec0ac0b", StudentId = "35f2f906-2a79-442e-b7d8-2f3f59c7c89c", ModuleId = "f74e3a7a-312f-4f80-86cb-25f7d52735bc", ModuleLevel = 3, ModuleScore = 72},
                new StudentModule { Id = 7, StudentModuleId  = "c20d89a0-8d3f-43c3-b9d1-8764ebd6f5a5", StudentId = "a4f15d45-0b76-4c1b-97b5-6a6b27229aae", ModuleId = "5488d35d-0a1e-4634-898e-92dbde019029", ModuleLevel = 2, ModuleScore = 22},
                new StudentModule { Id = 8, StudentModuleId  = "f145ac5c-49d2-44b2-a6c7-86a4bc855de5", StudentId = "e8011711-9367-404e-b0a1-3cfa0e54f015", ModuleId = "5488d35d-0a1e-4634-898e-92dbde019029", ModuleLevel = 3, ModuleScore = 92},
                new StudentModule { Id = 9, StudentModuleId  = "87225985-9f69-4fc7-9c54-8ec93b8e7021", StudentId = "a4f15d45-0b76-4c1b-97b5-6a6b27229aae", ModuleId = "9bb0a5cb-6bf6-418b-b549-4b3d8abbebb7", ModuleLevel = 1, ModuleScore = 65},
                new StudentModule { Id = 10, StudentModuleId = "c45f8b95-2c59-4e32-a2a2-29ecabe9e2cf", StudentId = "e8011711-9367-404e-b0a1-3cfa0e54f015", ModuleId = "9bb0a5cb-6bf6-418b-b549-4b3d8abbebb7", ModuleLevel = 2, ModuleScore = 22},
                new StudentModule { Id = 11, StudentModuleId = "2e5ef0f0-9a09-470b-87f5-c58f831bcf56", StudentId = "815dd6f4-2d41-4c6c-a032-5e78a1cf065b", ModuleId = "315fd315-6764-4514-a289-569c07b91894", ModuleLevel = 2, ModuleScore = 44},
                new StudentModule { Id = 12, StudentModuleId = "c9a1865c-bbf1-47d6-bd4e-4be5f1ee9e92", StudentId = "815dd6f4-2d41-4c6c-a032-5e78a1cf065b", ModuleId = "315fd315-6764-4514-a289-569c07b91894", ModuleLevel = 3, ModuleScore = 34},
                new StudentModule { Id = 13, StudentModuleId = "76b04144-1a4b-4c9e-af23-b6e494a621d2", StudentId = "573e5801-f47e-4a82-bcb5-90fc722e4d4f", ModuleId = "c485fd06-7b81-470d-a4d1-2e66e8fbd11d", ModuleLevel = 1, ModuleScore = 23},
                new StudentModule { Id = 14, StudentModuleId = "598a4f03-f9c8-4b4c-9fb7-2eb5b9941b3a", StudentId = "f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", ModuleId = "d067d9d4-2c4e-4dcf-85aa-30edc14263e2", ModuleLevel = 3, ModuleScore = 22},
                new StudentModule { Id = 15, StudentModuleId = "2c350499-b099-4d9a-a52b-4a77d8e31579", StudentId = "f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", ModuleId = "c8a4b3cc-78c5-4f20-804e-d617c35aa769", ModuleLevel = 1, ModuleScore = 74},
                new StudentModule { Id = 16, StudentModuleId = "3d97c81b-6e05-4d56-855b-8f570e1f88b4", StudentId = "573e5801-f47e-4a82-bcb5-90fc722e4d4f", ModuleId = "09cf6935-9c54-49c5-8a48-202268ad4f55", ModuleLevel = 2, ModuleScore = 93},
                new StudentModule { Id = 17, StudentModuleId = "b20933f4-f19c-4b65-bec7-48b9bcb105d8", StudentId = "d269b62c-c9dc-4d8f-8b3c-602d88e72438", ModuleId = "d067d9d4-2c4e-4dcf-85aa-30edc14263e2", ModuleLevel = 3, ModuleScore = 84},
                new StudentModule { Id = 18, StudentModuleId = "61be548f-67f2-4fd3-a717-7bc97d3b84dc", StudentId = "d269b62c-c9dc-4d8f-8b3c-602d88e72438", ModuleId = "c485fd06-7b81-470d-a4d1-2e66e8fbd11d", ModuleLevel = 2, ModuleScore = 77},
                new StudentModule { Id = 19, StudentModuleId = "7e904f7f-34a4-43e2-b5b1-36a3e50f15d8", StudentId = "97d39a95-2e4b-437f-a032-35e6357f06aa", ModuleId = "09cf6935-9c54-49c5-8a48-202268ad4f55", ModuleLevel = 2, ModuleScore = 65}
                );



            /*
            cd9fe541-1c6f-4e9e-a94b-1ff748186975
            7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50
            ea21b42d-89a2-4e9e-a6b6-d6a9c7b52480
*//*
            7e18a9ef-3723-472b-afd5-6786c4545d54
            a11e94d1-ebec-48d5-84e6-7c8589a1851f
            e921a7f7-78e8-4f13-82d5-2d2b2c35ab44
            e921a7f7-78e8-4f13-82d5-2d2b2c35ab44*/

            // Fake "Score" data
            modelBuilder.Entity<Score>().HasData(
                new Score { Id=1,    AssignmentId = "7e18a9ef-3723-472b-afd5-6786c4545d54", Score1 = 12, ScoreId = "a0b4ed42-0d4c-41cf-9b86-84b0f8e19db7", StudentId = "a4f15d45-0b76-4c1b-97b5-6a6b27229aae", SubmissionDate=DateTime.Now.AddDays(2)},
                new Score { Id = 2,  AssignmentId = "a11e94d1-ebec-48d5-84e6-7c8589a1851f", Score1 = 75, ScoreId = "1acbe9b9-6fc4-4e90-83cf-2143d10fbfd1", StudentId = "f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", SubmissionDate = DateTime.Now.AddDays(5) },
                new Score { Id = 3,  AssignmentId = "e921a7f7-78e8-4f13-82d5-2d2b2c35ab44", Score1 = 66, ScoreId = "3b3eb3b7-b755-4429-b92a-cb9c0f5198bb", StudentId = "d269b62c-c9dc-4d8f-8b3c-602d88e72438", SubmissionDate = DateTime.Now.AddDays(6) },
                new Score { Id = 4,  AssignmentId = "3f257a47-0c8b-4b06-aa0d-487d03e83db7", Score1 = 96, ScoreId = "c88edf8c-09b7-46cf-b2c1-d394d4f589c4", StudentId = "815dd6f4-2d41-4c6c-a032-5e78a1cf065b", SubmissionDate = DateTime.Now.AddDays(23) },
                new Score { Id = 5,  AssignmentId = "7e18a9ef-3723-472b-afd5-6786c4545d54", Score1 = 87, ScoreId = "2a09d83e-d639-4de8-81dd-30476b30084a", StudentId = "97d39a95-2e4b-437f-a032-35e6357f06aa", SubmissionDate = DateTime.Now.AddDays(12) },
                new Score { Id = 6,  AssignmentId = "a11e94d1-ebec-48d5-84e6-7c8589a1851f", Score1 = 64, ScoreId = "4d2db7d5-46f1-4697-a540-6b6c04384e5d", StudentId = "d5d5c72c-6b4a-4299-bc57-29cb8120e118", SubmissionDate = DateTime.Now.AddDays(23) },
                new Score { Id = 7,  AssignmentId = "e921a7f7-78e8-4f13-82d5-2d2b2c35ab44", Score1 = 60, ScoreId = "a8d318c7-1767-4345-b4cb-7e7731f19fe4", StudentId = "e8011711-9367-404e-b0a1-3cfa0e54f015", SubmissionDate = DateTime.Now.AddDays(9) },
                new Score { Id = 8,  AssignmentId = "3f257a47-0c8b-4b06-aa0d-487d03e83db7", Score1 = 90, ScoreId = "c033f1a2-67c5-4f9e-b3f2-99cbf8034db1", StudentId = "39ffcf76-0db7-4f95-b1ae-3f527c8fe5a7", SubmissionDate = DateTime.Now.AddDays(9) },
                new Score { Id = 9,  AssignmentId = "7e18a9ef-3723-472b-afd5-6786c4545d54", Score1 = 100,ScoreId = "2ebabf2b-5e2d-455f-b4e4-8f40cc32ad9c", StudentId = "573e5801-f47e-4a82-bcb5-90fc722e4d4f", SubmissionDate = DateTime.Now.AddDays(8) },
                new Score { Id = 10, AssignmentId = "a11e94d1-ebec-48d5-84e6-7c8589a1851f", Score1 = 87, ScoreId = "0dc10aaf-8887-420f-b3c1-70f4987b31e2", StudentId = "35f2f906-2a79-442e-b7d8-2f3f59c7c89c", SubmissionDate = DateTime.Now.AddDays(30) },
                new Score { Id = 11, AssignmentId = "e921a7f7-78e8-4f13-82d5-2d2b2c35ab44", Score1 = 78, ScoreId = "c4544428-1229-49fb-bbe5-c3f607f2e0b6", StudentId = "e8011711-9367-404e-b0a1-3cfa0e54f015", SubmissionDate = DateTime.Now.AddDays(19) },
                new Score { Id = 12, AssignmentId = "3f257a47-0c8b-4b06-aa0d-487d03e83db7", Score1 = 69, ScoreId = "e2f722f0-f570-49de-a2cc-dfb6483b7276", StudentId = "35f2f906-2a79-442e-b7d8-2f3f59c7c89c", SubmissionDate = DateTime.Now.AddDays(16) },

                new Score { Id = 13, AssignmentId = "7e18a9ef-3723-472b-afd5-6786c4545d54", Score1 = 28, ScoreId = "d36b67c8-0e0e-4873-80ec-b3fa0bfbb92c", StudentId = "cd9fe541-1c6f-4e9e-a94b-1ff748186975", SubmissionDate = DateTime.Now.AddDays(32) },
                new Score { Id = 14, AssignmentId = "a11e94d1-ebec-48d5-84e6-7c8589a1851f", Score1 = 82, ScoreId = "4a8f7f7e-ec5c-4b5b-8e52-8eae6c286e63", StudentId = "cd9fe541-1c6f-4e9e-a94b-1ff748186975", SubmissionDate = DateTime.Now.AddDays(11) },
                new Score { Id = 15, AssignmentId = "e921a7f7-78e8-4f13-82d5-2d2b2c35ab44", Score1 = 92, ScoreId = "6a5a89b8-2457-4d89-b7cb-1d255f286f47", StudentId = "cd9fe541-1c6f-4e9e-a94b-1ff748186975", SubmissionDate = DateTime.Now.AddDays(9) },
                new Score { Id = 16, AssignmentId = "3f257a47-0c8b-4b06-aa0d-487d03e83db7", Score1 = 72, ScoreId = "7e4884a4-6b2b-47e5-a982-d5c2a697309b", StudentId = "cd9fe541-1c6f-4e9e-a94b-1ff748186975", SubmissionDate = DateTime.Now.AddDays(7) },
                new Score { Id = 17, AssignmentId = "7e18a9ef-3723-472b-afd5-6786c4545d54", Score1 = 77, ScoreId = "e491e4a2-7cb5-4fe8-a6e0-5f2059eebf0f", StudentId = "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", SubmissionDate = DateTime.Now.AddDays(3) },
                new Score { Id = 18, AssignmentId = "a11e94d1-ebec-48d5-84e6-7c8589a1851f", Score1 = 65, ScoreId = "7ef2b3c8-1e0d-434e-b1ad-6a0b3782b8cb", StudentId = "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", SubmissionDate = DateTime.Now.AddDays(1) },
                new Score { Id = 19, AssignmentId = "e921a7f7-78e8-4f13-82d5-2d2b2c35ab44", Score1 = 87, ScoreId = "99e51e1c-246d-4e5d-af46-1fc605f28d8f", StudentId = "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", SubmissionDate = DateTime.Now.AddDays(10) },
                new Score { Id = 20, AssignmentId = "3f257a47-0c8b-4b06-aa0d-487d03e83db7", Score1 = 59, ScoreId = "e6a8b0e7-c19a-4b56-b4b7-2ec8fc6d26f5", StudentId = "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", SubmissionDate = DateTime.Now.AddDays(18) },
                new Score { Id = 21, AssignmentId = "7e18a9ef-3723-472b-afd5-6786c4545d54", Score1 = 50, ScoreId = "1d08c07e-8c67-4c8e-9d32-89d981f31f4b", StudentId = "ea21b42d-89a2-4e9e-a6b6-d6a9c7b52480", SubmissionDate = DateTime.Now.AddDays(23) },
                new Score { Id = 22, AssignmentId = "a11e94d1-ebec-48d5-84e6-7c8589a1851f", Score1 = 23, ScoreId = "60be68a2-b3fd-44b0-a187-d2b11b1d23d7", StudentId = "ea21b42d-89a2-4e9e-a6b6-d6a9c7b52480", SubmissionDate = DateTime.Now.AddDays(1) },
                new Score { Id = 23, AssignmentId = "e921a7f7-78e8-4f13-82d5-2d2b2c35ab44", Score1 = 34, ScoreId = "1b33f1d4-1ac4-492d-aa3e-b086de84dab1", StudentId = "ea21b42d-89a2-4e9e-a6b6-d6a9c7b52480", SubmissionDate = DateTime.Now.AddDays(7) },
                new Score { Id = 24, AssignmentId = "3f257a47-0c8b-4b06-aa0d-487d03e83db7", Score1 = 45, ScoreId = "f0f23e62-9a67-4a85-83ff-d8a5b2f83dd4", StudentId = "ea21b42d-89a2-4e9e-a6b6-d6a9c7b52480", SubmissionDate = DateTime.Now.AddDays(6) }
                );
        }

    }
}
