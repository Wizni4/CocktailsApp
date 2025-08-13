// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Domain.SeedWork;

using System.Linq.Expressions;


namespace CocktailsApp.Application.SeedWork
{
    public interface ISelector
    {
    }
    public interface ISelector<TEntity> : ISelector where TEntity : Entity
    {
        IQueryable<TEntity> Input { get;  }
        ISelector<TEntity> Where(ISpecification<TEntity> specification);
        ISelector<TEntity> Take(int limit);
        ISelector<TChild> Select<TChild>(Expression<Func<TEntity, TChild>> selector) where TChild : Entity;
        ISelector<TChild> SelectMany<TChild>(Expression<Func<TEntity, IEnumerable<TChild>>> selector) where TChild : Entity;
    }
}
