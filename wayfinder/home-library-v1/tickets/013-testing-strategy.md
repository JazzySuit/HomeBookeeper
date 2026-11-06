---
title: Testing strategy
label: wayfinder:grilling
status: open
assignee:
blocked_by: [012]
---

# Testing strategy

## Question

What gets tested at which level, and how? Decide: the test projects and their scope (unit tests on the domain, integration tests on Web and persistence, any browser-level tests); how SQLite is used in tests (file, in-memory, per-test isolation); how the `IIsbnLookup` seam and barcode scanning are covered; whether work is test-first; and the testing guidance that goes into `AGENTS.md`.

## Inputs so far

- [Solution layout and architecture](011-solution-layout-and-architecture.md): `HomeLibrary.Domain.Tests` exists in the layout; Title aggregate rules and `Result<T>` factories are the domain's testable core; `IIsbnLookup` has a fake implementation.
- [Repo practices](007-repo-practices.md): `AGENTS.md` has a slot for testing guidance; build fails on warnings and style; "How verified" is a required PR heading.
- [Data access and schema](012-data-access-and-schema.md): blocks this ticket, because the persistence approach decides how integration tests touch SQLite.
