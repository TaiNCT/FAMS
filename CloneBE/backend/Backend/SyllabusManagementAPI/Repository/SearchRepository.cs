using Entities.Context;
using Microsoft.EntityFrameworkCore;
using SyllabusManagementAPI.Contracts;
using Entities.Models;


namespace SyllabusManagementAPI.Repository
{
    public class SearchRepository : ISearchRepository
    {

        private readonly FamsContext _context;

        public SearchRepository(FamsContext context)
        {
            _context = context;
        }
        public async Task<List<Syllabus>> GetSyllabus()
        {
            return await _context.Syllabi.ToListAsync();
        }

        public async Task<bool> CreateSyllabus(Syllabus model)
        {
            if (model != null)
            {
                try
                {
                    var result = await _context.AddAsync(model);
                    return await _context.SaveChangesAsync() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Something went wrong: " + ex);
                }
            }
            return false;
        }
    }
}
