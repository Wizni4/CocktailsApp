using AutoMapper;

using CocktailsApp.API.Common;
using CocktailsApp.Application.Clubs;
using CocktailsApp.Domain.Clubs;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace CocktailsApp.API.Clubs
{
    [Route("api/clubs")]
    [Tags("Clubs")]
    [ApiController]
    [Authorize]
    public class ClubController(IMediator mediator, IMapper autoMapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _autoMapper = autoMapper;

        [RequireIdempotency]
        [HttpPost(Name = "CreateClub")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Create a new club.")]
        public async Task<IActionResult> Create([FromBody] CreateClubRequest request)
        {
            // Create the club using a command
            var command = new CreateClubCommand(
                new Application.Common.Address(
                    request.Address.Street,
                    request.Address.StreetNumber,
                    request.Address.City,
                    request.Address.PostalCode,
                    request.Address.State,
                    request.Address.Country
                ),
                request.Description,
                request.Name,
                request.Visibility == null ? null : request.Visibility.ToEnum<Visibility>()
            ); 
            var clubId = await _mediator.Send(command);

            return Created(
                uri: $"/api/clubs/{clubId}",
                value: clubId
            );
        }

        [RequireIdempotency]
        [HttpDelete("{clubId}", Name = "DeleteClub")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation(Summary = "Delete a club.")]
        public async Task<IActionResult> DeleteClub(Guid clubId)
        {
            var command = new DeleteClubCommand(clubId);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("{clubId}/details", Name = "GetClubDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Retrieve club details.")]
        public async Task<ActionResult<ClubDetailsResponse>> GetClubDetails(Guid clubId)
        {
            var query = new GetClubDetailsQuery(clubId);
            var clubDetails = await _mediator.Send(query);
            var response = _autoMapper.Map<ClubDetailsResponse>(clubDetails);
            return Ok(response);
        }

        [HttpGet("{clubId}/info", Name = "GetClubListItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Retrieve club info")]
        public async Task<ActionResult<ClubListItemResponse>> GetClubListItem(Guid clubId)
        {
            var query = new GetClubListItemQuery(clubId);
            var clubListItem = await _mediator.Send(query);
            var response = _autoMapper.Map<ClubListItemResponse>(clubListItem);
            return Ok(response);
        }

        [HttpGet("{clubId}/menu", Name = "GetClubMenu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Retrieve club menu")]
        public async Task<ActionResult<ClubMenuResponse>> GetClubMenu(Guid clubId)
        {
            var query = new GetClubMenuQuery(clubId);
            var clubMenu = await _mediator.Send(query);
            var response = _autoMapper.Map<ClubMenuResponse>(clubMenu);
            return Ok(response);
        }

        [RequireIdempotency]
        [HttpPatch("{clubId}", Name = "UpdateClub")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Change the address, name, description & visibility of a club.")]
        public async Task<IActionResult> UpdateClub(Guid clubId, [FromBody] UpdateClubRequest request)
        {
            // Prepare the update club command
            var address = _autoMapper.Map<Application.Common.Address>(request.Address);
            var visibility = request.Visibility is null
                ? (Visibility?)null
                : request.Visibility.ToEnum<Visibility>();

            var command = new UpdateClubCommand(
                clubId,
                address,
                request.Name,
                request.Description,
                visibility
            );
            await _mediator.Send(command);

            return Ok();
        }
    }
}
