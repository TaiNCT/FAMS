using AutoMapper;
using Nest;
using SyllabusManagementAPI.Contracts;
using SyllabusManagementAPI.Middleware;
using SyllabusManagementAPI.ServiceContracts;

namespace SyllabusManagementAPI.Service
{
    public class ServiceWrapper : IServiceWrapper
    {
        private readonly Lazy<IAssessmentSchemeService> _assessmentSchemeService;
        private readonly Lazy<ISyllabusService> _syllabusService;
        private readonly Lazy<ISyllabusDayService> _syllabusDayService;
        private readonly Lazy<IElasticService> _elasticService;
		private readonly Lazy<IOutputStandardService> _outputStandardService;

        public ServiceWrapper(ILoggerManager logger, IRepositoryWrapper repositoryWrapper, IMapper mapper, ResponseHandler responseHandler, IElasticClient elasticClient)
        {
            _assessmentSchemeService = new Lazy<IAssessmentSchemeService>(() => new AssessmentSchemeService(repositoryWrapper, logger, mapper, responseHandler));
            _syllabusService = new Lazy<ISyllabusService>(() => new SyllabusService(repositoryWrapper, logger, mapper, responseHandler));
            _syllabusDayService = new Lazy<ISyllabusDayService>(() => new SyllabusDayService(repositoryWrapper, logger, mapper, responseHandler));
            _elasticService = new Lazy<IElasticService>(() => new ElasticService(elasticClient, logger, mapper, responseHandler));
            _outputStandardService = new Lazy<IOutputStandardService>(() => new OutputStandardService(repositoryWrapper, logger, mapper, responseHandler));
        }

        public IAssessmentSchemeService AssessmentSchemeService => _assessmentSchemeService.Value;

        public ISyllabusService SyllabusService => _syllabusService.Value;

        public ISyllabusDayService SyllabusDayService => _syllabusDayService.Value;

        public IElasticService elasticService => _elasticService.Value;

		public IOutputStandardService OutputStandardService => _outputStandardService.Value;
	}
}
