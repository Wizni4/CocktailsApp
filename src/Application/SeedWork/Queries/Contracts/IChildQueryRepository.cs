// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CocktailsApp.Domain.SeedWork;


namespace CocktailsApp.Application.SeedWork
{
    public interface IChildQueryRepository<TRoot, TEntity, TDTO>
        where TRoot : Entity, IAggregateRoot
        where TEntity : Entity
        where TDTO : EntityDTO
    {
        Task<TDTO?> ReadAsync(IChildQuerySpecification<TRoot, TEntity, TDTO> spec);
        Task<IEnumerable<TDTO>> ReadRangeAsync(IChildQuerySpecification<TRoot, TEntity, TDTO> spec);
        Task<IEnumerable<TDTO>> ReadAllAsync(IChildQuerySpecification<TRoot, TEntity, TDTO> spec);
    }
}
