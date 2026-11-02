---
title: Solution layout and architecture
label: wayfinder:grilling
status: open
assignee:
blocked_by: [008]
---

# Solution layout and architecture

## Question

How is the .NET 10 Razor Pages + htmx solution laid out, and how does DDD show up in it? Decide: projects and folders, layering and dependency direction, which DDD tactical patterns earn their place (aggregates, value objects, repositories, domain events) and which do not, and how it stays easy to run (one `dotnet run`).

## Inputs so far

- [Choose the stack](008-choose-the-stack.md): owner wants strong DDD and a well laid out solution, but the domain is ~100 books and mostly CRUD, so heavy tactics can work against simple and minimal. Resolve that tension here.
