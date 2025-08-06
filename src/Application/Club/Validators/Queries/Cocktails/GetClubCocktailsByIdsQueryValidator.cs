// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.SeedWork;

namespace CocktailsApp.Application.Club
{
    public class GetClubCocktailsByIdsQueryValidator : ClubQueryValidator<GetClubCocktailsByIdsQuery>
    {
        public GetClubCocktailsByIdsQueryValidator(IClubRepository clubRepository) : base(clubRepository)
        {
            RuleFor(q => q.CocktailIds)
                .ValidList();
            RuleForEach(q => q.CocktailIds)
                .ValidGuid();
        }
    }
}
