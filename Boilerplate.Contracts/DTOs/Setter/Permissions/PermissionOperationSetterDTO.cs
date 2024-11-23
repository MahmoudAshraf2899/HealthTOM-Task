using Boilerplate.Contracts.Bases;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Boilerplate.Contracts.DTOs.Setter
{
    public class PermissionOperationSetterDTO : BaseUpdateSetterDTO
    {
        [Display(Name = "name")]
        public string Name { get; set; }
    }
}