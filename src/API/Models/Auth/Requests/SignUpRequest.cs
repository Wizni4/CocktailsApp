// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations;

namespace CocktailsApp.API.Models.Authentication
{
    public class SignUpRequest
    {
        [Required]
        public required string Login { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
