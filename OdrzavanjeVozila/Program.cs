using Microsoft.EntityFrameworkCore;
using OdrzavanjeVozila;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register DbContext with SQL Server
builder.Services.AddDbContext<OdrzavanjeVozilaDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("OdrzavanjeVozilaDbContext")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Enable attribute routing for controllers
app.MapControllers();

app.Run();
