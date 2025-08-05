// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.Club;
using CocktailsApp.Application.Search;

using Microsoft.Extensions.Options;

namespace CocktailsApp.API.Search
{
    public class SearchSettingsConfiguration(IConfiguration configuration) : IConfigureNamedOptions<SearchSettingsDTO>
    {
        private const string ConfigurationSectionName = "SearchSettings";

        public void Configure(SearchSettingsDTO options)
        {
            configuration.GetSection(ConfigurationSectionName).Bind(options);
        }

        public void Configure(string? name, SearchSettingsDTO options)
        {
            Configure(options);
        }
    }
}
