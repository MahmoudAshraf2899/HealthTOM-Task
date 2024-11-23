using System.ComponentModel.DataAnnotations;
#nullable disable
namespace Boilerplate.Contracts.DTOs.Auth.Setter
{
    public class RoleUpdateSetter1DTO
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
