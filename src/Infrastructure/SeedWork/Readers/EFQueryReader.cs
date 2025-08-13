// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the projecancellationToken root for more information.
using CocktailsApp.Application.SeedWork;

using Microsoft.EntityFrameworkCore;

using System.Collections.ObjectModel;


namespace CocktailsApp.Infrastructure.SeedWork
{
    public class EFQueryReader<TRead, TDTO> : IQueryReader<TRead, TDTO>
        where TRead : ReadEntity
        where TDTO : EntityDTO
    {
        private readonly EFReadDbContext _dbContext;
        private readonly bool _useSplitQueries;

        // Toggle split queries if your read models use multiple tables (not JSON)
        public EFQueryReader(EFReadDbContext dbContext, bool useSplitQueries = false)
        {
            _dbContext = dbContext;
            _useSplitQueries = useSplitQueries;
        }

        public Task<TDTO?> FirstOrDefaultAsync(IQuerySpecification<TRead, TDTO> spec, CancellationToken cancellationToken)
        {
            var baseQ = Base();
            var filtered = spec.Filter(baseQ);
            var shaped = filtered.ApplySorts(spec.Sorts);
            var projecancellationTokened = spec.Select(shaped);

            return projecancellationTokened.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<TDTO>> ListAsync(IQuerySpecification<TRead, TDTO> spec, CancellationToken cancellationToken)
        {
            var baseQ = Base();
            var filtered = spec.Filter(baseQ);
            var shaped = filtered
                .ApplySorts(spec.Sorts)
                .ApplyPaging(spec.Skip, spec.Take);
            var projecancellationTokened = spec.Select(shaped);

            return await Task.FromResult(projecancellationTokened.AsEnumerable());
        }

        public Task<PagedResult<TDTO>> PagedAsync(IQuerySpecification<TRead, TDTO> spec, CancellationToken cancellationToken)
        {
            var baseQ = Base();
            var filtered = spec.Filter(baseQ);

            var total = filtered.CountAsync(cancellationToken);

            var pageQ = filtered
                .ApplySorts(spec.Sorts)
                .ApplyPaging(spec.Skip, spec.Take);

            return spec
                .Select(pageQ)
                .ToListAsync(cancellationToken)
                .ContinueWith(a => new PagedResult<TDTO>(a.Result, total.Result));
        }

        private IQueryable<TRead> Base()
        {
            IQueryable<TRead> q = _dbContext.Set<TRead>().AsNoTracking();

            // Helpful for debugging query origin in SQL traces
            q = q.TagWith($"{nameof(EFQueryReader<TRead, TDTO>)}<{typeof(TRead).Name}>");

            // If your read models are split across multiple tables (not JSON/owned),
            // split queries can avoid cartesian explosions.
            if (_useSplitQueries)
                q = q.AsSplitQuery();

            return q;
        }
    }

}
