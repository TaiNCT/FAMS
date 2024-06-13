using Microsoft.EntityFrameworkCore;
using Nest;
using SyllabusManagementAPI.Contracts;
using Entities.Models;
using SyllabusManagementAPI.Entities.Helpers;
using Entities.Context;
using SyllabusManagementAPI.Entities.Parameters;

namespace SyllabusManagementAPI.Repository
{
    public class SyllabusRepository : RepositoryBase<Syllabus>, ISyllabusRepository
    {
        private readonly FamsContext _context;

        public SyllabusRepository(FamsContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all syllabus records based on the specified parameters.
        /// </summary>
        /// <param name="syllabusParameters">The parameters for pagination.</param>
        /// <returns>A paged list of syllabus records.</returns>
        public async Task<PagedList<Syllabus>> GetAllSyllabusAsync(SyllabusParameters syllabusParameters)
        {
            var syllabus = await FindAll()
                .OrderByDescending(sy => sy.CreatedDate)
                .ToListAsync();

            return PagedList<Syllabus>.ToPagedList(
                syllabus,
                syllabusParameters.PageNumber,
                syllabusParameters.PageSize);
        }

        public async Task<Syllabus?> GetSyllabusByIdAsync(string Id)
        {
            var syllabus = await FindByCondition(s => s.SyllabusId.Equals(Id)).FirstOrDefaultAsync();
            return syllabus;
        }
        public async Task<Syllabus?> GetSyllabusByIdAsyncV2(string syllabusId)
        {
            var syllabus = await FindByCondition(s => s.SyllabusId.Equals(syllabusId))
                    .Include(a => a.AssessmentSchemes)
                    .Include(t => t.TrainingProgramSyllabi)
                    .Include(sd => sd.SyllabusDays)
                    .FirstOrDefaultAsync();
            if (syllabus!.SyllabusDays.Any())
            {
                foreach (var syllabusDay in syllabus.SyllabusDays)
                {
                    var syllabusUnits = await _context.SyllabusUnits
                                              .Where(u => u.SyllabusDayId == syllabusDay.SyllabusDayId)
                                              .ToListAsync();
                    syllabusDay.SyllabusUnits = syllabusUnits;
                    foreach (var unit in syllabusUnits)
                    {
                        unit.UnitChapters = await _context.UnitChapters
                                            .Include(x => x.DeliveryType)
                                            .Include(x => x.OutputStandard)
                                            .Include(x => x.TrainingMaterials)
                                            .Where(uc => uc.SyllabusUnitId == unit.SyllabusUnitId)
                                            .ToListAsync();
                    }

                }
            }
            return syllabus;
        }

        public async Task<PagedList<Syllabus>> FilterByDateSyllabusAsync(SyllabusParameters syllabusParameters, DateTime from, DateTime to)
        {
            var syllabus = await FindAll()
                .Where(sy => sy.CreatedDate >= from && sy.CreatedDate <= to)
                .ToListAsync();

            return PagedList<Syllabus>.ToPagedList(
                syllabus,
                syllabusParameters.PageNumber,
                syllabusParameters.PageSize);
        }

        public async Task<PagedList<Syllabus>> SortSyllabusAsync(SyllabusParameters syllabusParameters, string SortBy)
        {
            try
            {
                if (!string.IsNullOrEmpty(SortBy))
                    switch (SortBy)
                    {
                        case "code":
                            return PagedList<Syllabus>.ToPagedList(
                            FindAll().OrderBy(sy => sy.TopicCode),
                            syllabusParameters.PageNumber,
                            syllabusParameters.PageSize);
                        case "code-desc":
                            return PagedList<Syllabus>.ToPagedList(
                            FindAll().OrderByDescending(sy => sy.TopicCode),
                            syllabusParameters.PageNumber,
                            syllabusParameters.PageSize);
                        case "created-on":
                            return PagedList<Syllabus>.ToPagedList(
                            FindAll().OrderBy(sy => sy.CreatedDate),
                            syllabusParameters.PageNumber,
                            syllabusParameters.PageSize);
                        case "created-on-desc":
                            return PagedList<Syllabus>.ToPagedList(
                            FindAll().OrderByDescending(sy => sy.CreatedDate),
                            syllabusParameters.PageNumber,
                            syllabusParameters.PageSize);
                        case "created-by":
                            return PagedList<Syllabus>.ToPagedList(
                            FindAll().OrderBy(sy => sy.CreatedBy),
                            syllabusParameters.PageNumber,
                            syllabusParameters.PageSize);
                        case "created-by-desc":
                            return PagedList<Syllabus>.ToPagedList(
                            FindAll().OrderByDescending(sy => sy.CreatedBy),
                            syllabusParameters.PageNumber,
                            syllabusParameters.PageSize);
                        case "duration":
                            return PagedList<Syllabus>.ToPagedList(
                            FindAll().OrderBy(sy => sy.Days),
                            syllabusParameters.PageNumber,
                            syllabusParameters.PageSize);
                        case "duration-desc":
                            return PagedList<Syllabus>.ToPagedList(
                            FindAll().OrderByDescending(sy => sy.Days),
                            syllabusParameters.PageNumber,
                            syllabusParameters.PageSize);
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            var syllabus = await FindAll().ToListAsync();

            return PagedList<Syllabus>.ToPagedList(
                syllabus,
                syllabusParameters.PageNumber,
                syllabusParameters.PageSize);
        }

        public void CreateSyllabus(Syllabus syllabus) => Create(syllabus);

        public void DeleteSyllabus(Syllabus syllabus) => Delete(syllabus);

        public void UpdateSyllabus(Syllabus syllabus)
        {
            syllabus.AssessmentSchemes.Clear();
            syllabus.SyllabusDays.Clear();
            Update(syllabus);
        }

        public async Task<IDictionary<string, int>> GetDeliveryTypeCounts(string syllabusId)
        {
            var syllabus = await FindByCondition(s => s.SyllabusId == syllabusId)
                .Include(s => s.SyllabusDays)
                .ThenInclude(sd => sd.SyllabusUnits)
                .ThenInclude(su => su.UnitChapters)
                .ThenInclude(uc => uc.DeliveryType)
                .FirstOrDefaultAsync();

            //_context.Syllabi
            //.Include(s => s.SyllabusDays)
            //.ThenInclude(sd => sd.SyllabusUnits)
            //.ThenInclude(su => su.UnitChapters)
            //.ThenInclude(uc => uc.DeliveryType)
            //.FirstOrDefault(s => s.SyllabusId == syllabusId);

            if (syllabus == null)
            {
                throw new ArgumentException("Syllabus not found.");
            }

            var query = syllabus.SyllabusDays
                .SelectMany(sd => sd.SyllabusUnits)
                .SelectMany(su => su.UnitChapters)
                .Select(uc => uc.DeliveryType)
                .Where(dt => dt != null && ("Ass,Con,Gui,Tes,Exa".Contains(dt.DeliveryTypeId)));

            var counts = query.GroupBy(dt => dt.DeliveryTypeId)
                .ToDictionary(g => g.Key, g => g.Count());

            return counts;
        }

        //public void ReadExcelFile(string filePath)
        //{
        //    Application excel = new Application();
        //    Workbook workbook = excel.Workbooks.Open(filePath);
        //    Worksheet worksheet = workbook.Sheets[1];

        //    // Assuming the data starts from cell A1
        //    int rowCount = worksheet.UsedRange.Rows.Count;
        //    int colCount = worksheet.UsedRange.Columns.Count;

        //    for (int i = 1; i <= rowCount; i++)
        //    {
        //        for (int j = 1; j <= colCount; j++)
        //        {
        //            // Access cell value
        //            Console.Write(worksheet.Cells[i, j].Value2.ToString() + "\t");
        //        }
        //        Console.WriteLine();
        //    }

        //    workbook.Close();
        //    excel.Quit();
        //    return counts;
        //}

        public async Task<IDictionary<string, double>> GetDeliveryTypePercentages(string syllabusId)
        {
            var counts = await GetDeliveryTypeCounts(syllabusId);
            var totalCount = counts.Values.Sum();

            var percentages = new Dictionary<string, double>();
            foreach (var count in counts)
            {
                var percentage = count.Value / (double)totalCount * 100;
                percentages[count.Key] = Math.Round(percentage, 2);
            }

            return percentages;
        }

        public async Task<Syllabus?> GetOutputStandardBySyllabusIdAsync(string syllabusId)
        {
            return await _context.Syllabi
                .Include(s => s.SyllabusDays)
                .ThenInclude(sd => sd.SyllabusUnits)
                .ThenInclude(su => su.UnitChapters)
                .ThenInclude(uc => uc.OutputStandard)
                .FirstOrDefaultAsync(s => s.SyllabusId == syllabusId);
        }

        public Task<Syllabus?> GetSyllabusName(string syllabusName)
        {
            return FindByCondition(a => a.TopicName == syllabusName).FirstOrDefaultAsync();
        }

        public async Task DeleteDuplicateSyllabuses(string syllabusName)
        {
            // Get all duplicate syllabuses with the given name
            var duplicates = await FindByCondition(s => s.TopicName == syllabusName).ToListAsync();

            // Delete all duplicate syllabuses
            foreach (var duplicate in duplicates)
            {
                _context.Syllabi.Remove(duplicate);
            }

            await _context.SaveChangesAsync();
        }

		public async Task<PagedList<Syllabus>> Search(string keywords, SyllabusParameters syllabusParameters)
		{
			var keywordList = keywords.Split(' ').ToList();

			var syllabus = _context.Syllabi
				.Where(x => keywordList.Any(keyword =>
								x.TopicCode.Contains(keyword) ||
								x.TopicName.Contains(keyword) ||
								x.CreatedBy.Contains(keyword) ||
								x.Status.Contains(keyword)));

			return PagedList<Syllabus>.ToPagedList(
				await syllabus.ToListAsync(),
				syllabusParameters.PageNumber,
				syllabusParameters.PageSize);
		}  
	}
}
