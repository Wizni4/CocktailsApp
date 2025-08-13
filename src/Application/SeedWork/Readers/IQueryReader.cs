// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CocktailsApp.Domain.SeedWork;

using System.Collections.ObjectModel;


namespace CocktailsApp.Application.SeedWork
{

    public interface IQueryReader<TRead, TDTO>
        where TRead : ReadEntity
        where TDTO : EntityDTO
    {
        Task<TDTO?> FirstOrDefaultAsync(IQuerySpecification<TRead, TDTO> spec, CancellationToken cancellationToken);
        Task<IEnumerable<TDTO>> ListAsync(IQuerySpecification<TRead, TDTO> spec, CancellationToken cancellationToken);
        Task<PagedResult<TDTO>> PagedAsync(IQuerySpecification<TRead, TDTO> spec, CancellationToken cancellationToken);
    }

    public sealed record PagedResult<T>(IReadOnlyList<T> Items, int Total);
}
