// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;
using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;


namespace CocktailsApp.Application.Club
{
    public class ClubByIdQuerySpecification(
        Guid clubId
    ) : ClubQuerySpecification<DomainClub, ClubDTO>,
        IQuerySpecification<DomainClub, ClubDTO>
    {
        private readonly Guid _clubId = clubId;
        public Func<ISelector<DomainClub>, ISelector> Selector => opt => opt.Where(new ClubByIdSpecification(_clubId));
        public override Func<IIncludable<DomainClub>, IIncludable>? Includes => null;
    }
}
