using Entities.Context;
using ScoreManagementAPI.DAO;
using ScoreManagementAPI.DTO;

namespace ScoreManagementAPI.Repository
{
    public class ImportExcelScoreRepository : IimportExcelScoreRepository
    {
        private readonly FamsContext _context;
        public ImportExcelScoreRepository(FamsContext context)
        {
            _context = context;
        }

        public ImportExcelScoreResponse AddExcelWithListScore(ImportExcelScoreRequest request, Stream steam, string option, IStudentRepository repo)
        {
            ImportExcelScoreDAO excelDao = (new ImportExcelScoreDAO(_context));

            excelDao._studentrepo = repo;

            return excelDao.AddExcelWithListScore(request, steam, option);
        }
    }
}
