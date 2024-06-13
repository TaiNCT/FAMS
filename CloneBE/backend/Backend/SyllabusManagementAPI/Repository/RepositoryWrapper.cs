using SyllabusManagementAPI.Contracts;
using Entities.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace SyllabusManagementAPI.Repository
{
    /// <summary>
    /// Represents a wrapper class for accessing repositories in the application.
    /// </summary>
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly FamsContext _famsContext;
        private readonly Lazy<IAssessmentSchemeRepository> _assessmentSchemeRepository;
        private readonly Lazy<IDeliveryTypeRepository> _deliveryTypeRepository;
        private readonly Lazy<IOutputStandardRepository> _outputStandardRepository;
        private readonly Lazy<ISyllabusRepository> _syllabusRepository;
        private readonly Lazy<ISyllabusDayRepository> _syllabusDayRepository;
        private readonly Lazy<ISyllabusUnitRepository> _syllabusUnitRepository;
        private readonly Lazy<ITrainingMaterialRepository> _trainingMaterialRepository;
        private readonly Lazy<IUnitChapterRepository> _unitChapterRepository;
        private readonly IDbContextTransaction _transaction;

        public RepositoryWrapper(FamsContext famsContext)
        {
            _famsContext = famsContext;
            _transaction = _famsContext.Database.BeginTransaction();
			_famsContext = famsContext;
			_assessmentSchemeRepository = new Lazy<IAssessmentSchemeRepository>(() => new AssessmentSchemeRepository(famsContext));
            _deliveryTypeRepository = new Lazy<IDeliveryTypeRepository>(() => new DeliveryTypeRepository(famsContext));
            _outputStandardRepository = new Lazy<IOutputStandardRepository>(() => new OutputStandardRepository(famsContext));
            _syllabusRepository = new Lazy<ISyllabusRepository>(() => new SyllabusRepository(famsContext));
            _syllabusDayRepository = new Lazy<ISyllabusDayRepository>(() => new SyllabusDayRepository(famsContext));
            _syllabusUnitRepository = new Lazy<ISyllabusUnitRepository>(() => new SyllabusUnitRepository(famsContext));
            _trainingMaterialRepository = new Lazy<ITrainingMaterialRepository>(() => new TrainingMaterialRepository(famsContext));
            _unitChapterRepository = new Lazy<IUnitChapterRepository>(() => new UnitChapterRepository(famsContext));
        }

        public IAssessmentSchemeRepository AssessmentSchemeRepository => _assessmentSchemeRepository.Value;

        public ISyllabusRepository Syllabus => _syllabusRepository.Value;

        public ISyllabusDayRepository SyllabusDay => _syllabusDayRepository.Value;

        public IDeliveryTypeRepository DeliveryType => _deliveryTypeRepository.Value;

        public IOutputStandardRepository OutputStandard => _outputStandardRepository.Value;

        public ISyllabusUnitRepository SyllabusUnit => _syllabusUnitRepository.Value;

        public ITrainingMaterialRepository TrainingMaterial => _trainingMaterialRepository.Value;

        public IUnitChapterRepository UnitChapter => _unitChapterRepository.Value;

        public async Task SaveAsync()
        {
            try
            {
                await _famsContext.SaveChangesAsync();
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
            }
        }

		public async Task SaveAsyncV1() => await _famsContext.SaveChangesAsync();
	}
}
