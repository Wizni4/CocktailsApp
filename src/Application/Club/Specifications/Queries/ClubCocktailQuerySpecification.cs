// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;

using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;


namespace CocktailsApp.Application.Club
{
    public class ClubCocktailQuerySpecification(
        Guid clubId
    ) : ClubQuerySpecification<ClubCocktail, ClubDTO>,
        IChildQuerySpecification<DomainClub, ClubCocktail, ClubCocktailDTO>
    {
        private readonly Guid _clubId = clubId;
        public Func<ISelector<DomainClub>, ISelector<ClubCocktail>> Selector
        {
            get
            {
                return opt => opt.Where(new ClubByIdSpecification(_clubId))
                                 .SelectMany(c => c.Cocktails);
            }
        }

        public override Func<IIncludable<ClubCocktail>, IIncludable>? Includes => null;

    }
}
