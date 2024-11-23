using Boilerplate.Contracts.Enums;
using Boilerplate.Contracts.Helpers;

namespace Boilerplate.Contracts.Filters;

public class VideoLibraryFilter : AdminFilterBase
{
    public string? Title { get; set; }
    public VideoType? VideoType { get; set; }
    public int VideoTypee { get; set; }

}
public class VideoAndRadioFilter : Pager
{
    public VideoType? VideoType { get; set; }
}