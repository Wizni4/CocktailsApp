using AutoMapper;

using CocktailsApp.Application.Clubs;
using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Clubs;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

using System.Linq;


namespace CocktailsApp.API.Users
{
    [Route("api/me")]
    [ApiController]
    [Authorize]
    public class UserController(
        IMapper autoMapper,
        IMediator mediator,
        IImageUrlProvider imageUrlProvider
    ) : ControllerBase
    {
        private readonly IMapper _autoMapper = autoMapper;
        private readonly IMediator _mediator = mediator;
        private readonly IImageUrlProvider _imageUrlProvider = imageUrlProvider;

        [HttpGet("clubs", Name = "GetUserClubs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get the user's clubs.")]
        public async Task<ActionResult<UserClubsResponse>> GetUserClubs()
        {
            var query = new GetUserClubsQuery();
            var userClubs = await _mediator.Send(query);

            // Map DTOs to response
            var response = new UserClubsResponse(
                UserId: userClubs!.UserId,
                Clubs: userClubs.Clubs.Select(c => new UserClubItemResponse(
                    ClubId              : c.ClubId,
                    ClubName            : c.ClubName,
                    IsOwner             : c.IsOwner,
                    ImageUrl            : _imageUrlProvider.GetUrl(ImageSubject.Club, c.ImageId, ImageVariant.Small),
                    RoleNames           : c.RoleNames,
                    EffectivePermissions: c.EffectivePermissions
                )).ToList().AsReadOnly()
            );
            return Ok(response);
        }
    }
}
