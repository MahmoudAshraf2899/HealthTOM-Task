using Boilerplate.Contracts.Helpers;
#nullable disable

namespace Boilerplate.Contracts.Filters
{
    public class DepartmentAdminFilter : Pager
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsDeleted { get; set; }
        public int? Status { get; set; }
        public long? ParentId { get; set; }
        public long? BoilerplateSectorId { get; set; }
    }
}
