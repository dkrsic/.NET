using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OdrzavanjeVozila;
using OdrzavanjeVozila.Klase;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdrzavanjeVozila.Models;

namespace OdrzavanjeVozila.Controllers
{
    [Route("mehanicari")]
    public class MehanicarController : Controller
    {
        private readonly OdrzavanjeVozilaDbContext _ctx;

        public MehanicarController(OdrzavanjeVozilaDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            var mehanicari = _ctx.Mehanicari.Include(m => m.Radionica).ToList();
            return View(mehanicari);
        }

        [HttpGet("pretrazi")]
        public IActionResult Pretrazi(string? q)
        {
            var search = (q ?? string.Empty).Trim().ToLower();

            var mehanicari = _ctx.Mehanicari
                .Include(m => m.Radionica)
                .Where(m => string.IsNullOrEmpty(search)
                    || m.Ime.ToLower().Contains(search)
                    || m.Prezime.ToLower().Contains(search)
                    || m.Specijalizacija.ToLower().Contains(search)
                    || m.Radionica.Naziv.ToLower().Contains(search))
                .ToList();

            return PartialView("_Rows", mehanicari);
        }

        [HttpGet("autocomplete-radionice")]
        public IActionResult AutocompleteRadionice(string? q)
        {
            var search = (q ?? string.Empty).Trim().ToLower();

            var radionice = _ctx.Radionice
                .Where(r => string.IsNullOrEmpty(search)
                    || r.Naziv.ToLower().Contains(search)
                    || r.Adresa.ToLower().Contains(search)
                    || r.Email.ToLower().Contains(search))
                .OrderBy(r => r.Naziv)
                .Take(15)
                .Select(r => new { id = r.Id, text = r.Naziv })
                .ToList();

            return Json(radionice);
        }

        [HttpGet("kreiraj")]
        public IActionResult Kreiraj()
        {
            PopulateRadioniceDropdown();
            return View(new MehanicarCreateModel { DatumZaposlenja = DateTime.Today });
        }

        [HttpPost("kreiraj")]
        [ValidateAntiForgeryToken]
        public IActionResult Kreiraj(MehanicarCreateModel createModel)
        {
            if (!ModelState.IsValid)
            {
                PopulateRadioniceDropdown(createModel.RadionicaId);
                return View(createModel);
            }

            var radionica = _ctx.Radionice.FirstOrDefault(r => r.Id == createModel.RadionicaId);
            if (radionica == null)
            {
                ModelState.AddModelError(nameof(createModel.RadionicaId), "Odaberite radionicu.");
                PopulateRadioniceDropdown(createModel.RadionicaId);
                return View(createModel);
            }

            var mehanicar = new Mehanicar
            {
                Ime = createModel.Ime,
                Prezime = createModel.Prezime,
                Specijalizacija = createModel.Specijalizacija,
                DatumZaposlenja = createModel.DatumZaposlenja,
                SatnicaEUR = createModel.SatnicaEUR,
                RadionicaId = createModel.RadionicaId!.Value
            };

            _ctx.Mehanicari.Add(mehanicar);
            _ctx.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = mehanicar.Id });
        }

        [HttpGet("{id:int}")]
        [HttpGet("detalji/{id:int}")]
        public IActionResult Details(int id)
        {
            var mehanicar = _ctx.Mehanicari
                .Include(m => m.Radionica)
                .Include(m => m.Nalozi)
                    .ThenInclude(n => n.Automobil)
                .FirstOrDefault(m => m.Id == id);
            if (mehanicar == null) return NotFound();
            return View(mehanicar);
        }

        [HttpGet("uredi/{id:int}")]
        public IActionResult Uredi(int id)
        {
            var mehanicar = _ctx.Mehanicari.FirstOrDefault(m => m.Id == id);
            if (mehanicar == null) return NotFound();

            PopulateRadioniceDropdown(mehanicar.RadionicaId);

            return View(new MehanicarEditModel
            {
                Id = mehanicar.Id,
                Ime = mehanicar.Ime,
                Prezime = mehanicar.Prezime,
                Specijalizacija = mehanicar.Specijalizacija,
                DatumZaposlenja = mehanicar.DatumZaposlenja,
                SatnicaEUR = mehanicar.SatnicaEUR,
                RadionicaId = mehanicar.RadionicaId
            });
        }

        [HttpPost("uredi/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult Uredi(int id, MehanicarEditModel editModel)
        {
            if (id != editModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                PopulateRadioniceDropdown(editModel.RadionicaId);
                return View(editModel);
            }

            var radionica = _ctx.Radionice.FirstOrDefault(r => r.Id == editModel.RadionicaId);
            if (radionica == null)
            {
                ModelState.AddModelError(nameof(editModel.RadionicaId), "Odaberite radionicu.");
                PopulateRadioniceDropdown(editModel.RadionicaId);
                return View(editModel);
            }

            var mehanicar = _ctx.Mehanicari.FirstOrDefault(m => m.Id == id);
            if (mehanicar == null) return NotFound();

            mehanicar.Ime = editModel.Ime;
            mehanicar.Prezime = editModel.Prezime;
            mehanicar.Specijalizacija = editModel.Specijalizacija;
            mehanicar.DatumZaposlenja = editModel.DatumZaposlenja;
            mehanicar.SatnicaEUR = editModel.SatnicaEUR;
            mehanicar.RadionicaId = editModel.RadionicaId!.Value;

            _ctx.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = mehanicar.Id });
        }

        [HttpGet("obrisi/{id:int}")]
        public IActionResult Obrisi(int id)
        {
            var mehanicar = _ctx.Mehanicari
                .Include(m => m.Radionica)
                .Include(m => m.Nalozi)
                .FirstOrDefault(m => m.Id == id);

            if (mehanicar == null) return NotFound();

            return View(mehanicar);
        }

        [HttpPost("obrisi/{id:int}")]
        [ValidateAntiForgeryToken]
        [ActionName("Obrisi")]
        public IActionResult ObrisiPotvrdi(int id)
        {
            var mehanicar = _ctx.Mehanicari.FirstOrDefault(m => m.Id == id);
            if (mehanicar == null) return NotFound();

            _ctx.Mehanicari.Remove(mehanicar);
            _ctx.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("radionica/{radionicaId:int}")]
        public IActionResult PorRadionica(int radionicaId)
        {
            var mehanicari = _ctx.Mehanicari
                .Where(m => m.RadionicaId == radionicaId)
                .Include(m => m.Radionica)
                .ToList();
            return View(mehanicari);
        }

        private void PopulateRadioniceDropdown(int? selectedRadionicaId = null)
        {
            ViewBag.Radionice = new SelectList(
                _ctx.Radionice.OrderBy(r => r.Naziv).ToList(),
                nameof(Radionica.Id),
                nameof(Radionica.Naziv),
                selectedRadionicaId);
        }
    }
}