namespace SyllabusManagementAPI.ServiceContracts
{
    public interface IServiceWrapper
    {
        IAssessmentSchemeService AssessmentSchemeService { get; }

        ISyllabusService SyllabusService { get; }

        ISyllabusDayService SyllabusDayService { get; }

        IElasticService elasticService { get; }

        IOutputStandardService OutputStandardService { get; }
    }
}
