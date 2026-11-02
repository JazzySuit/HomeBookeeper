---
title: Barcode scanning in the browser
label: wayfinder:research
status: closed
assignee: jazzysuit (research subagent)
blocked_by: []
---

# Barcode scanning in the browser

## Question

Which JS barcode-scanning library should a web app/PWA use to read a book's ISBN (EAN-13) with the phone camera? [Stack options cross-analysis](001-stack-options-cross-analysis.md) established that native BarcodeDetector is off in iOS Safari and absent in Firefox, so a library is required. Compare candidates (e.g. ZXing-based) on maintenance status, bundle size, EAN-13 accuracy, and behaviour on current iOS Safari, installed iOS PWAs and Android Chrome, plus permission and HTTPS requirements.

Also: the app may be self-hosted on a home server or Raspberry Pi. What does camera access (secure context) and PWA installation need in that setup, i.e. practical ways to serve HTTPS on a home LAN or via a tunnel/VPN (e.g. Tailscale certs, Caddy with a real domain, self-signed certs) and how each behaves on iOS and Android?

## Resolution

Findings: branch `research/barcode-scanning`, commit `0ec4b7a`, file `research/barcode-scanning.md` (sourced, confidence-tagged).

- Two actively maintained open-source decoder families: ZXing-C++ via WASM (zxing-wasm; Sec-ant `barcode-detector` polyfill) and Quagga2 (1D only). @zxing/library and @zxing/browser are maintenance-mode; html5-qrcode is dominated (no release since 2023-04).
- `barcode-detector` is the only surveyed library that uses native BarcodeDetector where present (Android Chrome) and falls back to WASM elsewhere (iOS Safari, Firefox). Cost: ~417 KB gzipped WASM (self-hostable); the getUserMedia frame loop is hand-written.
- No independent EAN-13 phone benchmark. Only a vendor (Dynamsoft) still-image test: zxing-wasm ~78.5% vs Dynamsoft ~97.3%. Real-device accuracy is unknown.
- Camera needs HTTPS or localhost. Installed iOS home-screen PWAs re-prompt for camera permission (WebKit bug 327237, open). Some iPhone Pro models cannot focus closer than ~15 cm via getUserMedia.
- HTTPS for a home server: Tailscale (free up to 6 users, trusted certs, works away from home, app on every phone) or Caddy + real domain via DNS-01 (~USD 10-20/yr, no phone setup, LAN only by itself). Cloudflare Tunnel exposes the app publicly and decrypts traffic; mkcert/self-signed need a root CA installed on each phone.
