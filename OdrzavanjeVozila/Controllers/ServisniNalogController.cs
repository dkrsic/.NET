using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdrzavanjeVozila;
using OdrzavanjeVozila.Klase;
using OdrzavanjeVozila.Models;

namespace OdrzavanjeVozila.Controllers
{
    [Route("servisi")]
    public class ServisniNalogController : Controller
    {
        private readonly OdrzavanjeVozilaDbContext _ctx;

        public ServisniNalogController(OdrzavanjeVozilaDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            var nalozi = _ctx.ServisniNalozi
                .Include(s => s.Automobil)
                .Include(s => s.Mehanicar)
                .ToList();
            return View(nalozi);
        }

        [HttpGet("pretrazi")]
        public IActionResult Pretrazi(string? q)
        {
            var search = (q ?? string.Empty).Trim().ToLower();

            var nalozi = _ctx.ServisniNalozi
                .Include(s => s.Automobil)
                .Include(s => s.Mehanicar)
                .ToList();

            if (!string.IsNullOrEmpty(search))
            {
                nalozi = nalozi.Where(s =>
                    (s.Automobil != null && s.Automobil.Marka.ToLower().Contains(search))
                    || (s.Automobil != null && s.Automobil.Model.ToLower().Contains(search))
                    || (s.Automobil != null && s.Automobil.RegistracijskiBroj.ToLower().Contains(search))
                    || (s.Mehanicar != null && s.Mehanicar.Ime.ToLower().Contains(search))
                    || (s.Mehanicar != null && s.Mehanicar.Prezime.ToLower().Contains(search))
                    || s.Status.ToString().ToLower().Contains(search)
                    || s.OpisRadova.ToLower().Contains(search)
                    || (s.Napomena != null && s.Napomena.ToLower().Contains(search)))
                    .ToList();
            }

            return PartialView("_Rows", nalozi);
        }

        [HttpGet("autocomplete-automobili")]
        public IActionResult AutocompleteAutomobili(string? q)
        {
            var search = (q ?? string.Empty).Trim().ToLower();

            var automobili = _ctx.Automobili
                .Include(a => a.Korisnik)
                .Where(a => string.IsNullOrEmpty(search)
                    || a.Marka.ToLower().Contains(search)
                    || a.Model.ToLower().Contains(search)
                    || a.RegistracijskiBroj.ToLower().Contains(search)
                    || (a.Korisnik != null && a.Korisnik.Ime.ToLower().Contains(search))
                    || (a.Korisnik != null && a.Korisnik.Prezime.ToLower().Contains(search)))
                .OrderBy(a => a.Marka)
                .ThenBy(a => a.Model)
                .Take(15)
                .Select(a => new { id = a.Id, text = a.Naziv })
                .ToList();

            return Json(automobili);
        }

        [HttpGet("autocomplete-mehanicari")]
        public IActionResult AutocompleteMehanicari(string? q)
        {
            var search = (q ?? string.Empty).Trim().ToLower();

            var mehanicari = _ctx.Mehanicari
                .Where(m => string.IsNullOrEmpty(search)
                    || m.Ime.ToLower().Contains(search)
                    || m.Prezime.ToLower().Contains(search)
                    || m.Specijalizacija.ToLower().Contains(search))
                .OrderBy(m => m.Prezime)
                .ThenBy(m => m.Ime)
                .Take(15)
                .Select(m => new { id = m.Id, text = m.PunoIme })
                .ToList();

            return Json(mehanicari);
        }

        [HttpGet("kreiraj")]
        public IActionResult Kreiraj()
        {
            PopulateLookups();
            return View(new ServisniNalogCreateModel());
        }

