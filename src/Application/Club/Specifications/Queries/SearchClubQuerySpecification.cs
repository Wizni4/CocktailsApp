// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;
using AutoMapper.QueryableExtensions;

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;

using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;


namespace CocktailsApp.Application.Club
{
    public class SearchClubQuerySpecification(
        int limit,
        string term,
        Guid userId,
        IMapper autoMapper
    ) : IQuerySpecification<ClubRead, ClubDTO>
    {
        private readonly int _limit = limit;
        private readonly string _term = term;
        private readonly Guid _userId = userId;
        private readonly IMapper _autoMapper = autoMapper;

        {
            {
                                 .Take(_limit);
            }
        }
        public override Func<IIncludable<DomainClub>, IIncludable>? Includes => null;
    }
}
