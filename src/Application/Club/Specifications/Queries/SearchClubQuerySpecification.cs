// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;

using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;


namespace CocktailsApp.Application.Club
{
    public class SearchClubQuerySpecification(
        string term,
        Guid userId,
        int limit
    ) : ClubQuerySpecification<DomainClub, ClubDTO>,
        IQuerySpecification<DomainClub, ClubDTO>
    {
        private readonly int _limit = limit;
        private readonly string _term = term;
        private readonly Guid _userId = userId;

        public Func<ISelector<DomainClub>, ISelector> Selector
        {
            get
            {
                return opt => opt.Where(new ClubByTermSpecification(_term, _userId))
                                 .Take(_limit);
            }
        }
        public override Func<IIncludable<DomainClub>, IIncludable>? Includes => null;
    }
}
