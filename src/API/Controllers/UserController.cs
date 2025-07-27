// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.User;
using Microsoft.AspNetCore.Mvc;

namespace CocktailsApp.API.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        [HttpPost("create", Name = "CreateUser")]
        [Tags("User")]
        public async Task<IActionResult> Create()
        {
            var user = await _userService.CreateUserAsync();
            return Ok(user);
        }
    }
}
