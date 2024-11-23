using Boilerplate.Contracts.Helpers;
#nullable disable

namespace Boilerplate.Contracts.Filters
{
    public class ElementFilter : Pager
    {
        public long? Id { get; set; }
        public string Key { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
