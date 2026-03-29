---
# This file defines workspace-wide Copilot behavior for the OdrzavanjeVozila solution.
# It is intentionally lightweight and focused on repository conventions.

description: |
  Use when working in the OdrzavanjeVozila .NET solution.
  This workspace uses .NET 8.0 with an ASP.NET Core MVC web app (OdrzavanjeVozila)
  and a domain model class library (Vjezba.Model).

# Optional: limit to .NET code files if the engine supports it.
applyTo: "**"

---

## 1) Project overview

- Solution: `OdrzavanjeVozila/OdrzavanjeVozila.sln`
- Web app: `OdrzavanjeVozila/OdrzavanjeVozila.csproj` (ASP.NET Core 8.0, MVC)
- Domain model: `Vjezba.Model/Vjezba.Model.csproj` (class library)
- Key layers currently present: Controllers, Views, Models and enums.

## 2) Standard local commands

- Build all projects:
  `dotnet build`
- Run app:
  `dotnet run --project OdrzavanjeVozila/OdrzavanjeVozila.csproj`
- Run unit tests (not present yet, add `dotnet test` when tests exist)

## 3) Rely on existing patterns

- Keep MVC controller actions in `OdrzavanjeVozila/Controllers`.
- Keep Razor views in `OdrzavanjeVozila/Views`.
- Keep domain entities in `Vjezba.Model/Klase` and enums in `Vjezba.Model/Tools`.
- Use strong typing, nullability conventions, and `HttpGet`/`HttpPost` attributes for MVC routes.

## 4) Common tasks for Copilot

- Add new domain model class with validation attributes
- Create service layer and dependency injection in `Program.cs`
- Create DbContext + Entity Framework migration scaffolding
- Add REST API controllers + DTOs
- Write unit tests for controllers and model logic

## 5) “Link, don’t embed” instruction

When giving documentation advice, prefer linking to official .NET docs:
- https://learn.microsoft.com/dotnet
- https://learn.microsoft.com/aspnet/core

## 6) Feedback TODO

- If user adds tests or CI workflows (GitHub Actions), update this file with test commands and action paths.
