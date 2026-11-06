---
title: Repo practices
label: wayfinder:grilling
status: closed
assignee: jazzysuit
blocked_by: []
---

# Repo practices

## Question

How should the repo be run? Decide: AI agent instruction files (CLAUDE.md / AGENTS.md content and scope), the ADR convention and location, documentation standards, coding standards and formatting, branching and commit conventions, and how wayfinder/tracking material lives alongside code.

## Resolution

Decided 2026-11-07 (grilling with jazzysuit).

- **Agent files**: `AGENTS.md` at the repo root is the single source; `CLAUDE.md` contains only `@AGENTS.md`. Root only, no nested files. Under one screen: purpose plus "use `CONTEXT.md` terms exactly"; build, test and format commands; hard rules one line each, linking their ADR (Domain has no package references, writes go through the Title aggregate, user-triggerable failures return `Result<T>`, no application layer); the workflow rules below; a pointer to `docs/adr/` for the why. Testing guidance is added once [Testing strategy](013-testing-strategy.md) is decided.
- **ADRs**: `docs/adr/NNNN-slug.md` in the existing format (`status` frontmatter, context paragraph, Considered Options, Consequences). An accepted ADR is superseded by a new one, never edited (typo fixes excepted).
- **Documentation**: README (what it is, how to run), `CONTEXT.md` (glossary), ADRs. No XML doc comment requirement. Code comments explain why, never what.
- **Formatting**: rewrite `.editorconfig` lean. `[*]`: tabs, LF, UTF-8, final newline, trim trailing whitespace. C#: Allman braces, file-scoped namespaces. YAML: 2 spaces. Add `.gitattributes` with `* text=auto eol=lf`.
- **Enforcement**: `Nullable` enabled, `TreatWarningsAsErrors`, `EnforceCodeStyleInBuild`, so style and warnings fail the build. `dotnet format --verify-no-changes` is a CI gate; where it runs is decided with CI.
- **Branching**: one slice = one worktree + one branch (plain kebab slug, no type prefix) + one PR, squash-merged into `main`.
- **Commits**: plain imperative subject under about 72 characters, body explains why. Agent-written changes keep the `Co-Authored-By` trailer. No Conventional Commits.
- **Agent git permissions**: commits freely on its own branch; never commits to `main`; pushes and opens a PR only when asked; never merges. The human merges.
- **Model upkeep during implementation**: a new or changed term updates `CONTEXT.md` in the same PR. Work that contradicts an accepted ADR stops and asks; an agreed change of course ships a superseding ADR in the same PR as the code.
- **PR template**: `.github/pull_request_template.md` with three headings: What and why, Linked ADR or ticket, How verified.
- **Wayfinder material**: `wayfinder/` stays in the repo as history after handoff; future maps sit beside this one.
- The files above (`AGENTS.md`, `CLAUDE.md`, `.editorconfig`, `.gitattributes`, PR template, build properties) are written at implementation, not during this map.
- No ADR: all cheap to reverse.
