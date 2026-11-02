---
title: Stack options cross-analysis
label: wayfinder:research
status: closed
assignee: jazzysuit (research subagent)
blocked_by: []
---

# Stack options cross-analysis

## Question

How do the candidate stacks compare for this app, against the agreed criteria?

Candidates from the user: (a) .NET C# Blazor, (b) SAFE stack (F#, https://safe-stack.github.io/), (c) Go with a Go UI framework (research picks the strongest Go web UI option and says why).
Candidates added: (d) ASP.NET minimal API or Razor Pages with htmx, (e) SvelteKit with SQLite, (f) PocketBase with a light frontend.

Criteria: simplicity (moving parts, number of languages), running cost, hosting effort, mobile and PWA fit (installable, camera access), offline potential later, how well AI coding agents work with it (training-data depth, tooling, typed feedback), learning value, maturity and community health as of 2026.

Output facts and a comparison matrix with sources and confidence. Do not choose: the choice is made in [Choose the stack](008-choose-the-stack.md).

## Resolution

Findings: branch `research/stack-options-cross-analysis`, commit `e63cacc`, file `research/stack-options-cross-analysis.md` (sourced, confidence-tagged; synthesis labelled as analysis).

- Barcode scanning is stack-neutral: native BarcodeDetector is off in iOS Safari and absent in Firefox, so every option needs a JS scanning library.
- No option needs a managed DB: Blazor, ASP.NET + htmx, Go + templ/htmx and PocketBase each run as one process plus SQLite. SvelteKit needs Node; SAFE adds a Node/Vite/Paket build on .NET.
- Blazor: viable variants are Static SSR with interactive islands, or standalone WASM PWA. Server/Auto modes look dominated on phone fit and simplicity.
- Go: templ + htmx is the strongest Go UI option; Go WASM UIs are heavy and immature.
- PocketBase is pre-1.0 (v0.40.x), its docs advise against production-critical use, and it recently shipped breaking changes.
- SAFE scores lowest on simplicity, AI-agent friendliness and maintenance signals (template last released 2025-08, Saturn 2024-04); it leads only on learning novelty.
- Sharpest trade-offs: C# familiarity vs learning something new; server-rendered simplicity now vs offline readiness later; less backend code (PocketBase) vs less room to practise architecture.
- Open questions carried forward: Blazor WASM first-load size on phones, SAFE template's .NET target, scanning library choice, auth scope, hosting, what "offline" would mean.
