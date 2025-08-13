// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Domain.SeedWork;
using System.Linq.Expressions;


namespace CocktailsApp.Domain.ClubAggregate
{
    public class ClubRoleByIdsSpecification(IEnumerable<Guid> roleIds) : Specification<ClubRole>
    {
        private readonly IEnumerable<Guid> _roleIds = roleIds;
        public override Expression<Func<ClubRole, bool>> SpecExpression => cr => _roleIds.Any(id => new ClubRoleByIdSpecification(id).IsSatisfiedBy(cr));
    }
}
