﻿using Autofac.Features.OwnedInstances;
using Boilerplate.Contracts.IServices.Custom;
using Boilerplate.Core.Entities.Visit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Core.IServices.Repositories.Visits
{
    public interface IVisitRepository : IGenericRepository<Visit>
    {
    }
}