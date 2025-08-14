// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.API.Common;

using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace CocktailsApp.API.Swagger
{
    public sealed class IdempotencyHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation op, OperationFilterContext ctx)
        {
            var hasAttr = ctx.MethodInfo.GetCustomAttributes(true).OfType<RequireIdempotencyAttribute>().Any()
                          || ctx.ApiDescription.ActionDescriptor.EndpointMetadata.OfType<RequireIdempotencyAttribute>().Any();
            if (!hasAttr) return;

            op.Parameters ??= new List<OpenApiParameter>();
            op.Parameters.Add(new OpenApiParameter
            {
                Name = IdempotencyKey.HeaderName,
                In = ParameterLocation.Header,
                Required = true,
                Schema = new OpenApiSchema { Type = "string", Format = "uuid" },
                Description = "Client-generated idempotency key reused on retries."
            });
        }
    }
}
