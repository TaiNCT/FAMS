using Entities.Context;
using Entities.Models;

namespace TrainingProgramManagementAPI.Utils
{
	public class SortingHelper
	{
		public static IEnumerable<TrainingProgram> SortByColumn(IEnumerable<TrainingProgram> paginatedTrainingPrograms, string sort)
		{
			string sortOrder = sort.TrimStart('+', '-');
			bool descendingOrder = sort.StartsWith("-");

			var sortMappings = new Dictionary<string, Func<TrainingProgram, object>>
				{
					{ "id", p => p.Id },
					{ "programname", p => p.Name },
					{ "createdon", p =>
						{
							return p.CreatedDate.HasValue ? p.CreatedDate.Value : null!;
						}
					},
					{ "createdby", p =>
						{
							return p.CreatedBy != null ? p.CreatedBy : null!;
						}
					},
					{ "duration", p =>
						{
							return p.Days.HasValue ? p.Days.Value : 0;
						}
					},
					{ "status", p => p.Status }
				};

			if (sortMappings.TryGetValue(sortOrder.ToLower(), out var sortExpression))
			{
				return descendingOrder
					? paginatedTrainingPrograms.OrderByDescending(sortExpression)
					: paginatedTrainingPrograms.OrderBy(sortExpression);
			}
			else
			{
				return paginatedTrainingPrograms;
			}
		}
	}
}

