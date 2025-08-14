using AutoMapper;

using CocktailsApp.API.Common;
using CocktailsApp.Application.Clubs;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace CocktailsApp.API.Clubs
{
    [Route("api/clubs/{clubId}/roles")]
    [Tags("Clubs - Roles")]
    [ApiController]
    [Authorize]
    public class ClubRolesController(IMediator mediator, IMapper autoMapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _autoMapper = autoMapper;

        [RequireIdempotency]
        [HttpPost(Name = "CreateRoles")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Create new roles for a club.")]
        public async Task<ActionResult<ClubDetails>> CreateRoles(Guid clubId, [FromBody] IEnumerable<CreateRoleRequest> request)
        {
            // Create and add new role to a club
            var newRoles = _autoMapper.Map<IEnumerable<CreateRoleModel>>(request);
            var command = new CreateRolesCommand(clubId, newRoles);
            var roleIds = await _mediator.Send(command);

            // Query club details
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
        [HttpDelete(Name = "DeleteRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Delete roles from a club.")]
        public async Task<ActionResult<ClubDetails>> DeleteRole(Guid clubId, [FromBody] IEnumerable<Guid> request)
        {
            // Remove specified roles from a club
            var command = new DeleteRolesCommand(clubId, request);
            await _mediator.Send(command);

            // Query club details
            var query = new GetClubDetailsQuery(clubId);
            var clubDetails = await _mediator.Send(query);

            // Map DTOs to response
            var response = _autoMapper.Map<ClubDetails>(clubDetails);

            return Ok(response);
        }

        [RequireIdempotency]
        [HttpPatch(Name = "UpdateRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Update a role from a club.")]
        public async Task<ActionResult<ClubDetails>> UpdateRole(Guid clubId, [FromBody] IEnumerable<UpdateRoleRequest> request)
        {
            // Update specified roles
            var updatedRoles = _autoMapper.Map<IEnumerable<UpdateRoleModel>>(request);
            var command = new UpdateRolesCommand(clubId, updatedRoles);
            await _mediator.Send(command);

            // Query club details
            var query = new GetClubDetailsQuery(clubId);
            var clubDetails = await _mediator.Send(query);

            // Map DTOs to response
            var response = _autoMapper.Map<ClubDetails>(clubDetails);

            return Ok(response);
        }

        [RequireIdempotency]
        [HttpPost("permissions", Name = "AddPermissionsToRoles")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Add permissions to roles of a club.")]
        public async Task<ActionResult<ClubDetails>> AddPermissionsToRoles(Guid clubId, [FromBody] IEnumerable<RolePermissionsUpdateRequest> request)
        {
            // Add permissions to specified roles
            var updatedRoles = _autoMapper.Map<IEnumerable<RolePermissionsUpdateModel>>(request);
            var command = new AddPermissionsToRolesCommand(clubId, updatedRoles);
            await _mediator.Send(command);

            // Query club details
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
        [HttpDelete("permissions", Name = "RemovePermissionsToRoles")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Remove permissions from roles of a club.")]

        public async Task<ActionResult<ClubDetails>> RemovePermissionsToRoles(Guid clubId, [FromBody] IEnumerable<RolePermissionsUpdateRequest> request)
        {
            // Remove specified permissions from specified roles
            var updatedRoles = _autoMapper.Map<IEnumerable<RolePermissionsUpdateModel>>(request);
            var command = new RemovePermissionsFromRolesCommand(clubId, updatedRoles);
            await _mediator.Send(command);

            // Query club details
            var query = new GetClubDetailsQuery(clubId);
            var clubDetails = await _mediator.Send(query);

            // Map DTOs to response
            var response = _autoMapper.Map<ClubDetails>(clubDetails);

            return Ok(response);
        }
    }
}
