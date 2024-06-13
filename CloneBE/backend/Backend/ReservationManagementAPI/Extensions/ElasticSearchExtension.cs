using ReservationManagementAPI.Entities.Models;
using Nest;
using ReservationManagementAPI.Entities.DTOs;
using FluentResults;
using Elasticsearch.Net;

namespace ReservationManagementAPI.Extensions
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
            settings.DefaultMappingFor <StudentReservedDTO>(d => d);

            #endregion
        }

        private static void CreateIndex(IElasticClient client, string indexName)
        {
            client.Indices.Create(indexName, i => i.Map<StudentReservedDTO>(x => x.Properties(props => props
                .Text(text => text
                    .Name(p => p.ReservedClassId)
                        .Fielddata(true))
                .Text(text => text
                    .Name(p => p.StudentId)
                        .Fielddata(true))
                 .Text(text => text
                    .Name(p => p.MutatableStudentId)
                        .Fielddata(true))
                .Text(text => text
                    .Name(p => p.ClassId)
                        .Fielddata(true))
                .Text(text => text
                    .Name(p => p.Reason)
                        .Fielddata(true))
                .Text(text => text
                    .Name(p => p.StartDate)
                        .Fielddata(true))
                .Text(text => text
                    .Name(p => p.EndDate)
                        .Fielddata(true))
                .Text(text => text
                    .Name(p => p.ClassName)
                        .Fielddata(true))
                .Text(text => text
                    .Name(p => p.ModuleName)
                        .Fielddata(true))
                .Text(text => text
                    .Name(p => p.StudentName)
                        .Fielddata(true))
                .Text(text => text
                    .Name(p => p.Dob)
                        .Fielddata(true))
                .Text(text => text
                    .Name(p => p.Gender)
                        .Fielddata(true))
                .Text(text => text
                    .Name(p => p.Address)
                        .Fielddata(true))
                .Text(text => text
                    .Name(p => p.Email)
                        .Fielddata(true))
                .Text(text => text
                    .Name(p => p.ClassEndDate)
                        .Fielddata(true))
                .Date(date => date.Name(p=>p.CreatedDate))
                ).AutoMap()
            ));
        }
    }
   
}

