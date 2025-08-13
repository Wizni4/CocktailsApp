// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;
using AutoMapper.QueryableExtensions;

using CocktailsApp.Application.SeedWork;


namespace CocktailsApp.Application.Club
{
    public class ClubCocktailsByIdsQuerySpecification(
        Guid clubId,
        IEnumerable<Guid> clubCocktailIds,
        IMapper autoMapper
    ) : IQuerySpecification<ClubRead, ClubCocktailDTO>
    {
        private readonly Guid _clubId = clubId;
        private readonly IEnumerable<Guid> _clubCocktailIds = clubCocktailIds;
        private readonly IMapper _autoMapper = autoMapper;

        public IQueryable<ClubRead> Filter(IQueryable<ClubRead> query)
        {
            return query.Where(c => c.Id == _clubId);
        }

        public IQueryable<ClubCocktailDTO> Select(IQueryable<ClubRead> filtered)
        {
            return filtered
               .SelectMany(c => c.Cocktails)
               .Where(c => _clubCocktailIds.Contains(c.ClubCocktailId))
               .ProjectTo<ClubCocktailDTO>(_autoMapper.ConfigurationProvider);
        }
    }
}
