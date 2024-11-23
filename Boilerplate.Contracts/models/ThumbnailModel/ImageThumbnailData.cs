using Microsoft.AspNetCore.Http;

namespace Boilerplate.Contracts.models.ThumbnailModel;

public class ImageThumbnailData 
{
    public IFormFile file;
    public string relativePath;
    public string extention;
    public string rootFolder;
}