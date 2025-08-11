// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.SeedWork;
using System.Linq.Expressions;


namespace CocktailsApp.Infrastructure.SeedWork
{
    public class Selector<TEntity> : ISelector<TEntity> where TEntity : Entity 
    {
        public IQueryable<TEntity> Input { get; }

        public Selector(IQueryable<TEntity> queryable)
        {
            Input = queryable;
        }

        public ISelector<TChild> Select<TChild>(Expression<Func<TEntity, TChild>> selector) where TChild : Entity
        {
            var result = Input.Select(selector);
            return new Selector<TChild>(result);
        }

        public ISelector<TChild> SelectMany<TChild>(Expression<Func<TEntity, IEnumerable<TChild>>> selector) where TChild : Entity
        {
            var result = Input.SelectMany(selector);
            return new Selector<TChild>(result);
        }

        public ISelector<TEntity> Where(ISpecification<TEntity> specification)
        {
            var result = Input.Where(specification.SpecExpression);
            return new Selector<TEntity>(result);
        }

    }
}
