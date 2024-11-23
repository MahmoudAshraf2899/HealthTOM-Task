using Boilerplate.Contracts.Helpers;

namespace Boilerplate.Contracts.Filters
{
    public class PhotoAlbumFilter : Pager
    {
        public long? photoCategoryId { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }

    }
    public class PhotoAlbumAdminFilter : PhotoAlbumFilter
    {
        public string? title { get; set; }
        public bool? isDeleted { get; set; }
        public int? status { get; set; }
        public bool? isPublished { get; set; }
    }
}