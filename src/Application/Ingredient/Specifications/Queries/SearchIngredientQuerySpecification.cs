// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;
using AutoMapper.QueryableExtensions;

using CocktailsApp.Application.SeedWork;


namespace CocktailsApp.Application.Ingredient
{
    public class SearchIngredientQuerySpecification(
        int limit,
        string term,
        IMapper autoMapper
    ) : IQuerySpecification<IngredientRead, IngredientDTO>
    {
        private readonly int _limit = limit;
        private readonly string _term = term;
        private readonly IMapper _autoMapper = autoMapper;

        public IQueryable<IngredientRead> Filter(IQueryable<IngredientRead> query)
        {
            return query.Where(i => i.Name.Contains(_term));
        }

        public IQueryable<IngredientDTO> Select(IQueryable<IngredientRead> filtered)
        {
            return filtered.ProjectTo<IngredientDTO>(_autoMapper.ConfigurationProvider);
        }
    }
}
