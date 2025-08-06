// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.CocktailAggregate;

using DomainCocktails = CocktailsApp.Domain.CocktailAggregate.Cocktail;

namespace CocktailsApp.Application.Cocktail
{
    public class CreateCocktailCommandHandler(
        IUnitOfWork unitOfWork
    ) : ICommandHandler<CreateCocktailCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Guid> Handle(CreateCocktailCommand request, CancellationToken cancellationToken)
        {
            var cocktailBuilder = new CocktailBuilder()
                .WithCreatorId(request.CreatorId)
                .WithDescription(request.Description)
                .WithName(request.Name);

            foreach (var ingredient in request.Ingredients)
                cocktailBuilder.AddIngredient(ingredient.Id, ingredient.Quantity);

            var cocktail = cocktailBuilder.Build();

            _unitOfWork.Set<DomainCocktails>().Create(cocktail);
            await _unitOfWork.SaveChangesAsync();

            return cocktail.Id;
        }
    }
}
