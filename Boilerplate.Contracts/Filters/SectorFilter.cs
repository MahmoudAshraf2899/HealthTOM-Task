using Boilerplate.Contracts.Helpers;
#nullable disable

namespace Boilerplate.Contracts.Filters
{
    public class SectorFilter : Pager
    {
        public long? Id { get; set; }
        public string Title { get; set; }
    }
}
