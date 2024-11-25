using AutoMapper;
using Boilerplate.Contracts.Interfaces.Custom;
using Boilerplate.Contracts.Interfaces.Services.Patient;
using Boilerplate.Core.Bases;
using Boilerplate.Core.IServices.Custom;
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

namespace Boilerplate.Application.Services.Patients
{
    public class PatientService : BaseService<PatientService>, IPatientService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public PatientService(IUnitOfWork unitOfWork, IMapper mapper, IHolderOfDTO holderOfDTO, ICulture culture,
            ILogger<PatientService> logger, IHostEnvironment environment, IHttpContextAccessor httpContextAccessor, IHostingEnvironment hostingEnvironment)
            : base(unitOfWork, mapper, holderOfDTO, logger, culture, environment, httpContextAccessor)
        {
            _hostingEnvironment = hostingEnvironment;
        }

    }
}
