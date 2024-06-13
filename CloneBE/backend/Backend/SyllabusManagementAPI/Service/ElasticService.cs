using AutoMapper;
using Nest;
using SyllabusManagementAPI.Contracts;
using SyllabusManagementAPI.Entities.DTO;
using Entities.Models;
using SyllabusManagementAPI.Entities.Parameters;
using SyllabusManagementAPI.Middleware;
using SyllabusManagementAPI.ServiceContracts;

namespace SyllabusManagementAPI.Service
{
    public class ElasticService : IElasticService
    {
        private readonly IElasticClient _elasticClient;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;

        public ElasticService(IElasticClient elasticClient, ILoggerManager logger, IMapper mapper, ResponseHandler responseHandler)
        {
            _elasticClient = elasticClient;
            _logger = logger;
            _mapper = mapper;
            _responseHandler = responseHandler;
        }

        public async Task<ResponseDTO> SearchAsync(string[] keywords, SyllabusParameters syllabusParameters)
        {
            var syllabus = await _elasticClient.SearchAsync<Syllabus>(
                s => s.Query(
                    q => q.Bool(
                        b => b.Should(
                            keywords?.Select(keyword =>
                                q.QueryString(
                                    d => d.Query($"*{keyword}*")
                                )
                            ).ToArray()
                        )
                    )
                )
                .From(syllabusParameters.PageNumber - 1)
                .Size(syllabusParameters.PageSize)
            );

            var syllabusDTO = _mapper.Map<IEnumerable<SyllabusDTO>>(syllabus.Documents);

            ResponseDTO response = _responseHandler.GetSuccessResponse(
                $"Returned syllabus for keywords '{string.Join(", ", keywords)}' from Elasticsearch.",
                new ResultDTO
                {
                    Data = syllabusDTO,
                    Metadata = null
                });

            return response;
        }
    }
}
