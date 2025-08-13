// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;
using AutoMapper.QueryableExtensions;

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;

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

        public IQueryable<ClubRead> Filter(IQueryable<ClubRead> query)
        {
            return query.Where(c =>
                c.Name.Contains(_term) &&
                (
                    c.Visibility == ClubVisibility.Public.ToString() ||
                    c.Members.Any(m => m.UserId == _userId)
                ));
        }

        public IQueryable<ClubDTO> Select(IQueryable<ClubRead> filtered)
        {
            return filtered
                .ProjectTo<ClubDTO>(_autoMapper.ConfigurationProvider)
                .Take(_limit);
        }
    }
}
