---
title: Choose hosting
label: wayfinder:grilling
status: open
assignee:
blocked_by: [008]
---

# Choose hosting

## Question

Where does v1 run (home server vs cloud tier, and which), given the chosen stack, the access model and running-cost limits? Record as an ADR.

## Inputs so far

- 2026-10-17, user leaning: local server or Raspberry Pi rather than cloud. Open: how household members reach it away from home (if at all), HTTPS on the LAN, backups of the SQLite file.
- HTTPS options and trade-offs are in [Barcode scanning in the browser](003-barcode-scanning-in-the-browser.md): Tailscale vs Caddy + real domain lead; Cloudflare Tunnel and self-signed look weaker for a private household app.
- [Household access model](004-household-access-model.md): LAN-only in v1, so a home server with Caddy + real domain (DNS-01 cert) fits better than Tailscale. Backups are a hard requirement: they are the only protection against accidental edits.
