// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.SeedWork;

using MediatR;


namespace CocktailsApp.Application.Cocktail
{
    public class DeleteCocktailCommandHandler(
        ICocktailRepository cocktailRepository,
        IUnitOfWork unitOfWork
    ) : ICommandHandler<DeleteCocktailCommand, Unit>
    {
        private readonly ICocktailRepository _cocktailRepository = cocktailRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Unit> Handle(DeleteCocktailCommand request, CancellationToken cancellationToken)
        {
            var cocktail = await _cocktailRepository.ReadAsync(
                new CocktailByIdCommandSpecification(request.Id),
                cancellationToken);

            await _cocktailRepository.DeleteAsync(cocktail!, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
