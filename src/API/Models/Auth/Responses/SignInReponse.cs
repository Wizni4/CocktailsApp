// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations;

namespace CocktailsApp.API.Models.Auth
{
    public class SignInResponse
    {
        [Required]
        public required string Token { get; set; }
        [Required]
        public required Guid UserId { get; set; }
    }
}
