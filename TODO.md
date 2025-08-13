# Refactor CQRS / DDD — TODO

> **Objectif**: clarifier Domain vs Application, séparer read/write, centraliser le mapping, et agréger les erreurs dans les handlers.

## 1) Specs (Domain)
- [x] Déplacer toutes les **specs pures** (prédicats sur entités) dans **`Domain/.../Specifications`**.
- [x] Éliminer de ces specs tout **Include / SelectMany / DTO / AutoMapper**.

## 2) QuerySpecifications (Application)
- [ ] Ecrire les **QuerySpecs** (parcours + includes + mapping) sous **`Application/<Feature>/Specifications/Queries`**.
- [ ] Réutiliser la **spec Domain** interne pour les filtres.

## 3) IQueryService / EFQueryService (read side)
- [ ] Ajouter l’overload **child** (root → `SelectMany`) à **l’interface** `IQueryService`.
- [ ] Aligner l’impl EF (mêmes signatures, **`CancellationToken` partout**).
- [ ] Forcer **`AsNoTracking()`** + `ProjectTo` côté EF.

## 4) AutoMapper
- [ ] Centraliser **`IConfigurationProvider`** via DI (startup).
- [ ] Supprimer tout `new MapperConfiguration(...)` dans specs/handlers.

## 5) Write Repositories / Unit of Work
- [ ] Choisir un style et l’appliquer partout :
  - [ ] **A)** Puriste : **injecter** `I<Aggregate>Repository` dans les handlers et **supprimer** `.Set<T>()`, ou
- [ ] Ajouter **`CancellationToken`** aux méthodes async des repos/UoW.

## 6) Validators (MediatR/FluentValidation)
- [ ] Limiter aux **règles de forme** (sans DB) : required, formats, ranges, doublons, etc.
- [ ] Retirer les **vérifs d’existence DB** (elles seront refaites en lot dans les handlers).

## 7) Handlers (write) — agrégation d’erreurs, throw unique
- [ ] Charger l’agrégat via **spec Domain** + includes d’app si besoin.
- [ ] **Batch-check** des entités externes (ex. `ByIds`) pour éviter N+1.
- [ ] **Collecter toutes les erreurs** (ex. `List<ValidationFailure>`).
- [ ] **Throw une seule fois** (ex. `new ValidationException(failures)`).
- [ ] Appliquer la logique métier sur l’agrégat, puis **commit**.

## 8) Séparation Read vs Write (par feature)
- [ ] **Application** :
  - [ ] `.../Repositories/` = **write** (agrégats).
  - [ ] `.../Queries/Contracts/` = **read** (DTO).
  - [ ] `Shared/Queries/Abstractions/` = `IQueryService` / `IReadRepository<TRead>`.
- [ ] **Infrastructure** :
  - [ ] `.../Persistence/Write/` (EF write repos, UoW).
  - [ ] `.../Persistence/Read/` (EF/Dapper query services).

## 9) Includes d’application (write)
- [ ] Créer de petits **helpers d’includes** par agrégat (ex. `ClubIncludes.Cocktails`, `ClubIncludes.Members`) **ou** des “load specs” d’app.
- [ ] Les utiliser par use-case dans les handlers.

## 10) Exceptions → API
- [ ] Middleware/Behavior pour mapper **`ValidationException` (multi-erreurs)** en **422/400** (format unifié).
- [ ] Mapper `NotFoundException` (si utilisée) en **404**.

## 11) Tests d’intégration (critiques)
- [ ] Pour chaque use-case clé (ex: **AddCocktails**):
  - [ ] **OK**: succès nominal.
  - [ ] **Multi-erreurs**: IDs manquants → assert que **toutes** les erreurs remontent en une réponse.

## 12) Nettoyage arborescence
- [ ] Renommer **`Application/<Feature>/Repositories/Queries/`** → **`Application/<Feature>/Queries/Contracts/`**.
- [ ] Supprimer **`Application/<Feature>/Specifications/Specifications/`** (déplacer en **Domain**).
- [ ] Déplacer impls read vers **`Infrastructure/<Feature>/Read/`** (au lieu de `.../Services/`).

---

### Notes rapides
- **Validators** = forme/UX (sans DB).
- **Handlers** = vérité transactionnelle (lectures en lot + throw unique).
- **Repos write** = agrégats; **Query services** = DTO.
- **Domain** reste pur ; **Application** orchestre ; **Infrastructure** implémente.

