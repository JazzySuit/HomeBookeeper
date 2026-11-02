---
title: ISBN lookup source
label: wayfinder:research
status: closed
assignee: jazzysuit (research subagent)
blocked_by: []
---

# ISBN lookup source

## Question

Which book-metadata source should fill in Title details from an ISBN? Compare Open Library, Google Books and any other credible free options on coverage (including NZ/UK/AU editions and children's books), data returned (title, authors, cover, page count, subjects), rate limits, auth requirements, cost and terms of use.

## Resolution

Findings: branch `research/isbn-lookup-source`, commit `6067f0d`, file `research/isbn-lookup-source.md` (sourced, confidence-tagged, live lookups on 8 ISBNs).

- Open Library: no key, found 8 of 8 test ISBNs. NZ editions lacked cover and page count; one record had a wrong title and translators as authors. `/api/books` is marked legacy; `/isbn/{isbn}.json` and Search are current. Limits: 1 req/s anonymous, 3 req/s with User-Agent + email; covers by ISBN 100 per IP per 5 min. Depends on Internet Archive uptime (multi-day outage Oct 2024).
- Google Books: untestable without a key (keyless quota is 0). NZ coverage unknown.
- No source reliably returns binding/format (a board book came back as "Hardcover").
- LoC, Wikidata, DigitalNZ, WorldCat look dominated; no public National Library of NZ ISBN API found; ISBNdb is paid.
- Unknown: whether storing cover images locally is permitted.
