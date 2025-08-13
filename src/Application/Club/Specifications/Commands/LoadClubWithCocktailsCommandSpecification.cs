// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.SeedWork;


namespace CocktailsApp.Application.Club
{
    public class LoadClubWithCocktailsCommandSpecification(Guid clubId) : ClubCommandSpecification(clubId)
    {
        public override Func<IIncludable<Domain.ClubAggregate.Club>, IIncludable>? Includes
        {
            get
            {
                return opt =>
                {
                    var query = opt.Include(c => c.Cocktails);
                    return base.Includes!(query);
                };
            }
        }
    }
}
