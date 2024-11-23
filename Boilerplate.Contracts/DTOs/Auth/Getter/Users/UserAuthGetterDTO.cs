using Boilerplate.Contracts.Enums;
using Boilerplate.Contracts.Interfaces.Custom;
#nullable disable

namespace Boilerplate.Contracts.DTOs.Auth.Getter.Users
{
    public class UserAuthGetterDTO : IFilePathDTO
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public Degree Degree { get; set; }
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public string? Path { get; set; }
        public string? DisplayPath { get; set; }
    }
    public class AdminAuthGetterDTO : UserAuthGetterDTO
    {
        public Dictionary<string, List<long>> Permissions { get; set; }
        public List<string> Roles { get; set; }
    }
}
