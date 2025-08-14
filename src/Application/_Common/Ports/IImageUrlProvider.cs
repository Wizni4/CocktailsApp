
namespace CocktailsApp.Application.Common
{
    public interface IImageUrlProvider
    {
        string? GetUrl(ImageSubject subject, string? imageId, ImageVariant variant = ImageVariant.Small);
    }
}
