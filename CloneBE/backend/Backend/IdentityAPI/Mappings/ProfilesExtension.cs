using Contracts.IdentityManagement;
using Contracts.UserManagement;
using IdentityAPI.Payloads.Requests;

namespace IdentityAPI.Mappings;

public class ProfilesExtension : Profile
{
    public ProfilesExtension()
    {
        CreateMap<ApplicationUser, IdentityRegisterRequest>().ReverseMap();   
        CreateMap<ApplicationUser, IdentityCreated>().ReverseMap();
        CreateMap<ApplicationUser, UserCreated>().ReverseMap();
        CreateMap<ApplicationUser, UserUpdated>().ReverseMap();
        CreateMap<ApplicationUser, UserChangeInfo>().ReverseMap();
    }
}