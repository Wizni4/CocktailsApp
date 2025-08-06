// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.SeedWork;

using FluentValidation;


namespace CocktailsApp.Application.Club
{
    public class GetClubByIdQueryValidator : ClubQueryValidator<GetClubByIdQuery>
    {
        public GetClubByIdQueryValidator(IClubRepository clubRepository) : base(clubRepository)
        {
        }
    }
}
