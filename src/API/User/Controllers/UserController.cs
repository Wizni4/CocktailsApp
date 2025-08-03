// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using AutoMapper;
using CocktailsApp.API.Club;
using CocktailsApp.API.Extensions;
using CocktailsApp.Application.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


namespace CocktailsApp.API.User
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController(
        IMapper autoMapper,
        IMediator mediator
    ) : ControllerBase
    {
        private readonly IMapper _autoMapper = autoMapper;
        private readonly IMediator _mediator = mediator;
        [HttpGet("me/clubs", Name = "GetUserClubs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get the user's clubs.")]
        public async Task<ActionResult<IEnumerable<ClubResponse>>> GetClubById()
        {
            var userId = this.GetUserId();
            var query = new GetUserClubsQuery(userId);
            var clubDTOs = await _mediator.Send(query);
            var response = _autoMapper.Map<IEnumerable<ClubResponse>>(clubDTOs);
            return Ok(response);
        }
    }
}
