using CardosoResort.Application.Common.Interfaces;
using CardosoResort.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CardosoResort.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            PaginaPrincipalVM paginaPrincipalVM = new()
            {
                VillaLista = _unitOfWork.Villas.GetAll(includeProperties: "VillaExtra"),//Obtemos todas as villas do banco de dados e incluímos os extras relacionados no includeProperties
                CheckInDate = DateOnly.FromDateTime(DateTime.Now),
                Noites = 1
            };
            return View(paginaPrincipalVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}