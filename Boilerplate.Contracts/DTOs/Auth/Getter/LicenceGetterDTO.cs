#nullable disable
using Boilerplate.Contracts.Bases;

namespace Boilerplate.Contracts.DTOs.Auth.Getter
{
    public class LicenceGetterDTO : BaseGetterWithUpdateDTO
    {
        public string PublicKey { get; set; }
        public string LicenceKey { get; set; }
    }
}
