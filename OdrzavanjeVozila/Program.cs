using Microsoft.EntityFrameworkCore;
using OdrzavanjeVozila;
using OdrzavanjeVozila.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register DbContext with SQL Server
builder.Services.AddDbContext<OdrzavanjeVozilaDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("OdrzavanjeVozilaDbContext")));

// Keep mock repositories for now (will be replaced with EF repositories later)
builder.Services.AddSingleton<KorisnikMockRepository>();
builder.Services.AddSingleton<AutomobilMockRepository>();
builder.Services.AddSingleton<MehanicarMockRepository>();
builder.Services.AddSingleton<RadionicaMockRepository>();
builder.Services.AddSingleton<DioMockRepository>();
builder.Services.AddSingleton<ServisniNalogMockRepository>();

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

// Custom routes (attribute routing on controllers take precedence)
app.MapControllerRoute(
    name: "vozila_by_korisnik",
    pattern: "vozila/korisnika/{korisnikId:int}",
    defaults: new { controller = "Automobil", action = "PoBenzinskoj" });

app.MapControllerRoute(
    name: "servisi_status",
    pattern: "servisi/status/{status}",
    defaults: new { controller = "ServisniNalog", action = "PriStatusu" });

app.MapControllerRoute(
    name: "dijelovi_kategorija",
    pattern: "dijelovi/kategorija/{kategorija}",
    defaults: new { controller = "Dio", action = "PrioCategory" });

app.MapControllerRoute(
    name: "mehanicari_radionica",
    pattern: "mehanicari/radionica/{radionicaId:int}",
    defaults: new { controller = "Mehanicar", action = "PorRadionica" });

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
