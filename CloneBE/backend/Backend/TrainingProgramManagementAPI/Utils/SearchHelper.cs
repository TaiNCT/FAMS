using Entities.Context;
using Entities.Models;

namespace TrainingProgramManagementAPI.Utils
{
    public class SearchHelper
    {
        public static async Task<List<TrainingProgram>> SearchByPatternAsync(
            IQueryable<TrainingProgram> trainingPrograms,
            string? searchValue,
            AppSettings _appSettings)
        {
            // Building search query
            if (!string.IsNullOrEmpty(searchValue)) // Is not null or empty string 
            {
                // Is a number
                if (int.TryParse(searchValue, out var numSearch))
                {
                    // Building query
                    trainingPrograms = trainingPrograms.Where(x =>
                       // From Days
                       x.Days.HasValue && (x.Days.Value == numSearch)
                    // From Hours
                    || x.Hours.HasValue && (x.Hours.Value == numSearch)
                    // From Start Time
                    || x.StartTime.Hour == numSearch
                    || x.StartTime.Minute == numSearch
                    // From day 
                    || x.CreatedDate.HasValue && x.CreatedDate.Value.Day == numSearch
                    || x.UpdatedDate.HasValue && x.UpdatedDate.Value.Day == numSearch
                    // From Month
                    || x.CreatedDate.HasValue && x.CreatedDate.Value.Month == numSearch
                    || x.UpdatedDate.HasValue && x.UpdatedDate.Value.Month == numSearch
                    // From Year
                    || x.CreatedDate.HasValue && x.CreatedDate.Value.Year == numSearch
                    || x.UpdatedDate.HasValue && x.UpdatedDate.Value.Year == numSearch
                    );
                }
                // Is Datetime: Only use from html date selector
                else if (DateTime.TryParse(searchValue, out var dateSearch))
                {
                    // Format date
                    var dateFormat = DateTime.ParseExact(dateSearch.ToString(_appSettings.DateTimeFormat),
                    _appSettings.DateTimeFormat, CultureInfo.InvariantCulture);

                    // Building query
                    trainingPrograms = trainingPrograms.Where(x =>
                        x.CreatedDate.HasValue // Mark that createDate is not null
                     && x.CreatedDate.Value.Date.Equals(dateFormat)
                     || x.UpdatedDate.HasValue // Mark that updatedDate is not null
                     && x.UpdatedDate.Value.Date.Equals(dateFormat));
                }
                // Is pure string
                else
                {
                    // Building query
                    trainingPrograms = trainingPrograms.Where(
                        x => x.CreatedBy != null && x.CreatedBy.Contains(searchValue)
                        || x.UpdatedBy != null && x.UpdatedBy.Contains(searchValue)
                        || x.Name.Contains(searchValue)
                        || x.Status.Contains(searchValue));
                }
            }

            return await trainingPrograms.ToListAsync();
        }
    }
}