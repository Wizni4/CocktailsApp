// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using FluentValidation;


namespace CocktailsApp.Application.SeedWork
{
    public class CommandValidator<T> : AbstractValidator<T> where T : Command
    {
        public CommandValidator()
        {
            RuleFor(c => c.ActorId).ValidGuid();
        }
    }
}
