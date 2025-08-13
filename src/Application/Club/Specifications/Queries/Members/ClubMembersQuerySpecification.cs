// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;
using AutoMapper.QueryableExtensions;

using CocktailsApp.Application.SeedWork;


namespace CocktailsApp.Application.Club
{
    public class ClubMembersQuerySpecification(
        Guid clubId,
        IMapper autoMapper
    ) : IQuerySpecification<ClubRead, ClubMemberDTO>
    {
        private readonly Guid _clubId = clubId;
        private readonly IMapper _autoMapper = autoMapper;

        public IQueryable<ClubRead> Filter(IQueryable<ClubRead> query)
        {
            return query.Where(c => c.Id == _clubId);
        }

        public IQueryable<ClubMemberDTO> Select(IQueryable<ClubRead> filtered)
        {
            return filtered
                .SelectMany(c => c.Members)
                .ProjectTo<ClubMemberDTO>(_autoMapper.ConfigurationProvider);
        }
    }
}
