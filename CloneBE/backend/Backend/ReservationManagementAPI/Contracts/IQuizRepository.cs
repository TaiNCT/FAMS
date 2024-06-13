using ReservationManagementAPI.Entities.DTOs;
using Entities.Models;

namespace ReservationManagementAPI.Contracts
{
    public interface IQuizRepository : IRepositoryBase<Quiz>
    {
        Task<Quiz> GetQuizByQuizId (string quizId);
    }
}
