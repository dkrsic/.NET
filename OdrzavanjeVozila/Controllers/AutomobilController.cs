using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdrzavanjeVozila;
using OdrzavanjeVozila.Klase;
using OdrzavanjeVozila.Models;

namespace OdrzavanjeVozila.Controllers
{
    [Route("vozila")]
    public class AutomobilController : Controller
    {
        private readonly OdrzavanjeVozilaDbContext _ctx;

        public AutomobilController(OdrzavanjeVozilaDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            var automobili = _ctx.Automobili.Include(a => a.Korisnik).ToList();
            return View(automobili);
        }

        [HttpGet("pretrazi")]
        public IActionResult Pretrazi(string? q)
        {
            var search = (q ?? string.Empty).Trim().ToLower();

            var automobili = _ctx.Automobili
                .Include(a => a.Korisnik)
                .Where(a => string.IsNullOrEmpty(search)
                    || a.Marka.ToLower().Contains(search)
                    || a.Model.ToLower().Contains(search)
                    || a.RegistracijskiBroj.ToLower().Contains(search)
                    || a.BrojSasije.ToLower().Contains(search)
                    || (a.Korisnik != null && a.Korisnik.Ime.ToLower().Contains(search))
                    || (a.Korisnik != null && a.Korisnik.Prezime.ToLower().Contains(search)))
                .ToList();

            return PartialView("_Rows", automobili);
        }

        [HttpGet("autocomplete-korisnici")]
        public IActionResult AutocompleteKorisnici(string? q)
        {
            var search = (q ?? string.Empty).Trim().ToLower();

            var korisnici = _ctx.Korisnici
                .Where(k => string.IsNullOrEmpty(search)
                    || k.Ime.ToLower().Contains(search)
                    || k.Prezime.ToLower().Contains(search)
                    || k.Email.ToLower().Contains(search)
                    || k.Telefon.ToLower().Contains(search))
                .OrderBy(k => k.Prezime)
                .ThenBy(k => k.Ime)
                .Take(15)
                .Select(k => new { id = k.Id, text = k.PunoIme })
                .ToList();

            return Json(korisnici);
        }

        [HttpGet("kreiraj")]
        public IActionResult Kreiraj()
        {
            PopulateKorisniciDropdown();
            return View(new AutomobilCreateModel { DatumPrvogServisa = DateTime.Today });
        }

