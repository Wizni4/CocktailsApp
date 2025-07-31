// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.API.Extensions;
using CocktailsApp.Application.Authentication;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CocktailsApp.API.Authentication
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IMediator mediator, IMapper autoMapper) : ControllerBase
    {
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
            var response = _autoMapper.Map<SignInResponse>(await _mediator.Send(command));
            return Ok(response);
        }

        [HttpPost("signout")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize]
        public new async Task<IActionResult> SignOut()
        {
            var username = this.GetUsername();
            var command = new SignOutCommand(username);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SignInResponse>> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var command = new RefreshTokenCommand(request.RefreshToken);
            var response = _autoMapper.Map<SignInResponse>(await _mediator.Send(command));
            return Ok(response);
        }
    }
}
