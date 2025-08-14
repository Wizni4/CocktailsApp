namespace CocktailsApp.Application.Common
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(Stream imageStream, ImageSubject variant, string fileName);
        Task DeleteImageAsync(ImageSubject variant, string imageId);
    }
}