        [HttpPost("kreiraj")]
        [ValidateAntiForgeryToken]
        public IActionResult Kreiraj(AutomobilCreateModel createModel)
        {
            if (!ModelState.IsValid)
            {
                PopulateKorisniciDropdown(createModel.KorisnikId);
                return View(createModel);
            }

            var korisnik = _ctx.Korisnici.FirstOrDefault(k => k.Id == createModel.KorisnikId);
            if (korisnik == null)
            {
                ModelState.AddModelError(nameof(createModel.KorisnikId), "Odaberite vlasnika vozila.");
                PopulateKorisniciDropdown(createModel.KorisnikId);
                return View(createModel);
            }

            if (IsDuplicate(createModel))
            {
                ModelState.AddModelError(string.Empty, "Već postoji identično vozilo.");
                PopulateKorisniciDropdown(createModel.KorisnikId);
                return View(createModel);
            }

            var automobil = new Automobil
            {
                Marka = createModel.Marka,
                Model = createModel.Model,
                Godiste = createModel.Godiste,
                RegistracijskiBroj = createModel.RegistracijskiBroj,
                BrojSasije = createModel.BrojSasije,
                TrenutnaKilometraza = createModel.TrenutnaKilometraza,
                VrstaPogona = createModel.VrstaPogona,
                DatumPrvogServisa = createModel.DatumPrvogServisa,
                KorisnikId = createModel.KorisnikId!.Value
            };

            _ctx.Automobili.Add(automobil);
            _ctx.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = automobil.Id });
        }

        [HttpGet("{id:int}")]
        [HttpGet("detalji/{id:int}")]
        public IActionResult Details(int id)
        {
            var automobil = _ctx.Automobili
                .Include(a => a.Korisnik)
                .Include(a => a.ServisniNalozi)
                .FirstOrDefault(a => a.Id == id);
            if (automobil == null) return NotFound();
            return View(automobil);
        }

        [HttpGet("uredi/{id:int}")]
        public IActionResult Uredi(int id)
        {
            var automobil = _ctx.Automobili.FirstOrDefault(a => a.Id == id);
            if (automobil == null) return NotFound();

            PopulateKorisniciDropdown(automobil.KorisnikId);

            var model = new AutomobilEditModel
            {
                Id = automobil.Id,
                Marka = automobil.Marka,
                Model = automobil.Model,
                Godiste = automobil.Godiste,
                RegistracijskiBroj = automobil.RegistracijskiBroj,
                BrojSasije = automobil.BrojSasije,
                TrenutnaKilometraza = automobil.TrenutnaKilometraza,
                VrstaPogona = automobil.VrstaPogona,
                DatumPrvogServisa = automobil.DatumPrvogServisa,
                KorisnikId = automobil.KorisnikId
            };

            return View(model);
        }

        [HttpPost("uredi/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult Uredi(int id, AutomobilEditModel editModel)
        {
            if (id != editModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                PopulateKorisniciDropdown(editModel.KorisnikId);
                return View(editModel);
            }

            var automobil = _ctx.Automobili.FirstOrDefault(a => a.Id == id);
            if (automobil == null) return NotFound();

            if (IsDuplicate(editModel, id))
            {
                ModelState.AddModelError(string.Empty, "Već postoji identično vozilo.");
                PopulateKorisniciDropdown(editModel.KorisnikId);
                return View(editModel);
            }

            automobil.Marka = editModel.Marka;
            automobil.Model = editModel.Model;
            automobil.Godiste = editModel.Godiste;
            automobil.RegistracijskiBroj = editModel.RegistracijskiBroj;
            automobil.BrojSasije = editModel.BrojSasije;
            automobil.TrenutnaKilometraza = editModel.TrenutnaKilometraza;
            automobil.VrstaPogona = editModel.VrstaPogona;
            automobil.DatumPrvogServisa = editModel.DatumPrvogServisa;
            automobil.KorisnikId = editModel.KorisnikId!.Value;

            _ctx.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = automobil.Id });
        }

        [HttpGet("obrisi/{id:int}")]
        public IActionResult Obrisi(int id)
        {
            var automobil = _ctx.Automobili
                .Include(a => a.Korisnik)
                .Include(a => a.ServisniNalozi)
                .FirstOrDefault(a => a.Id == id);

            if (automobil == null) return NotFound();

            return View(automobil);
        }

        [HttpPost("obrisi/{id:int}")]
        [ValidateAntiForgeryToken]
        [ActionName("Obrisi")]
        public IActionResult ObrisiPotvrdi(int id)
        {
            var automobil = _ctx.Automobili.FirstOrDefault(a => a.Id == id);
            if (automobil == null) return NotFound();

            automobil.DeletedAt = DateTime.UtcNow;
            _ctx.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("korisnika/{korisnikId:int}")]
        public IActionResult PoBenzinskoj(int korisnikId)
        {
            var automobili = _ctx.Automobili
                .Where(a => a.KorisnikId == korisnikId)
                .Include(a => a.Korisnik)
                .ToList();
            return View(automobili);
        }

        private void PopulateKorisniciDropdown(int? selectedKorisnikId = null)
        {
            ViewBag.Korisnici = new SelectList(
                _ctx.Korisnici.OrderBy(k => k.Prezime).ThenBy(k => k.Ime).ToList(),
                nameof(Korisnik.Id),
                nameof(Korisnik.PunoIme),
                selectedKorisnikId);
        }

        private bool IsDuplicate(AutomobilCreateModel model, int? excludeId = null)
        {
            var registracijskiBroj = model.RegistracijskiBroj.Trim();
            var brojSasije = model.BrojSasije.Trim();

            return _ctx.Automobili.Any(a =>
                (!excludeId.HasValue || a.Id != excludeId.Value) &&
                a.RegistracijskiBroj == registracijskiBroj &&
                a.BrojSasije == brojSasije);
        }
    }
}