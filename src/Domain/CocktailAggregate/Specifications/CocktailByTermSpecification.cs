// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Domain.SeedWork;

using System.Linq.Expressions;


namespace CocktailsApp.Domain.CocktailAggregate
{
    public class CocktailByTermSpecification(string term) : Specification<Cocktail>
    {
        private readonly string _term = term;

        public override Expression<Func<Cocktail, bool>> SpecExpression => cocktail => cocktail.Name.Contains(_term);
    }
}
