#nullable disable

using Boilerplate.Contracts.Helpers;

namespace Boilerplate.Contracts.Filters
{
    public class TagFilter : Pager
    {
        public string TagName { get; set; }

    }
    public class TagAdminFilter : TagFilter
    {
        public long? Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
