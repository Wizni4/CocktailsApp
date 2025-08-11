// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using AutoMapper;
using CocktailsApp.Domain.SeedWork;


namespace CocktailsApp.Application.SeedWork
{
    public interface IQuerySpecificationBase<TEntity, TDTO>
        where TEntity : Entity
        where TDTO : EntityDTO
    {
        Func<IIncludable<TEntity>, IIncludable>? Includes { get; }
        IProfileConfiguration MapperConfiguration { get; }
    }
    public interface IQuerySpecification<TEntity, TDTO>
        : IQuerySpecificationBase<TEntity, TDTO>
        where TEntity : Entity
        where TDTO : EntityDTO
    {
        Func<ISelector<TEntity>, ISelector> Selector { get; }
    }

    public interface IChildQuerySpecification<TRoot, TEntity, TDTO>
        : IQuerySpecificationBase<TEntity, TDTO>
        where TRoot : Entity, IAggregateRoot
        where TEntity : Entity
        where TDTO : EntityDTO
    {
        Func<ISelector<TRoot>, ISelector<TEntity>> Selector { get; }
    }
}
