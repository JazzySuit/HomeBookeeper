---
status: accepted
---

# ASP.NET Core Razor Pages + htmx + SQLite on .NET 10

Home Library v1 is a server-rendered ASP.NET Core Razor Pages app on .NET 10 LTS, with htmx 2.x for partial page updates and a single SQLite file for storage. The goal is a working household app built with disciplined AI-assisted engineering and Domain-Driven Design, so we chose a platform the owner can review fluently (C#) and kept the UI layer thin so attention goes to the domain. Toolchain is `dotnet` only: no Node/npm build; htmx, the barcode scanner library and CSS are vendored as static files under `wwwroot`.

## Considered Options

- **Go + templ + htmx**: runner-up and the strongest new-language option. Rejected because learning a new language makes it harder to judge AI output, which is the process being practised.
- **Blazor (Static SSR + islands, or WASM PWA)**: render-mode choices add concepts, and render-mode churn since .NET 8 misleads AI agents. Its offline advantage (WASM PWA) was given no weight since offline is out of scope.
- **Razor Pages vs MVC vs minimal API + Razor components**: Razor Pages, because named page handlers map cleanly onto htmx requests and the app is a handful of pages.
- **htmx 4 vs 2**: 2.x (still patched) because agents have a much deeper corpus for it; upgrading a vendored file later is cheap.
- **SAFE (F#), SvelteKit, PocketBase**: ruled out in the stack cross-analysis (complexity, AI-friendliness, maturity).

## Consequences

- Continuity with the old `HomeBookeper` code and future offline use were deliberately given no weight. Offline later would need a client-side layer added on top.
- Data access (EF Core or otherwise), schema and migrations are decided separately.
