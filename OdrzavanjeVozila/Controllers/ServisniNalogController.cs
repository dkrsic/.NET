using Microsoft.AspNetCore.Mvc;
using OdrzavanjeVozila.Repositories;


namespace OdrzavanjeVozila.Controllers
{
    public class ServisniNalogController : Controller
    {
        private readonly ServisniNalogMockRepository _nalogRepo;
        private readonly AutomobilMockRepository _automobilRepo;
        private readonly MehanicarMockRepository _mehanicarRepo;

        public ServisniNalogController(ServisniNalogMockRepository nalogRepo, AutomobilMockRepository automobilRepo, MehanicarMockRepository mehanicarRepo)
        {
            _nalogRepo = nalogRepo;
            _automobilRepo = automobilRepo;
            _mehanicarRepo = mehanicarRepo;
        }

        public IActionResult Index()
        {
            var nalozi = _nalogRepo.GetAll();
            foreach (var n in nalozi)
            {
                n.Automobil = _automobilRepo.GetById(n.AutomobilId);
                n.Mehanicar = _mehanicarRepo.GetById(n.MehanicarId);
            }
            return View(nalozi);
        }

        public IActionResult Details(int id)
        {
            var nalog = _nalogRepo.GetById(id);
            if (nalog == null) return NotFound();
            nalog.Automobil = _automobilRepo.GetById(nalog.AutomobilId);
            nalog.Mehanicar = _mehanicarRepo.GetById(nalog.MehanicarId);
            return View(nalog);
        }
    }
}