namespace CocktailsApp.Application.Common
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(Stream imageStream, string fileName);
        Task DeleteImageAsync(string imageId);
    }
}
