using Autofac.Features.OwnedInstances;
using Boilerplate.Contracts.DTOs.Getter.Visits;
using Boilerplate.Contracts.Features.Visit.Queries;
using Boilerplate.Core.Entities.Visit;
using Boilerplate.Core.IServices.Repositories.Visits;
using Boilerplate.Infrastructure.DBContexts;
using Boilerplate.Infrastructure.Services.Custom;
using Lucene.Net.Search;
using Microsoft.EntityFrameworkCore;
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
        public IQueryable<VisitsGetterDto> GetAll(GetAllVisitsQuery request)
        {
            var query = _db.Visits.AsNoTracking()
                                 .Where(c => !c.IsDeleted)
                                 .Select(c => new VisitsGetterDto
                                 {
                                     Id = c.Id,
                                     Birthdate = c.Patient.BirthDate,
                                     Comment = c.Comment,
                                     Email = c.Patient.Email,
                                     ExamStatus = c.ExamStatus.ToString(),
                                     ExamType = c.ExamType.ToString(),
                                     Gender = c.Patient.Gender.ToString(),
                                     PatientId = c.PatientId,
                                     PatientName = c.Patient.Name,
                                     CreatedAt = c.CreatedAt
                                 }).AsQueryable();

            if (request is not null)
            {
                try
                {
                    if (!string.IsNullOrEmpty(request.Name))
                        query = query.Where(x => x.PatientName.ToLower().Contains(request.Name.ToLower()));

                    if (request.Date is not null)
                        query = query.Where(x => x.CreatedAt.Value.Date == request.Date.Value.Date);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return query;
        }
    }
}
