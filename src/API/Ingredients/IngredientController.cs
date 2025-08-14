// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.API.Common;
using CocktailsApp.Application.Ingredients;
using CocktailsApp.Domain.Ingredients;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;


namespace CocktailsApp.API.Ingredients
{
    [Route("api/ingredients")]
    [Tags("Ingredients")]
    [ApiController]
    [Authorize]
    public class IngredientController(
        IMediator mediator,
        IMapper autoMapper
    ) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _autoMapper = autoMapper;

        [RequireIdempotency]
        [HttpPost(Name = "CreateIngredient")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Create a new ingredient.")]
        public async Task<ActionResult<IngredientDetailsResponse>> CreateIngredient([FromBody] CreateIngredientRequest request)
        {
            // Create the ingredient
            var command = new CreateIngredientCommand(
                request.Allergens,
                request.Name,
                request.Type.ToEnum<IngredientType>(),
                request.IsAlcoholic
            );
            var ingredientId = await _mediator.Send(command);

            // Get ingredient details
            var query = new GetIngredientDetailsQuery(ingredientId);
            var ingredientDetails = await _mediator.Send(query);

            // Map DTOs to response
            var response = _autoMapper.Map<IngredientDetailsResponse>(ingredientDetails);

            return Created(
                uri: $"/api/ingredients/{response.IngredientId}",
                value: response);
        }

        [RequireIdempotency]
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
        public async Task<ActionResult<PagedResponse<IngredientDetailsResponse>>> GetAllIngredients(int? limit, int? offset)
        {
            var query = new GetAllIngredientDetailsQuery(limit, offset);
            var pagedIngredients = await _mediator.Send(query);

            // Map DTOs to response
            var response = _autoMapper.Map<PagedResponse<IngredientDetailsResponse>>(pagedIngredients);

            return Ok(response);
        }

        [HttpPost("{ingredientId}/image", Name = "UploadIngredientImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Consumes("multipart/form-data")]
        [SwaggerIgnore]
        public async Task<ActionResult<IngredientDetailsResponse>> UploadIngredientImage(Guid ingredientId, [FromForm] IFormFile image)
        {
            var command = new UploadIngredientImageCommand(
                ingredientId,
                image.OpenReadStream(),
                image.FileName
            );
            await _mediator.Send(command);

            // Query the ingredient
            var query = new GetIngredientDetailsQuery(ingredientId);
            var ingredientDetails = await _mediator.Send(query);

            // Map DTOs to response
            var response = _autoMapper.Map<IngredientDetailsResponse>(ingredientDetails);

            return Ok(response);
        }
    }
}
