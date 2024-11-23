using Boilerplate.Contracts.DTOs.Auth.Setter;
using Boilerplate.Contracts.Filters.Auth;
using Boilerplate.Contracts.Interfaces.Custom;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Boilerplate.Contracts.IServices.Services.Auth
{
    public interface IUserService
    {
        Task<IHolderOfDTO> SearchAsync(UserFilter userFilter);
        Task<IHolderOfDTO> GetByIdAsync(string id);

    }
}
