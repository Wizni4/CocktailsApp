// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.SeedWork;

using DomainIngredient = CocktailsApp.Domain.IngredientAggregate.Ingredient;

namespace CocktailsApp.Application.Ingredient
{
    public class GetAllIngredientsQueryHandler(
        IMapper autoMapper,
        IUnitOfWork unitOfWork
    ) : IQueryHandler<GetAllIngredientsQuery, IEnumerable<IngredientDTO>>
    {
        private readonly IMapper _autoMapper = autoMapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<IEnumerable<IngredientDTO>> Handle(GetAllIngredientsQuery request, CancellationToken cancellationToken)
        {
            var ingredients = await _unitOfWork.Set<DomainIngredient>().ReadAllAsync();

            return _autoMapper.Map<IEnumerable<IngredientDTO>>(ingredients);
        }
    }
}
