// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.SeedWork;

using DomainIngredient = CocktailsApp.Domain.IngredientAggregate.Ingredient;


namespace CocktailsApp.Application.Ingredient
{
    public class GetIngredientByIdQueryHandler(
        IMapper autoMapper,
        IUnitOfWork unitOfWork
    ) : IQueryHandler<GetIngredientByIdQuery, IngredientDTO>
    {
        private readonly IMapper _autoMapper = autoMapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IngredientDTO> Handle(GetIngredientByIdQuery request, CancellationToken cancellationToken)
        {
            var ingredient = await _unitOfWork.Set<DomainIngredient>().ReadAsync(
                new IngredientByIdSpecification(request.IngredientId));

            return _autoMapper.Map<IngredientDTO>(ingredient);
        }
    }
}
