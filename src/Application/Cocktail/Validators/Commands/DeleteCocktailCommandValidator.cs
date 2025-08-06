// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CocktailsApp.Application.SeedWork;

using FluentValidation;

using DomainCocktail = CocktailsApp.Domain.CocktailAggregate.Cocktail;

namespace CocktailsApp.Application.Cocktail
{
    public class DeleteCocktailCommandValidator : AbstractValidator<DeleteCocktailCommand>
    {
        public DeleteCocktailCommandValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(c => c.Id)
                .ValidGuid()
                .IsCocktailExists(unitOfWork.Set<DomainCocktail>());
        }
    }
}
