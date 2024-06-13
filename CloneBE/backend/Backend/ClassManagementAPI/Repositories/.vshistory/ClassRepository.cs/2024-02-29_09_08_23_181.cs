using ClassManagementAPI.Dto;
using ClassManagementAPI.Dto.ClassDTO;
using ClassManagementAPI.Interface;
using ClassManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using Nest;
using System.Globalization;

namespace ClassManagementAPI.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly FamsContext _context;

        public ClassRepository(FamsContext context)
        {
            _context = context;
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
                query = query.Where(pro => filterData.Locations_id.Contains(pro.Location.LocationId));
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
                query = query.Where(pro => pro.TrainingProgramCodeNavigation != null
                                    && pro.TrainingProgramCodeNavigation.TrainingProgramCode == filterData.TrainingProgramCode);
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
                 .Include(c => c.ClassUsers)
                 .Include(c => c.Fsu)
                 .Include(c => c.ReservedClasses)
                 .Include(c => c.StudentClasses)
                 .Include(c => c.TrainingProgramCodeNavigation)
                 .FirstOrDefaultAsync(c => c.ClassId == id);
        }

        public async Task<List<Class>> GetClassByAttendeeId(string id)
        {
            IQueryable<Class> query = _context.Classes
                 .Include(c => c.AttendeeLevel)
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
            IQueryable<Class> query = _context.Classes
                 .Include(c => c.AttendeeLevel)
                 .Include(c => c.Location)
                 .Include(c => c.ClassUsers)
                 .Include(c => c.Fsu)
                 .Include(c => c.ReservedClasses)
                 .Include(c => c.StudentClasses)
                 .Include(c => c.TrainingProgramCodeNavigation);
            return await query.ToListAsync();
        }

        public async Task<PagedResult<WeekResultDto>> GetListOfClassesByDateRange(DateOnly startDate, DateOnly endDate)
        {
            // Get the classes that occur during the date range
            var classes = await _context.Classes
                .Where(c => !(c.EndDate < startDate) && !(c.StartDate > endDate))
                .ToListAsync();

            // Group the classes by day
            var classesByDay = new List<WeekResultDto>();
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var daySchedule = new WeekResultDto
                {
                    DayOfWeek = date.DayOfWeek.ToString(),
                    ClassOfWeek = classes.Where(c => c.StartDate <= date && c.EndDate >= date).ToList()
                };
                classesByDay.Add(daySchedule);
            }

            // Define the order of days
            var daysOrder = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

            // Sort the list by day of the week from Monday to Sunday
            var sortedClassesByDay = classesByDay
                .OrderBy(d => daysOrder.IndexOf(d.DayOfWeek))
                .ToList();

            // Return the result
            return new PagedResult<WeekResultDto>
            {
                TotalCount = sortedClassesByDay.Count,
                Items = sortedClassesByDay
            };
        }

    }
}

