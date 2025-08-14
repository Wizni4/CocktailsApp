// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.API.Common;
using CocktailsApp.Application.Clubs;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace CocktailsApp.API.Clubs
{
    [Route("api/clubs/{clubId}/cocktails")]
    [Tags("Clubs - Cocktails")]
    [ApiController]
    [Authorize]
    public class ClubCocktailsController(IMediator mediator, IMapper autoMapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _autoMapper = autoMapper;

        [RequireIdempotency]
        [HttpPost(Name = "AddCocktails")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Add a cocktail to a club.")]
        public async Task<ActionResult<ClubMenuResponse>> AddCocktails(Guid clubId, [FromBody] IEnumerable<Guid> request)
        {
            // Command to add a cocktail to the club
            var command = new AddCocktailsCommand(clubId, request);
            await _mediator.Send(command);

            // Get the updated club menu 
            var query = new GetClubMenuQuery(clubId);
            var clubMenu = await _mediator.Send(query);

            // Map DTOs to response
            var response = _autoMapper.Map<ClubMenuResponse>(clubMenu);

            return Created(
                uri: $"/api/clubs/{clubId}/menu",
                value: response
            );
        }

        [RequireIdempotency]
        [HttpDelete(Name = "RemoveCocktails")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Remove a cocktail from a club.")]
        public async Task<ActionResult<ClubMenuResponse>> RemoveCocktails(Guid clubId, [FromBody] IEnumerable<Guid> request)
        {
            // Command to remove cocktails from a club
            var command = new RemoveCocktailsCommand(clubId, request);
            await _mediator.Send(command);

            // Get the updated club menu 
            var query = new GetClubMenuQuery(clubId);
            var clubMenu = await _mediator.Send(query);

            // Map DTOs to response
            var response = _autoMapper.Map<IEnumerable<ClubCocktailResponse>>(clubMenu);

            return Ok(response);
        }
    }
}
