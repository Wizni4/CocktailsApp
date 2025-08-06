// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.SeedWork;

using DomainIngredient = CocktailsApp.Domain.IngredientAggregate.Ingredient;

namespace CocktailsApp.Application.Ingredient
{
    public class GetIngredientsByIdsQueryHandler(
        IMapper autoMapper,
        IUnitOfWork unitOfWork
    ) : IQueryHandler<GetIngredientsByIdsQuery, IEnumerable<IngredientDTO>>
    {
        private readonly IMapper _autoMapper = autoMapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<IEnumerable<IngredientDTO>> Handle(GetIngredientsByIdsQuery request, CancellationToken cancellationToken)
        {
            var ingredients = await _unitOfWork.Set<DomainIngredient>().ReadRangeAsync(
                new IngredientByIdsSpecification(request.Ids));

            return _autoMapper.Map<IEnumerable<IngredientDTO>>(ingredients);
        }
    }
}
