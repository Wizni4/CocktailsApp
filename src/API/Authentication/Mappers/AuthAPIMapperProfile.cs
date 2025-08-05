/// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.Authentication;


namespace CocktailsApp.API.Authentication
{
    public class AuthAPIMapperProfile : Profile
    {
        public AuthAPIMapperProfile()
        {
            // Response
            CreateMap<AuthDTO, SignInResponse>();
        }
    }
}
