using CocktailsApp.Application.Shared;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace CocktailsApp.API.Shared
{
    [Route("api/shared/units")]
    [Tags("Shared")]
    [ApiController]
    [Authorize]
    public class UnitOfMeasureController(
        IMediator mediator
    ) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet(Name = "GetUnitsOfMeasure")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get the list of unit of measure.")]
        public ActionResult<IEnumerable<string>> GetUnitsOfMeasure()
        {
            var query = new GetUnitOfMeasureQuery();
            var response = _mediator.Send(query);
            return Ok(response);
        }
    }
}
