// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.API.Extensions;

using Application.Authentication.Commands;
using CocktailsApp.API.Models.Authentication;
using CocktailsApp.Application.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using API.Models.Club.Responses;
using CocktailsApp.API.Models.Auth;

namespace CocktailsApp.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpPost("signup")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            var command = new SignUpCommand(request.Login, request.Password);
            await _mediator.Send(command);
            return Created();
        }

        [HttpPost("signin")]
        public async Task<ActionResult<SignInResponse>> SignIn([FromBody] SignInRequest request)
        {
            var command = new SignInCommand(request.Login, request.Password);
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("signout")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public new async Task<IActionResult> SignOut()
        {
            var userId = this.GetUserId();
            var command = new SignOutCommand(userId);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
