using Autofac.Features.OwnedInstances;
using Boilerplate.Core.Entities.Visit;
using Boilerplate.Core.IServices.Repositories.Visits;
using Boilerplate.Infrastructure.DBContexts;
using Boilerplate.Infrastructure.Services.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Infrastructure.Services.Repositories.Visits
{
    public class VisitRepository : GenericRepository<Visit>, IVisitRepository
    {
        private readonly BoilerplateDBContext _db;
        public VisitRepository(BoilerplateDBContext context) : base(context)
        {
            _db = context;

        }
    }
}
