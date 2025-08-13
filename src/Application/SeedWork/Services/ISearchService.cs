// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


using System.Collections.ObjectModel;


namespace CocktailsApp.Application.SeedWork
{
    public interface ISearchService<TResult, TSearchQuery>
        : IService
        where TResult : EntityDTO
        where TSearchQuery : SearchQuery<ReadOnlyCollection<TResult>>
    {
        Task<ReadOnlyCollection<TResult>> GetSearchResultsAsync(TSearchQuery query, CancellationToken cancellationToken);
    }
}
