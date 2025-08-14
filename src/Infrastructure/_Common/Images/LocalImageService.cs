using CocktailsApp.Application.Common;

using Microsoft.Extensions.Options;


namespace CocktailsApp.Infrastructure.Common
{
    public class LocalImageService(
        IOptions<ImageStorageOptions> options
    ) : IImageService
    {
        private readonly IOptions<ImageStorageOptions> _options = options;
        public Task DeleteImageAsync(ImageSubject subject, string fileName)
        {
            var imagePath = Path.Combine(
                _options.Value.RootPath!,
                subject
                .ToString()
                .ToLowerInvariant(),
                fileName
            );
            File.Delete(imagePath);
            return Task.CompletedTask;
        }

        public async Task<string> UploadImageAsync(Stream imageStream, ImageSubject subject, string fileName)
        {
            var imageId = $"{Guid.NewGuid():N}{fileName}";
            var imagePath = Path.Combine(_options.Value.RootPath!,
                subject
                .ToString()
                .ToLowerInvariant(),
                imageId
            );
            using var fileStream = new FileStream(imagePath, FileMode.Create);
            await imageStream.CopyToAsync(fileStream);

            return imageId;
        }
    }
}
