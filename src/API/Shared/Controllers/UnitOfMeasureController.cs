// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Domain.IngredientAggregate;
using CocktailsApp.Domain.Shared;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace CocktailsApp.API.Shared.Controllers
{
    [Route("api/shared/units")]
    [Tags("Shared")]
    [ApiController]
    [Authorize]
    public class UnitOfMeasureController : ControllerBase
    {
        [HttpGet(Name = "GetUnitsOfMeasure")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get the list of unit of measure.")]
        public ActionResult<IEnumerable<string>> GetUnitsOfMeasure()
        {
            var unitsOfMeasure = Enum.GetNames(typeof(UnitOfMeasure));
            return Ok(unitsOfMeasure);
        }
    }
}
