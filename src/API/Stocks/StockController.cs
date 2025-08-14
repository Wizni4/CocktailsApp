// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CocktailsApp.API.Stocks
{
    [Route("api/stocks")]
    [Tags("Search")]
    [ApiController]
    [Authorize]
    public class StockController : ControllerBase
    {
    }
}
