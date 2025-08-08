// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Infrastructure.Shared;

using Microsoft.Extensions.Options;

namespace CocktailsApp.API.Shared
{
    public class ImageSettingsConfiguration(IConfiguration configuration)
    : IConfigureNamedOptions<ImageSettings>
    {
        private const string ConfigurationSectionName = "ImageSettings";
        public void Configure(ImageSettings options)
        {
            configuration.GetSection(ConfigurationSectionName).Bind(options);
        }

        public void Configure(string? name, ImageSettings options)
        {
            Configure(options);
        }
    }
}
