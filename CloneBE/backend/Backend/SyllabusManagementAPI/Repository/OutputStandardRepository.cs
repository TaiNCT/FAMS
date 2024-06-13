using Microsoft.EntityFrameworkCore;
using SyllabusManagementAPI.Contracts;
using Entities.Context;
using Entities.Models;

namespace SyllabusManagementAPI.Repository
{
    public class OutputStandardRepository : RepositoryBase<OutputStandard>, IOutputStandardRepository
    {
        private readonly FamsContext _context;

        public OutputStandardRepository(FamsContext context) : base(context)
        {
            _context = context;
        }

        public void CreateOutputStandardAsync(OutputStandard outputStandard)
        {
            Create(outputStandard);
        }

        public async Task<List<OutputStandard?>> GetAllOutputStandardAsync()
        {
            return await FindAll()
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<OutputStandard?> GetOutputStandardByIdAsync(string outputStandardId)
        {
            return await FindByCondition(x => x.OutputStandardId == outputStandardId).FirstOrDefaultAsync();
        }
        public async Task<OutputStandard?> GetOutputStandardByIdAsyncV2(string outputStandardId)
        {
            return await FindByConditionV2(x => x.OutputStandardId == outputStandardId).FirstOrDefaultAsync();
        }
    }
}