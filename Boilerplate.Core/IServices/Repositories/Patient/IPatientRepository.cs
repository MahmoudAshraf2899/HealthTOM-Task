using Autofac.Features.OwnedInstances;
using Boilerplate.Contracts.IServices.Custom;
using Boilerplate.Core.Entities.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Core.IServices.Repositories.Patients
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        //Task<OwnerByIdGetterDTO> GetByIdAsync(GetOwnerByIdQuery request);
        //IQueryable<OwnersDataGetterDTO> buildFilterQuery(GetAllOwnersQuery filter);
    }
}
