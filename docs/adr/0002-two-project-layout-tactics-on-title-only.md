---
status: accepted
---

# Two-project layout, no application layer, DDD tactics only on the Title aggregate

The solution is `HomeLibrary.Domain` (no package or framework references) and `HomeLibrary.Web` (Razor Pages, persistence, Open Library), plus `HomeLibrary.Domain.Tests`. Domain-Driven Design is applied strategically everywhere (ubiquitous language from `CONTEXT.md`) but tactically only where an invariant needs it: the Title aggregate, which holds its Copies and Wishlist state so the Owned/Wishlist/Gone/last-Copy rules are methods on one object. Razor Page handlers are the use cases; there is no application layer, no MediatR, no DTO mapping. Writes go through the aggregate; reads go through per-area query classes (`TitleQueries` etc.) in Web that return page-shaped read models. Using DDD tactics only where they pay is a deliberate part of the practice the project exists for.

## Considered Options

- **Four-layer Clean Architecture (Domain / Application / Infrastructure / Web)**, as in the old `HomeBookeper` code: rejected because each use case is a few lines of orchestration, so an Application layer would be empty ceremony.
- **Single web project with folders**: rejected because the domain's independence is the one boundary worth enforcing with the compiler, and a second project is the cheapest way to do that.
- **Room and Genre as aggregates with repositories**: rejected; they have no invariant beyond name validation and case-insensitive uniqueness, which the database enforces.
- **Title and Copy as separate aggregates, or a Wishlist aggregate**: rejected; the cross-rules would become coordination code. At about 100 Titles there is no concurrency or size pressure against one aggregate.
- **Domain events**: none in v1, deliberately. Nothing reacts to changes elsewhere.

## Consequences

- Value objects: `Isbn` (checksum, ISBN-10 to ISBN-13) and strongly typed ids (`readonly record struct` over Guid v7, created in the domain). `Condition` is an enum; authors are a validated list inside Title.
- Types are built through static factory methods returning a hand-rolled `Result<T>` for anything a user can trigger, including aggregate rule violations. Exceptions are for programmer errors only.
- ISBN lookup sits behind `IIsbnLookup` in Web (`IsbnLookup/`), with Open Library and fake implementations. It moves to its own project if integrations grow.
- Revisit the no-application-layer choice if the same orchestration appears in two page handlers.
