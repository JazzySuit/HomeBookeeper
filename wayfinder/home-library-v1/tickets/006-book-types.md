---
title: Book types
label: wayfinder:grilling
status: closed
assignee: jazzysuit
blocked_by: []
---

# Book types

## Question

How are books classified in the first release? The old code tried a hierarchy (board book, children's, fiction, non-fiction). Are these one axis or several (e.g. format vs audience vs genre: a children's fiction board book)? Do types belong to the Title or the Copy? Which values are needed for v1, and is the list fixed or user-editable?

## Inputs so far

- [ISBN lookup source](002-isbn-lookup-source.md): no metadata source reliably returns binding/format, so any format axis (e.g. board book) must be set by hand.

## Resolution

Decided 2026-11-04 (grilling with jazzysuit).

- One axis only: **Genre**. No separate format or audience axis; "Baby" is a Genre value like "Sci-fi" or "Science".
- Genre belongs to the **Title**, not the Copy (a Title carries the ISBN, so it is already edition-level). Wishlist Titles take a Genre too.
- A Title has **zero or one** Genre, so counts per Genre are clean and scanning need not stop to classify.
- Each Genre has a **Genre kind**: Fiction, Non-fiction or Unspecified. Fiction/non-fiction of a Title is derived from its Genre.
- The Household owns the list. Genres are created inline from the Title's Genre field, with autocomplete offering existing Genres. Names are unique ignoring case.
- Lifecycle: rename and delete (Titles fall back to no Genre). No merge in v1.
- Seed Genres are left to [First-release feature behaviour](005-first-release-feature-behaviour.md).
- Strong domain types (the old code's motive) are an implementation choice and apply regardless (e.g. a Genre value object).
- No ADR: cheap to reverse.
