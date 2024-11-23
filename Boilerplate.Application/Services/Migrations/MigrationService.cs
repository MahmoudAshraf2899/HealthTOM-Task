using AutoMapper;
using Boilerplate.Contracts.Interfaces.Custom;
using Boilerplate.Contracts.IServices.Services.Migrations;
using Boilerplate.Core.Bases;
using Boilerplate.Core.IServices.Custom;
using Boilerplate.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Boilerplate.Application.Services.Migrations
{
    public class MigrationService : BaseService<MigrationService>, IMigrationService
    {

        public MigrationService(IUnitOfWork unitOfWork, IMapper mapper, IHolderOfDTO holderOfDTO, ICulture culture,
            IHostEnvironment environment, IHttpContextAccessor httpContextAccessor, ILogger<MigrationService> logger)
            : base(unitOfWork, mapper, holderOfDTO, logger, culture, environment, httpContextAccessor)
        {
        }
    }
}
