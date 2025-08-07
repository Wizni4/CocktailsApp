// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.API.SeedWork;
using CocktailsApp.Application.Ingredient;
using CocktailsApp.Domain.IngredientAggregate;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;


namespace CocktailsApp.API.Ingredient
{
    [Route("api/ingredients")]
    [Tags("Ingredient")]
    [ApiController]
    [Authorize]
    public class IngredientController(IMediator mediator, IMapper autoMapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _autoMapper = autoMapper;

        [HttpPost(Name = "CreateIngredient")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Create a new ingredient.")]
        public async Task<ActionResult<IngredientResponse>> CreateIngredient([FromBody] CreateIngredientRequest request)
        {
            var userId = this.GetUserId();
            var command = new CreateIngredientCommand(
                request.Allergens,
                request.Name,
                request.Type.ToEnum<IngredientType>(),
                request.IsAlcoholic,
                userId
            );
            var response = _autoMapper.Map<IngredientResponse>(await _mediator.Send(command));
            return Created(
                uri: $"/api/ingredients/{response.Id}",
                value: response);
        }

        [HttpDelete("{ingredientId}", Name = "DeleteIngredient")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation(Summary = "Delete an ingredient.")]
        public async Task<IActionResult> DeleteIngredient(Guid ingredientId)
        {
            var command = new DeleteIngredientCommand(ingredientId);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet(Name = "GetAllIngredients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get all ingredients.")]
        public async Task<ActionResult<IEnumerable<IngredientResponse>>> GetAllIngredients()
        {
            var query = new GetAllIngredientsQuery();
            var ingredientDTOs = await _mediator.Send(query);

            // Map DTOs to response
            var response = _autoMapper.Map<IEnumerable<IngredientResponse>>(ingredientDTOs);

            return Ok(response);
        }

        [HttpGet("search", Name = "SearchIngredient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Search ingredients.")]
        public async Task<IActionResult> DeleteIngredient(string term)
        {
            var command = new SearchIngredientQuery(term);
            var response = _autoMapper.Map<IEnumerable<IngredientResponse>>(await _mediator.Send(command));
            return Ok(response);
        }
    }
}
