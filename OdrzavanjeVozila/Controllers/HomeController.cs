using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OdrzavanjeVozila.Models;
using OdrzavanjeVozila;
using OdrzavanjeVozila.Tools;

namespace OdrzavanjeVozila.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly OdrzavanjeVozilaDbContext _ctx;

        public HomeController(ILogger<HomeController> logger, OdrzavanjeVozilaDbContext ctx)
        {
            _logger = logger;
            _ctx = ctx;
        }

        [HttpGet("/")]
        [HttpGet("/Index")]
        public IActionResult Index()
        {
            var korisnici = _ctx.Korisnici.ToList();
            var automobili = _ctx.Automobili.ToList();
            var nalozi = _ctx.ServisniNalozi.ToList();
            var mehanicari = _ctx.Mehanicari.ToList();
            var radionice = _ctx.Radionice.ToList();
            var dijelovi = _ctx.Dijelovi.ToList();

            ViewData["StatUsersVehicles"] = $"{korisnici.Count} korisnika / {automobili.Count} vozila";
            ViewData["StatOrders"] = $"{nalozi.Count} naloga / {nalozi.Count(n => n.Status == StatusNaloga.Otvoren || n.Status == StatusNaloga.UObradi)} aktivno";
            ViewData["StatTeam"] = $"{mehanicari.Count} mehaničara / {radionice.Count} radionice";
            ViewData["StatStock"] = $"{dijelovi.Count} dijelova / {dijelovi.Count(d => d.KolicinaNaSkladistu < 5)} niska zaliha";

            return View();
        }

        [HttpGet("/Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("/Home/Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
