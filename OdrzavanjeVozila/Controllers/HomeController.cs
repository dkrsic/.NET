using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OdrzavanjeVozila.Models;
using OdrzavanjeVozila.Repositories;
using OdrzavanjeVozila.Tools;

namespace OdrzavanjeVozila.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly KorisnikMockRepository _korisnikRepository;
        private readonly AutomobilMockRepository _automobilRepository;
        private readonly ServisniNalogMockRepository _servisniNalogRepository;
        private readonly MehanicarMockRepository _mehanicarRepository;
        private readonly RadionicaMockRepository _radionicaRepository;
        private readonly DioMockRepository _dioRepository;

        public HomeController(
            ILogger<HomeController> logger,
            KorisnikMockRepository korisnikRepository,
            AutomobilMockRepository automobilRepository,
            ServisniNalogMockRepository servisniNalogRepository,
            MehanicarMockRepository mehanicarRepository,
            RadionicaMockRepository radionicaRepository,
            DioMockRepository dioRepository)
        {
            _logger = logger;
            _korisnikRepository = korisnikRepository;
            _automobilRepository = automobilRepository;
            _servisniNalogRepository = servisniNalogRepository;
            _mehanicarRepository = mehanicarRepository;
            _radionicaRepository = radionicaRepository;
            _dioRepository = dioRepository;
        }

        public IActionResult Index()
        {
            var korisnici = _korisnikRepository.GetAll();
            var automobili = _automobilRepository.GetAll();
            var nalozi = _servisniNalogRepository.GetAll();
            var mehanicari = _mehanicarRepository.GetAll();
            var radionice = _radionicaRepository.GetAll();
            var dijelovi = _dioRepository.GetAll();

            ViewData["StatUsersVehicles"] = $"{korisnici.Count} korisnika / {automobili.Count} vozila";
            ViewData["StatOrders"] = $"{nalozi.Count} naloga / {nalozi.Count(n => n.Status == StatusNaloga.Otvoren || n.Status == StatusNaloga.UObradi)} aktivno";
            ViewData["StatTeam"] = $"{mehanicari.Count} mehaničara / {radionice.Count} radionice";
            ViewData["StatStock"] = $"{dijelovi.Count} dijelova / {dijelovi.Count(d => d.KolicinaNaSkladistu < 5)} niska zaliha";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
