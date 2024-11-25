using AutoMapper;
using Boilerplate.Contracts.Features.Visit.Commands;
using Boilerplate.Contracts.Interfaces.Custom;
using Boilerplate.Contracts.Interfaces.Services.Visit;
using Boilerplate.Core.Bases;
using Boilerplate.Core.Entities.Patient;
using Boilerplate.Core.Entities.Visit;
using Boilerplate.Core.IServices.Custom;
using Boilerplate.Shared.Consts;
using Boilerplate.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
namespace Boilerplate.Application.Services.Visits
{
    public class VisitService : BaseService<VisitService>, IVisitService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public VisitService(IUnitOfWork unitOfWork, IMapper mapper, IHolderOfDTO holderOfDTO, ICulture culture,
            ILogger<VisitService> logger, IHostEnvironment environment, IHttpContextAccessor httpContextAccessor, IHostingEnvironment hostingEnvironment)
            : base(unitOfWork, mapper, holderOfDTO, logger, culture, environment, httpContextAccessor)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IHolderOfDTO> SaveAsync(VisitAddCommand command)
        {
            List<bool> lIndicators = new List<bool>();

            try
            {
                if (command is not null)
                {
                    //Check If This Patient is Exist Before 
                    var oPatient = await _unitOfWork.Patient.FindAsync(c => c.Name.ToLower() == command.Name.ToLower());
                    if (oPatient != null)
                    {
                        //Add New Visit With Exist Patient
                        command.PatientId = (int)oPatient.Id;
                        await _unitOfWork.Visit.AddAsync(_mapper.Map<Visit>(command));
                        if (_unitOfWork.Complete() > 0)
                        {
                            lIndicators.Add(true);
                            _holderOfDTO.Add(Res.message, Res.Added);
                            _logger.LogInformation(Res.Added, Res.Added);
                        }
                    }
                    else
                    {
                        //Create New Patient 
                        var oNewPatient = new Patient();
                        oNewPatient.Gender = (Contracts.Enums.Gender)command.Gender;
                        oNewPatient.Email = command.Email;
                        oNewPatient.BirthDate = command.BirthDate;
                        oNewPatient.Name = command.Name;
                        AddCreateData(oNewPatient);


                        await _unitOfWork.Patient.AddAsync(oNewPatient);
                        if (_unitOfWork.Complete() > 0)
                        {
                            command.PatientId = (int)oNewPatient.Id;
                            //The Block of code we could move it to private method in this services but for time of the task we avoid this ..
                            var oVisit = await _unitOfWork.Visit.AddAsync(_mapper.Map<Visit>(command));
                            if (_unitOfWork.Complete() > 0)
                            {
                                lIndicators.Add(true);
                                _holderOfDTO.Add(Res.message, Res.Added);
                                _logger.LogInformation(Res.Added, Res.Added);

                            }



                        }

                    }

                }


            }
            catch (Exception ex)
            {
                ExceptionError(lIndicators, ex.Message);

            }
            _holderOfDTO.Add(Res.state, lIndicators.All(x => x));
            return _holderOfDTO;
        }
    }
}
