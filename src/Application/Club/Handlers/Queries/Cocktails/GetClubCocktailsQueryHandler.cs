// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.SeedWork;

using System.Collections.ObjectModel;


namespace CocktailsApp.Application.Club
{
    public class GetClubCocktailsQueryHandler(
        IClubCocktailReader clubCocktailReader,
        IMapper autoMapper
    ) : IQueryHandler<GetClubCocktailsQuery, ReadOnlyCollection<ClubCocktailDTO>>
    {
        private readonly IClubCocktailReader _clubCocktailReader = clubCocktailReader;
        private readonly IMapper _autoMapper = autoMapper;
        public async Task<ReadOnlyCollection<ClubCocktailDTO>> Handle(GetClubCocktailsQuery request, CancellationToken cancellationToken)
        {
            return (await _clubCocktailReader.ListAsync(
                new ClubCocktailsQuerySpecification(request.ClubId, _autoMapper),
                cancellationToken)).ToList().AsReadOnly();
        }
    }
}
