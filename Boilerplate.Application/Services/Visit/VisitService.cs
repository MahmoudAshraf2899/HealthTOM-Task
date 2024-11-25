using AutoMapper;
using Boilerplate.Contracts.Interfaces.Custom;
using Boilerplate.Contracts.Interfaces.Services.Visit;
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
namespace Boilerplate.Application.Services.Visit
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
    }
}
