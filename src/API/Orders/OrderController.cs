
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CocktailsApp.API.Orders
{
    [Route("api/orders")]
    [Tags("Orders")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
    }
}
