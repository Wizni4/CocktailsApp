// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;
using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;


namespace CocktailsApp.Application.Club
{
    public class ClubMemberQuerySpecification(
        Guid clubId
    ) : ClubQuerySpecification<ClubMember, ClubDTO>,
        IChildQuerySpecification<DomainClub, ClubMember, ClubMemberDTO>
    {
        private readonly Guid _clubId = clubId;

        public Func<ISelector<DomainClub>, ISelector<ClubMember>> Selector
        {
            get
            {
                return opt => opt.Where(new ClubByIdSpecification(_clubId))
                                 .SelectMany(c => c.Members);
            }
        }

        public override Func<IIncludable<ClubMember>, IIncludable>? Includes => opt => opt.Include(m => m.Roles);

    }
}
