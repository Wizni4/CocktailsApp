// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.API.SeedWork;
using CocktailsApp.Application.Club;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace CocktailsApp.API.Club
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
            var newRoles = _autoMapper.Map<IEnumerable<CreateRoleModel>>(request);
            var command = new CreateRolesCommand(clubId, newRoles, userId);
            var response = _autoMapper.Map<IEnumerable<ClubRoleResponse>>(await _mediator.Send(command));
            return Created(
                uri: $"/api/clubs/{clubId}/roles",
                value: response
            );
        }

        [HttpDelete(Name = "DeleteRoles")]
        [SwaggerOperation(Summary = "Delete roles from a club.")]
        public async Task<ActionResult<IEnumerable<ClubRoleResponse>>> DeleteRole(Guid clubId, [FromBody] IEnumerable<Guid> request)
        {
            var userId = this.GetUserId();
            var command = new DeleteRolesCommand(clubId, request, userId);
            var response = _autoMapper.Map<IEnumerable<ClubRoleResponse>>(await _mediator.Send(command));
            return Ok(response);
        }

        [HttpPatch(Name = "UpdateRoles")]
        [SwaggerOperation(Summary = "Update a role from a club.")]
        public async Task<ActionResult<IEnumerable<ClubRoleResponse>>> UpdateRole(Guid clubId, [FromBody] IEnumerable<UpdateRoleRequest> request)
        {
            var userId = this.GetUserId();
            var updatedRoles = _autoMapper.Map<IEnumerable<UpdateRoleModel>>(request);
            var command = new UpdateRolesCommand(clubId, updatedRoles, userId);
            var response = _autoMapper.Map<IEnumerable<ClubRoleResponse>>(await _mediator.Send(command));
            return Ok(response);
        }

        [HttpPost("permissions", Name = "AddPermissionsToRoles")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Add permissions to roles of a club.")]
        public async Task<ActionResult<IEnumerable<ClubRoleResponse>>> AddPermissionsToRoles(Guid clubId, [FromBody] IEnumerable<RolePermissionsUpdateRequest> request)
        {
            var userId = this.GetUserId();
            var updatedRoles = _autoMapper.Map<IEnumerable<RolePermissionsUpdateModel>>(request);
            var command = new AddPermissionsToRolesCommand(clubId, updatedRoles, userId);
            var clubDTO = await _mediator.Send(command);
            var response = _autoMapper.Map<IEnumerable<ClubRoleResponse>>(clubDTO);
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
            var updatedRoles = _autoMapper.Map<IEnumerable<RolePermissionsUpdateModel>>(request);
            var command = new RemovePermissionsFromRolesCommand(clubId, updatedRoles, userId);
            var response = _autoMapper.Map<IEnumerable<ClubRoleResponse>>(await _mediator.Send(command));
            return Created(
                uri: $"/api/clubs/{clubId}/roles/permissions",
                value: response
            );
        }
    }
}
