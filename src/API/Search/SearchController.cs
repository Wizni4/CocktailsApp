using AutoMapper;

using CocktailsApp.API.Common;
using CocktailsApp.Application.Search;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace CocktailsApp.API.Search
{
    [Route("api/search")]
    [Tags("Search")]
    [ApiController]
    [Authorize]
    public class SearchController(
        IMapper automapper,
        IMediator mediator
    ) : ControllerBase
    {
        private readonly IMapper _automapper = automapper;
        private readonly IMediator _mediator = mediator;

        [HttpGet(Name = "Search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Search the clubs, the cocktails and the users matching a term.")]
        public async Task<ActionResult<PagedResponse<SearchItemResponse>>> Search(string term, int? limit, int? offset)
        {
            var query = new SearchQuery(term, limit, offset);
            var pagedResult = await _mediator.Send(query);

            // Map DTOs to response
            var response = _automapper.Map<PagedResponse<SearchItemResponse>>(pagedResult);

            return Ok(response);
        }
    }
}
