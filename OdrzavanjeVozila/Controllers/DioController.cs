using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OdrzavanjeVozila;
using OdrzavanjeVozila.Klase;
using OdrzavanjeVozila.Models;

namespace OdrzavanjeVozila.Controllers
{
    [Route("dijelovi")]
    public class DioController : Controller
    {
        private readonly OdrzavanjeVozilaDbContext _ctx;

        public DioController(OdrzavanjeVozilaDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            var dijelovi = _ctx.Dijelovi.ToList();
            return View(dijelovi);
        }

        [HttpGet("pretrazi")]
        public IActionResult Pretrazi(string? q)
        {
            var search = (q ?? string.Empty).Trim().ToLower();

            var dijelovi = _ctx.Dijelovi.ToList();

            if (!string.IsNullOrEmpty(search))
            {
                dijelovi = dijelovi.Where(d =>
                    d.Naziv.ToLower().Contains(search)
                    || d.KatalogBroj.ToLower().Contains(search)
                    || d.Proizvodac.ToLower().Contains(search)
                    || d.Opis.ToLower().Contains(search)
                    || d.Kategorija.ToString().ToLower().Contains(search))
                    .ToList();
            }

            return PartialView("_Rows", dijelovi);
        }

        [HttpGet("kreiraj")]
        public IActionResult Kreiraj()
        {
            return View(new DioCreateModel());
        }

        [HttpPost("kreiraj")]
        [ValidateAntiForgeryToken]
        public IActionResult Kreiraj(DioCreateModel createModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createModel);
            }

            if (IsDuplicate(createModel))
            {
                ModelState.AddModelError(string.Empty, "Već postoji identičan dio.");
                return View(createModel);
            }

            var dio = new Dio
            {
                Naziv = createModel.Naziv,
                KatalogBroj = createModel.KatalogBroj,
                Proizvodac = createModel.Proizvodac,
                Cijena = createModel.Cijena,
                Kategorija = createModel.Kategorija,
                KolicinaNaSkladistu = createModel.KolicinaNaSkladistu,
                Opis = createModel.Opis
            };

            _ctx.Dijelovi.Add(dio);
            _ctx.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = dio.Id });
        }

        [HttpGet("{id:int}")]
        [HttpGet("detalji/{id:int}")]
        public IActionResult Details(int id)
        {
            var dio = _ctx.Dijelovi.Find(id);
            if (dio == null) return NotFound();
            return View(dio);
        }

        [HttpGet("uredi/{id:int}")]
        public IActionResult Uredi(int id)
        {
            var dio = _ctx.Dijelovi.FirstOrDefault(d => d.Id == id);
            if (dio == null) return NotFound();

            return View(new DioEditModel
            {
                Id = dio.Id,
                Naziv = dio.Naziv,
                KatalogBroj = dio.KatalogBroj,
                Proizvodac = dio.Proizvodac,
                Cijena = dio.Cijena,
                Kategorija = dio.Kategorija,
                KolicinaNaSkladistu = dio.KolicinaNaSkladistu,
                Opis = dio.Opis
            });
        }

        [HttpPost("uredi/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult Uredi(int id, DioEditModel editModel)
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
                ModelState.AddModelError(string.Empty, "Već postoji identičan dio.");
                return View(editModel);
            }

            var dio = _ctx.Dijelovi.FirstOrDefault(d => d.Id == id);
            if (dio == null) return NotFound();

            dio.Naziv = editModel.Naziv;
            dio.KatalogBroj = editModel.KatalogBroj;
            dio.Proizvodac = editModel.Proizvodac;
            dio.Cijena = editModel.Cijena;
            dio.Kategorija = editModel.Kategorija;
            dio.KolicinaNaSkladistu = editModel.KolicinaNaSkladistu;
            dio.Opis = editModel.Opis;

            _ctx.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = dio.Id });
        }

        [HttpGet("obrisi/{id:int}")]
        public IActionResult Obrisi(int id)
        {
            var dio = _ctx.Dijelovi.Include(d => d.Stavke).FirstOrDefault(d => d.Id == id);
            if (dio == null) return NotFound();
            return View(dio);
        }

        [HttpPost("obrisi/{id:int}")]
        [ValidateAntiForgeryToken]
        [ActionName("Obrisi")]
        public IActionResult ObrisiPotvrdi(int id)
        {
            var dio = _ctx.Dijelovi.FirstOrDefault(d => d.Id == id);
            if (dio == null) return NotFound();

            dio.DeletedAt = DateTime.UtcNow;
            _ctx.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("kategorija/{kategorija}")]
        public IActionResult PrioCategory(string kategorija)
        {
            if (!Enum.TryParse(typeof(OdrzavanjeVozila.Tools.KategorijaDijela), kategorija, true, out var parsed))
                return BadRequest("Nepoznata kategorija");

            var k = (OdrzavanjeVozila.Tools.KategorijaDijela)parsed!;
            var dijelovi = _ctx.Dijelovi.Where(d => d.Kategorija == k).ToList();
            return View(dijelovi);
        }

        private bool IsDuplicate(DioCreateModel model, int? excludeId = null)
        {
            return _ctx.Dijelovi.Any(d =>
                (!excludeId.HasValue || d.Id != excludeId.Value) &&
                d.Naziv == model.Naziv &&
                d.KatalogBroj == model.KatalogBroj &&
                d.Proizvodac == model.Proizvodac &&
                d.Cijena == model.Cijena &&
                d.Kategorija == model.Kategorija &&
                d.KolicinaNaSkladistu == model.KolicinaNaSkladistu &&
                d.Opis == model.Opis);
        }
    }
}