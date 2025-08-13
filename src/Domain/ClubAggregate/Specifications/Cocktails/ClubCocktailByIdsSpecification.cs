// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Domain.SeedWork;

using System.Linq.Expressions;


namespace CocktailsApp.Domain.ClubAggregate
{
    public class ClubCocktailByIdsSpecification(IEnumerable<Guid> clubCocktailIds) : Specification<ClubCocktail>
    {
        private readonly IEnumerable<Guid> _clubCocktailIds = clubCocktailIds;

        public override Expression<Func<ClubCocktail, bool>> SpecExpression => cc => _clubCocktailIds.Any(id => new ClubCocktailByIdSpecification(id).IsSatisfiedBy(cc));
    }
}
