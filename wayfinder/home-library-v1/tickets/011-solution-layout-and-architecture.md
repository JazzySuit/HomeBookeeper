---
title: Solution layout and architecture
label: wayfinder:grilling
status: closed
assignee: jazzysuit
blocked_by: [008]
---

# Solution layout and architecture

## Question

How is the .NET 10 Razor Pages + htmx solution laid out, and how does DDD show up in it? Decide: projects and folders, layering and dependency direction, which DDD tactical patterns earn their place (aggregates, value objects, repositories, domain events) and which do not, and how it stays easy to run (one `dotnet run`).

## Inputs so far

- [Choose the stack](008-choose-the-stack.md): owner wants strong DDD and a well laid out solution, but the domain is ~100 books and mostly CRUD, so heavy tactics can work against simple and minimal. Resolve that tension here.

## Resolution

Decided 2026-11-07 (grilling with jazzysuit). ADR: [0002 Two-project layout, no application layer, DDD tactics only on the Title aggregate](../../../docs/adr/0002-two-project-layout-tactics-on-title-only.md). Glossary: **Owned** added, **Wishlist** sharpened.

**DDD stance**
- Strategic DDD everywhere; tactical patterns only on the Title/Copy/Wishlist cluster. Seeing where a pattern does not pay is part of the practice.
- Title is the aggregate root: holds its Copies and Wishlist state; cross-rules (Owned vs Wishlist, Gone, deleting the last Copy) are methods on it.
- Room and Genre are plain reference entities; uniqueness enforced by the database.
- No domain events in v1, deliberately.

**Layout**
```
HomeLibrary.slnx                (dotnet new sln defaults to slnx on SDK 10.0.402, checked)
src/HomeLibrary.Domain/         Title (+Copy), Room, Genre, Isbn, ids, Result. No package/framework refs.
src/HomeLibrary.Web/
  Pages/<Area>/                 Titles, Wishlist, Rooms, Genres, Search
  Queries/                      TitleQueries, RoomQueries, GenreQueries
  Persistence/                  per Data access and schema
  IsbnLookup/                   IIsbnLookup, OpenLibraryIsbnLookup, fake
  wwwroot/lib/                  vendored htmx, scanner, CSS
tests/HomeLibrary.Domain.Tests/
```
- `IIsbnLookup` stays in Web; a separate project only if integrations grow.

**Flow**
- No application layer, no MediatR. Page handlers are the use cases: load Title, call method, save, render partial. Revisit if orchestration duplicates across handlers.
- Writes through the aggregate. Reads through per-area query classes in Web (one method per question, criteria record, page-shaped read models). Not "catalogue": glossary avoids it.

**Types**
- Value objects: `Isbn`; strongly typed ids as `readonly record struct` over Guid v7 (`Guid.CreateVersion7()`), created in the domain. `Condition` is an enum; authors a validated list inside Title.
- Static factory methods with validation/parsing, returning a hand-rolled `Result<T>` for anything user-triggerable, aggregate rule violations included. Exceptions for programmer errors only.

**Running**
- Clone, `dotnet run` on Web, empty Library works: SQLite file created and migrated at startup, default path overridable by config, no secrets. `dotnet test` at the root runs all tests.

**Handed on**
- To [Data access and schema](012-data-access-and-schema.md): repository vs DbContext directly; mapping Guid ids, `Isbn`, authors list; Added/Updated timestamps stamped by persistence on save (domain never sees them); how startup migration works.
- To Testing strategy (fog): whether Web gets a test project.
