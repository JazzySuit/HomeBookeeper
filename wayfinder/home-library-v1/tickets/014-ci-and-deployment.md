---
title: CI and deployment
label: wayfinder:grilling
status: open
assignee:
blocked_by: [012, 013]
---

# CI and deployment

## Question

How does a change get from a merged PR onto the host? Decide: the build and test pipeline (where it runs, what gates a merge); how a release reaches the host-neutral home server (self-contained publish per RID, run as Windows Service or systemd unit, see [Choose hosting](009-choose-hosting.md)), and how the service is updated and restarted; how schema migrations are applied on deploy; and the written restore checklist for the in-app SQLite backups.
