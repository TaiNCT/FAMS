using ClassManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassManagementAPI.Tests.MockData
{
    public static class ClassMockData
    {
        public static List<Class> GetClasses()
        {
            return new List<Class>
            {
                new Class
                {
                ClassId = "0",
                CreatedBy = "Hung",
                CreatedDate = DateTime.Parse("2024-01-29 09:50:57.687"),
                ClassStatus = "Plaining",
                ClassCode = "NET_02",
                Duration = 4,
                StartDate = DateOnly.Parse("2021-12-08"),
                EndDate = DateOnly.Parse("2021-12-28"),
                StartTime = TimeOnly.MinValue,
                EndTime = TimeOnly.MaxValue,
                AcceptedAttendee = 0,
                ActualAttendee = 0,
                ClassName = "string",
                FsuId = "1",
                LocationId = "3",
                AttendeeLevelId = "1",
                TrainingProgramCode = "3",
                PlannedAttendee = 0,
                SlotTime = "Morning",
                ClassUsers = new List<ClassUser>(),
                ReservedClasses = new List<ReservedClass>(),
                StudentClasses = new List<StudentClass>()
                }
            };
        }
    }
}
