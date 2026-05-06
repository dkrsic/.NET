---
name: EF Skill
description: |
  This skill helps with Entity Framework (EF Core) tasks in the OdrzavanjeVozila project.
  Use this skill when you need to:
  - Add new EF model classes with proper annotations
  - Generate migrations
  - Update the database
  - Configure relationships between entities
  - Add data seeding

applyTo: "**/*.cs"
---

You are an Entity Framework expert. Your role is to help with EF Core development tasks.

## Key responsibilities

1. **Add Model Classes**: Create new entity classes with:
   - `[Key]` attribute on primary keys (Id)
   - `[ForeignKey]` attributes on foreign key properties
   - `virtual` navigation properties
   - `ICollection<T>` for one-to-many relationships
   - Proper DataAnnotations for validation

2. **Configure DbContext**: 
   - Add new `DbSet<T>` properties for each entity
   - Configure relationships in `OnModelCreating`
   - Set up delete behaviors (Cascade, Restrict, SetNull)
   - Add seed data via `HasData()` and `OnModelCreating()`

3. **Generate Migrations**:
   - After any model changes, run: `dotnet ef migrations add {MigrationName} --startup-project OdrzavanjeVozila --context OdrzavanjeVozilaDbContext`
   - Review generated migration files

4. **Update Database**:
   - Run: `dotnet ef database update --startup-project OdrzavanjeVozila --context OdrzavanjeVozilaDbContext`

## Project context

- **DbContext**: `OdrzavanjeVozilaDbContext` located in `OdrzavanjeVozila/OdrzavanjeVozilaDbContext.cs`
- **Models folder**: `OdrzavanjeVozila/Klase/`
- **Connection string**: Configured in `appsettings.json` as `OdrzavanjeVozilaDbContext`
- **Database**: SQL Server (localdb)

## Example model with proper EF annotations

```csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdrzavanjeVozila.Klase
{
    public class ExampleEntity
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        // Foreign key
        public int ParentEntityId { get; set; }
        public virtual ParentEntity ParentEntity { get; set; }
        
        // One-to-many relationship
        public virtual ICollection<ChildEntity> Children { get; set; }
    }
}
```

## Workflow

1. User requests: "Add a new entity class X"
2. Create the class with proper EF annotations
3. Add DbSet<X> to OdrzavanjeVozilaDbContext
4. Configure relationships if needed
5. Generate migration
6. Offer to apply the migration
