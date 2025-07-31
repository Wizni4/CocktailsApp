// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.Club;

using Microsoft.Extensions.Options;

namespace API.Club.Configurations
{
    public class ClubSettingsConfiguration(IConfiguration configuration) : IConfigureNamedOptions<ClubSettingsDTO>
    {
        private const string ConfigurationSectionName = "ClubSettings";

        public void Configure(ClubSettingsDTO options)
        {
            configuration.GetSection(ConfigurationSectionName).Bind(options);
        }

        public void Configure(string? name, ClubSettingsDTO options)
        {
            Configure(options);
        }
    }
}
