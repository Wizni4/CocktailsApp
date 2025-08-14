using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CocktailsApp.API.Prices
{
    [Route("api/prices")]
    [Tags("Prices")]
    [ApiController]
    [Authorize]
    public class PricingController : ControllerBase
    {
    }
}
