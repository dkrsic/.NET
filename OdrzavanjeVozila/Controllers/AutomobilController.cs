using Microsoft.AspNetCore.Mvc;
using OdrzavanjeVozila.Repositories;
using OdrzavanjeVozila.Repositories;

namespace OdrzavanjeVozila.Controllers
{
    public class AutomobilController : Controller
    {
        private readonly AutomobilMockRepository _automobilRepo;
        private readonly KorisnikMockRepository _korisnikRepo;
        private readonly ServisniNalogMockRepository _nalogRepo;

        public AutomobilController(AutomobilMockRepository automobilRepo, KorisnikMockRepository korisnikRepo, ServisniNalogMockRepository nalogRepo)
        {
            _automobilRepo = automobilRepo;
            _korisnikRepo = korisnikRepo;
            _nalogRepo = nalogRepo;
        }

        public IActionResult Index()
        {
            var automobili = _automobilRepo.GetAll();
            foreach (var a in automobili)
                a.Korisnik = _korisnikRepo.GetById(a.KorisnikId);
            return View(automobili);
        }

        public IActionResult Details(int id)
        {
            var automobil = _automobilRepo.GetById(id);
            if (automobil == null) return NotFound();
            automobil.Korisnik = _korisnikRepo.GetById(automobil.KorisnikId);
            automobil.ServisniNalozi = _nalogRepo.GetByAutomobilId(id);
            return View(automobil);
        }
    }
}