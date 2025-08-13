// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.CocktailAggregate;


namespace CocktailsApp.Application.Cocktail
{
    public class CreateCocktailCommandHandler(
        ICocktailRepository cocktailRepository,
        IUnitOfWork unitOfWork
    ) : ICommandHandler<CreateCocktailCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICocktailRepository _cocktailRepository = cocktailRepository;

        public async Task<Guid> Handle(CreateCocktailCommand request, CancellationToken cancellationToken)
        {
            var cocktailBuilder = new CocktailBuilder()
                .WithCreatorId(request.ActorId)
                .WithDescription(request.Description)
                .WithName(request.Name);

            foreach (var ingredient in request.Ingredients)
                cocktailBuilder.AddIngredient(
                    ingredient.Id,
                    ingredient.Quantity,
                    ingredient.Unit
                );

            var cocktail = cocktailBuilder.Build();

            await _cocktailRepository.CreateAsync(cocktail, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return cocktail.Id;
        }
    }
}
