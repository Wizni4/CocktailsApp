// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Infrastructure.Shared;

using Microsoft.Extensions.Options;

namespace CocktailsApp.API.SeedWork
{
    public class ImageURLResolver<TSource, TDestination>(
        IOptions<ImageSettings> options
    ) : IValueResolver<TSource, TDestination, string?> where TSource : EntityDTO where TDestination : ImageResponse
    {
        private readonly IOptions<ImageSettings> _options = options;
        public string? Resolve(
            TSource source,
            TDestination destination,
            string? destMember,
            ResolutionContext context
         )
        {
            if (source.ImageId == null)
                return null;

            return $"{_options.Value.PublicBaseUrl}/{source.ImageId}";
        }
    }
}
