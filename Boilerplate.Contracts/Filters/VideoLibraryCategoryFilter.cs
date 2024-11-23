using Boilerplate.Contracts.Enums;
using Boilerplate.Contracts.Helpers;

namespace Boilerplate.Contracts.Filters;

public class VideoLibraryCategoryFilter :Pager
{
    public string? Title { get; set; }
    public VideoType? VideoType { get; set; }
}