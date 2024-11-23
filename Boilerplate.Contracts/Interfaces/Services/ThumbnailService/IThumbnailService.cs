namespace Boilerplate.Contracts.IServices.Services.ThumbnailService;

public interface IThumbnailService<T> where T : class
{
    public string GenerateThumbnail(T thumbnailData);
}