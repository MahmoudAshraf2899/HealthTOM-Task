using Boilerplate.Contracts.Interfaces.Custom;

namespace Boilerplate.Contracts.Helpers
{
    public class HolderOfDTO : Dictionary<string, object>, IHolderOfDTO
    {
        public HolderOfDTO()
        {
        }
    }
}
