---
title: Household access model
label: wayfinder:grilling
status: closed
assignee: jazzysuit
blocked_by: []
---

# Household access model

## Question

Who uses the app and how do they get in? Do household members (including kids) have individual accounts or share one? Are there different permissions (e.g. kids can view but not edit)? Is authentication needed at all if the app is reachable only on a private network?

## Resolution

Decided 2026-11-02 (grilling with jazzysuit).

- Who: Household members (glossary term), adults and teenagers. Younger children are not expected to use it.
- Reach: home network only in v1. No remote access.
- No login. The home LAN is the gate. Accepted risk: anyone on the Wi-Fi (e.g. guests) can view and edit; backups cover accidental damage.
- No individual identity and no roles. Everyone can do everything; Wishlist, Copies and Rooms belong to the Library, not to a person.
- No preparation for identity or remote access. They return when a later feature needs them (e.g. Tailscale for bookshop Wishlist checks).
- No ADR: cheap to reverse, so it fails the hard-to-reverse test.
