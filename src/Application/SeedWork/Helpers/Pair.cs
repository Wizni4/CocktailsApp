// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace CocktailsApp.Application.SeedWork
{
    public sealed class Pair<TLeft, TRight>
    {
        public TLeft Left { get; init; } = default!;
        public TRight? Right { get; init; }
    }
}
