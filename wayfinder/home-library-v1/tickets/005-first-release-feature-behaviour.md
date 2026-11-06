---
title: First-release feature behaviour
label: wayfinder:grilling
status: open
assignee: jazzysuit
blocked_by: [006]
---

# First-release feature behaviour

## Question

How does each first-release feature behave? Covers: adding a Copy (manual, ISBN lookup, barcode scan) and how it relates to an existing Title; the Condition scale; how Rooms are created and managed; how a Wishlist Title becomes owned; what search and filter cover. Blocked on Book types so the Title shape is known.

## Inputs so far

- [ISBN lookup source](002-isbn-lookup-source.md): Open Library (keyless) is the only tested viable source; NZ editions come back incomplete, so lookup pre-fills and the member corrects. Decide here whether Google Books (needs a key) is worth adding as a fallback, and whether covers are stored or hotlinked.
- [Book types](006-book-types.md): Title has zero or one Genre, created inline with autocomplete. Decide here: the seed Genres (or none), filter and count by Genre and by Genre kind, whether "how many science books" counts Titles or Copies, and the Genres page (rename, delete).
