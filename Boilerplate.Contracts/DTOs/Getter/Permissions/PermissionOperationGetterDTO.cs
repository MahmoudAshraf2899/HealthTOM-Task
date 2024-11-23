using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Boilerplate.Contracts.DTOs.Getter
{
    public class PermissionOperationGetterDTO 
    {
        public long Id { get; set; }
        [Display(Name = "name")]
        public string Name { get; set; }
    }
}