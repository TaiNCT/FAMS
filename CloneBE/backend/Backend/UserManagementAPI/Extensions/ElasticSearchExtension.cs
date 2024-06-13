using Entities.Models;
using Nest;
using UserManagementAPI.Models;

namespace UserManagementAPI.Extensions
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
            settings.DefaultMappingFor<User>(p => p
            .Ignore(x => x.Id)
            .Ignore(x => x.Username)
            .Ignore(x => x.Password)
            .Ignore(x => x.CreatedBy)
            .Ignore(x => x.CreatedDate)
            .Ignore(x => x.ModifiedBy)
            .Ignore(x => x.ModifiedDate)
            .Ignore(x => x.Avatar)
            .Ignore(x => x.ClassUsers)
            .Ignore(x => x.EmailSends)
            .Ignore(x => x.TrainingPrograms)
            );
            #endregion
        }

        private static void CreateIndex(IElasticClient client, string indexName)
        {
            client.Indices.Create(indexName, i => i.Map<User>(x => x.AutoMap()));
        }
    }
}