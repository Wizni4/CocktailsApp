using CocktailsApp.Application.Ingredients;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;


namespace CocktailsApp.API.Ingredients
{
    [Route("api/ingredients/types")]
    [Tags("Ingredients - Types")]
    [ApiController]
    [Authorize]
    public class IngredientTypeController(
        IMediator mediator
    ) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet(Name = "GetIngredientTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get the list of ingredient type.")]
        public ActionResult<IEnumerable<string>> GetIngredientTypes()
        {
            var query = new GetIngredientTypesQuery();
            var response = _mediator.Send(query);
            return Ok(response);
        }
    }
}
