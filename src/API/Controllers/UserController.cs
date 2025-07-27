// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


using CocktailsApp.Application.User;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CocktailsApp.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        [HttpPost("create", Name = "CreateUser")]
        [Tags("User")]
        public async Task<IActionResult> Create()
        {;
            return Ok();
        }
    }
}
