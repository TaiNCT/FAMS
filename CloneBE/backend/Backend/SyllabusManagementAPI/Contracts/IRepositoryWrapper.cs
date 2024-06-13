namespace SyllabusManagementAPI.Contracts
{
    public interface IRepositoryWrapper
    {
        IAssessmentSchemeRepository AssessmentSchemeRepository { get; }

        IDeliveryTypeRepository DeliveryType { get; }

        IOutputStandardRepository OutputStandard { get; }

        ISyllabusRepository Syllabus { get; }

        ISyllabusDayRepository SyllabusDay { get; }

        ISyllabusUnitRepository SyllabusUnit { get; }

        ITrainingMaterialRepository TrainingMaterial { get; }

        IUnitChapterRepository UnitChapter { get; }

        Task SaveAsync();

		Task SaveAsyncV1();
	}
}
