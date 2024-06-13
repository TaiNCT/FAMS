using Entities.Models;

namespace SyllabusManagementAPI.Contracts
{
	public interface ISearchRepository
	{
		Task<bool> CreateSyllabus(Syllabus model);
		Task<List<Syllabus>> GetSyllabus();
	}
}