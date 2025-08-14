using AutoMapper;

using CocktailsApp.API.Common;
using CocktailsApp.Application.Clubs;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;


namespace CocktailsApp.API.Clubs
{
    [Route("api/clubs/{clubId}/members")]
    [Tags("Clubs - Members")]
    [ApiController]
    [Authorize]
    public class ClubMembersController(IMediator mediator, IMapper autoMapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _autoMapper = autoMapper;

        [RequireIdempotency]
        [HttpPost(Name = "AddMembers")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Add new members to a club.")]
        public async Task<ActionResult<ClubDetails>> AddMembers(Guid clubId, [FromBody] IEnumerable<AddMemberRequest> request)
        {
            // Define the command
            var newMembers = _autoMapper.Map<IEnumerable<AddMemberModel>>(request);
            var command = new AddMembersCommand(
                clubId,
                newMembers
            );
            await _mediator.Send(command);

            // Get club details
            var query = new GetClubDetailsQuery(clubId);
            var clubDetails = await _mediator.Send(query);

            // Map DTOs to response
            var response = _autoMapper.Map<ClubDetailsResponse>(clubDetails);

            return Created(
                uri: $"/api/clubs/{clubId}/details",
                value: response
            );
        }

        [RequireIdempotency]
        [HttpDelete(Name = "RemoveMembers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Remove members from a club.")]
        public async Task<ActionResult<ClubDetails>> RemoveMembers(Guid clubId, [FromBody] IEnumerable<Guid> request)
        {
            // Remove the specified members
            var command = new RemoveMembersCommand(clubId, request);
            await _mediator.Send(command);

            // Get club details
            var query = new GetClubDetailsQuery(clubId);
            var clubDetails = await _mediator.Send(query);

            // Map DTOs to response
            var response = _autoMapper.Map<ClubDetails>(clubDetails);

            return Ok(response);
        }

        [RequireIdempotency]
        [HttpPost("roles", Name = "AddRolesToMembers")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Add roles to members of a club.")]
        public async Task<ActionResult<ClubDetails>> AddRolesToMembers(Guid clubId, [FromBody] IEnumerable<MemberRolesUpdateRequest> request)
        {
            // Add specified roles to specified members
            var members = _autoMapper.Map<IEnumerable<MemberRolesUpdateModel>>(request);
            var command = new AddRolesToMembersCommand(clubId, members);
            await _mediator.Send(command);

            // Get club details
            var query = new GetClubDetailsQuery(clubId);
            var clubDetails = await _mediator.Send(query);

            // Map DTOs to response
            var response = _autoMapper.Map<ClubDetails>(clubDetails);

            return Created(
                uri: $"/api/clubs/{clubId}/details",
                value: response
            );
        }

        [RequireIdempotency]
        [HttpDelete("roles", Name = "RemoveRolesFromMember")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Remove roles from members of a club.")]
        public async Task<ActionResult<ClubDetails>> RemoveRolesFromMember(Guid clubId, [FromBody] IEnumerable<MemberRolesUpdateRequest> request)
        {
            // remove specified roles to specified members
            var members = _autoMapper.Map<IEnumerable<MemberRolesUpdateModel>>(request);
            var command = new RemoveRolesFromMembersCommand(clubId, members);
            await _mediator.Send(command);

            // Get club details
            var query = new GetClubDetailsQuery(clubId);
            var clubDetails = await _mediator.Send(query);

            // Map DTOs to response
            var response = _autoMapper.Map<ClubDetails>(clubDetails);

            return Ok(response);
        }
    }
}
