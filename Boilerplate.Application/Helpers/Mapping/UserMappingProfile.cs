using Boilerplate.Contracts.Bases;
using Boilerplate.Contracts.DTOs.Auth.Getter;
using Boilerplate.Contracts.DTOs.Auth.Getter.Users;
using Boilerplate.Contracts.DTOs.Auth.Setter;
using Boilerplate.Contracts.DTOs.Getter.Lookups;
using Boilerplate.Core.Entities;
using Boilerplate.Core.Entities.Auth;

namespace Boilerplate.Application.Helpers
{
    public partial class MappingProfile
    {
        private void UserMappingProfile()
        {

            CreateMap<UserRegisterSetterDTO, User>();

            CreateMap<UserSetterDTO, User>().ReverseMap();

            CreateMap<User, UserGetterDTO>()
                .ForMember(d => d.DisplayPath, o => o.MapFrom(s => buildProfileImagePath(s.Path))).ReverseMap();

            CreateMap<User, UserDataGetterDTO>()
                .ForMember(d => d.Username, o => o.MapFrom(s => s.UserName));

            CreateMap<User, UserAuthGetterDTO>()
              .ForMember(d => d.FullName, o => o.MapFrom(s => s.FirstName + " " + s.LastName))
              .ForMember(d => d.DisplayPath, o => o.MapFrom(s => buildProfileImagePath(s.Path)));

            CreateMap<User, AdminAuthGetterDTO>()
              .ForMember(d => d.FullName, o => o.MapFrom(s => s.FirstName + " " + s.LastName))
              .ForMember(d => d.DisplayPath, o => o.MapFrom(s => buildProfileImagePath(s.Path)));

            CreateMap<User, UserAdminDataGetterDTO>()
                .ForMember(d => d.Roles, o => o.MapFrom(s => s.UserRoles.Select(q => q.Role)))                 
                .ForMember(d => d.Username, o => o.MapFrom(s => s.UserName));

            CreateMap<User, UserInfoGetterDTO>().ReverseMap();

            CreateMap<BaseEntityWithUpdate, BaseGetterWithUpdateDTO>();

            CreateMap<BaseEntityUpdate, BaseGetterUpdateDTO>();

            CreateMap<ProfilePictureSetterDTO, ProfilePicture>().ReverseMap();

            

            CreateMap<User, LookupStringGetterDTO>()
               .ForMember(d => d.Name, o => o.MapFrom(s => s.FullName))
               .ForMember(d => d.Translations, o => o.Ignore());
        }
    }
}
