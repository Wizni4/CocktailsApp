// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;

using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;

namespace CocktailsApp.Application.Club
{
    public class ClubRolesQuerySpecification(
        Guid clubId
    ) : ClubQuerySpecification<ClubRole, ClubDTO>,
        IChildQuerySpecification<DomainClub, ClubRole, ClubRoleDTO>
    {
        private readonly Guid _clubId = clubId;

        public Func<ISelector<DomainClub>, ISelector<ClubRole>> Selector
        {
            get
            {
                return opt => opt.Where(new ClubByIdSpecification(_clubId))
                                 .SelectMany(c => c.Roles);
            }
        }

        public override Func<IIncludable<ClubRole>, IIncludable>? Includes => null;
    }
}
