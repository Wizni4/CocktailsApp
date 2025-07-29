// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.API.Extensions;
using CocktailsApp.API.Models.Club;
using CocktailsApp.Application.Club;
using CocktailsApp.Domain.ClubAggregate;
using AutoMapper;
using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers.Club
{
    [Route("api/clubs/{clubId}/roles")]
    [Tags("Club - Roles")]
    [ApiController]
    [Authorize]
    public class ClubRolesController(IMediator mediator, IMapper autoMapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _autoMapper = autoMapper;

        [HttpPost(Name = "CreateRole")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Create a new role for a club.")]
        public async Task<ActionResult<ClubResponse>> CreateRole(Guid clubId, [FromBody] CreateRoleRequest request)
        {
            var userId = this.GetUserId();
            var permissions = request.Permissions?.Select(p => p.ToEnum<ClubPermissionType>());
            var command = new CreateRoleWithOptionsCommand(clubId, request.Name, permissions, userId);
            var response = _autoMapper.Map<ClubResponse>(await _mediator.Send(command));
            return Created(
                uri: $"/api/clubs/{clubId}/roles",
                value: response
            );
        }

        [HttpDelete("{roleId}", Name = "DeleteRole")]
        [SwaggerOperation(Summary = "Delete a role from a club.")]
        public async Task<ActionResult<ClubResponse>> DeleteRole(Guid clubId, Guid roleId)
        {
            var userId = this.GetUserId();
            var command = new DeleteRoleCommand(clubId, roleId, userId);
            var response = _autoMapper.Map<ClubResponse>(await _mediator.Send(command));
            return Ok(response);
        }

        [HttpPost("{roleId}/permissions", Name = "AddPermissionsToRole")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Add permissions to a role of a club.")]
        public async Task<ActionResult<ClubResponse>> AddPermissionsToRole(Guid clubId, Guid roleId, [FromBody] PermissionsRequest request)
        {
            var userId = this.GetUserId();
            var permissions = request.Permissions.Select(p => p.ToEnum<ClubPermissionType>());
            var command = new AddPermissionsToRoleCommand(clubId, roleId, permissions, userId);
            var clubDTO = await _mediator.Send(command);
            var response = _autoMapper.Map<ClubResponse>(clubDTO);
            return Created(
                uri: $"/api/clubs/{clubId}/roles/{roleId}/permissions",
                value: response
            );
        }

        [HttpDelete("{roleId}/permissions", Name = "RemovePermissionsToRole")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Remove permissions from a role of a club.")]

        public async Task<ActionResult<ClubResponse>> AddRolePermission(Guid clubId, Guid roleId, [FromBody] PermissionsRequest request)
        {
            var userId = this.GetUserId();
            var permissions = request.Permissions.Select(p => p.ToEnum<ClubPermissionType>());
            var command = new RemovePermissionsToRoleCommand(clubId, roleId, permissions, userId);
            var response = _autoMapper.Map<ClubResponse>(await _mediator.Send(command));
            return Created(
                uri: $"/api/clubs/{clubId}/roles/{roleId}/permissions",
                value: response
            );
        }
    }
}
