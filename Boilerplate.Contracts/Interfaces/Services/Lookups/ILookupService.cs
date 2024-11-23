using Boilerplate.Contracts.Interfaces.Custom;

namespace Boilerplate.Contracts.IServices.Services.Lookups
{
    public interface ILookupService
    {
        Task<IHolderOfDTO> GetUserAsync();
        Task<IHolderOfDTO> GetRoleAsync();

    }
}
