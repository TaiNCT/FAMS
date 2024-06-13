using Entities.Context;
using Entities.Models;

namespace TrainingProgramManagementAPI.Utils;

public class FilterHelper
{
    public static async Task<IEnumerable<TrainingProgram>> FilterByPattern(
        IQueryable<TrainingProgram> trainingPrograms,
        string[]? status = null, // Training program status 
        string createdBy = "", // Create by
        DateTime? programTimeFrameFrom = null, // From Datetime  
        DateTime? programTimeFrameTo = null, // To Datetime
        int _page = 1, // Current page, 
        string _perPage = "" // Page size
    )
    {
        // Check each selected option is empty or null
        if (!status.IsNullOrEmpty() && status != null)
        {
            // Convert all elements of status array to lowercase
            var lowerStatus = status.Select(s => s.ToLower()).ToArray();

            // Filter training programs based on lowercase status
            trainingPrograms = trainingPrograms.Where(x => lowerStatus.Contains(x.Status.ToLower()));
        }
        // Check each selected option is empty or null
        if (!string.IsNullOrEmpty(createdBy))
        {
            var lowerCreatedBy = createdBy.ToLower();
            trainingPrograms = trainingPrograms.Where(x => x.CreatedBy != null && x.CreatedBy.ToLower() == lowerCreatedBy);
        }
        // Check each selected option is empty or null
        if (programTimeFrameFrom.HasValue)
        {
            trainingPrograms = trainingPrograms.Where(x => x.CreatedDate >= programTimeFrameFrom);
        }
        // Check each selected option is empty or null

        if (programTimeFrameTo.HasValue)
        {
            trainingPrograms = trainingPrograms.Where(x => x.CreatedDate <= programTimeFrameTo);
        }
        // get the list match with the filter selected
        return await trainingPrograms.ToListAsync();
    }
}