// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;
using AutoMapper.QueryableExtensions;

using CocktailsApp.Application.SeedWork;


namespace CocktailsApp.Application.Cocktail
{
    public class CocktailByIdQuerySpecification(
        Guid coktailId,
        IMapper autoMapper
    ) : IQuerySpecification<CocktailRead, CocktailDTO>
    {
        private readonly Guid _cocktailId = coktailId;
        private readonly IMapper _autoMapper = autoMapper;

        public IQueryable<CocktailRead> Filter(IQueryable<CocktailRead> query)
        {
            return query.Where(c => c.Id == _cocktailId);
        }

        public IQueryable<CocktailDTO> Select(IQueryable<CocktailRead> filtered)
        {
            return filtered.ProjectTo<CocktailDTO>(_autoMapper.ConfigurationProvider);
        }
    }
}
