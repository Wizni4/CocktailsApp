// Infrastructure/Read/Projections/ClubProjection.cs
// (adjust namespaces/usings to your project)

using AutoMapper;

using CocktailsApp.Application.Clubs;
using CocktailsApp.Application.Cocktails;
using CocktailsApp.Application.Users;
using CocktailsApp.Domain.Clubs;
using CocktailsApp.Domain.Cocktails;
using CocktailsApp.Infrastructure.SeedWork;

using Microsoft.EntityFrameworkCore;


namespace CocktailsApp.Infrastructure.ClubAggregate
{
    public sealed class ClubProjection : IProjection
    {
        public IProjection Inner { get; }

        public ClubProjection(IMapper mapper)
        {
            Inner = new Projection<ClubRead>(mapper)

                // --- Create root via AutoMapper ---
                .CreateMapOn<ClubCreatedEvent>(e => e.ClubId)

                // --- Scalar updates (same aggregate) via AutoMapper / async when needed ---
                .UpdateMapOn<ClubRenamedEvent>(e => e.ClubId)
                .UpdateMapOn<ClubDescriptionChangedEvent>(e => e.ClubId)
                .UpdateMapOn<ClubVisibilityChangedEvent>(e => e.ClubId)
                .UpdateOnAsync<ClubAddressChangedEvent>(e => e.ClubId, async (c, e, db, ct) =>
                {
                    c.Address = mapper.Map<ClubAddressRead>(e.Address);
                    await Task.CompletedTask;
                })
                .UpdateMapOn<ClubImageUpdatedEvent>(e => e.ClubId)

                // --- Members ---
                .UpdateOnAsync<ClubMemberAddedEvent>(e => e.ClubId, async (c, e, db, ct) =>
                {
                    if (c.Members.Any(m => m.ClubMemberId == e.ClubMemberId)) return;

                    // hydrate username from read-side lookup (fed by User events)
                    var username = await db.Set<UserRead>()
                        .Where(u => u.Id == e.UserId)
                        .Select(u => u.Username)
                        .FirstOrDefaultAsync(ct) ?? string.Empty;

                    var member = mapper.Map<ClubMemberRead>(e);
                    member.Username = username;
                    member.Roles = member.Roles ?? new();
                    c.Members.Add(member);
                })
                .UpdateOnAsync<ClubMemberRemovedEvent>(e => e.ClubId, async (c, e, db, ct) =>
                {
                    c.Members.RemoveAll(m => m.ClubMemberId == e.ClubMemberId);
                    await Task.CompletedTask;
                })

                // --- Roles (club-owned) ---
                .UpdateOnAsync<ClubRoleCreatedEvent>(e => e.ClubId, async (c, e, db, ct) =>
                {
                    if (!c.Roles.Any(r => r.Id == e.RoleId))
                        c.Roles.Add(mapper.Map<ClubRoleRead>(e));
                    await Task.CompletedTask;
                })
                .UpdateOnAsync<ClubRoleRenamedEvent>(e => e.ClubId, async (c, e, db, ct) =>
                {
                    var r = c.Roles.FirstOrDefault(r => r.Id == e.RoleId);
                    if (r != null) mapper.Map(e, r);
                    await Task.CompletedTask;
                })
                .UpdateOnAsync<ClubRoleDeletedEvent>(e => e.ClubId, async (c, e, db, ct) =>
                {
                    c.Roles.RemoveAll(r => r.Id == e.RoleId);
                    foreach (var m in c.Members) m.Roles.RemoveAll(r => r.Id == e.RoleId);
                    await Task.CompletedTask;
                })
                .UpdateOnAsync<ClubRolePermissionAddedEvent>(e => e.ClubId, async (c, e, db, ct) =>
                {
                    var r = c.Roles.FirstOrDefault(r => r.Id == e.RoleId);
                    if (r != null)
                    {
                        var perm = e.Permission.ToString();
                        if (!r.Permissions.Contains(perm)) r.Permissions.Add(perm);
                    }
                    await Task.CompletedTask;
                })
                .UpdateOnAsync<ClubRolePermissionRemovedEvent>(e => e.ClubId, async (c, e, db, ct) =>
                {
                    var r = c.Roles.FirstOrDefault(r => r.Id == e.RoleId);
                    r?.Permissions.Remove(e.Permission.ToString());
                    await Task.CompletedTask;
                })
                .UpdateOnAsync<ClubMemberRoleAddedEvent>(e => e.ClubId, async (c, e, db, ct) =>
                {
                    var idx = c.Members.FindIndex(x => x.ClubMemberId == e.ClubMemberId);
                    if (idx < 0) return;

                    var clubRole = c.Roles.FirstOrDefault(x => x.Id == e.RoleId);
                    if (clubRole is null) return;

                    var m = c.Members[idx];
                    if (m.Roles.Any(x => x.Id == clubRole.Id)) return; // idempotent

                    // copy existing roles + add the new one (use a fresh instance)
                    var newRoles = (m.Roles ?? new()).Select(x => new ClubRoleRead
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Permissions = x.Permissions?.ToList() ?? new List<string>()
                    }).ToList();

                    newRoles.Add(new ClubRoleRead
                    {
                        Id = clubRole.Id,
                        Name = clubRole.Name,
                        Permissions = clubRole.Permissions?.ToList() ?? new List<string>()
                    });

                    // replace the whole member at its index
                    c.Members[idx] = new ClubMemberRead
                    {
                        ClubMemberId = m.ClubMemberId,
                        UserId = m.UserId,
                        Username = m.Username,
                        Roles = newRoles
                    };

                    await Task.CompletedTask;
                })
                .UpdateOnAsync<ClubMemberRoleRemovedEvent>(e => e.ClubId, async (c, e, db, ct) =>
                {
                    var m = c.Members.FirstOrDefault(m => m.ClubMemberId == e.ClubMemberId);
                    m?.Roles.RemoveAll(r => r.Id == e.RoleId);
                    await Task.CompletedTask;
                })

