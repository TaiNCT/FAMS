using AutoMapper;
using Contracts.IdentityManagement;
using Contracts.UserManagement;
using Entities.Models;

namespace UserManagementAPI.Models.DTO
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Role, UserDTO>().ReverseMap();
            CreateMap<User, AddUserDTO>().ReverseMap();
            CreateMap<User, Role>().ReverseMap();

            // Synchronized data from IdentityAPI to UserManagementAPI
            CreateMap<User, IdentityCreated>().ReverseMap();
            CreateMap<User, UserCreated>().ReverseMap();
            CreateMap<User, UserUpdated>().ReverseMap();
            CreateMap<User, UserChangeInfo>().ReverseMap();
        }
    }
}
