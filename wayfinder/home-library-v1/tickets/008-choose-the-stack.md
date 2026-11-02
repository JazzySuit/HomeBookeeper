---
title: Choose the stack
label: wayfinder:grilling
status: closed
assignee: jazzysuit
blocked_by: [001, 003, 004]
---

# Choose the stack

## Question

Which stack does v1 use, given the cross-analysis, barcode feasibility and the access model? Record as an ADR.

## Inputs so far

- 2026-10-17, user leaning: shortlist is (a) Blazor, (c) Go + UI, (d) ASP.NET + htmx. SAFE set aside: liked F#/FP and a past POC, but too many moving parts for simple and minimal. Not a decision until this ticket is worked.
- [Household access model](004-household-access-model.md): no login, identity or roles in v1, so built-in auth support is not a criterion.

## Resolution

Decided 2026-11-02 (grilling with jazzysuit). ADR: [0001 ASP.NET Core Razor Pages + htmx + SQLite on .NET 10](../../../docs/adr/0001-aspnet-razor-pages-htmx-sqlite.md).

- Owner's aim: a working app built with AI tooling and engineering discipline, strong DDD, a well laid out solution that is easy to run.
- Learning value means a new way of working on a familiar platform (C#), not a new language: fluency is needed to judge AI output.
- Zero weight: future offline use, continuity with `HomeBookeper`. Decided on paper, no spike.
- Stack: ASP.NET Core Razor Pages, htmx 2.x, .NET 10 LTS, SQLite (engine only).
- `dotnet`-only toolchain: no Node/npm; htmx, scanner library and CSS vendored under `wwwroot`.
- Runner-up / top new-language option: Go + templ + htmx.
- Open tension handed to Solution layout and architecture: heavy DDD tactics vs simple and minimal for a ~100-book, mostly CRUD domain.
