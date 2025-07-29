// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.API.Extensions;
using CocktailsApp.API.Models.Club;
using CocktailsApp.Application.Club;
using CocktailsApp.Domain.ClubAggregate;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers.Club
{
    [Route("api/clubs/{clubId}/members")]
    [Tags("Club - Members")]
    [ApiController]
    [Authorize]
    public class ClubMembersController(IMediator mediator, IMapper autoMapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _autoMapper = autoMapper;

        [HttpPost(Name = "AddMember")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Add a new member to a club.")]
        public async Task<ActionResult<ClubResponse>> AddMember(Guid clubId, [FromBody] AddMemberRequest request)
        {
            var userId = this.GetUserId();
            var permissions = request.Permissions?.Select(p => p.ToEnum<ClubPermissionType>());
            var command = new AddMemberWithOptionsCommand(
                clubId,
                request.NewMemberUserId,
                userId,
                request.RoleIds,
                permissions
            );

            var response = _autoMapper.Map<ClubResponse>(await _mediator.Send(command));
            return Created(
                uri: $"/api/clubs/{clubId}/members",
                value: response
            );
        }

        [HttpDelete("{memberId}", Name = "RemoveMember")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Remove a member from a club.")]
        public async Task<ActionResult<ClubResponse>> RemoveMember(Guid clubId, Guid memberId)
        {
            var userId = this.GetUserId();
            var command = new RemoveMemberCommand(clubId, memberId, userId);
            var response = _autoMapper.Map<ClubResponse>(await _mediator.Send(command));
            return Ok(response);
        }

        [HttpPost("{memberId}/permissions", Name = "AddPermissionsToMember")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Add permissions to a member of a club.")]
        public async Task<ActionResult<ClubResponse>> AddPermissionsToMember(Guid clubId, Guid memberId, [FromBody] PermissionsRequest request)
        {
            var userId = this.GetUserId();
            var permissions = request.Permissions.Select(p => p.ToEnum<ClubPermissionType>());
            var command = new AddPermissionsToMemberCommand(clubId, memberId, permissions, userId);
            var response = _autoMapper.Map<ClubResponse>(await _mediator.Send(command));
            return Created(
                uri: $"/api/clubs/{clubId}/members/{memberId}/permissions",
                value: response
            );
        }

        [HttpDelete("{memberId}/permissions", Name = "RemovePermissionsToMember")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Remove permissions from a member of a club.")]
        public async Task<ActionResult<ClubResponse>> RemovePermissionsToMember(Guid clubId, Guid memberId, [FromBody] PermissionsRequest request)
        {
            var userId = this.GetUserId();
            var permissions = request.Permissions.Select(p => p.ToEnum<ClubPermissionType>());
            var command = new RemovePermissionsToMemberCommand(clubId, memberId, permissions, userId);
            var response = _autoMapper.Map<ClubResponse>(await _mediator.Send(command));
            return Ok(response);
        }

        [HttpPost("{memberId}/roles", Name = "AddRolesToMember")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Add roles to a member of a club.")]
        public async Task<ActionResult<ClubResponse>> AddRolesToMember(Guid clubId, Guid memberId, [FromBody] RolesRequest request)
        {
            var userId = this.GetUserId();
            var command = new AddRolesToMemberCommand(clubId, memberId, request.RoleIds, userId);
            var response = _autoMapper.Map<ClubResponse>(await _mediator.Send(command));
            return Created(
                uri: $"/api/clubs/{clubId}/members/{memberId}/permissions",
                value: response
            );
        }

        [HttpDelete("{memberId}/roles", Name = "RemoveRolesToMember")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Remove roles from a member of a club.")]
        public async Task<ActionResult<ClubResponse>> RemoveRolesToMember(Guid clubId, Guid memberId, [FromBody] RolesRequest request)
        {
            var userId = this.GetUserId();
            var command = new RemoveRolesToMemberCommand(clubId, memberId, request.RoleIds, userId);
            var response = _autoMapper.Map<ClubResponse>(await _mediator.Send(command));
            return Ok(response);
        }
    }
}
