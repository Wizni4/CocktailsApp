// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Domain.SeedWork;

using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;


namespace CocktailsApp.Application.Club
{
    public abstract class ClubCommandSpecification(Guid clubId) : ICommandSpecification<DomainClub>
    {
        private readonly Guid _clubId = clubId;
        public virtual ISpecification<DomainClub>? Specification => new ClubByIdSpecification(_clubId);
        public Func<IIncludable<DomainClub>, IIncludable>? Includes 
        {
            get
            {
                return opt => opt.Include(c => c.Members)
                                    .ThenInclude(m => m.Roles)
                                 .Include(c => c.Roles);
            }
        }

    }
}
