using Boilerplate.Contracts.IServices.Services.ThumbnailService;
using Boilerplate.Contracts.models.ThumbnailModel;
using Boilerplate.Shared.Helpers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Drawing;
using System.Drawing.Imaging;

namespace Boilerplate.Application.Services.ThumbnailService;

public class ImageThumbnailService : IThumbnailService<ImageThumbnailData>
{
    private readonly IHostEnvironment _environment;
    private String _resourcesPath;

    public ImageThumbnailService(IOptions<ResourcesPath> resourcesPath, IHostEnvironment environment)
    {
        _environment = environment;
        this._resourcesPath = resourcesPath.Value.Path.Replace(@"\", @"/");
    }


    public string GenerateThumbnail(ImageThumbnailData thumbnailData)
    {
        var img = Image.FromStream(thumbnailData.file.OpenReadStream());
        var resizedImg = new Bitmap(img, new Size(300, 300));
        using var imageStream = new MemoryStream();
        string fileName = Guid.NewGuid().ToString() + thumbnailData.extention;
        string fileRelativePath = Path.Combine(thumbnailData.relativePath, fileName);
        var absolutePath = Path.Combine(_environment.ContentRootPath, thumbnailData.rootFolder, fileRelativePath);
        resizedImg.Save(absolutePath, ImageFormat.Png);
        return fileName;
    }
}