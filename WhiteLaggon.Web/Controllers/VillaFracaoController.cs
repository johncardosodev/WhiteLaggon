using CardosoResort.Domain.Entities;
using CardosoResort.Infrastructure.Data;
using CardosoResort.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CardosoResort.Web.Controllers
{
    public class VillaFracaoController : Controller
    {
        //Com .net Core o contexto do banco de dados é injetado no controlador por meio do construtor
        //Portanto apenas buscamos o DB que já foi configurado no Program.cs e buscamos a implementação do DbContext
        //O que vai configurar a connectionString, abrir connexao e dar a connexao
        private readonly ApplicationDbContext _db;

        public VillaFracaoController(ApplicationDbContext db)
        {
            //Aqui, Dependency Injection é usada para injetar o contexto do banco de dados no controlador
            _db = db;
        }

        public IActionResult Index()
        {
            //Usamos o método Include para incluir a entidade relacionada Villa na consulta. Assim, podemos acessar as propriedades da entidade relacionada na visualização.
            //Até podemos usar outro Include para incluir mais entidades relacionadas
            var villasFracoes = _db.VillaFracoes.Include(u => u.Villa).ToList();
            return View(villasFracoes);
        }

        public IActionResult Create()
        {
            //DropdownList Com viewBag para a view
            //ViewBag.VILLAS = new SelectList(_db.Villas, "Id", "Nome");

            //Fazer o dropdownList com ViewModel sem usar ViewBag. É mais seguro e mais limpo segundo o livro
            VillaFracaoVM villaFracaoVM = new VillaFracaoVM //Instanciamos um novo objeto da classe VillaFracaoVM
            {
                VillaFracao = new VillaFracao(),
                VillaLista = new SelectList(_db.Villas, "Id", "Nome")
            };

            return View(villaFracaoVM);
        }

        [HttpPost]
        public IActionResult Create(VillaFracaoVM objeto)
        {
            //ModelState.Remove("Villa"); //caso nao metemos [validadeNever] no propriedade  VillaFracao

            //Verificar se existe Villa_Fracao na base de dados com o mesmo numero da villaFracao
            bool villaFracaoExiste = _db.VillaFracoes.Any(vf => vf.Villa_Fracao == objeto.VillaFracao.VillaId);
            if (villaFracaoExiste)
            {
                TempData["error"] = "Numero da Fração já existe"; //Usamos TempData para enviar uma mensagem de erro para a próxima solicitação
                ModelState.AddModelError("Villa_Fracao", "Numero da villa já existe"); //Adicionamos um erro ao modelo

                //Precisamos recarregar a lista de villas para o dropdownList após a validação, caso contrário, a lista será perdida
                //DropdownList para o ViewModel
                objeto.VillaLista = new SelectList(_db.Villas, "Id", "Nome");
                return View(objeto); //Se o modelo não for válido mandar de volta para a página de criação
            }

            if (ModelState.IsValid && !villaFracaoExiste)
            {
                _db.VillaFracoes.Add(objeto.VillaFracao);
                _db.SaveChanges();
                TempData["success"] = "Numero para a vila foi criada com sucesso"; //Usamos TempData para enviar uma mensagem de sucesso para a próxima solicitação
                return RedirectToAction("Index"); //Se o modelo for válido, redirecionamos para a página de índice
            }
            return View(); //Se o modelo não for válido mandar de volta para a página de criação
        }

        public IActionResult Atualizar(int villaFracaoId)
        {
            VillaFracao? villaFracaoObj = _db.VillaFracoes.FirstOrDefault(v => v.Villa_Fracao == villaFracaoId); //Buscamos a fraccao pelo id
            if (villaFracaoObj == null) //Se a fracao não for encontrada, retornamos um erro 404
            {
                return RedirectToAction("Error", "Home");
            }

            return View(villaFracaoObj);
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
                //      TempData["success"] = "Villa foi atualizada com sucesso"; //Usamos TempData para enviar uma mensagem de sucesso para a próxima solicitação
                return RedirectToAction("Index"); //Se o modelo for válido, redirecionamos para a página de índice
            }
            // TempData["error"] = "Erro ao atualizar a villa"; //Usamos TempData para enviar uma mensagem de erro para a próxima solicitação
            return View(); //Se o modelo não for válido mandar de volta para a página de criação
        }

        public IActionResult Apagar(int villaId)
        {
            Villa? villa = _db.Villas.FirstOrDefault(v => v.Id == villaId); //Buscamos a villa pelo id
            if (villa is null) //Usar o operador de nulidade para verificar se a villa é nula, evita overflow
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
            //
            Villa? villaDb = _db.Villas.FirstOrDefault(v => v.Id == villa.Id); //Buscamos a villa pelo id
            if (villaDb is not null)
            {
                _db.Villas.Remove(villaDb);
                _db.SaveChanges();
                //        TempData["success"] = "Villa foi removida com sucesso"; //Usamos TempData para enviar uma mensagem de sucesso para a próxima solicitação
                //palavra success tem que ser igual a que está no arquivo _Layout.cshtml
                return RedirectToAction("Index");
            }
            //    TempData["error"] = "Villa não encontrada"; //Usamos TempData para enviar uma mensagem de erro para a próxima solicitação
            return View();
        }
    }
}