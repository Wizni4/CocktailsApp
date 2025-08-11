// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper.QueryableExtensions;
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;



namespace CocktailsApp.Infrastructure.SeedWork
{
    public class EFQueryService<TDomain, TDTO>(EFDbContext dbContext) 
        : IQueryRepository<TDomain, TDTO> 
        where TDomain : Entity
        where TDTO : EntityDTO
    {
        private readonly EFDbContext _dbContext = dbContext;
        public Task<IEnumerable<TDTO>> ReadAllAsync(IQuerySpecification<TDomain, TDTO> spec)
        {
            var query = _dbContext.Set<TDomain>()
                .AsQueryable()
                .AsNoTracking();

            // Add include to the query
            if (spec.Includes != null)
                query = query.IncludeMultiples(spec.Includes);

            return Task.FromResult(
                query.Select(spec.Selector)
                     .ProjectTo<TDTO>(spec.MapperConfiguration)
                     .AsEnumerable());
        }

        public Task<IEnumerable<TDTO>> ReadAllAsync<TRoot>(IChildQuerySpecification<TRoot, TDomain, TDTO> spec)
            where TRoot: Entity, IAggregateRoot
        {
            var query = _dbContext.Set<TRoot>()
                .AsQueryable()
                .AsNoTracking();

            // Get the children
            var subQuery = query.SelectChild(spec.Selector);

            // Add include to the query
            if (spec.Includes != null)
                subQuery = subQuery.IncludeMultiples(spec.Includes);

            return Task.FromResult(
                subQuery.ProjectTo<TDTO>(spec.MapperConfiguration)
                        .AsEnumerable());
        }

        public Task<TDTO?> ReadAsync(IQuerySpecification<TDomain, TDTO> spec)
        {
            var query = _dbContext.Set<TDomain>()
                .AsQueryable()
                .AsNoTracking();

            // Add include to the query
            if (spec.Includes != null)
                query = query.IncludeMultiples(spec.Includes);

            return Task.FromResult(
                query.Select(spec.Selector)
                     .ProjectTo<TDTO>(spec.MapperConfiguration)
                     .AsEnumerable()
                     .FirstOrDefault());
        }

        public Task<TDTO?> ReadAsync<TRoot>(IChildQuerySpecification<TRoot, TDomain, TDTO> spec)
            where TRoot : Entity, IAggregateRoot
        {
            var query = _dbContext.Set<TRoot>()
                .AsQueryable()
                .AsNoTracking();

            // Get the children
            var subQuery = query.SelectChild(spec.Selector);

            // Add include to the query
            if (spec.Includes != null)
                subQuery = subQuery.IncludeMultiples(spec.Includes);

            return Task.FromResult(
                subQuery.ProjectTo<TDTO>(spec.MapperConfiguration)
                        .AsEnumerable()
                        .FirstOrDefault());
        }

        public Task<IEnumerable<TDTO>> ReadRangeAsync(IQuerySpecification<TDomain, TDTO> spec)
        {
            var query = _dbContext.Set<TDomain>()
                .AsQueryable()
                .AsNoTracking();

            // Add include to the query
            if (spec.Includes != null)
                query = query.IncludeMultiples(spec.Includes);

            return Task.FromResult(
                query.Select(spec.Selector)
                     .ProjectTo<TDTO>(spec.MapperConfiguration)
                     .AsEnumerable());
        }

        public Task<IEnumerable<TDTO>> ReadRangeAsync<TRoot>(IChildQuerySpecification<TRoot, TDomain, TDTO> spec)
            where TRoot : Entity, IAggregateRoot
        {
            var query = _dbContext.Set<TRoot>()
                .AsQueryable()
                .AsNoTracking();

            // Get the children
            var subQuery = query.SelectChild(spec.Selector);

            // Add include to the query
            if (spec.Includes != null)
                subQuery = subQuery.IncludeMultiples(spec.Includes);

            return Task.FromResult(
                subQuery.ProjectTo<TDTO>(spec.MapperConfiguration)
                        .AsEnumerable());
        }
    }
}
