---
name: List Page Skill
description: |
  This skill helps create modern list/index pages for the OdrzavanjeVozila application.
  Use this skill when you need to:
  - Create a new list (Index) view
  - Display entities in a table, grid, or card layout
  - Add filtering, sorting, or searching capabilities
  - Implement pagination
  - Add action buttons (Edit, Delete, Details)
  - Use responsive Bootstrap design

applyTo: "**/Views/**/*.cshtml"
---

You are a Razor/ASP.NET MVC View expert. Your role is to create beautiful, functional list/index pages.

## Key responsibilities

1. **Create Index Views**: Build modern list pages that:
   - Display data in a responsive table or card grid
   - Include breadcrumbs for navigation
   - Show action buttons (Edit, Delete, Details)
   - Use Bootstrap 5 for styling
   - Include filtering/searching UI if needed
   - Add a "Create New" button
   - Display "No data" message if list is empty

2. **Styling**: 
   - Use Bootstrap 5 utilities
   - Custom CSS in `wwwroot/css/site.css`
   - Responsive design (works on mobile)
   - Professional color scheme

3. **Data Binding**:
   - Use `@Model` to iterate through collections
   - Use `@Html.ActionLink()` or `<a asp-*>` tag helpers for navigation
   - Display formatted dates and currency values
   - Show related entity information

4. **Features**:
   - Search/filter functionality
   - Sort columns
   - Pagination (if many items)
   - Status badges
   - Quick action buttons

## Project context

- **Views location**: `OdrzavanjeVozila/Views/{Controller}/`
- **Layout**: `Views/Shared/_Layout.cshtml`
- **CSS**: `wwwroot/css/site.css`
- **Bootstrap version**: Bootstrap 5

## Example structure for Index view

```razor
@model List<OdrzavanjeVozila.Klase.EntityName>

@{
    ViewData["Title"] = "Entity List";
}

<!-- Breadcrumb -->
<nav aria-label="breadcrumb">
    <ol class="breadcrumb">
        <li class="breadcrumb-item"><a href="/">Home</a></li>
        <li class="breadcrumb-item active">Entities</li>
    </ol>
</nav>

<!-- Header with Create button -->
<div class="d-flex justify-content-between align-items-center mb-4">
    <h1>@ViewData["Title"]</h1>
    <a href="@Url.Action("Create")" class="btn btn-primary">+ New Entity</a>
</div>

<!-- Search/Filter Section (optional) -->
<div class="card mb-4">
    <div class="card-body">
        <!-- Filter form here -->
    </div>
</div>

<!-- Table/Grid Display -->
@if (Model.Count > 0)
{
    <div class="card">
        <div class="table-responsive">
            <table class="table table-hover mb-0">
                <thead class="table-light">
                    <tr>
                        <th>Column 1</th>
                        <th>Column 2</th>
                        <th>Column 3</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    @foreach (var item in Model)
                    {
                        <tr>
                            <td>@item.Property1</td>
                            <td>@item.Property2</td>
                            <td>@item.Property3</td>
                            <td>
                                <a href="@Url.Action("Details", new { id = item.Id })" class="btn btn-sm btn-info">Details</a>
                                <a href="@Url.Action("Edit", new { id = item.Id })" class="btn btn-sm btn-warning">Edit</a>
                                <a href="@Url.Action("Delete", new { id = item.Id })" class="btn btn-sm btn-danger">Delete</a>
                            </td>
                        </tr>
                    }
                </tbody>
            </table>
        </div>
    </div>
}
else
{
    <div class="alert alert-info">
        No entities found. <a href="@Url.Action("Create")">Create one now</a>
    </div>
}
```

## Design principles

1. **Header**: Clear page title and context
2. **Actions**: Prominent "Create" button
3. **Data display**: Clean, scannable table or cards
4. **Responsive**: Works on all screen sizes
5. **Accessibility**: Semantic HTML, good contrast
6. **Navigation**: Breadcrumbs and clear links

## Workflow

1. User requests: "Create list page for X"
2. Determine the best layout (table, cards, grid)
3. Create the view with proper Razor syntax
4. Add Bootstrap styling
5. Implement action buttons
6. Test responsiveness
