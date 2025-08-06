// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.SeedWork;

using MediatR;

using DomainCocktail = CocktailsApp.Domain.CocktailAggregate.Cocktail;

namespace CocktailsApp.Application.Cocktail
{
    public class DeleteCocktailCommandHandler(
        IUnitOfWork unitOfWork
    ) : ICommandHandler<DeleteCocktailCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Unit> Handle(DeleteCocktailCommand request, CancellationToken cancellationToken)
        {
            var cocktail = await _unitOfWork.Set<DomainCocktail>().ReadAsync(
                new CocktailByIdSpecification(request.Id));

            _unitOfWork.Set<DomainCocktail>().Delete(cocktail!);
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
