// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.SeedWork;


namespace CocktailsApp.Application.Club
{
    public abstract class ClubQuerySpecification<TEntity, TDTO>
        : IQuerySpecificationBase<TEntity, TDTO>
        where TEntity : Entity
        where TDTO : EntityDTO
    {
        public abstract Func<IIncludable<TEntity>, IIncludable>? Includes { get; }

        public IProfileConfiguration MapperConfiguration => new ClubApplicationProfile();
    }
}
