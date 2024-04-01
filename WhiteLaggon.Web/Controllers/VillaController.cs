using CardosoResort.Domain.Entities;
using CardosoResort.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace CardosoResort.Web.Controllers
{
    public class VillaController : Controller
    {
        //Com .net Core o contexto do banco de dados é injetado no controlador por meio do construtor
        //Portanto apenas buscamos o DB que já foi configurado no Program.cs e buscamos a implementação do DbContext
        //O que vai configurar a connectionString, abrir connexao e dar a connexao
        private readonly ApplicationDbContext _db;

        public VillaController(ApplicationDbContext db)
        {
            //Aqui, Dependency Injection é usada para injetar o contexto do banco de dados no controlador
            _db = db;
        }

        public IActionResult Index()
        {
            var villas = _db.Villas.ToList();
            return View(villas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa villa)
        {
            if (villa.Nome == villa.Descricao)
            {
                ModelState.AddModelError("Descricao", "O nome e a descrição não podem ser iguais");
            }
            if (ModelState.IsValid)
            {
                _db.Villas.Add(villa);
                _db.SaveChanges();
                return RedirectToAction("Index"); //Se o modelo for válido, redirecionamos para a página de índice
            }
            return View(); //Se o modelo não for válido mandar de volta para a página de criação
        }

        public IActionResult Atualizar(int villaId)
        {
            Villa? villa = _db.Villas.FirstOrDefault(v => v.Id == villaId); //Buscamos a villa pelo id
            if (villa == null) //Se a villa não for encontrada, retornamos um erro 404
            {
                return RedirectToAction("Error", "Home");
            }

            return View(villa);
        }

        [HttpPost]
        public IActionResult Atualizar(Villa villa)
        {
            if (villa.Nome == villa.Descricao)
            {
                ModelState.AddModelError("Descricao", "O nome e a descrição não podem ser iguais");
            }
            if (ModelState.IsValid)
            {
                _db.Villas.Update(villa);
                _db.SaveChanges();
                return RedirectToAction("Index"); //Se o modelo for válido, redirecionamos para a página de índice
            }
            return View(); //Se o modelo não for válido mandar de volta para a página de criação
        }

        public IActionResult Apagar(int villaId)
        {
            Villa? villa = _db.Villas.FirstOrDefault(v => v.Id == villaId); //Buscamos a villa pelo id
            if (villa is null) //Se a villa não for encontrada, retornamos um erro 404
            {
                return RedirectToAction("Error", "Home");
            }
            else
            {
                return View(villa);
            }
        }

        [HttpPost]
        public IActionResult Apagar(Villa villa)
        {
            if (villa is not null)
            {
                _db.Villas.Remove(villa);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}