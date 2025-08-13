// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using FluentValidation;

using System.Collections.ObjectModel;

namespace CocktailsApp.Application.SeedWork
{
    public abstract class SearchQueryValidator<TSearchQuery, TResult>
        : AbstractValidator<TSearchQuery> where TSearchQuery : SearchQuery<ReadOnlyCollection<TResult>> where TResult : EntityDTO
    {
        public SearchQueryValidator()
        {
            RuleFor(c => c.Term)
                .ValidString();
        }
    }
}
