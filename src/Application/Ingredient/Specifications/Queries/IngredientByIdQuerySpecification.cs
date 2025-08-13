// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using AutoMapper;
using AutoMapper.QueryableExtensions;

using CocktailsApp.Application.SeedWork;

namespace CocktailsApp.Application.Ingredient
{
    public class IngredientByIdQuerySpecification(
        Guid ingredientId,
        IMapper autoMapper
    ) : IQuerySpecification<IngredientRead, IngredientDTO>
    {
        private readonly Guid _ingredientId = ingredientId;
        private readonly IMapper _autoMapper = autoMapper;

        public IQueryable<IngredientRead> Filter(IQueryable<IngredientRead> query)
        {
            return query.Where(i => i.Id == _ingredientId);
        }

        public IQueryable<IngredientDTO> Select(IQueryable<IngredientRead> filtered)
        {
            return filtered.ProjectTo<IngredientDTO>(_autoMapper.ConfigurationProvider);
        }
    }
}
