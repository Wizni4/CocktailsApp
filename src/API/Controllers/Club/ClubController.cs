// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using API.Models.Club.Requests;

using AutoMapper;
using CocktailsApp.API.Extensions;
using CocktailsApp.API.Models.Club;
using CocktailsApp.API.Models.Shared;
using CocktailsApp.Application.Club;
using CocktailsApp.Application.Shared;
using CocktailsApp.Domain.ClubAggregate;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace CocktailsApp.API.Controllers.Club
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
            var response = _autoMapper.Map<ClubResponse>(await _mediator.Send(command));
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

        [HttpPut("{clubId}/owner", Name = "UpdateOwner")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation(Summary = "Change the owner of a club.")]
        public async Task<ActionResult<ClubResponse>> UpdateOwner(Guid clubId, [FromBody] UpdateOwnerRequest request)
        {
            var userId = this.GetUserId();
            var command = new UpdateOwnerCommand(clubId, request.NewOwnerId, userId);
            var response = _autoMapper.Map<ClubResponse>(await _mediator.Send(command));
            return Ok(response);
        }

        [HttpPut("{clubId}/address", Name = "UpdateAddress")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation(Summary = "Change the address of a club.")]
        public async Task<ActionResult<ClubResponse>> UpdateAddress(Guid clubId, [FromBody] Address request)
        {
            var userId = this.GetUserId();
            var addressDTO = _autoMapper.Map<AddressDTO>(request);
            var command = new UpdateAddressCommand(clubId, addressDTO, userId);
            var response = _autoMapper.Map<ClubResponse>(await _mediator.Send(command));
            return Ok(response);
        }

        [HttpPut("{clubId}", Name = "UpdateClub")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation(Summary = "Change the name & description of a club.")]
        public async Task<ActionResult<ClubResponse>> UpdateClub(Guid clubId, [FromBody] UpdateClubRequest request)
        {
            var userId = this.GetUserId();
            // TODO: MANAGE THAT IN THE APP LAYER
            // Update description
            var descCommand = new UpdateDescriptionCommand(clubId, request.Description, userId);
            var descResponse = await _mediator.Send(descCommand);

            // Update name
            var nameCommand = new UpdateNameCommand(clubId, request.Name, userId);
            var nameResponse = await _mediator.Send(nameCommand);

            var response = _autoMapper.Map<ClubResponse>(nameResponse);
            return Ok(response);
        }

        [HttpPut("{clubId}/visibility", Name = "UpdateVisibility")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation(Summary = "Change the owner of a club.")]
        public async Task<ActionResult<ClubResponse>> UpdateVisibility(Guid clubId, [FromBody] UpdateVisibilityRequest request)
        {
            var userId = this.GetUserId();
            var visibility = request.Visibility.ToEnum<ClubVisibility>();
            var command = new UpdateVisibilityCommand(clubId, visibility, userId);
            var response = _autoMapper.Map<ClubResponse>(await _mediator.Send(command));
            return Ok(response);
        }
    }
}
