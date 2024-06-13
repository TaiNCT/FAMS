using Entities;
using Entities.Models;
using Nest;

namespace EmailInformAPI.Extensions
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
            settings.DefaultMappingFor<EmailTemplate>(p => p
            .Ignore(x => x.CreatedBy)
            .Ignore(x => x.CreatedDate)
            .Ignore(x => x.UpdatedBy)
            .Ignore(x => x.UpdatedDate)
            );
            #endregion
        }

        private static void CreateIndex(IElasticClient client, string indexName)
        {
            client.Indices.Create(indexName, i => i.Map<EmailTemplate>(x => x.AutoMap()));
        }
    }
}
