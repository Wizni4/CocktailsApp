// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.API.Models;
using CocktailsApp.Application.Club;
using CocktailsApp.Application.Shared;

using Microsoft.AspNetCore.Mvc;

namespace CocktailsApp.API.Controllers
{
    [Route("club")]
    [ApiController]
    public class ClubController(IClubService clubService) : ControllerBase
    {
        private readonly IClubService _clubService = clubService;

        [HttpPost("create", Name = "CreateClub")]
        [Tags("club")]
        public async Task<IActionResult> Create([FromBody] CreateClubRequest request)
        {
            var club = await _clubService.CreateClubAsync(
                new AddressDTO()
                {
                    Street = request.Address.Street,
                    StreetNumber = request.Address.StreetNumber,
                    City = request.Address.City,
                    PostalCode = request.Address.PostalCode,
                    State = request.Address.State,
                    Country = request.Address.Country
                },
                request.Description,
                request.Name,
                request.Visibility,
                request.UserId
                );
            return Ok(club);
        }
    }
}
