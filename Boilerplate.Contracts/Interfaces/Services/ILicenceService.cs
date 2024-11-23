using Boilerplate.Contracts.DTOs;
using Boilerplate.Contracts.DTOs.Auth.Setter;
using Boilerplate.Contracts.Interfaces.Custom;

namespace Boilerplate.Contracts.IServices.Services
{
    public interface ILicenceService
    {
        public Task<Key> GetValidLicence(string Licence);
        public Task<IHolderOfDTO> GetLicence();
        public Task<IHolderOfDTO> SaveAsync(LicenceSetterDTO LicenceSetterDTO);
        public IHolderOfDTO Update(LicenceSetterDTO LicenceSetterDTO);
        public IHolderOfDTO Delete();
        public Task<bool> SaveTimeLogAsync();
        public Task<bool> CheckLicenceTime(Key key);
    }
}
