using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using OJTStudentManagement;


namespace StudentInfoManagementAPITesting
{
    public static class ConfigMapper
    {
        public static IMapper ConfigureAutoMapper()
        {

            // Configure mappings
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Mapping>();
            });

            return mapperConfiguration.CreateMapper();
        }
    }
}