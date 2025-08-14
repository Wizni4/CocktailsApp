using CocktailsApp.Application.Common;

using Microsoft.Extensions.Options;

namespace CocktailsApp.API.Assets
{
    public sealed class ImageUrlProvider(
        IOptions<ImageOptions> options
    ) : IImageUrlProvider
    {
        private readonly ImageOptions _options = options.Value;

        public string? GetUrl(ImageSubject subject, string? imageId, ImageVariant variant = ImageVariant.Small)
        {
            string? path = null, fallback = null;

            switch (subject)
            {
                case ImageSubject.Club:
                    path = _options.ClubPath;
                    fallback = _options.DefaultClubImage; break;
                case ImageSubject.Cocktail:
                    path = _options.CocktailPath;
                    fallback = _options.DefaultCocktailImage; break;
                case ImageSubject.Ingredient:
                    path = _options.IngredientPath;
                    fallback = _options.DefaultIngredientImage; break;
                case ImageSubject.User:
                    path = _options.UserPath;
                    fallback = _options.DefaultUserImage; break;
            }

            if (string.IsNullOrWhiteSpace(imageId))
                return string.IsNullOrWhiteSpace(fallback) ? null : Combine(_options.PublicBaseUrl, fallback!);

            var key = path!.Replace("{id}", imageId);
            return Combine(_options.PublicBaseUrl, key);
        }

        private static string Combine(string baseUrl, string rel) => $"{baseUrl.TrimEnd('/')}/{rel.TrimStart('/')}";
    }
}
