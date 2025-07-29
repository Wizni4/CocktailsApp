// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CocktailsApp.API.Models.Club;
using CocktailsApp.Domain.ClubAggregate;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace CocktailsApp.API.Swagger
{
    public class ClubPermissionSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (typeof(Models.Club.PermissionsRequest).IsAssignableFrom(context.Type))
            {
                var enumNames = Enum.GetNames(typeof(ClubPermissionType));
                schema.Properties["permissions"].Enum = enumNames
                    .Select(name => new OpenApiString(name))
                    .Cast<IOpenApiAny>()
                    .ToList();
            }
        }
    }
}
