using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StudentInfoManagementAPI.DTO;
using Entities.Models;

namespace StudentInfoManagementAPITesting
{
    public class Mapping:Profile
    {
        public Mapping(){
            CreateMap<StudentInfoDTO, Student>().ReverseMap();
            CreateMap<ClassDTO, Class>().ReverseMap();
            CreateMap<StudentClassDTO, StudentClass>().ReverseMap();
            CreateMap<MajorDTO, Major>().ReverseMap();
        }
    }
}