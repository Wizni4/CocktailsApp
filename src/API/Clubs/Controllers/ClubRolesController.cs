// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.API.SeedWork;
using CocktailsApp.Application.Clubs;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace CocktailsApp.API.Clubs
{
    [Route("api/clubs/{clubId}/roles")]
    [Tags("Club - Roles")]
    [ApiController]
    [Authorize]
    public class ClubRolesController(IMediator mediator, IMapper autoMapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _autoMapper = autoMapper;

        [HttpPost(Name = "CreateRoles")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Create new roles for a club.")]
        public async Task<ActionResult<IEnumerable<ClubRoleResponse>>> CreateRoles(Guid clubId, [FromBody] IEnumerable<CreateRoleRequest> request)
        {
            var userId = this.GetUserId();

            // Create and add new role to a club
            var newRoles = _autoMapper.Map<IEnumerable<CreateRoleModel>>(request);
            var command = new CreateRolesCommand(clubId, newRoles, userId);
            var roleIds = await _mediator.Send(command);

            // Query the newly added/created roles
            var query = new GetClubRolesByIdsQuery(clubId, roleIds);
            var roleDTOs = await _mediator.Send(query);

            // Map DTOs to response
            var response = _autoMapper.Map<IEnumerable<ClubRoleResponse>>(roleDTOs);

            return Created(
                uri: $"/api/clubs/{clubId}/roles",
                value: response
            );
        }

        [HttpDelete(Name = "DeleteRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Delete roles from a club.")]
        public async Task<ActionResult<IEnumerable<ClubRoleResponse>>> DeleteRole(Guid clubId, [FromBody] IEnumerable<Guid> request)
        {
            var userId = this.GetUserId();

            // Remove specified roles from a club
            var command = new DeleteRolesCommand(clubId, request, userId);
            await _mediator.Send(command);

            // Query remaining roles
            var query = new GetClubRolesQuery(clubId);
            var clubRoleDTOs = await _mediator.Send(query);

            // Map DTOs to response
            var response = _autoMapper.Map<IEnumerable<ClubRoleResponse>>(clubRoleDTOs);

            return Ok(response);
        }

        [HttpPatch(Name = "UpdateRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Update a role from a club.")]
        public async Task<ActionResult<IEnumerable<ClubRoleResponse>>> UpdateRole(Guid clubId, [FromBody] IEnumerable<UpdateRoleRequest> request)
        {
            var userId = this.GetUserId();

            // Update specified roles
            var updatedRoles = _autoMapper.Map<IEnumerable<UpdateRoleModel>>(request);
            var command = new UpdateRolesCommand(clubId, updatedRoles, userId);
            await _mediator.Send(command);

            // Query updated roles
            var query = new GetClubRolesQuery(clubId);
            var clubRoleDTOs = await _mediator.Send(query);

            // Map DTOs to response
            var response = _autoMapper.Map<IEnumerable<ClubRoleResponse>>(clubRoleDTOs);

            return Ok(response);
        }

        [HttpPost("permissions", Name = "AddPermissionsToRoles")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Add permissions to roles of a club.")]
        public async Task<ActionResult<IEnumerable<ClubRoleResponse>>> AddPermissionsToRoles(Guid clubId, [FromBody] IEnumerable<RolePermissionsUpdateRequest> request)
        {
            var userId = this.GetUserId();

            // Add permissions to specified roles
            var updatedRoles = _autoMapper.Map<IEnumerable<RolePermissionsUpdateModel>>(request);
            var command = new AddPermissionsToRolesCommand(clubId, updatedRoles, userId);
            await _mediator.Send(command);


            // Query updated roles
            var query = new GetClubRolesByIdsQuery(clubId, updatedRoles.Select(r => r.Id));
            var roleDTOs = await _mediator.Send(query);

            // Map DTOs to response
            var response = _autoMapper.Map<IEnumerable<ClubRoleResponse>>(roleDTOs);

            return Created(
                uri: $"/api/clubs/{clubId}/roles/permissions",
                value: response
            );
        }

        [HttpDelete("permissions", Name = "RemovePermissionsToRoles")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Remove permissions from roles of a club.")]

        public async Task<ActionResult<IEnumerable<ClubRoleResponse>>> RemovePermissionsToRoles(Guid clubId, [FromBody] IEnumerable<RolePermissionsUpdateRequest> request)
        {
            var userId = this.GetUserId();

            // Remove specified permissions from specified roles
            var updatedRoles = _autoMapper.Map<IEnumerable<RolePermissionsUpdateModel>>(request);
            var command = new RemovePermissionsFromRolesCommand(clubId, updatedRoles, userId);
            await _mediator.Send(command);

            // Query updated roles
            var query = new GetClubRolesByIdsQuery(clubId, updatedRoles.Select(r => r.Id));
            var roleDTOs = await _mediator.Send(query);

            // Map DTOs to response
            var response = _autoMapper.Map<IEnumerable<ClubRoleResponse>>(roleDTOs);

            return Created(
                uri: $"/api/clubs/{clubId}/roles/permissions",
                value: response
            );
        }
    }
}
