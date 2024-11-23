
using Boilerplate.Contracts.Bases;

namespace Boilerplate.Contracts.DTOs.Auth.Setter
{
    public class LicenceSetterDTO : BaseSetterWithUpdateDTO
    {
        public string PublicKey { get; set; }
        public string LicenceKey { get; set; }
    }
}
