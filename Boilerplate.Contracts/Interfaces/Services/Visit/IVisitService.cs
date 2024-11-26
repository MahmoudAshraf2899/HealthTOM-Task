using Boilerplate.Contracts.Features.Visit.Commands;
using Boilerplate.Contracts.Features.Visit.Queries;
using Boilerplate.Contracts.Interfaces.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Contracts.Interfaces.Services.Visit
{
    public interface IVisitService
    {
        Task<IHolderOfDTO> GetAllAsync(GetAllVisitsQuery request);
        //Task<IHolderOfDTO> GetAllAsync(GetAllExtractsQuery request);
        //Task<IHolderOfDTO> GetProjectSupplierTotalsAsync(GetExtractsProjectSupplierTotalsQuery request);
        //Task<IHolderOfDTO> GetByIdAsync(GetExtractByIdQuery request);

        Task<IHolderOfDTO> SaveAsync(VisitAddCommand command);
        //Task<IHolderOfDTO> UpdateAsync(ExtractUpdateCommand command);
        //Task<IHolderOfDTO> DeleteAsync(ExtractDeleteCommand command);
    }
}
