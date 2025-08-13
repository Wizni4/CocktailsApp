// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CocktailsApp.Application.Shared;

using Microsoft.Extensions.Options;

using System.Collections.ObjectModel;


namespace CocktailsApp.Application.SeedWork
{
    public abstract class SearchService<TResult, TSearchQuery>(IOptions<SearchSettingsDTO> options)
        : ISearchService<TResult, TSearchQuery>
        where TResult : EntityDTO
        where TSearchQuery : SearchQuery<ReadOnlyCollection<TResult>>
    {
        private protected IOptions<SearchSettingsDTO> _options = options;
        public abstract Task<ReadOnlyCollection<TResult>> GetSearchResultsAsync(TSearchQuery query, CancellationToken cancellationToken);

        private protected int GetRelevanceScore(string term, string target)
        {
            var result = 0;
            if (string.IsNullOrWhiteSpace(term) || string.IsNullOrWhiteSpace(target))
                return result;

            term = term.ToLowerInvariant();
            target = target.ToLowerInvariant();

            if (target == term)
                result = 100;
            if (target.StartsWith(term))
                result = 80;
            if (target.Contains(term))
                result = 60;

            return 0;
        }
    }
}
