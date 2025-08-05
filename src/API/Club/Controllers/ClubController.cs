// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


using AutoMapper;

using CocktailsApp.API.SeedWork;
using CocktailsApp.Application.Club;
using CocktailsApp.Application.Shared;
using CocktailsApp.Domain.ClubAggregate;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace CocktailsApp.API.Club
{
    [Route("api/clubs")]
    [Tags("Club")]
    [ApiController]
    [Authorize]
    public class ClubController(IMediator mediator, IMapper autoMapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _autoMapper = autoMapper;

        [HttpPost(Name = "CreateClub")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Create a new club.")]
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

        [HttpDelete("{clubId}", Name = "DeleteClub")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation(Summary = "Delete a club.")]
        public async Task<IActionResult> DeleteClub(Guid clubId)
        {
            var userId = this.GetUserId();
            var command = new DeleteClubCommand(clubId, userId);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("{clubId}", Name = "GetClubById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Retrieve a club thanks to its Id")]
        public async Task<ActionResult<ClubResponse>> GetClubById(Guid clubId)
        {
            var command = new GetClubByIdQuery(clubId);
            var clubDTO = await _mediator.Send(command);
            var response = _autoMapper.Map<ClubResponse>(clubDTO);
            return Ok(response);
        }

        [HttpPatch("{clubId}", Name = "UpdateClub")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Change the name & description of a club.")]
        public async Task<ActionResult<ClubResponse>> UpdateClub(Guid clubId, [FromBody] UpdateClubRequest request)
        {
            var userId = this.GetUserId();
            var addressDTO = _autoMapper.Map<AddressDTO>(request.Address);
            var visibility = request.Visibility is null
                ? (ClubVisibility?)null
                : request.Visibility.ToEnum<ClubVisibility>();

            var command = new UpdateClubCommand(
                clubId,
                addressDTO,
                request.Name,
                request.Description,
                visibility,
                userId
                );

            var response = _autoMapper.Map<ClubResponse>(await _mediator.Send(command));
            return Ok(response);
        }
    }
}
