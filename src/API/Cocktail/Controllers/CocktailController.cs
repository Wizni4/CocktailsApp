// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.API.Ingredient;
using CocktailsApp.API.SeedWork;
using CocktailsApp.Application.Cocktail;
using CocktailsApp.Application.Ingredient;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;


namespace CocktailsApp.API.Cocktail
{
    [Route("api/cocktails")]
    [Tags("Cocktail")]
    [ApiController]
    [Authorize]
    public class CocktailController(
        IMapper autoMapper,
        IMediator mediator
    ) : ControllerBase
    {
        private readonly IMapper _autoMapper = autoMapper;
        private readonly IMediator _mediator = mediator;

        [HttpPost(Name = "CreateCocktail")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Create a new cocktail.")]
        public async Task<ActionResult<CocktailResponse>> CreateCocktail([FromBody] CreateCocktailRequest request)
        {
            var userId = this.GetUserId();

            // Create a new cocktail
            var command = new CreateCocktailCommand(
                request.Description,
                [.. _autoMapper.Map<IEnumerable<Application.Cocktail.IngredientModel>>(request.Ingredients)],
                request.Name,
                userId
            );
            var cocktailId = await _mediator.Send(command);

            // Query the newly added cocktail
            var query = new GetCocktailByIdQuery(cocktailId);
            var cocktailDTO = await _mediator.Send(query);

            // Map DTO to response
            var response = _autoMapper.Map<CocktailResponse>(cocktailDTO);

            return Created(
                uri: $"/api/cocktails/{response.Id}",
                value: response);
        }

        [HttpDelete("{cocktailId}", Name = "DeleteCoktail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation(Summary = "Delete an ingredient.")]
        public async Task<IActionResult> DeleteIngredient(Guid cocktailId)
        {
            var userId = this.GetUserId();
            var command = new DeleteCocktailCommand(cocktailId, userId);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("search", Name = "SearchCocktails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Search a cocktail matching a term.")]
        public async Task<ActionResult<IEnumerable<CocktailResponse>>> SearchCocktails(string term)
        {
            // Search cocktails
            var queryCocktails = new SearchCocktailQuery(term);
            var cocktailDTOs = await _mediator.Send(queryCocktails);

            // Add ingredient to the response
            var response = _autoMapper.Map<IEnumerable<CocktailResponse>>(cocktailDTOs);

            return Ok(response);
        }
    }
}
