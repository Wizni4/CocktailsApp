/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

using MediatR;

using Microsoft.EntityFrameworkCore;
/*
* Framework namespaces
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailsApp.Infrastructure.SeedWork
{
    public class DomainEventDispatcher(IMediator mediator)
    {
        private readonly IMediator _mediator = mediator;
        public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }
        }
    }
}
