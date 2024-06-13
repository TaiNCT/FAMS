using SyllabusManagementAPI.Contracts;
using Entities.Models;
using Entities.Context;
using Microsoft.EntityFrameworkCore;

namespace SyllabusManagementAPI.Repository
{
    public class TrainingMaterialRepository : RepositoryBase<TrainingMaterial>, ITrainingMaterialRepository
    {
        private readonly FamsContext _context;

        public TrainingMaterialRepository(FamsContext context) : base(context)
        {
            _context = context;
        }

        public void Create(string unitChapterId, TrainingMaterial trainingMaterial)
        {
            trainingMaterial.UnitChapterId = unitChapterId;
            Create(trainingMaterial);
        }

        public async Task<List<TrainingMaterial>> GetTrainingMaterial() {
            return await _context.TrainingMaterials.ToListAsync();
        }

    }
}