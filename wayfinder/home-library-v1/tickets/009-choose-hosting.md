---
title: Choose hosting
label: wayfinder:grilling
status: closed
assignee: jazzysuit
blocked_by: [008]
---

# Choose hosting

## Question

Where does v1 run (home server vs cloud tier, and which), given the chosen stack, the access model and running-cost limits? Record as an ADR.

## Inputs so far

- 2026-10-17, user leaning: local server or Raspberry Pi rather than cloud. Open: how household members reach it away from home (if at all), HTTPS on the LAN, backups of the SQLite file.
- HTTPS options and trade-offs are in [Barcode scanning in the browser](003-barcode-scanning-in-the-browser.md): Tailscale vs Caddy + real domain lead; Cloudflare Tunnel and self-signed look weaker for a private household app.
- [Household access model](004-household-access-model.md): LAN-only in v1, so a home server with Caddy + real domain (DNS-01 cert) fits better than Tailscale. Backups are a hard requirement: they are the only protection against accidental edits.
- [Choose the stack](008-choose-the-stack.md): .NET 10 Razor Pages + SQLite, `dotnet`-only toolchain. Can ship as a self-contained publish (linux-arm64 for a Pi, unverified on the specific hardware) or a container, plus one SQLite file to back up.

## Resolution

Decided 2026-11-07 (grilling with jazzysuit). Home server over cloud for cost and to avoid extra security setup. Zero budget.

- Host: host-neutral. First host is an existing machine (Windows now; Linux desktop or Raspberry Pi possible later). Not always on: the Library is reachable only while the host is on. Downtime of days is acceptable.
- Packaging: self-contained `dotnet publish` (`win-x64`, `linux-x64` or `linux-arm64`), run as a Windows Service or systemd unit. No Docker. Moving hosts = copy the publish output and the SQLite file. .NET 10 on arm64 is unverified on any specific Pi; test there first.
- Address: DHCP reservation on the router; Household members bookmark `http://<ip>:<port>`. No mDNS.
- HTTPS: none at first (plain HTTP on the LAN). If [Scanning spike on household phones](010-scanning-spike-on-household-phones.md) keeps barcode scanning, add a free DuckDNS subdomain pointing at the LAN IP plus Caddy (DuckDNS DNS plugin, unverified) with a free Let's Encrypt DNS-01 certificate. No per-phone setup, no open ports, zero cost. Router DNS-rebind protection may need a local override.
- Backups: in-app background job. On startup and every 24 hours, if the last backup is older than 24 hours, write a consistent SQLite snapshot (SQLite backup / `VACUUM INTO`, never a raw file copy) to a configured folder (a USB drive), prune snapshots older than 8 weeks, and log loudly when the folder is missing. One test restore before v1 is called done. Accepted risk: the USB drive sits beside the host (theft or fire). OneDrive is a later step, not v1.
- No ADR: cheap to reverse.
