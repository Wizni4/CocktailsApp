// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


using API.Models.Club.Responses;

using AutoMapper;
using AutoMapper.Execution;

using CocktailsApp.API.Extensions;
using CocktailsApp.API.Models;
using CocktailsApp.API.Models.Club;
using CocktailsApp.Application.Club;
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.Shared;
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Domain.UserAggregate;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace CocktailsApp.API.Controllers
{
    [Route("api/clubs")]
    [Tags("Club")]
    [ApiController]
    [Authorize]
    public class ClubController(IMediator mediator, IMapper autoMapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _autoMapper = autoMapper;

        [HttpPost("{clubId}/cocktails",Name = "AddCocktail")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ClubResponse>> AddCocktail(Guid clubId, [FromBody] AddCocktailRequest request)
        {
            var userId = this.GetUserId();
            var command = new AddCocktailCommand(clubId, request.CocktailId, userId);
            var response = _autoMapper.Map<ClubResponse>(await _mediator.Send(command));
            return Created(
                uri: $"/api/clubs/{clubId}/cocktails/{request.CocktailId}",
                value: response
            );
        }

        [HttpPost("{clubId}/members", Name = "AddMember")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ClubResponse>> AddMember(Guid clubId, [FromBody] AddMemberRequest request)
        {
            var userId = this.GetUserId();
            var command = new AddMemberCommand(clubId, request.NewMemberUserId, userId);
            var response = _autoMapper.Map<ClubResponse>(await _mediator.Send(command));
            return Created(
                uri: $"/api/clubs/{clubId}/members/{request.NewMemberUserId}",
                value: response
            );
        }

        [HttpPost("{clubId}/members/{memberId}/permissions", Name = "AddPermissionToMember")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ClubResponse>> AddPermissionToMember(Guid clubId, Guid memberId, [FromBody] AddPermissionToMemberRequest request)
        {
            var userId = this.GetUserId();
            var command = new AddPermissionToMemberCommand(clubId, memberId, (ClubPermission)request.Permission, userId);
            var response = _autoMapper.Map<ClubResponse>(await _mediator.Send(command));
            return Created(
                uri: $"/api/clubs/{clubId}/members/{memberId}/permissions/{request.Permission}",
                value: response
            );
        }

        [HttpPost("{clubId}/roles/{roleId}/permissions", Name = "AddRolePermission")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ClubResponse>> AddRolePermission(Guid clubId, Guid roleId, [FromBody] AddPermissionToMemberRequest request)
        {
            var userId = this.GetUserId();
            var command = new AddRolePermissionCommand(clubId, roleId, (ClubPermission)request.Permission, userId);
            var response = _autoMapper.Map<ClubResponse>(await _mediator.Send(command));
            return Created(
                uri: $"/api/clubs/{clubId}/roles/{roleId}/permissions/{request.Permission}",
                value: response
            );
        }

        [HttpPost("{clubId}/members/{memberId}/roles", Name = "AddRoleToMember")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ClubResponse>> AddRoleToMember(Guid clubId, Guid memberId, [FromBody] AddRoleToMemberRequest request)
        {
            var userId = this.GetUserId();
            var command = new AddRoleToMemberCommand(clubId, memberId, request.RoleId, userId);
            var response = _autoMapper.Map<ClubResponse>(await _mediator.Send(command));
            return Created(
                uri: $"/api/clubs/{clubId}/members/{memberId}/roles/{request.RoleId}",
                value: response
            );
        }

        [HttpPost(Name = "CreateClub")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ClubResponse>> Create([FromBody] CreateClubRequest request)
        {
            var userId = this.GetUserId();
            var command = new CreateClubCommand(
                new AddressDTO()
                {
                    Street = request.Address.Street,
                    StreetNumber = request.Address.StreetNumber,
                    City = request.Address.City,
                    PostalCode = request.Address.PostalCode,
                    State = request.Address.State,
                    Country = request.Address.Country
                },
                request.Description,
                request.Name,
                userId,
                (ClubVisibility)request.Visibility);
            var response = _autoMapper.Map<ClubResponse>(await _mediator.Send(command));
            return Created(
                uri: $"/api/clubs/{response.Id}",
                value: response
            );
        }

        [HttpPost("{clubId}/roles", Name = "CreateRole")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ClubResponse>> CreateRole(Guid clubId, [FromBody] CreateRoleRequest request)
        {
            var userId = this.GetUserId();
            var command = new CreateRoleCommand(clubId, request.Name, userId);
            var response = _autoMapper.Map<ClubResponse>(await _mediator.Send(command));
            return Created(
                uri: $"/api/clubs/{clubId}/roles",
                value: response
            );
        }

        [HttpDelete("{clubId}", Name = "DeleteClub")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteClub(Guid clubId)
        {
            var userId = this.GetUserId();
            var command = new DeleteClubCommand(clubId, userId);
            var response = _autoMapper.Map<ClubResponse>(await _mediator.Send(command));
            return NoContent();
        }

        [HttpDelete("{clubId}/roles/{roleId}", Name = "DeleteRole")]
        public async Task<ActionResult<ClubResponse>> DeleteRole(Guid clubId, Guid roleId)
        {
            var userId = this.GetUserId();
            var command = new DeleteRoleCommand(clubId, roleId, userId);
            var response = _autoMapper.Map<ClubResponse>(await _mediator.Send(command));
            return Ok(response);
        }

        [HttpGet("{clubId}", Name = "GetClubById")]
        public async Task<ActionResult<ClubResponse>> GetClubById(Guid clubId)
        {
            var command = new GetClubByIdQuery(clubId);
            var clubDTO = await _mediator.Send(command);
            var response = _autoMapper.Map<ClubResponse>(clubDTO);
            return Ok(response);
        }

        [HttpDelete("{clubId}/members/{memberId}", Name = "RemoveMember")]
        [SwaggerOperation(Summary = "Removes a member from the specified club")]
        public async Task<ActionResult<ClubResponse>> RemoveMember(Guid clubId, Guid memberId)
        {
            var userId = this.GetUserId();
            var command = new RemoveMemberCommand(clubId, memberId, userId);
            var clubDTO = await _mediator.Send(command);
            var response = _autoMapper.Map<ClubResponse>(clubDTO);
            return Ok(response);
        }
    }
}
