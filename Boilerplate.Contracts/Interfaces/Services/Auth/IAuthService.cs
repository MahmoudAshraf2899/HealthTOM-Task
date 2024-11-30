using Boilerplate.Contracts.DTOs.Auth.Setter;
using Boilerplate.Contracts.DTOs.Auth.Setter.ForgetPassword;
using Boilerplate.Contracts.DTOs.Auth.Setter.Roles.RoleUser;
using Boilerplate.Contracts.Interfaces.Custom;

namespace Boilerplate.Contracts.IServices.Services.Auth
{
    public interface IAuthService
    {
       
        Task<IHolderOfDTO> LoginAsync(UserLoginSetterDTO setterDTO);
        
        
      
    }
}
