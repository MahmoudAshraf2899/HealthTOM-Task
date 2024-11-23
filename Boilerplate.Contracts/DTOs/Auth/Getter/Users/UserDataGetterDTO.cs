using Boilerplate.Contracts.DTOs.Getter.Lookups;
#nullable disable

namespace Boilerplate.Contracts.DTOs.Auth.Getter.Users
{
    public class UserDataGetterDTO
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public string PhoneNumber { get; set; }
        public string LocalPhoneNumber { get; set; }
        public bool IsLdap { get; set; } = false;
        public string UserUrl { get; set; }
        public int Gender { get; set; }
        public bool IsBanned { get; set; }

    }
    public class UserAdminDataGetterDTO : UserDataGetterDTO
    {
        public List<LookupStringGetterDTO> Roles { get; set; }
        public List<LookupGetterDTO> Departments { get; set; }
    }
}