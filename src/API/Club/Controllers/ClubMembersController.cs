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

            // Define the command
            var newMembers = _autoMapper.Map<IEnumerable<AddMemberModel>>(request);
            var command = new AddMembersCommand(
                clubId,
                newMembers,
                userId
            );

            // get the response
            var clubMemberIds = await _mediator.Send(command);

            // Query the newly added members
            var query = new GetClubMembersByIdsQuery(clubId, clubMemberIds);
            var memberDTOs = await _mediator.Send(query);

            // Map DTOs to response
            var response = _autoMapper.Map<IEnumerable<ClubMemberDTO>>(memberDTOs);

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

            // Remove the specified members
            var command = new RemoveMembersCommand(clubId, request, userId);
            await _mediator.Send(command);

            // Query the remaining club members
            var query = new GetClubMembersQuery(clubId);
            var clubMemberDTOs = await _mediator.Send(query);

            // Map DTOs to response
            var response = _autoMapper.Map<IEnumerable<ClubMemberResponse>>(clubMemberDTOs);

            return Ok(response);
        }

        [HttpPost("roles", Name = "AddRolesToMembers")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Add roles to members of a club.")]
        public async Task<ActionResult<IEnumerable<ClubMemberResponse>>> AddRolesToMembers(Guid clubId, [FromBody] IEnumerable<MemberRolesUpdateRequest> request)
        {
            var userId = this.GetUserId();

            // Add specified roles to specified members
            var members = _autoMapper.Map<IEnumerable<MemberRolesUpdateModel>>(request);
            var command = new AddRolesToMembersCommand(clubId, members, userId);
            await _mediator.Send(command);

            // Query updated members
            var query = new GetClubMembersByIdsQuery(clubId, members.Select(m => m.Id));
            var clubMemberDTOs = await _mediator.Send(query);

            // Map DTOs to response
            var response = _autoMapper.Map<IEnumerable<ClubMemberDTO>>(clubMemberDTOs);

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

            // remove specified roles to specified members
            var members = _autoMapper.Map<IEnumerable<MemberRolesUpdateModel>>(request);
            var command = new RemoveRolesFromMembersCommand(clubId, members, userId);
            await _mediator.Send(command);

            // Query updated members
            var query = new GetClubMembersByIdsQuery(clubId, members.Select(m => m.Id));
            var memberDTOs = await _mediator.Send(query);

            // Map DTOs to response
            var response = _autoMapper.Map<IEnumerable<ClubMemberDTO>>(memberDTOs);

            return Ok(response);
        }

        [HttpGet(Name = "GetMembers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get the from members of a club.")]
        public async Task<ActionResult<IEnumerable<ClubMemberResponse>>> GetMembers(Guid clubId)
        {
            // Query the members of the specified club
            var query = new GetClubMembersQuery(clubId);
            var clubMemberDTOs = await _mediator.Send(query);

            // Map DTOs to response
            var response = _autoMapper.Map<IEnumerable<ClubMemberResponse>>(clubMemberDTOs);

            return Ok(response);
        }
    }
}
