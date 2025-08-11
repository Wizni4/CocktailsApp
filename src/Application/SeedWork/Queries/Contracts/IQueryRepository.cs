// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CocktailsApp.Domain.SeedWork;


namespace CocktailsApp.Application.SeedWork
{
    public interface IQueryRepository<TEntity, TDTO> 
        where TEntity : Entity
        where TDTO : EntityDTO
    {
        Task<TDTO?> ReadAsync(IQuerySpecification<TEntity, TDTO> spec);
        Task<IEnumerable<TDTO>> ReadRangeAsync(IQuerySpecification<TEntity, TDTO> spec);
        Task<IEnumerable<TDTO>> ReadAllAsync(IQuerySpecification<TEntity, TDTO> spec);
    }
}
