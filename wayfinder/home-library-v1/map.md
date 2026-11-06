---
label: wayfinder:map
status: open
created: 2026-10-17
---

# Home Library v1

## Destination

A first-release feature spec for a household home library web app (PWA, desktop and mobile), plus the chosen tech stack, solution layout, architecture and repo practices, recorded as decisions and ADRs. Everything decided, nothing built: ready to hand off to an implementation plan.

## Notes

- Domain: one household, one shared Library, a handful of known users. About 100 books, no existing data to import.
- Weighting is 1:2 (solving the problem : practising a disciplined AI-assisted process). Learning value and AI-friendliness count in stack choices.
- Simple and minimal is a standing preference.
- Existing code in `HomeBookeper/` is reference only (hints for domain language). It is removed at implementation time, not during this map.
- Grilling sessions use `/grilling` and `/domain-modeling`, and keep `CONTEXT.md` (glossary) and `docs/adr/` current.
- Tracker: local markdown. Tickets live in `tickets/`, frontmatter holds `type`, `status` (open | closed), `assignee` (the claim) and `blocked_by`. Frontier = open, unassigned tickets whose `blocked_by` are all closed.
- Research tickets are resolved by a subagent on a throwaway `research/<name>` branch; the ticket links the findings.

## Decisions so far

<!-- one line per closed ticket: [title](tickets/NNN-slug.md) — gist -->

- [Stack options cross-analysis](tickets/001-stack-options-cross-analysis.md): facts only. Viable field narrows to Blazor (Static SSR or WASM PWA), ASP.NET + htmx, Go + templ/htmx, SvelteKit; SAFE and PocketBase score weakest; scanning is stack-neutral.
- [Barcode scanning in the browser](tickets/003-barcode-scanning-in-the-browser.md): facts only. `barcode-detector` (native API with ZXing-WASM fallback) is the leading library; real-phone accuracy unknown; HTTPS is mandatory, with Tailscale or Caddy + domain as the home-server routes.
- [ISBN lookup source](tickets/002-isbn-lookup-source.md): facts only. Open Library (no key) found all test ISBNs but NZ editions are incomplete; Google Books needs a key and was untested; no source gives binding/format.
- [Household access model](tickets/004-household-access-model.md): LAN-only, no login, no identity, no roles; all Household members (adults and teens) can do everything.
- [Choose the stack](tickets/008-choose-the-stack.md): ASP.NET Core Razor Pages + htmx 2.x on .NET 10 LTS, SQLite, `dotnet`-only toolchain (ADR 0001); runner-up Go + templ + htmx.
- [Book types](tickets/006-book-types.md): one axis, Genre, on the Title (zero or one), with a Fiction/Non-fiction/Unspecified kind; household-editable list via inline create with autocomplete; rename and delete, no merge.
- [First-release feature behaviour](tickets/005-first-release-feature-behaviour.md): Title is an edition (ISBN-13, optional, unique; title-match warning when absent), authors as a plain ordered list; Copies have Room, Condition (Good/Worn/Damaged), note and can be Gone; Open Library lookup with manual fallback and run mode; search by text, Genre, Room, Condition, author, owned/Wishlist, counting Titles.

## Not yet specified

- Testing strategy: what gets tested at which level.
- CI and deployment: build, test and release pipeline, tied to hosting.

## Out of scope

- Loans, reading status, series and collections, kids' reading levels, import/export: later releases.
- Offline use: online-only for the first release.
- Location finer than Room (shelves).
- Multiple households / multi-tenancy.
- Remote access, login, per-member identity and roles: not needed in v1 (see Household access model).
- Format and audience axes, multiple Genres per Title, and Genre merge: not needed in v1 (see Book types).
- The implementation itself: this map ends at the handoff.
