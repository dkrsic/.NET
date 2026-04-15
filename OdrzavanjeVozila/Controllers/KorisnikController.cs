using Microsoft.AspNetCore.Mvc;
using OdrzavanjeVozila.Repositories;

namespace OdrzavanjeVozila.Controllers
{
    public class KorisnikController : Controller
    {
        private readonly KorisnikMockRepository _korisnikRepo;
        private readonly AutomobilMockRepository _automobilRepo;

        public KorisnikController(KorisnikMockRepository korisnikRepo, AutomobilMockRepository automobilRepo)
        {
            _korisnikRepo = korisnikRepo;
            _automobilRepo = automobilRepo;
        }

        public IActionResult Index()
        {
            var korisnici = _korisnikRepo.GetAll();
            return View(korisnici);
        }

        public IActionResult Details(int id)
        {
            var korisnik = _korisnikRepo.GetById(id);
            if (korisnik == null) return NotFound();
            korisnik.Vozila = _automobilRepo.GetByKorisnikId(id);
            return View(korisnik);
        }
    }
}