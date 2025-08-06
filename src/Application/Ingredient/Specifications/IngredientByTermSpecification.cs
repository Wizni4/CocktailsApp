// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Domain.SeedWork;

using System.Linq.Expressions;

using DomainIngredient = CocktailsApp.Domain.IngredientAggregate.Ingredient;

namespace CocktailsApp.Application.Ingredient
{
    public class IngredientByTermSpecification(string term) : Specification<DomainIngredient>
    {
        private readonly string _term = term;
        public override Expression<Func<DomainIngredient, bool>> SpecExpression => i => i.Name.Contains(_term);
    }
}
