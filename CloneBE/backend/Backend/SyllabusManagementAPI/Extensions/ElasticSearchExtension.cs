
using Nest;
using Microsoft.Extensions.DependencyInjection;
using Entities.Context;
using Entities.Models;

namespace SyllabusManagementAPI.Extensions
{
	public static class ElasticSearchExtension
	{
		public static void AddElasticSearch(
			this IServiceCollection services, IConfiguration configuration
			)
		{
			var url = configuration["ElasticConfiguration:Uri"];
			var defaultIndex = configuration["ElasticConfiguration:index"];

			var settings = new ConnectionSettings(new Uri(url))
									.PrettyJson()
									.DefaultIndex(defaultIndex);

			AddDefaultMappings(settings);

			var client = new ElasticClient(settings);
			services.AddSingleton<IElasticClient>(client);

			CreateIndex(client, defaultIndex);
		}

		private static void AddDefaultMappings(ConnectionSettings settings)
		{
			#region mapping
			settings.DefaultMappingFor<Syllabus>(p => p.Ignore(x => x.Version)
			.Ignore(x => x.Level)
			.Ignore(x => x.Hours)
			.Ignore(x => x.SyllabusDays)
			.Ignore(x => x.AttendeeNumber)
			.Ignore(x => x.CourseObjective)
			.Ignore(x => x.TrainingProgramSyllabi)
			.Ignore(x => x.AssessmentSchemes)
			.Ignore(x => x.DeliveryPrinciple)
			.Ignore(x => x.ModifiedBy)
			.Ignore(x => x.ModifiedDate)
			.Ignore(x => x.TechnicalRequirement)
			);
			#endregion
		}

		private static void CreateIndex(IElasticClient client, string indexName)
		{
			client.Indices.Create(indexName, i => i.Map<Syllabus>(x => x.AutoMap()));
		}
	}
}
