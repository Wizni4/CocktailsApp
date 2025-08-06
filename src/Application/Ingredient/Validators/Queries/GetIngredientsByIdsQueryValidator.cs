// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.SeedWork;

using FluentValidation;


namespace CocktailsApp.Application.Ingredient
{
    public class GetIngredientsByIdsQueryValidator : AbstractValidator<GetIngredientsByIdsQuery>
    {
        public GetIngredientsByIdsQueryValidator()
        {
            RuleFor(c => c.Ids).ValidList();
            RuleForEach(c => c.Ids).ValidGuid();
        }
    }
}
