using AutoMapper;

using CocktailsApp.Application.Clubs;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;


namespace CocktailsApp.API.Users
{
    [Route("api/me")]
    [ApiController]
    [Authorize]
    public class UserController(
        IMapper autoMapper,
        IMediator mediator
    ) : ControllerBase
    {
        private readonly IMapper _autoMapper = autoMapper;
        private readonly IMediator _mediator = mediator;

        [HttpGet("clubs", Name = "GetUserClubs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get the user's clubs.")]
        public async Task<ActionResult<UserClubs>> GetUserClubs()
        {
            var query = new GetUserClubsQuery();
            var userClubs = await _mediator.Send(query);

            // Map DTOs to response
            var response = _autoMapper.Map<UserClubs>(userClubs);
            return Ok(response);
        }
    }
}