                // --- Cocktails in club (IDs-only; hydrate from lookup) ---
                .UpdateOnAsync<ClubCocktailAddedEvent>(e => e.ClubId, async (c, e, db, ct) =>
                {
                    if (c.Cocktails.Any(k => k.ClubCocktailId == e.ClubCocktailId)) return;

                    var cocktail = mapper.Map<ClubCocktailRead>(e);

                    var info = await db.Set<CocktailRead>()
                        .Where(x => x.Id == e.CocktailId)
                        .Select(x => new { x.Name, x.Description, x.ImageId })
                        .FirstOrDefaultAsync(ct);

                    if (info != null)
                    {
                        cocktail.Name = info.Name;
                        cocktail.Description = info.Description;
                        cocktail.ImageId = info.ImageId;
                    }

                    c.Cocktails.Add(cocktail);
                })
                .UpdateOnAsync<ClubCocktailRemovedEvent>(e => e.ClubId, async (c, e, db, ct) =>
                {
                    c.Cocktails.RemoveAll(k => k.ClubCocktailId == e.ClubCocktailId);
                    await Task.CompletedTask;
                })

                // --- Cross-aggregate patches ---
                //.UpdateManyOnAsync<UserRenamedEvent>(
                //    (db, e) => db.Set<ClubRead>().Where(club => club.Members.Any(m => m.UserId == e.UserId)),
                //    async (club, e, db, ct) =>
                //    {
                //        foreach (var m in club.Members.Where(m => m.UserId == e.UserId))
                //            m.Username = e.Username;
                //        await Task.CompletedTask;
                //    })
                .UpdateManyOnAsync<CocktailRenamedEvent>(
                    (db, e) => db.Set<ClubRead>().Where(club => club.Cocktails.Any(k => k.CocktailId == e.CocktailId)),
                    async (club, e, db, ct) =>
                    {
                        foreach (var k in club.Cocktails.Where(k => k.CocktailId == e.CocktailId))
                            k.Name = e.Name;
                        await Task.CompletedTask;
                    })
                //.UpdateManyOnAsync<CocktailImageUpdatedEvent>(
                //    (db, e) => db.Set<ClubRead>().Where(club => club.Cocktails.Any(k => k.CocktailId == e.CocktailId)),
                //    async (club, e, db, ct) =>
                //    {
                //        foreach (var k in club.Cocktails.Where(k => k.CocktailId == e.CocktailId))
                //            k.ImageId = e.ImageId;
                //        await Task.CompletedTask;
                //    })

                // --- Delete ---
                .DeleteOn<ClubDeletedEvent>(e => e.ClubId);
        }

        public bool CanHandle(Type eventType) => Inner.CanHandle(eventType);

        public Task HandleAsync(object @event, EFReadDbContext db, CancellationToken ct)
            => Inner.HandleAsync(@event, db, ct);
    }
}
