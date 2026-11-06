---
title: First-release feature behaviour
label: wayfinder:grilling
status: closed
assignee: jazzysuit
blocked_by: [006]
---

# First-release feature behaviour

## Question

How does each first-release feature behave? Covers: adding a Copy (manual, ISBN lookup, barcode scan) and how it relates to an existing Title; the Condition scale; how Rooms are created and managed; how a Wishlist Title becomes owned; what search and filter cover. Blocked on Book types so the Title shape is known.

## Inputs so far

- [ISBN lookup source](002-isbn-lookup-source.md): Open Library (keyless) is the only tested viable source; NZ editions come back incomplete, so lookup pre-fills and the member corrects. Decide here whether Google Books (needs a key) is worth adding as a fallback, and whether covers are stored or hotlinked.
- [Book types](006-book-types.md): Title has zero or one Genre, created inline with autocomplete. Decide here: the seed Genres (or none), filter and count by Genre and by Genre kind, whether "how many science books" counts Titles or Copies, and the Genres page (rename, delete).

## Resolution

Decided 2026-11-07 (grilling with jazzysuit).

**Titles**
- Fields: title, authors (ordered list of names, plain text, not a managed list), ISBN, Genre, publisher, year, cover. ISBN, Genre, publisher and year are optional.
- A Title is an edition: each format and edition has its own ISBN. ISBNs are stored as ISBN-13; ISBN-10 is converted on entry. An ISBN is unique in the Library when present.
- No-ISBN duplicate check: creating a Title without an ISBN warns if any Title (with or without ISBN) has the same title ignoring case, shows the matches, and offers "add a Copy to this one" or "create anyway".
- Cover hotlinked from Open Library.
- Added and Updated timestamps on each Title.

**Copies**
- Room, Condition (Good / Worn / Damaged) and a free-text note, all optional. Added and Updated timestamps.
- A Copy can be marked **Gone** (no reason, no date) and un-marked. Gone Copies keep "we once owned this" history.
- Real deletion is for mistakes: a Copy can be deleted after confirmation. If that leaves its Title with no Copies and no Wishlist entry, the Title is deleted in the same confirmation.

**Rooms and Genres**
- Same pattern for both: created inline with autocomplete, names unique ignoring case, rename and delete on a list page. Deleting clears the reference. No seed Genres.

**Adding**
- Lookup: Open Library only, by typed ISBN or barcode scan. Scan ships only if [Scanning spike on household phones](010-scanning-spike-on-household-phones.md) passes; typed lookup and manual entry always ship.
- Lookup failure (not found, wrong, Open Library down): manual form with ISBN pre-filled. All looked-up fields editable before saving.
- ISBN matches an existing Title: owned, offer "add another Copy"; on the Wishlist, adding a Copy takes it off; only Gone Copies, adding a Copy makes it owned again.
- Run mode for bulk entry: "save and add another", last Room preselected.

**Wishlist**
- Same lookup flow, ending in "Add to Wishlist". A Title with an owned Copy cannot be on the Wishlist; one with only Gone Copies can.

**Search**
- Text search over title, author and ISBN. Filters: Genre, Genre kind, Room, Condition, owned vs Wishlist, author (case-insensitive substring of any author name).
- Gone Copies hidden unless filtered for; a Title whose Copies are all Gone is not owned.
- Counts show Titles. Sort by title, with "recently added" available.

No ADR: all cheap to reverse.
