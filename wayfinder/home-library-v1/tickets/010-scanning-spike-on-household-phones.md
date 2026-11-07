---
title: Scanning spike on household phones
label: wayfinder:prototype
status: open
assignee:
blocked_by: []
---

# Scanning spike on household phones

## Question

Does browser-based ISBN scanning work well enough on the household's actual phones to keep barcode scan in the first release? Build a throwaway single-page scanner (e.g. `barcode-detector` polyfill, served over HTTPS) and try it on real books across each phone, including an installed home-screen PWA on iOS. Judge read rate, speed, close-focus problems, and how annoying the iOS permission re-prompt is. See [Barcode scanning in the browser](003-barcode-scanning-in-the-browser.md).

## Inputs so far

- [Choose hosting](009-choose-hosting.md): production starts on plain HTTP. The spike still needs HTTPS on the phones (camera access), so it needs a temporary setup, e.g. a short-lived DuckDNS + Caddy or a tunnel. Its outcome decides whether production adds DuckDNS + Caddy HTTPS.
