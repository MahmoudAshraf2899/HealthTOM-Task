using AutoMapper;
using Boilerplate.Contracts.DTOs;
using Boilerplate.Contracts.DTOs.Auth.Getter;
using Boilerplate.Contracts.DTOs.Auth.Setter;
using Boilerplate.Contracts.Interfaces.Custom;
using Boilerplate.Contracts.IServices.Services;
using Boilerplate.Contracts.IServices.Services.EncryptionAndDecryption;
using Boilerplate.Core.Bases;
using Boilerplate.Core.Entities.Auth;
using Boilerplate.Core.IServices.Custom;
using Boilerplate.Shared.Consts;
using Boilerplate.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace Boilerplate.Application.Services.LicenceService
{
    public class LicenceService : BaseService<LicenceService>, ILicenceService
    {
        private readonly IEncryptionAndDecryptionService _encryptionAndDecryptionService;

        public LicenceService(IUnitOfWork unitOfWork, IMapper mapper, IHolderOfDTO holderOfDTO, ICulture culture, IEncryptionAndDecryptionService encryptionAndDecryptionService,
            IHostEnvironment environment, IHttpContextAccessor httpContextAccessor, ILogger<LicenceService> logger)
            : base(unitOfWork, mapper, holderOfDTO, logger, culture, environment, httpContextAccessor)
        {
            _encryptionAndDecryptionService = encryptionAndDecryptionService;
        }

        public async Task<Key> GetValidLicence(string Licence)
        {
            var options = new RestClientOptions("http://digitalhubeg-001-site3.atempurl.com/api/v1/admin/Licence/ValidateLicence?Licence=" + Licence)
            //var options = new RestClientOptions("https://localhost:44394/api/v1/admin/Licence/ValidateLicence?Licence=" + Licence)
            {
                ThrowOnAnyError = true,
                Timeout = -1
            };
            var client = new RestClient(options);
            var request = new RestRequest();
            var response = await client.GetAsync(request);
            var FromJson = JsonConvert.DeserializeObject<GeneralResponse>(response.Content);
            if (FromJson != null && FromJson.state)
            {
                var DecryptedMsg = _encryptionAndDecryptionService.DecryptData(FromJson.data);
                return JsonConvert.DeserializeObject<Key>(DecryptedMsg);
            }
            else
            {
                return null;
            }

            //var response = await client.GetAsync<Key>(request);
            //IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
        }

        public async Task<IHolderOfDTO> GetLicence()
        {
            List<bool> lIndicators = new List<bool>();
            try
            {
                _holderOfDTO.Add(Res.Response, _mapper.Map<LicenceGetterDTO>(await _unitOfWork.Licences.FirstOrDefaultAsync(q => !q.IsDeleted)));
                lIndicators.Add(true);
            }
            catch (Exception ex)
            {
                _holderOfDTO.Add(Res.message, ex.Message);
                lIndicators.Add(false);
            }
            _holderOfDTO.Add(Res.state, lIndicators.All(x => x));
            return _holderOfDTO;
        }

        public async Task<IHolderOfDTO> SaveAsync(LicenceSetterDTO LicenceSetterDTO)
        {
            List<bool> lIndicators = new List<bool>();
            try
            {
                if (_unitOfWork.Licences.CountAsync().Result > 0)
                {
                    lIndicators.Add(false);

                }
                else
                {
                    var Licence = _mapper.Map<Licence>(LicenceSetterDTO);
                    AddCreateData(Licence);
                    var oLicence = await _unitOfWork.Licences.AddAsync(Licence);
                    lIndicators.Add(_unitOfWork.Complete() > 0);
                    _holderOfDTO.Add(Res.Response, _mapper.Map<LicenceGetterDTO>(oLicence));
                }

            }
            catch (Exception ex)
            {
                _holderOfDTO.Add(Res.message, ex.Message);
                lIndicators.Add(false);
            }
            _holderOfDTO.Add(Res.state, lIndicators.All(x => x));
            return _holderOfDTO;
        }

        public IHolderOfDTO Update(LicenceSetterDTO LicenceSetterDTO)
        {
            List<bool> lIndicators = new List<bool>();
            try
            {
                var OldLicence = _unitOfWork.Licences.FirstOrDefaultAsync(q => q.Id != 0).Result;
                if (OldLicence != null)
                {
                    AddUpdateData(OldLicence);
                    OldLicence.LicenceKey = LicenceSetterDTO.LicenceKey;
                    OldLicence.PublicKey = LicenceSetterDTO.PublicKey;
                    var oLicence = _unitOfWork.Licences.Update(OldLicence);
                    lIndicators.Add(_unitOfWork.Complete() > 0);
                    _holderOfDTO.Add(Res.Response, _mapper.Map<LicenceGetterDTO>(oLicence));
                }
                else
                {
                    lIndicators.Add(false);
                }

            }
            catch (Exception ex)
            {
                _holderOfDTO.Add(Res.message, ex.Message);
                lIndicators.Add(false);
            }
            _holderOfDTO.Add(Res.state, lIndicators.All(x => x));
            return _holderOfDTO;
        }

        public IHolderOfDTO Delete()
        {
            List<bool> lIndicators = new List<bool>();
            try
            {
                var OldLicence = _unitOfWork.Licences.FirstOrDefaultAsync(q => q.Id != 0).Result;
                if (OldLicence != null)
                {
                    _unitOfWork.Licences.Delete(OldLicence);
                    lIndicators.Add(_unitOfWork.Complete() > 0);
                }
                else
                {
                    lIndicators.Add(false);
                }
            }
            catch (Exception ex)
            {
                _holderOfDTO.Add(Res.message, ex.Message);
                lIndicators.Add(false);
            }
            _holderOfDTO.Add(Res.state, lIndicators.All(x => x));
            return _holderOfDTO;
        }
        public async Task<bool> SaveTimeLogAsync()
        {
            List<bool> lIndicators = new List<bool>();
            try
            {
                TimeLog time = new TimeLog()
                {
                    CheckTime = DateTime.Now,
                    CreatedBy = GetUserId(),
                    Id = 0
                };
                var Licence = await _unitOfWork.TimeLogs.AddAsync(time);
                lIndicators.Add(_unitOfWork.Complete() > 0);
            }
            catch (Exception ex)
            {
                _holderOfDTO.Add(Res.message, ex.Message);
                lIndicators.Add(false);
            }
            return lIndicators.All(x => x);
        }
        public async Task<bool> CheckLicenceTime(Key key)
        {
            List<bool> lIndicators = new List<bool>();
            try
            {
                //var lastCheck = await _unitOfWork.TimeLogs.GetLastCheckDate();
                //var currentDate = DateTime.Now;

                //if (lastCheck != null)
                //{
                //    if (key != null && key.StartDate <= currentDate && key.ExpirationDate >= currentDate && currentDate> lastCheck. CheckTime )
                //        lIndicators.Add(true);
                //    else
                //        lIndicators.Add(false);

                //}
                //else
                //{
                //    if (key != null && key.StartDate <= currentDate && key.ExpirationDate >= currentDate)
                //        lIndicators.Add(true);
                //    else
                //        lIndicators.Add(false);
                //}
            }
            catch (Exception ex)
            {
                _holderOfDTO.Add(Res.message, ex.Message);
                lIndicators.Add(false);
            }
            return lIndicators.All(x => x);
        }

    }


}
