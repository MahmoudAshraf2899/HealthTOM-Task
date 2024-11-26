using Boilerplate.Contracts.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Contracts.Filters
{
    public class VisitsFilter : Pager
    {
        public string? Name { get; set; }
        public DateTime? Date { get; set; }
    }
}
