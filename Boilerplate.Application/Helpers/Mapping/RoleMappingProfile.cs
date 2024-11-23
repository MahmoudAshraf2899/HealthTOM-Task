using Boilerplate.Contracts.DTOs.Auth.Getter.Roles.Role;
using Boilerplate.Contracts.DTOs.Auth.Getter.Roles.RoleTranslation;
using Boilerplate.Contracts.DTOs.Auth.Setter.Roles.Role;
using Boilerplate.Contracts.DTOs.Auth.Setter.Roles.RoleTranslation;
using Boilerplate.Contracts.DTOs.Getter.Lookups;
using Boilerplate.Core.Entities.Auth.Roles;

namespace Boilerplate.Application.Helpers
{
    public partial class MappingProfile
    {
        private void RoleMappingProfile()
        {
            #region Role
            CreateMap<RoleSetterDTO, Role>()
                .ForMember(d => d.NormalizedName, s => s.MapFrom(o => o.Name.ToUpper()))
                .AfterMap((src, d) => d.Id = Guid.NewGuid().ToString())
                .AfterMap((src, d) => d.ConcurrencyStamp = Guid.NewGuid().ToString());

            CreateMap<RoleUpdateSetterDTO, Role>()
                .ForMember(d => d.NormalizedName, s => s.MapFrom(o => o.Name.ToUpper()));

            CreateMap<Role, RoleGetterDTO>()
                .ForMember(d => d.UserCount, s => s.MapFrom(o => getRoleUserCount(o.Id)));

            CreateMap<Role, RoleWithPermissionDataGetterDTO>()
                .ForMember(d => d.UserCount, s => s.MapFrom(o => getRoleUserCount(o.Id)));

            CreateMap<Role, LookupStringGetterDTO>()
              .ForMember(d => d.Translations, s => s.MapFrom(o => o.RoleTranslations));

            CreateMap<RoleTranslation, RoleTranslationSetterDTO>().ReverseMap();

            CreateMap<RoleTranslation, RoleTranslationGetterDTO>();

            CreateMap<RoleTranslation, LookupTranslationGetterDTO>();
            #endregion
        }
    }
}
