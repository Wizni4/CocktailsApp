// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.API.SeedWork;
using CocktailsApp.Application.Search;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace CocktailsApp.API.Search
{
    [Route("api/search")]
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
        public async Task<ActionResult<IEnumerable<SearchResponse>>> Search(string term)
        {
            var userId = this.GetUserId();
            var query = new GlobalSearchQuery(term, userId);
            var response = await _mediator.Send(query);
            return Ok(_automapper.Map<IEnumerable<SearchResponse>>(response));
        }
    }
}
