using AutoMapper;

using CocktailsApp.API.Auth;
using CocktailsApp.Application.Auth;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CocktailsApp.API.Identity
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(
        ICookieService cookieService,
        IMediator mediator,
        IMapper autoMapper
    ) : ControllerBase
    {
        private readonly ICookieService _cookieService = cookieService;
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _autoMapper = autoMapper;


        [HttpPost("signup")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            var command = new SignUpCommand(request.Username, request.Email, request.Password);
            await _mediator.Send(command);
            return Created();
        }

        [HttpPost("signin")]
        public async Task<ActionResult<SignInResponse>> SignIn([FromBody] SignInRequest request)
        {
            var command = new SignInCommand(request.Username, request.Password);
            var authTokens = await _mediator.Send(command);

            // Set refreshtoken
            _cookieService.SetRefreshTokenCookie(authTokens.RefreshToken);

            // Map DTOs to response
            var response = _autoMapper.Map<SignInResponse>(authTokens);

            return Ok(response);
        }

        [HttpPost("signout")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize]
        public new async Task<IActionResult> SignOut()
        {
            await _mediator.Send(new SignOutCommand());
            _cookieService.DeleteRefreshTokenCookie();
            return NoContent();
        }

        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<RefreshTokenResponse>> RefreshToken()
        {
            var authTokens = await _mediator.Send(new RefreshTokenCommand());

            // Map DTOs to response
            var response = _autoMapper.Map<RefreshTokenResponse>(authTokens);

            return Ok(response);
        }
    }
}
