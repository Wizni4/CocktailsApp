// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.API.SeedWork;
using CocktailsApp.Application.Clubs;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace CocktailsApp.API.Clubs
{
    [Route("api/clubs/{clubId}/cocktails")]
    [Tags("Club - Cocktails")]
    [ApiController]
    [Authorize]
    public class ClubCocktailsController(IMediator mediator, IMapper autoMapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _autoMapper = autoMapper;

        [HttpPost(Name = "AddCocktails")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Add a cocktail to a club.")]
        public async Task<ActionResult<IEnumerable<ClubCocktailResponse>>> AddCocktails(Guid clubId, [FromBody] IEnumerable<Guid> request)
        {
            var userId = this.GetUserId();

            // Command to add a cocktail to the club
            var command = new AddCocktailsCommand(clubId, request, userId);
            var clubCocktailsId = await _mediator.Send(command);

            // Query the added cocktails
            var query = new GetClubCocktailsByIdsQuery(clubId, clubCocktailsId);
            var cocktailDTOs = await _mediator.Send(query);

            // Map DTOs to response
            var response = _autoMapper.Map<IEnumerable<ClubCocktailResponse>>(cocktailDTOs);

            return Created(
                uri: $"/api/clubs/{clubId}/cocktails",
                value: response
            );
        }

        [HttpDelete(Name = "RemoveCocktails")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Remove a cocktail from a club.")]
        public async Task<ActionResult<IEnumerable<ClubCocktailResponse>>> RemoveCocktails(Guid clubId, [FromBody] IEnumerable<Guid> request)
        {
            var userId = this.GetUserId();

            // Command to remove cocktails from a club
            var command = new RemoveCocktailsCommand(clubId, request, userId);
            await _mediator.Send(command);

            // Query to get the list of remaining cocktail within the club
            var query = new GetClubCocktailsQuery(clubId);
            var cocktailsDTO = await _mediator.Send(query);

            // Map DTOs to response
            var response = _autoMapper.Map<IEnumerable<ClubCocktailResponse>>(cocktailsDTO);

            return Ok(response);
        }
    }
}
