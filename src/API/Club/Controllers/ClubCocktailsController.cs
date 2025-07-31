// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.API.Extensions;
using CocktailsApp.Application.Club;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace CocktailsApp.API.Club
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
            var command = new AddCocktailsCommand(clubId, request, userId);
            var response = _autoMapper.Map<ClubCocktailResponse>(await _mediator.Send(command));
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
            var command = new RemoveCocktailsCommand(clubId, request, userId);
            var response = _autoMapper.Map<ClubResponse>(await _mediator.Send(command));
            return Ok(response);
        }
    }
}
