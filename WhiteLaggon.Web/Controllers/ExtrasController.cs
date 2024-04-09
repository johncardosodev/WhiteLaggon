using CardosoResort.Application.Common.Interfaces;
using CardosoResort.Domain.Entities;
using CardosoResort.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CardosoResort.Web.Controllers
{
    public class ExtrasController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExtrasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var extras = _unitOfWork.Extras.GetAll(includeProperties: "Villa");

            //ViewBag.VillasLista = new SelectList(_unitOfWork.Villas.GetAll(), "Id", "Nome"); //Aqui estamos criando um SelectList com o nome e o id das villas

            return View(extras);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ExtraVM extraVM = new ExtraVM
            {
                Extra = new Extra(),
                VillaLista = new SelectList(_unitOfWork.Villas.GetAll(), "Id", "Nome")
            };
            return View(extraVM);
        }

        [HttpPost]
        public IActionResult Create(ExtraVM objeto)
        {
            bool extraExiste = _unitOfWork.Extras.GetAll().Any(e => e.Nome == objeto.Extra.Nome && e.VillaId == objeto.Extra.VillaId);
            if (extraExiste)
            {
                TempData["error"] = $"Extra {objeto.Extra.Nome} já existe na mesma Villa";
                ModelState.AddModelError("Nome", "Extra já existe");
                objeto.VillaLista = new SelectList(_unitOfWork.Villas.GetAll(), "Id", "Nome");
                return View(objeto);
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Extras.Add(objeto.Extra);
                _unitOfWork.Save();
                TempData["success"] = $"Extra {objeto.Extra.Nome} adicionado com sucesso";
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public IActionResult Atualizar(int extraId)
        {
            ExtraVM? extraVM = new ExtraVM
            {
                Extra = _unitOfWork.Extras.Get(e => e.Id == extraId),
                VillaLista = new SelectList(_unitOfWork.Villas.GetAll(), "Id", "Nome")
            };

            if (extraVM.Extra == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(extraVM);
        }

        [HttpPost]
        public IActionResult Atualizar(ExtraVM objeto)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Extras.Update(objeto.Extra);
                _unitOfWork.Save();
                TempData["success"] = $"Extra {objeto.Extra.Nome} atualizado com sucesso";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Erro ao atualizar o extra";
            objeto.VillaLista = new SelectList(_unitOfWork.Villas.GetAll(), "Id", "Nome");
            return View();
        }
    }
}