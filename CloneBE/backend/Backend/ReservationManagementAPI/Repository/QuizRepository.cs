using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReservationManagementAPI.Contracts;
using ReservationManagementAPI.Entities.DTOs;
using Entities.Models;
using System.Reflection;
using Entities.Context;

namespace ReservationManagementAPI.Repository
{
    public class QuizRepository : RepositoryBase<Quiz>, IQuizRepository
    {
        private readonly IMapper _mapper;
        public QuizRepository(FamsContext repositoryContext, IMapper mapper)
            : base(repositoryContext)
        {
             _mapper = mapper;
        }
       
        public async Task<Quiz> GetQuizByQuizId(string quizId)
        {
            var quiz = await RepositoryContext.Quizzes.Where(c => c.QuizId.Equals(quizId)).FirstOrDefaultAsync();
            return quiz;
        }
    }
}
