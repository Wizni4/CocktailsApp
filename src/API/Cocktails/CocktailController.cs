// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.API.Common;
using CocktailsApp.Application.Cocktails;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;


namespace CocktailsApp.API.Cocktails
{
    [Route("api/cocktails")]
    [Tags("Cocktails")]
    [ApiController]
    [Authorize]
    public class CocktailController(
        IMapper autoMapper,
        IMediator mediator
    ) : ControllerBase
    {
        private readonly IMapper _autoMapper = autoMapper;
        private readonly IMediator _mediator = mediator;

        [RequireIdempotency]
        [HttpPost(Name = "CreateCocktail")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Create a new cocktail.")]
        public async Task<ActionResult<CocktailListItem>> CreateCocktail([FromBody] CreateCocktailRequest request)
        {
            // Create a new cocktail
            var command = new CreateCocktailCommand(
                request.Name,
                request.Description,
                [.. _autoMapper.Map<IEnumerable<CocktailIngredientModel>>(request.Ingredients)]
            );
            var cocktailId = await _mediator.Send(command);

            // Get the newly created cocktail
            var query = new GetCocktailListItemQuery(cocktailId);
            var cocktailListItem = await _mediator.Send(query);

            // Map DTO to response
            var response = _autoMapper.Map<CocktailListItem>(cocktailListItem);

            return Created(
                uri: $"/api/cocktails/{response.CocktailId}",
                value: response);
        }

        [RequireIdempotency]
        [HttpDelete("{cocktailId}", Name = "DeleteCoktail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation(Summary = "Delete an ingredient.")]
        public async Task<IActionResult> DeleteIngredient(Guid cocktailId)
        {
            var command = new DeleteCocktailCommand(cocktailId);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("{cocktailId}", Name = "GetCocktailListItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Retrieve cocktail info")]
        public async Task<ActionResult<CocktailListItem>> SearchCocktails(Guid cocktailId)
        {
            // Search cocktails
            var query = new GetCocktailListItemQuery(cocktailId);
            var cocktailDTOs = await _mediator.Send(query);

            // Add ingredient to the response
            var response = _autoMapper.Map<CocktailListItem>(cocktailDTOs);

            return Ok(response);
        }

        [HttpGet("{cocktailId}/details", Name = "GetCocktailDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Retrieve cocktail details")]
        public async Task<ActionResult<CocktailDetails>> GetCocktailDetails(Guid cocktailId)
        {
            // Search cocktails
            var query = new GetCocktailDetailsQuery(cocktailId);
            var cocktailDTOs = await _mediator.Send(query);

            // Add ingredient to the response
            var response = _autoMapper.Map<CocktailDetails>(cocktailDTOs);

            return Ok(response);
        }
    }
}
