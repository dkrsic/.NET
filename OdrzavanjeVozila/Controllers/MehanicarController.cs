using Microsoft.AspNetCore.Mvc;
using OdrzavanjeVozila.Repositories;

namespace OdrzavanjeVozila.Controllers
{
    public class MehanicarController : Controller
    {
        private readonly MehanicarMockRepository _mehanicarRepo;
        private readonly RadionicaMockRepository _radionicaRepo;
        private readonly ServisniNalogMockRepository _nalogRepo;

        public MehanicarController(MehanicarMockRepository mehanicarRepo, RadionicaMockRepository radionicaRepo, ServisniNalogMockRepository nalogRepo)
        {
            _mehanicarRepo = mehanicarRepo;
            _radionicaRepo = radionicaRepo;
            _nalogRepo = nalogRepo;
        }

        public IActionResult Index()
        {
            var mehanicari = _mehanicarRepo.GetAll();
            foreach (var m in mehanicari)
                m.Radionica = _radionicaRepo.GetById(m.RadionicaId);
            return View(mehanicari);
        }

        public IActionResult Details(int id)
        {
            var mehanicar = _mehanicarRepo.GetById(id);
            if (mehanicar == null) return NotFound();
            mehanicar.Radionica = _radionicaRepo.GetById(mehanicar.RadionicaId);
            mehanicar.Nalozi = _nalogRepo.GetByMehanicarId(id);
            foreach (var n in mehanicar.Nalozi)
                n.Automobil = new AutomobilMockRepository().GetById(n.AutomobilId);
            return View(mehanicar);
        }
    }
}