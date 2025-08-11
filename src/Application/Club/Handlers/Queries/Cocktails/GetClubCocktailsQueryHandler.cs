// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CocktailsApp.Application.SeedWork;


namespace CocktailsApp.Application.Club
{
    public class GetClubCocktailsQueryHandler(
        IClubCocktailQueries clubCocktailQueries
    ) : IQueryHandler<GetClubCocktailsQuery, IEnumerable<ClubCocktailDTO>>
    {
        private readonly IClubCocktailQueries _clubCocktailQueries = clubCocktailQueries;

        public Task<IEnumerable<ClubCocktailDTO>> Handle(GetClubCocktailsQuery request, CancellationToken cancellationToken)
        {
            return _clubCocktailQueries.ReadRangeAsync(
                new ClubCocktailQuerySpecification(request.ClubId));
        }
    }
}
