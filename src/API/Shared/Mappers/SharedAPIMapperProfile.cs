// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;
using CocktailsApp.Application.Shared;
using CocktailsApp.Application.User;

namespace CocktailsApp.API.Shared
{
    public class SharedAPIMapperProfile : Profile
    {
        public SharedAPIMapperProfile()
        {
            // Shared
            CreateMap<Address, AddressDTO>();
            CreateMap<AddressDTO, Address>();

            
        }
    }
}
