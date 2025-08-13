// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using AutoMapper;

using CocktailsApp.Application.SeedWork;

using System.Collections.ObjectModel;

namespace CocktailsApp.Application.Club
{
    public class GetClubCocktailsByIdsQueryHandler(
        IClubCocktailReader clubCocktailReader,
        IMapper autoMapper
    ) : IQueryHandler<GetClubCocktailsByIdsQuery, ReadOnlyCollection<ClubCocktailDTO>>
    {
        private readonly IClubCocktailReader _clubCocktailReader = clubCocktailReader;
        private readonly IMapper _autoMapper = autoMapper;
        public async Task<ReadOnlyCollection<ClubCocktailDTO>> Handle(GetClubCocktailsByIdsQuery request, CancellationToken cancellationToken)
        {
            return (await _clubCocktailReader.ListAsync(
                new ClubCocktailsByIdsQuerySpecification(request.ClubId, request.CocktailIds, _autoMapper),
                cancellationToken)).ToList().AsReadOnly();
        }
    }
}
