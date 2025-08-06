// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Domain.SeedWork;


namespace CocktailsApp.Application.SeedWork
{
    public interface ISearchService<TResult, TSearchQuery>
        : IService where TResult : EntityDTO where TSearchQuery : SearchQuery<IEnumerable<TResult>>
    {
        Task<IEnumerable<TResult>> GetSearchResultsAsync(TSearchQuery query);
    }
}
