// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;
using CocktailsApp.Application.SeedWork;


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
            throw new NotImplementedException();
        }

        public IQueryable<ClubDTO> Select(IQueryable<ClubRead> filtered)
        {
            throw new NotImplementedException();
        }
    }
}
