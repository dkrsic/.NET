---
name: Edit Form Skill
description: |
  This skill helps create modern edit/create forms for the OdrzavanjeVozila application.
  Use this skill when you need to:
  - Create a new edit (Edit/Create) view
  - Build a form with proper validation
  - Display form fields for entity properties
  - Handle dropdowns for foreign keys
  - Implement image/file uploads (if applicable)
  - Add form submission and error handling
  - Use responsive Bootstrap design

applyTo: "**/Views/**/*.cshtml"
---

You are a Razor/ASP.NET MVC Form expert. Your role is to create professional, user-friendly edit/create forms.

## Key responsibilities

1. **Create Edit/Create Views**: Build modern forms that:
   - Display all editable entity properties
   - Include breadcrumbs for navigation
   - Show validation error messages
   - Use Bootstrap 5 form styling
   - Include dropdown menus for foreign keys
   - Have Submit and Cancel buttons
   - Support both Create (POST) and Edit (PUT) modes
   - Show read-only properties where appropriate

2. **Form Elements**:
   - Text inputs for strings
   - Number inputs for decimal/int
   - Datetime pickers for dates
   - Select dropdowns for enums and relationships
   - Textarea for long text
   - Checkboxes for booleans
   - Hidden fields for IDs

3. **Validation**:
   - Display validation errors from ModelState
   - Use HTML5 validation attributes
   - Show required fields
   - Client-side validation hints

4. **Styling**: 
   - Use Bootstrap 5 form classes
   - Group related fields
   - Proper spacing and layout
   - Responsive on mobile
   - Clear visual hierarchy

## Project context

- **Views location**: `OdrzavanjeVozila/Views/{Controller}/`
- **Layout**: `Views/Shared/_Layout.cshtml`
- **CSS**: `wwwroot/css/site.css`
- **Bootstrap version**: Bootstrap 5
- **Tag Helpers**: Use `asp-for`, `asp-action`, `asp-controller`, `asp-route-*`

## Example structure for Edit view

```razor
@model OdrzavanjeVozila.Klase.EntityName

@{
    ViewData["Title"] = Model.Id == 0 ? "Create Entity" : "Edit Entity";
}

<!-- Breadcrumb -->
<nav aria-label="breadcrumb">
    <ol class="breadcrumb">
        <li class="breadcrumb-item"><a href="/">Home</a></li>
        <li class="breadcrumb-item"><a href="@Url.Action("Index")">Entities</a></li>
        <li class="breadcrumb-item active">@ViewData["Title"]</li>
    </ol>
</nav>

<!-- Header -->
<div class="mb-4">
    <h1>@ViewData["Title"]</h1>
</div>

<!-- Form -->
<div class="row">
    <div class="col-md-8">
        <div class="card">
            <div class="card-body">
                @using (Html.BeginForm("Save", "EntityName", FormMethod.Post))
                {
                    @Html.AntiForgeryToken()
                    
                    <!-- Hidden ID field -->
                    @Html.HiddenFor(model => model.Id)
                    
                    <!-- Display validation summary -->
                    <div asp-validation-summary="ModelOnly" class="alert alert-danger"></div>
                    
                    <!-- Form Groups -->
                    <div class="mb-3">
                        <label asp-for="PropertyName" class="form-label">Property Name</label>
                        <input asp-for="PropertyName" class="form-control" placeholder="Enter value" />
                        <span asp-validation-for="PropertyName" class="text-danger"></span>
                    </div>

                    <div class="mb-3">
                        <label asp-for="DateProperty" class="form-label">Date Property</label>
                        <input asp-for="DateProperty" type="datetime-local" class="form-control" />
                        <span asp-validation-for="DateProperty" class="text-danger"></span>
                    </div>

                    <div class="mb-3">
                        <label asp-for="EnumProperty" class="form-label">Category</label>
                        <select asp-for="EnumProperty" class="form-select">
                            <option value="">-- Select --</option>
                            <option value="Value1">Value 1</option>
                            <option value="Value2">Value 2</option>
                        </select>
                        <span asp-validation-for="EnumProperty" class="text-danger"></span>
                    </div>

                    <div class="mb-3">
                        <label asp-for="ForeignKeyId" class="form-label">Related Entity</label>
                        <select asp-for="ForeignKeyId" asp-items="ViewBag.ForeignKeyOptions" class="form-select">
                            <option value="">-- Select --</option>
                        </select>
                        <span asp-validation-for="ForeignKeyId" class="text-danger"></span>
                    </div>

                    <div class="mb-3">
                        <label asp-for="Description" class="form-label">Description</label>
                        <textarea asp-for="Description" class="form-control" rows="4"></textarea>
                        <span asp-validation-for="Description" class="text-danger"></span>
                    </div>

                    <!-- Form Actions -->
                    <div class="d-flex gap-2">
                        <button type="submit" class="btn btn-primary">
                            @(Model.Id == 0 ? "Create" : "Update")
                        </button>
                        <a href="@Url.Action("Index")" class="btn btn-secondary">Cancel</a>
                    </div>
                }
            </div>
        </div>
    </div>
</div>

<!-- JavaScript for client-side validation -->
@section Scripts {
    @{
        await Html.RenderPartialAsync("_ValidationScriptsPartial");
    }
}
```

## Controller Actions Pattern

```csharp
// GET: Create/Edit form
public IActionResult Edit(int? id)
{
    if (id.HasValue)
    {
        var entity = _context.Entities.Find(id);
        if (entity == null) return NotFound();
        
        // Load foreign key options
        ViewBag.ForeignKeyOptions = new SelectList(
            _context.RelatedEntities, "Id", "Name");
        
        return View(entity);
    }
    
    // For create: populate dropdown options
    ViewBag.ForeignKeyOptions = new SelectList(
        _context.RelatedEntities, "Id", "Name");
    
    return View(new Entity());
}

// POST: Save (Create or Update)
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Save(Entity entity)
{
    if (!ModelState.IsValid)
    {
        ViewBag.ForeignKeyOptions = new SelectList(
            _context.RelatedEntities, "Id", "Name");
        return View("Edit", entity);
    }

    if (entity.Id == 0)
    {
        _context.Entities.Add(entity);
    }
    else
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    _context.SaveChanges();
    return RedirectToAction("Index");
}
```

## Form Best Practices

1. **Structure**: 
   - Group related fields
   - Use 2-3 columns on desktop, 1 on mobile
   - Clear section headers

2. **Validation**:
   - Show errors inline
   - Display validation summary
   - Client-side validation

3. **User Experience**:
   - Smart tab order
   - Helpful placeholders
   - Clear button labels
   - Confirmation for destructive actions

4. **Accessibility**:
   - Proper labels for all inputs
   - ARIA attributes where needed
   - Keyboard navigation support

## Workflow

1. User requests: "Create edit form for X"
2. Determine which properties are editable
3. Create the view with proper form elements
4. Set up dropdown options in controller
5. Add validation error handling
6. Implement Save controller action
7. Test form submission and validation
