using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;


namespace CocktailsApp.API.Common
{
    public sealed class IdempotencyKeyFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext ctx, ActionExecutionDelegate next)
        {
            // Run only if the endpoint has [RequireIdempotency]
            var requires = ctx.ActionDescriptor.EndpointMetadata
                .OfType<RequireIdempotencyAttribute>()
                .Any();
            if (!requires)
            {
                await next();
                return;
            }

            if (!ctx.HttpContext.Request.Headers.TryGetValue(IdempotencyKey.HeaderName, out StringValues v)
                || StringValues.IsNullOrEmpty(v))
            {
                ctx.Result = new BadRequestObjectResult($"Missing {IdempotencyKey.HeaderName} header.");
                return;
            }

            var key = v.ToString().Trim();
            ctx.HttpContext.Items[IdempotencyKey.HttpItemKey] = key;

            await next();
        }
    }
}
