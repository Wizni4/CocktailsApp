// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.SeedWork;

using FluentValidation;


namespace CocktailsApp.Application.Ingredient
{
    public class UploadIngredientImageCommandValidator : CommandValidator<UploadIngredientImageCommand>
    {
        public UploadIngredientImageCommandValidator()
        {
            RuleFor(c => c.IngredientId)
                .ValidGuid();
            RuleFor(c => c.Image)
                .NotNull();
            RuleFor(c => c.ImageName)
                .ValidString();
        }
    }
}
