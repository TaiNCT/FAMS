using System.Globalization;
using AutoMapper;
using ClassManagementAPI.Dto;
using ClassManagementAPI.Dto.ClassDTO;
using ClassManagementAPI.Dto.FilterDTO;
using ClassManagementAPI.Interface;
using Entities.Models;
using Entities.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ClassManagementAPI.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly FamsContext _context;
        private readonly IMapper _mapper;

        public ClassRepository(FamsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> CreateClass(Class model)
        {
            if (model != null)
            {
                try
                {
                    var result = await _context.AddAsync(model);
                    return await _context.SaveChangesAsync() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Something went wrong: " + ex);
                }
            }
            return false;
        }

        public Task<bool> DeleteClass(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Class>> GetClasses()
        {
            return await _context.Classes.ToListAsync();
        }

        public Task<bool> UpdateClass(Class model)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetTotalCount()
        {
            return await _context.Classes.CountAsync();
        }

        public async Task<PagedResult<Class>> GetClassListByFilter(FilterFormatDto? filterData)
        {
            IQueryable<Class> query = _context.Classes;

            if (filterData.FromDate.HasValue)
            {
                DateOnly fromDate = filterData.FromDate.Value;
                query = query.Where(pro => pro.StartDate >= fromDate);
            }

            if (filterData.ToDate.HasValue)
            {
                DateOnly toDate = filterData.ToDate.Value;
                query = query.Where(pro => pro.EndDate <= toDate);
            }

            if (filterData.Locations_id != null && filterData.Locations_id.Count > 0)
            {
                query = query.Where(pro =>
                    filterData.Locations_id.Contains(pro.Location.LocationId)
                );
            }

            if (filterData.SlotTimes != null && filterData.SlotTimes.Count > 0)
            {
                query = query.Where(pro => filterData.SlotTimes.Contains(pro.SlotTime));
            }

            if (filterData.Class_status != null && filterData.Class_status.Count > 0)
            {
                query = query.Where(pro => filterData.Class_status.Contains(pro.ClassStatus));
            }

            if (filterData.Fsu_ID.HasValue)
            {
                query = query.Where(pro =>
                    pro.Fsu != null && pro.Fsu.Id == filterData.Fsu_ID.Value
                );
            }

            if (!string.IsNullOrEmpty(filterData.TrainingProgramCode))
            {
                query = query.Where(pro =>
                    pro.TrainingProgramCodeNavigation != null
                    && pro.TrainingProgramCodeNavigation.TrainingProgramCode
                        == filterData.TrainingProgramCode
                );
            }

            if (filterData.AttendeeLevelID != null && filterData.AttendeeLevelID.Count > 0)
            {
                query = query.Where(pro =>
                    pro.AttendeeLevel != null
                    && filterData.AttendeeLevelID.Contains(pro.AttendeeLevel.AttendeeTypeId)
                );
            }
            var items = await query.ToListAsync();
            var totalCount = await query.CountAsync();

            return new PagedResult<Class> { Items = items, TotalCount = totalCount };
        }

        public async Task<Class> GetClassById(string id)
        {

            return await _context.Classes
                 .Include(c => c.AttendeeLevel)
                 .Include(c => c.Location)
                 .Include(c => c.ClassUsers).ThenInclude(cu => cu.User).ThenInclude(cu => cu.Role)
                 .Include(c => c.Fsu)
                 .Include(c => c.StudentClasses)
                 .Include(c => c.TrainingProgramCodeNavigation).ThenInclude(c => c.TrainingProgramSyllabi).ThenInclude(c => c.Syllabus)
                 .FirstOrDefaultAsync(c => c.ClassId == id);
        }

        public async Task<List<Class>> GetClassByAttendeeId(string id)
        {
            IQueryable<Class> query = _context
                .Classes.Include(c => c.AttendeeLevel)
                .Include(c => c.Location)
                .Include(c => c.ClassUsers)
                .Include(c => c.Fsu)
                .Include(c => c.ReservedClasses)
                .Include(c => c.StudentClasses)
                .Include(c => c.TrainingProgramCodeNavigation);
            query.Where(c => c.AttendeeLevelId == id);
            return await query.ToListAsync();
        }

        public async Task<List<Class>> GetClassByTime(FilterTime? filterTime)
        {
            IQueryable<Class> query = _context
                .Classes.Include(c => c.AttendeeLevel)
                .Include(c => c.Location)
                .Include(c => c.ClassUsers)
                .Include(c => c.Fsu)
                .Include(c => c.ReservedClasses)
                .Include(c => c.StudentClasses)
                .Include(c => c.TrainingProgramCodeNavigation);
            return await query.ToListAsync();
        }

        private List<WeekResultDto> GetClassesByDay(List<Class> classes, DateOnly startDate, DateOnly endDate)
        {
            var classesByDay = new List<WeekResultDto>();

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var daySchedule = new WeekResultDto
                {
                    DayOfWeek = date.DayOfWeek.ToString(),
                    ClassOfWeek = classes
                        .Where(c => c.StartDate <= date && c.EndDate >= date)
                        .ToList()
                };
                classesByDay.Add(daySchedule);
            }

            return classesByDay;
        }

        public async Task<PagedResult<WeekResultDto>> GetListOfClassesByDateRange(DateOnly startDate, DateOnly endDate)
        {
            var classes = await _context.Classes
                .Where(c => !(c.EndDate < startDate) && !(c.StartDate > endDate))
                .ToListAsync();

            var classesByDay = GetClassesByDay(classes, startDate, endDate);
            var daysOrder = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            var sortedClassesByDay = classesByDay
                .OrderBy(d => daysOrder.IndexOf(d.DayOfWeek))
                .ToList();

            return new PagedResult<WeekResultDto>
            {
                TotalCount = sortedClassesByDay.Count,
                Items = sortedClassesByDay
            };
        }

        public async Task<PagedResult<WeekResultDto>> GetClassListInWeekByFilter(FilterFormatDto? filterData)
        {
            IQueryable<Class> query = _context.Classes;

            if (filterData.FromDate.HasValue)
            {
                DateOnly fromDate = filterData.FromDate.Value;
                query = query.Where(pro => pro.StartDate >= fromDate);
            }

            if (filterData.ToDate.HasValue)
            {
                DateOnly toDate = filterData.ToDate.Value;
                query = query.Where(pro => pro.EndDate <= toDate);
            }

            if (filterData.Locations_id != null && filterData.Locations_id.Count > 0)
            {
                query = query.Where(pro =>
                    filterData.Locations_id.Contains(pro.Location.LocationId)
                );
            }

            if (filterData.SlotTimes != null && filterData.SlotTimes.Count > 0)
            {
                query = query.Where(pro => filterData.SlotTimes.Contains(pro.SlotTime));
            }

            if (filterData.Class_status != null && filterData.Class_status.Count > 0)
            {
                query = query.Where(pro => filterData.Class_status.Contains(pro.ClassStatus));
            }

            if (filterData.Fsu_ID.HasValue)
            {
                query = query.Where(pro =>
                    pro.Fsu != null && pro.Fsu.Id == filterData.Fsu_ID.Value
                );
            }

            if (!string.IsNullOrEmpty(filterData.TrainingProgramCode))
            {
                query = query.Where(pro =>
                    pro.TrainingProgramCodeNavigation != null
                    && pro.TrainingProgramCodeNavigation.TrainingProgramCode
                        == filterData.TrainingProgramCode
                );
            }

            if (filterData.AttendeeLevelID != null && filterData.AttendeeLevelID.Count > 0)
            {
                query = query.Where(pro =>
                    pro.AttendeeLevel != null
                    && filterData.AttendeeLevelID.Contains(pro.AttendeeLevel.AttendeeTypeId)
                );
            }

            DateOnly startDate = query.Min(c => c.StartDate);
            DateOnly endDate = query.Max(c => c.EndDate);

            if (filterData.StartOfWeek.HasValue == true && filterData.EndOfWeek.HasValue == true)
            {
                startDate = filterData.StartOfWeek.Value;
                endDate = filterData.EndOfWeek.Value;
            }

            var classes = await query
                 .Where(c => !(c.EndDate < startDate) && !(c.StartDate > endDate))
                 .ToListAsync();

            var classesByDay = GetClassesByDay(classes, startDate, endDate);

            var daysOrder = new List<string>
            {
                "Monday",
                "Tuesday",
                "Wednesday",
                "Thursday",
                "Friday",
                "Saturday",
                "Sunday"
            };

            var sortedClassesByDay = classesByDay
                .OrderBy(d => daysOrder.IndexOf(d.DayOfWeek))
                 .ToList();

            return new PagedResult<WeekResultDto>
            {
                TotalCount = sortedClassesByDay.Count,
                Items = sortedClassesByDay
            };
        }

        public async Task<bool> DuplicatedClass(string Id)
        {
            if (Id != null)
            {
                try
                {
                    var classNow = await _context.Classes.Where(c => c.ClassId == Id).FirstOrDefaultAsync();
                    
                    if (classNow != null)
                    {
                        var duplicatedClass = _mapper.Map<DuplicateClassRequest>(classNow);
                        var classMap = _mapper.Map<Class>(duplicatedClass);
                        classMap.CreatedDate = DateTime.UtcNow;
                        classMap.UpdatedBy = null;
                        classMap.UpdatedDate = null;
                        await _context.AddAsync(classMap);
                        return await _context.SaveChangesAsync() > 0;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception e)
                {
                    throw new Exception("Error" + e.Message);
                }
            }
            return false;
        }
        public async Task UpdateClassStatus(string classId, string newStatus)
        {
            var classEntity = await _context.Classes.FirstOrDefaultAsync(c => c.ClassId == classId);

            if (classEntity == null)
            {
                throw new ArgumentException($"Class with ID {classId} not found");
            }

            classEntity.ClassStatus = newStatus;
            await _context.SaveChangesAsync();
        }
        public async Task<Class> GetClassById1(string classId)
        {
            return await _context.Classes
                .Include(c => c.ClassUsers).ThenInclude(cu => cu.User).ThenInclude(cu => cu.Role)
                .Include(c => c.AttendeeLevel)
                /*.Include(c => c.Fsu)
                .Include(c => c.TrainingProgramCodeNavigation)*/
                .FirstOrDefaultAsync(c => c.ClassId == classId);
        }

        public async Task UpdateClass1(Class updatedClass)
        {
            _context.Classes.Update(updatedClass);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task AddUserToClassAsync(string userId, string classId)
        {
            var classObj = await _context.Classes.Include(c => c.ClassUsers).FirstOrDefaultAsync(c => c.ClassId == classId);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            if (classObj == null)
            {
                throw new Exception("Class not found.");
            }

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            classObj.ClassUsers.Add(new ClassUser { User = user });
            await _context.SaveChangesAsync();
        }

        public async Task<List<AttendeeType>> GetALlAttendeeTypeAsync()
        {
            return await _context.AttendeeTypes.ToListAsync();
        }
    }
}
