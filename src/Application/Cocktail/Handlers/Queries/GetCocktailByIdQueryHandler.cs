// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.Ingredient;
using CocktailsApp.Application.SeedWork;

using DomainCocktail = CocktailsApp.Domain.CocktailAggregate.Cocktail;
using DomainIngredient = CocktailsApp.Domain.IngredientAggregate.Ingredient;

namespace CocktailsApp.Application.Cocktail
{
    public class GetCocktailByIdQueryHandler(
        IMapper autoMapper,
        IUnitOfWork unitOfWork
    ) : IQueryHandler<GetCocktailByIdQuery, CocktailDTO>
    {
        private readonly IMapper _autoMapper = autoMapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<CocktailDTO> Handle(GetCocktailByIdQuery request, CancellationToken cancellationToken)
        {
            var cocktail = await _unitOfWork.Set<DomainCocktail>().ReadAsync(
                new CocktailByIdSpecification(request.CocktailId),
                opt => opt.Include(c => c.Ingredients));

            var ingredients = await _unitOfWork.Set<DomainIngredient>().ReadAsync(
                new IngredientByIdsSpecification(cocktail!.Ingredients.Select(i => i.IngredientId)));

            return _autoMapper.Map<CocktailDTO>(cocktail, opt =>
            {
                opt.Items["Ingredients"] = ingredients;
            });
        }
    }
}
