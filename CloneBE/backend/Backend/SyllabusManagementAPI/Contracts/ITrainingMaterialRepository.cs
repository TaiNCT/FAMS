using Entities.Models;

namespace SyllabusManagementAPI.Contracts
{
    public interface ITrainingMaterialRepository
    {
        void Create(string unitChapterId, TrainingMaterial trainingMaterial);
    }
}