// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.Shared;

using Microsoft.Extensions.Options;


namespace CocktailsApp.Infrastructure.Shared
{
    public class LocalImageService(
        IOptions<ImageSettings> options
    ) : IImageService
    {
        private readonly IOptions<ImageSettings> _options = options;
        public Task DeleteImageAsync(string fileName)
        {
            var imagePath = Path.Combine(_options.Value.RootPath, fileName);
            File.Delete(imagePath);
            return Task.CompletedTask;
        }

        public async Task<string> UploadImageAsync(Stream imageStream, string fileName)
        {
            var imageId = $"{Guid.NewGuid():N}{fileName}";
            var imagePath = Path.Combine(_options.Value.RootPath, imageId);
            using var fileStream = new FileStream(imagePath, FileMode.Create);
            await imageStream.CopyToAsync(fileStream);

            return imageId;
        }
    }
}
