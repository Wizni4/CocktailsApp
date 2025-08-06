// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.SeedWork;

using MediatR;

namespace CocktailsApp.Application.Club
{
    public interface IClubQuery : IBaseRequest
    {
        Guid ClubId { get; }
    }
    public record ClubQuery<TResult>(Guid ClubId) : IQuery<TResult>, IClubQuery;
}
