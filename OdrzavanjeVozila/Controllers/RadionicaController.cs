using Microsoft.AspNetCore.Mvc;
using OdrzavanjeVozila.Repositories;

namespace OdrzavanjeVozila.Controllers
{
    public class RadionicaController : Controller
    {
        private readonly RadionicaMockRepository _radionicaRepo;
        private readonly MehanicarMockRepository _mehanicarRepo;

        public RadionicaController(RadionicaMockRepository radionicaRepo, MehanicarMockRepository mehanicarRepo)
        {
            _radionicaRepo = radionicaRepo;
            _mehanicarRepo = mehanicarRepo;
        }

        public IActionResult Index()
        {
            var radionice = _radionicaRepo.GetAll();
            return View(radionice);
        }

        public IActionResult Details(int id)
        {
            var radionica = _radionicaRepo.GetById(id);
            if (radionica == null) return NotFound();
            radionica.Mehanicari = _mehanicarRepo.GetByRadionicaId(id);
            return View(radionica);
        }
    }
}