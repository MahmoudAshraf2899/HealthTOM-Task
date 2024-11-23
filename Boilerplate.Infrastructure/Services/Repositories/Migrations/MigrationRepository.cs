using Boilerplate.Contracts.IServices.Repositories.Migrations;
using Boilerplate.Core.Entities.Migrations;
using Boilerplate.Infrastructure.Services.Custom;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate.Infrastructure.Services.Repositories.Migrations
{
    public class MigrationRepository : GenericRepository<Migration>, IMigrationRepository
    {
        public MigrationRepository(DbContext context) : base(context)
        {
        }
    }
}
