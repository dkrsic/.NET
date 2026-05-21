using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OdrzavanjeVozila;
using OdrzavanjeVozila.Klase;
using OdrzavanjeVozila.Models;

namespace OdrzavanjeVozila.Controllers
{
    [Route("radionice")]
    public class RadionicaController : Controller
    {
        private readonly OdrzavanjeVozilaDbContext _ctx;

        public RadionicaController(OdrzavanjeVozilaDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            var radionice = _ctx.Radionice.ToList();
            return View(radionice);
        }

        [HttpGet("pretrazi")]
        public IActionResult Pretrazi(string? q)
        {
            var search = (q ?? string.Empty).Trim().ToLower();

            var radionice = _ctx.Radionice
                .Where(r => string.IsNullOrEmpty(search)
                    || r.Naziv.ToLower().Contains(search)
                    || r.Adresa.ToLower().Contains(search)
                    || r.Telefon.ToLower().Contains(search)
                    || r.Email.ToLower().Contains(search))
                .ToList();

            return PartialView("_Rows", radionice);
        }

        [HttpGet("kreiraj")]
        public IActionResult Kreiraj()
        {
            return View(new RadionicaCreateModel());
        }

        [HttpPost("kreiraj")]
        [ValidateAntiForgeryToken]
        public IActionResult Kreiraj(RadionicaCreateModel createModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createModel);
            }

            if (IsDuplicate(createModel))
            {
                ModelState.AddModelError(string.Empty, "Već postoji identična radionica.");
                return View(createModel);
            }

            var radionica = new Radionica
            {
                Naziv = createModel.Naziv,
                Adresa = createModel.Adresa,
                Telefon = createModel.Telefon,
                Email = createModel.Email
            };

            _ctx.Radionice.Add(radionica);
            _ctx.SaveChanges();

            TempData["ToastSuccess"] = "Radionica je uspješno dodana.";

            return RedirectToAction(nameof(Details), new { id = radionica.Id });
        }

        [HttpGet("{id:int}")]
        [HttpGet("detalji/{id:int}")]
        public IActionResult Details(int id)
        {
            var radionica = _ctx.Radionice
                .Include(r => r.Mehanicari)
                .FirstOrDefault(r => r.Id == id);
            if (radionica == null) return NotFound();
            return View(radionica);
        }

        [HttpGet("uredi/{id:int}")]
        public IActionResult Uredi(int id)
        {
            var radionica = _ctx.Radionice.FirstOrDefault(r => r.Id == id);
            if (radionica == null) return NotFound();

            return View(new RadionicaEditModel
            {
                Id = radionica.Id,
                Naziv = radionica.Naziv,
                Adresa = radionica.Adresa,
                Telefon = radionica.Telefon,
                Email = radionica.Email
            });
        }

        [HttpPost("uredi/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Uredi(int id, RadionicaEditModel editModel)
        {
            if (id != editModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(editModel);
            }

            if (IsDuplicate(editModel, id))
            {
                ModelState.AddModelError(string.Empty, "Već postoji identična radionica.");
                return View(editModel);
            }

            var radionica = _ctx.Radionice.FirstOrDefault(r => r.Id == id);
            if (radionica == null) return NotFound();

            // Update only required fields (Naziv, Adresa)
            var ok = await TryUpdateModelAsync(radionica, string.Empty,
                r => r.Naziv,
                r => r.Adresa);

            if (ok && ModelState.IsValid)
            {
                radionica.Telefon = editModel.Telefon;
                radionica.Email = editModel.Email;

                _ctx.SaveChanges();
                TempData["ToastSuccess"] = "Radionica je uspješno ažurirana.";
                return RedirectToAction(nameof(Details), new { id = radionica.Id });
            }

            return View(editModel);
        }

        [HttpGet("obrisi/{id:int}")]
        public IActionResult Obrisi(int id)
        {
            var radionica = _ctx.Radionice
                .Include(r => r.Mehanicari)
                .FirstOrDefault(r => r.Id == id);

            if (radionica == null) return NotFound();

            return View(radionica);
        }

        [HttpPost("obrisi/{id:int}")]
        [ValidateAntiForgeryToken]
        [ActionName("Obrisi")]
        public IActionResult ObrisiPotvrdi(int id)
        {
            var radionica = _ctx.Radionice.FirstOrDefault(r => r.Id == id);
            if (radionica == null) return NotFound();

            radionica.DeletedAt = DateTime.UtcNow;
            _ctx.SaveChanges();

            TempData["ToastSuccess"] = "Radionica je uspješno obrisana.";

            return RedirectToAction(nameof(Index));
        }

        private bool IsDuplicate(RadionicaCreateModel model, int? excludeId = null)
        {
            return _ctx.Radionice.Any(r =>
                (!excludeId.HasValue || r.Id != excludeId.Value) &&
                r.Naziv == model.Naziv &&
                r.Adresa == model.Adresa);
        }
    }
}