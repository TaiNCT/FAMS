using Entities.Context;
using Entities.Models;
using SyllabusManagementAPI.Entities.Helpers;
using SyllabusManagementAPI.Entities.Parameters;

namespace SyllabusManagementAPI.Contracts
{
    public interface ISyllabusRepository : IRepositoryBase<Syllabus>
    {
        Task<PagedList<Syllabus>> GetAllSyllabusAsync(SyllabusParameters syllabusParameters);

        Task<Syllabus?> GetSyllabusByIdAsync(string syllabusId);
		Task<Syllabus?> GetSyllabusByIdAsyncV2(string syllabusId);

		Task<PagedList<Syllabus>> SortSyllabusAsync(SyllabusParameters syllabusParameters, string Sortby);

        Task<PagedList<Syllabus>> FilterByDateSyllabusAsync(SyllabusParameters syllabusParameters, DateTime from, DateTime to);

        void CreateSyllabus(Syllabus syllabus);

        void DeleteSyllabus(Syllabus syllabus);

        void UpdateSyllabus(Syllabus syllabus);

        Task<IDictionary<string, double>> GetDeliveryTypePercentages(string syllabusId);

        Task<IDictionary<string, int>> GetDeliveryTypeCounts(string syllabusId);

        Task<Syllabus?> GetOutputStandardBySyllabusIdAsync(string syllabusId);

		Task<Syllabus?> GetSyllabusName(string syllabusName);

        Task DeleteDuplicateSyllabuses(string syllabusName);

		Task<PagedList<Syllabus>> Search(string keywords, SyllabusParameters syllabusParameters);
	}
}
