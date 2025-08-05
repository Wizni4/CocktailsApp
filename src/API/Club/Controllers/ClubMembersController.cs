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
    [Route("api/clubs/{clubId}/members")]
    [Tags("Club - Members")]
    [ApiController]
    [Authorize]
    public class ClubMembersController(IMediator mediator, IMapper autoMapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _autoMapper = autoMapper;

        [HttpPost(Name = "AddMembers")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Add new members to a club.")]
        public async Task<ActionResult<IEnumerable<ClubMemberResponse>>> AddMembers(Guid clubId, [FromBody] IEnumerable<AddMemberRequest> request)
        {
            var userId = this.GetUserId();
            var newMembers = _autoMapper.Map<IEnumerable<AddMemberModel>>(request);

            // Define the command
            var command = new AddMembersCommand(
                clubId,
                newMembers,
                userId
            );

            // get the response
            var response = _autoMapper.Map<IEnumerable<ClubMemberResponse>>(await _mediator.Send(command));

            // Return the updated club
            return Created(
                uri: $"/api/clubs/{clubId}/members",
                value: response
            );
        }

        [HttpDelete(Name = "RemoveMembers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Remove members from a club.")]
        public async Task<ActionResult<IEnumerable<ClubMemberResponse>>> RemoveMembers(Guid clubId, [FromBody] IEnumerable<Guid> request)
        {
            var userId = this.GetUserId();
            var command = new RemoveMembersCommand(clubId, request, userId);
            var response = _autoMapper.Map<IEnumerable<ClubMemberResponse>>(await _mediator.Send(command));
            return Ok(response);
        }

        [HttpPost("roles", Name = "AddRolesToMembers")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Add roles to members of a club.")]
        public async Task<ActionResult<IEnumerable<ClubMemberResponse>>> AddRolesToMembers(Guid clubId, [FromBody] IEnumerable<MemberRolesUpdateRequest> request)
        {
            var userId = this.GetUserId();
            var members = _autoMapper.Map<IEnumerable<MemberRolesUpdateModel>>(request);
            var command = new AddRolesToMembersCommand(clubId, members, userId);
            var response = _autoMapper.Map<IEnumerable<ClubMemberResponse>>(await _mediator.Send(command));
            return Created(
                uri: $"/api/clubs/{clubId}/members/roles",
                value: response
            );
        }

        [HttpDelete("roles", Name = "RemoveRolesFromMember")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Remove roles from members of a club.")]
        public async Task<ActionResult<IEnumerable<ClubMemberResponse>>> RemoveRolesFromMember(Guid clubId, [FromBody] IEnumerable<MemberRolesUpdateRequest> request)
        {
            var userId = this.GetUserId();
            var members = _autoMapper.Map<IEnumerable<MemberRolesUpdateModel>>(request);
            var command = new RemoveRolesFromMembersCommand(clubId, members, userId);
            var response = _autoMapper.Map<IEnumerable<ClubMemberResponse>>(await _mediator.Send(command));
            return Ok(response);
        }
    }
}
