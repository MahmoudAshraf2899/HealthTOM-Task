using Autofac.Features.OwnedInstances;
using Boilerplate.Core.Entities.Patient;
using Boilerplate.Core.IServices.Repositories.Patients;
using Boilerplate.Infrastructure.DBContexts;
using Boilerplate.Infrastructure.Services.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Infrastructure.Services.Repositories.Patients
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        private readonly BoilerplateDBContext  _db;
        public PatientRepository(BoilerplateDBContext context) : base(context)
        {
            _db = context;
        }
    }
}
