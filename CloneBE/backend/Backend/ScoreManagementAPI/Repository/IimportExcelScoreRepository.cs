using ScoreManagementAPI.DTO;

namespace ScoreManagementAPI.Repository
{
    public interface IimportExcelScoreRepository
    {
       ImportExcelScoreResponse AddExcelWithListScore(ImportExcelScoreRequest request, Stream path, string option, IStudentRepository repo);
    }
}
