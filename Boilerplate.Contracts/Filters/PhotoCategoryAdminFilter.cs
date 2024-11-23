using Boilerplate.Contracts.Helpers;

namespace Boilerplate.Contracts.Filters
{
    public class PhotoCategoryAdminFilter : Pager
    {
        public string? title { get; set; }
        public bool? isDeleted { get; set; }
        public int? status { get; set; }
        public bool? isPublished { get; set; }
    }
}