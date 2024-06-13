using Entities.Models;
using Nest;

namespace ClassManagementAPI.Extension
{
    public static class ElasticSearchExtensions
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
            settings.DefaultMappingFor<Class>(p =>
                        p.Ignore(x => x.ClassId)
                            .Ignore(x => x.UpdatedBy)
                            .Ignore(x => x.UpdatedDate)
                            .Ignore(x => x.ClassStatus)
                            .Ignore(x => x.StartDate)
                            .Ignore(x => x.ApprovedBy)
                            .Ignore(x => x.EndTime)
                            .Ignore(x => x.StartTime)
                            .Ignore(x => x.AcceptedAttendee)
                            .Ignore(x => x.ReviewDate)
                            .Ignore(x => x.ReviewBy)
                            .Ignore(x => x.ApprovedDate)
                            .Ignore(x => x.TrainingProgramCode)                           
                            .Ignore(x => x.PlannedAttendee)
                            .Ignore(x => x.ActualAttendee)
                            .Ignore(x => x.TrainingProgramCodeNavigation)
                            .Ignore(x => x.Location)
                            .Ignore(x => x.Fsu)
                            .Ignore(x => x.ClassUsers)
                            .Ignore(x => x.AttendeeLevel));
            #endregion
        }

        private static void CreateIndex(IElasticClient client, string indexName)
        {
            client.Indices.Create(indexName, i => i.Map<Class>(x => x.AutoMap()));
        }
    }
}