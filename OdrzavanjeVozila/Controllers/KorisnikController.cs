using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OdrzavanjeVozila;
using OdrzavanjeVozila.Klase;
using OdrzavanjeVozila.Models;

namespace OdrzavanjeVozila.Controllers
{
    [Route("korisnici")]
    public class KorisnikController : Controller
    {
        private readonly OdrzavanjeVozilaDbContext _ctx;

        public KorisnikController(OdrzavanjeVozilaDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            var korisnici = _ctx.Korisnici.ToList();
            return View(korisnici);
        }

        [HttpGet("pretrazi")]
        public IActionResult Pretrazi(string? q)
        {
            var search = (q ?? string.Empty).Trim().ToLower();

            var korisnici = _ctx.Korisnici
                .Where(k => string.IsNullOrEmpty(search)
                    || k.Ime.ToLower().Contains(search)
                    || k.Prezime.ToLower().Contains(search)
                    || k.Email.ToLower().Contains(search)
                    || k.Telefon.ToLower().Contains(search)
                    || k.Adresa.ToLower().Contains(search))
                .ToList();

            return PartialView("_Rows", korisnici);
        }

        [HttpGet("kreiraj")]
        public IActionResult Kreiraj()
        {
            return View(new KorisnikCreateModel());
        }

        [HttpPost("kreiraj")]
        [ValidateAntiForgeryToken]
        public IActionResult Kreiraj(KorisnikCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var korisnik = new Korisnik
            {
                Ime = model.Ime,
                Prezime = model.Prezime,
                Email = model.Email,
                Telefon = model.Telefon,
                Adresa = model.Adresa,
                DatumRegistracije = DateTime.Now
            };

            _ctx.Korisnici.Add(korisnik);
            _ctx.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = korisnik.Id });
        }

        [HttpGet("{id:int}")]
        [HttpGet("detalji/{id:int}")]
        public IActionResult Details(int id)
        {
            var korisnik = _ctx.Korisnici
                .Include(k => k.Vozila)
                .FirstOrDefault(k => k.Id == id);
            if (korisnik == null) return NotFound();
            return View(korisnik);
        }

        [HttpGet("uredi/{id:int}")]
        public IActionResult Uredi(int id)
        {
            var korisnik = _ctx.Korisnici.FirstOrDefault(k => k.Id == id);
            if (korisnik == null) return NotFound();

            var model = new KorisnikEditModel
            {
                Id = korisnik.Id,
                Ime = korisnik.Ime,
                Prezime = korisnik.Prezime,
                Email = korisnik.Email,
                Telefon = korisnik.Telefon,
                Adresa = korisnik.Adresa,
                DatumRegistracije = korisnik.DatumRegistracije
            };

            return View(model);
        }

        [HttpPost("uredi/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult Uredi(int id, KorisnikEditModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingKorisnik = _ctx.Korisnici.FirstOrDefault(k => k.Id == id);
            if (existingKorisnik == null)
            {
                return NotFound();
            }

            existingKorisnik.Ime = model.Ime;
            existingKorisnik.Prezime = model.Prezime;
            existingKorisnik.Email = model.Email;
            existingKorisnik.Telefon = model.Telefon;
            existingKorisnik.Adresa = model.Adresa;
            existingKorisnik.DatumRegistracije = model.DatumRegistracije;

            _ctx.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = existingKorisnik.Id });
        }

        [HttpGet("obrisi/{id:int}")]
        public IActionResult Obrisi(int id)
        {
            var korisnik = _ctx.Korisnici
                .Include(k => k.Vozila)
                .FirstOrDefault(k => k.Id == id);

            if (korisnik == null)
            {
                return NotFound();
            }

            return View(korisnik);
        }

        [HttpPost("obrisi/{id:int}")]
        [ValidateAntiForgeryToken]
        [ActionName("Obrisi")]
        public IActionResult ObrisiPotvrdi(int id)
        {
            var korisnik = _ctx.Korisnici.FirstOrDefault(k => k.Id == id);
            if (korisnik == null)
            {
                return NotFound();
            }

            _ctx.Korisnici.Remove(korisnik);
            _ctx.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}