---
title: Data access and schema
label: wayfinder:grilling
status: open
assignee:
blocked_by: [011, 006]
---

# Data access and schema

## Question

How does the app talk to SQLite? Decide: data access approach (EF Core or alternatives), how Title, Copy, Room, Condition and Wishlist map to tables, and how migrations are created and applied.

## Inputs so far

- [Choose the stack](008-choose-the-stack.md): SQLite as a single file, engine fixed by ADR 0001.
- Blocked on [Solution layout and architecture](011-solution-layout-and-architecture.md) (where persistence sits).
- [Book types](006-book-types.md): add Genre (name unique ignoring case, kind Fiction/Non-fiction/Unspecified) and an optional Genre reference from Title; deleting a Genre clears it on its Titles.
