using CardosoResort.Application.Common.Interfaces;
using CardosoResort.Application.Common.Utility;
using CardosoResort.Domain.Entities;
using CardosoResort.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CardosoResort.Web.Controllers
{
    [Authorize(Roles = SD.Role_Administrador)] //Atributo que indica que apenas utilizadores com a role de administrador podem aceder a este controlador. Também pode ser aplicado a um método específico
    public class ExtrasController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExtrasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(int? dropDownVilla)
        {
            var extras = _unitOfWork.Extras.GetAll(includeProperties: "Villa").Where(e => dropDownVilla == null || e.VillaId == dropDownVilla).ToList();

            List<Villa> villas = _unitOfWork.Villas.GetAll().ToList(); // Fetch the villas from your database
            villas.Insert(0, new Villa { Id = 0, Nome = "Todas as villas" }); // Insert the new item at the beginning
            ViewBag.LISTAVILLAS = new SelectList(villas, "Id", "Nome"); // C

            ViewBag.LISTAVILLAS = new SelectList(_unitOfWork.Villas.GetAll(), "Id", "Nome"); //Aqui estamos criando um SelectList com o nome e o id das villas

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

        [HttpGet]
        public IActionResult Apagar(int extraId)
        {
            ExtraVM? extraVM = new ExtraVM
            {
                Extra = _unitOfWork.Extras.Get(e => e.Id == extraId),
                VillaLista = new SelectList(_unitOfWork.Villas.GetAll(), "Id", "Nome")
            };

            if (extraVM.Extra == null)
            {
                TempData["error"] = "Extra não encontrado";
                return RedirectToAction("Error", "Home");
            }
            return View(extraVM);
        }

        [HttpPost]
        public IActionResult Apagar(ExtraVM objeto)
        {
            Extra? objetoBd = _unitOfWork.Extras.Get(v => v.Id == objeto.Extra.Id);

            //Verificar se a fracao foi encontrada
            if (objetoBd is not null)
            {
                _unitOfWork.Extras.Remove(objetoBd);
                _unitOfWork.VillasFracao.Save(); //Salvamos as alterações no banco de dados
                TempData["success"] = $"Extra {objeto.Extra.Nome} foi apagado com sucesso"; //U
                return RedirectToAction(nameof(Index)); //Se o modelo for válido, redirecionamos para a página de índice com o nameof(Index) para evitar erros de digitação
            }

            TempData["error"] = "Erro ao apagar extra"; //Usamos TempData para enviar uma mensagem de erro para a próxima solicitação
            objeto.VillaLista = new SelectList(_unitOfWork.Villas.GetAll(), "Id", "Nome"); //Recarregar a lista de villas para o dropdownList após a validação, caso contrário, a lista será perdida
            return View(objeto); //Se o modelo não for válido mandar de volta para a página de criação
        }
    }
}