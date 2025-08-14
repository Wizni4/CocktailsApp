using CocktailsApp.Domain.Ingredients;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace CocktailsApp.API.Ingredient
{
    [Route("api/ingredients/types")]
    [Tags("Ingredient - Type")]
    [ApiController]
    [Authorize]
    public class IngredientTypeController : ControllerBase
    {
        [HttpGet(Name = "GetIngredientTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get the list of ingredient type.")]
        public ActionResult<IEnumerable<string>> GetIngredientTypes()
        {
            var ingredientTypes = Enum.GetNames(typeof(IngredientType));
            return Ok(ingredientTypes);
        }
    }
}
