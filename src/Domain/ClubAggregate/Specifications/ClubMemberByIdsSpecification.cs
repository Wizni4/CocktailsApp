// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Domain.SeedWork;
using System.Linq.Expressions;

namespace CocktailsApp.Domain.ClubAggregate
{
    public class ClubMemberByIdsSpecification(IEnumerable<Guid> clubMemberIds) : Specification<ClubMember>
    {
        private readonly IEnumerable<Guid> _clubMemberIds = clubMemberIds;
        public override Expression<Func<ClubMember, bool>> SpecExpression => cm => _clubMemberIds.Any(id => new ClubMemberByIdSpecification(id).IsSatisfiedBy(cm));
    }
}