        [HttpPost("kreiraj")]
        [ValidateAntiForgeryToken]
        public IActionResult Kreiraj(ServisniNalogCreateModel createModel)
        {
            if (!ModelState.IsValid)
            {
                PopulateLookups(createModel.AutomobilId, createModel.MehanicarId);
                return View(createModel);
            }

            if (IsDuplicate(createModel))
            {
                ModelState.AddModelError(string.Empty, "Već postoji identičan servisni nalog.");
                PopulateLookups(createModel.AutomobilId, createModel.MehanicarId);
                return View(createModel);
            }

            var nalog = new ServisniNalog
            {
                DatumOtvaranja = createModel.DatumOtvaranja,
                DatumZatvaranja = createModel.DatumZatvaranja,
                SljedecaPreporucenaPregleda = createModel.SljedecaPreporucenaPregleda,
                OpisRadova = createModel.OpisRadova,
                UkupnaCijena = createModel.UkupnaCijena,
                Status = createModel.Status,
                KilometrazaPrilikomServisa = createModel.KilometrazaPrilikomServisa,
                Napomena = createModel.Napomena ?? string.Empty,
                AutomobilId = createModel.AutomobilId!.Value,
                MehanicarId = createModel.MehanicarId!.Value
            };

            _ctx.ServisniNalozi.Add(nalog);
            _ctx.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = nalog.Id });
        }

        [HttpGet("{id:int}")]
        [HttpGet("detalji/{id:int}")]
        public IActionResult Details(int id)
        {
            var nalog = _ctx.ServisniNalozi
                .Include(s => s.Automobil)
                .Include(s => s.Mehanicar)
                .FirstOrDefault(s => s.Id == id);
            if (nalog == null) return NotFound();
            return View(nalog);
        }

        [HttpGet("uredi/{id:int}")]
        public IActionResult Uredi(int id)
        {
            var nalog = _ctx.ServisniNalozi.FirstOrDefault(s => s.Id == id);
            if (nalog == null) return NotFound();

            PopulateLookups(nalog.AutomobilId, nalog.MehanicarId);

            return View(new ServisniNalogEditModel
            {
                Id = nalog.Id,
                DatumOtvaranja = nalog.DatumOtvaranja,
                DatumZatvaranja = nalog.DatumZatvaranja,
                SljedecaPreporucenaPregleda = nalog.SljedecaPreporucenaPregleda,
                OpisRadova = nalog.OpisRadova,
                UkupnaCijena = nalog.UkupnaCijena,
                Status = nalog.Status,
                KilometrazaPrilikomServisa = nalog.KilometrazaPrilikomServisa,
                Napomena = nalog.Napomena,
                AutomobilId = nalog.AutomobilId,
                MehanicarId = nalog.MehanicarId
            });
        }

        [HttpPost("uredi/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult Uredi(int id, ServisniNalogEditModel editModel)
        {
            if (id != editModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                PopulateLookups(editModel.AutomobilId, editModel.MehanicarId);
                return View(editModel);
            }

            var nalog = _ctx.ServisniNalozi.FirstOrDefault(s => s.Id == id);
            if (nalog == null) return NotFound();

            if (IsDuplicate(editModel, id))
            {
                ModelState.AddModelError(string.Empty, "Već postoji identičan servisni nalog.");
                PopulateLookups(editModel.AutomobilId, editModel.MehanicarId);
                return View(editModel);
            }

            nalog.DatumOtvaranja = editModel.DatumOtvaranja;
            nalog.DatumZatvaranja = editModel.DatumZatvaranja;
            nalog.SljedecaPreporucenaPregleda = editModel.SljedecaPreporucenaPregleda;
            nalog.OpisRadova = editModel.OpisRadova;
            nalog.UkupnaCijena = editModel.UkupnaCijena;
            nalog.Status = editModel.Status;
            nalog.KilometrazaPrilikomServisa = editModel.KilometrazaPrilikomServisa;
            nalog.Napomena = editModel.Napomena ?? string.Empty;
            nalog.AutomobilId = editModel.AutomobilId!.Value;
            nalog.MehanicarId = editModel.MehanicarId!.Value;

            _ctx.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = nalog.Id });
        }

        [HttpGet("obrisi/{id:int}")]
        public IActionResult Obrisi(int id)
        {
            var nalog = _ctx.ServisniNalozi
                .Include(s => s.Automobil)
                .Include(s => s.Mehanicar)
                .FirstOrDefault(s => s.Id == id);

            if (nalog == null) return NotFound();

            return View(nalog);
        }

        [HttpPost("obrisi/{id:int}")]
        [ValidateAntiForgeryToken]
        [ActionName("Obrisi")]
        public IActionResult ObrisiPotvrdi(int id)
        {
            var nalog = _ctx.ServisniNalozi.FirstOrDefault(s => s.Id == id);
            if (nalog == null) return NotFound();

            nalog.DeletedAt = DateTime.UtcNow;
            _ctx.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("status/{status}")]
        public IActionResult PriStatusu(string status)
        {
            if (!Enum.TryParse(typeof(OdrzavanjeVozila.Tools.StatusNaloga), status, true, out var parsed))
                return BadRequest("Nepoznat status");

            var s = (OdrzavanjeVozila.Tools.StatusNaloga)parsed!;
            var nalozi = _ctx.ServisniNalozi
                .Where(n => n.Status == s)
                .Include(n => n.Automobil)
                .Include(n => n.Mehanicar)
                .ToList();
            return View(nalozi);
        }

        private void PopulateLookups(int? selectedAutomobilId = null, int? selectedMehanicarId = null)
        {
            ViewBag.Automobili = new SelectList(
                _ctx.Automobili.Include(a => a.Korisnik).OrderBy(a => a.Marka).ThenBy(a => a.Model).ToList(),
                nameof(Automobil.Id),
                nameof(Automobil.Naziv),
                selectedAutomobilId);

            ViewBag.Mehanicari = new SelectList(
                _ctx.Mehanicari.Include(m => m.Radionica).OrderBy(m => m.Prezime).ThenBy(m => m.Ime).ToList(),
                nameof(Mehanicar.Id),
                nameof(Mehanicar.PunoIme),
                selectedMehanicarId);
        }

        private bool IsDuplicate(ServisniNalogCreateModel model, int? excludeId = null)
        {
            return _ctx.ServisniNalozi.Any(s =>
                (!excludeId.HasValue || s.Id != excludeId.Value) &&
                s.DatumOtvaranja == model.DatumOtvaranja &&
                s.DatumZatvaranja == model.DatumZatvaranja &&
                s.SljedecaPreporucenaPregleda == model.SljedecaPreporucenaPregleda &&
                s.OpisRadova == model.OpisRadova &&
                s.UkupnaCijena == model.UkupnaCijena &&
                s.Status == model.Status &&
                s.KilometrazaPrilikomServisa == model.KilometrazaPrilikomServisa &&
                s.Napomena == (model.Napomena ?? string.Empty) &&
                s.AutomobilId == model.AutomobilId &&
                s.MehanicarId == model.MehanicarId);
        }
    }
}