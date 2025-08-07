// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


using DomainCocktail = CocktailsApp.Domain.CocktailAggregate.Cocktail;
using CocktailsApp.Domain.SeedWork;
using System.Linq.Expressions;

namespace CocktailsApp.Application.Cocktail
{
    public class CocktailByNameSpecification(string name) : Specification<DomainCocktail>
    {
        private readonly string _name = name;

        public override Expression<Func<DomainCocktail, bool>> SpecExpression => cocktail => cocktail.Name == _name;
    }
}
