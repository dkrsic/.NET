using Microsoft.AspNetCore.Mvc;
using OdrzavanjeVozila.Repositories;

namespace OdrzavanjeVozila.Controllers
{
    public class DioController : Controller
    {
        private readonly DioMockRepository _dioRepo;

        public DioController(DioMockRepository dioRepo)
        {
            _dioRepo = dioRepo;
        }

        public IActionResult Index()
        {
            var dijelovi = _dioRepo.GetAll();
            return View(dijelovi);
        }

        public IActionResult Details(int id)
        {
            var dio = _dioRepo.GetById(id);
            if (dio == null) return NotFound();
            return View(dio);
        }
    }
}