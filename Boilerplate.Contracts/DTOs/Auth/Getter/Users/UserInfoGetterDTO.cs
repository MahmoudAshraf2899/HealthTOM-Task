#nullable disable
using Boilerplate;

namespace Boilerplate.Contracts.DTOs.Auth.Getter.Users
{
    public class UserInfoGetterDTO
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
    }
}