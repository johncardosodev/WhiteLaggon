using CardosoResort.Application.Common.Interfaces;
using CardosoResort.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardosoResort.Web.Controllers
{
    [Authorize] //Atributo que indica que apenas utilizadores autenticados podem aceder a este controlador
    public class VillaController : Controller
    {
        //##################################################################################################################################Agora usando IVillaRepository
        //private readonly IVillaRepository _villaRepo; //Aqui estamos injetando a dependência do IVillaRepository no controlador

        //Usando UnitOfWork
        private readonly IUnitOfWork _unitOfWork; //Aqui estamos injetando a dependência do IUnitOfWork no controlador

        private readonly IWebHostEnvironment _webHostingEnvironment; //Aqui estamos injetando a dependência do IWebHostEnvironment no controlador

        public VillaController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostingEnvironment)
        {
            //Aqui, Dependency Injection é usada para injetar o contexto do banco de dados no controlador
            _unitOfWork = unitOfWork;
            _webHostingEnvironment = webHostingEnvironment;
        }

        public IActionResult Index()
        {
            var villas = _unitOfWork.Villas.GetAll(); //Aqui estamos chamando o método GetAll do repositório de Villa para obter todas as villas
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
                if (villa.Imagem != null) //Se a imagem não for nula
                {
                    string nomeFicheiro = Guid.NewGuid().ToString() + Path.GetExtension(villa.Imagem.FileName); //Criamos um nome de ficheiro único para a imagem da villa
                    string caminho = Path.Combine(_webHostingEnvironment.WebRootPath, @"images\VillaImagens"); //Definimos o caminho onde a imagem será guardada no servidor web
                    using var ficheiroStream = new FileStream(Path.Combine(caminho, nomeFicheiro), FileMode.Create); //Criamos um ficheiro no servidor web  para guardar a imagem da villa com using para garantir que o ficheiro é fechado após o uso
                    villa.Imagem.CopyTo(ficheiroStream); //Copiamos a imagem para o ficheiro

                    villa.ImagemUrl = "/images/VillaImagens/" + nomeFicheiro; //Definimos a propriedade ImagemUrl da villa com o caminho da imagem
                }
                else
                {
                    villa.ImagemUrl = "https://placehold.co/600x400";
                }

                _unitOfWork.Villas.Add(villa); //Adicionamos a villa ao repositório
                _unitOfWork.Villas.Save(); //Salvamos as alterações no banco de dados

                TempData["success"] = "Villa foi criada com sucesso"; //Usamos TempData para enviar uma mensagem de sucesso para a próxima solicitação
                return RedirectToAction(nameof(Index)); //Se o modelo for válido, redirecionamos para a página de índice
            }
            TempData["error"] = "Erro ao criar a villa"; //Usamos TempData para enviar uma mensagem de erro para a próxima solicitação
            return View(); //Se o modelo não for válido mandar de volta para a página de criação
        }

        public IActionResult Atualizar(int villaId)
        {
            Villa? objeto = _unitOfWork.Villas.Get(v => v.Id == villaId); //Buscamos a villa pelo id usando o método Get do repositório
            if (objeto == null) //Se a villa não for encontrada, retornamos um erro 404
            {
                return RedirectToAction("Error", "Home");
            }

            return View(objeto);
        }

        [HttpPost]
        public IActionResult Atualizar(Villa villa)
        {
            if (villa.Nome == villa.Descricao)
            {
                ModelState.AddModelError("Descricao", "O nome e a descrição não podem ser iguais");
            }

            if (ModelState.IsValid && villa.Id > 0)
            {
                if (villa.Imagem != null) //Se a imagem não for nula
                {
                    string nomeFicheiro = Guid.NewGuid().ToString() + Path.GetExtension(villa.Imagem.FileName); //Criamos um nome de ficheiro único para a imagem da villa
                    string caminho = Path.Combine(_webHostingEnvironment.WebRootPath, @"images\VillaImagens"); //Definimos o caminho onde a imagem será guardada no servidor web

                    //Sempre que atualizamos, devemos excluir a imagem antiga
                    if (!string.IsNullOrEmpty(villa.ImagemUrl)) //Se a propriedade ImagemUrl da villa não for nula ou vazia
                    {
                        var caminhoImagemAntiga = Path.Combine(_webHostingEnvironment.WebRootPath, villa.ImagemUrl.TrimStart('/')); //Definimos o caminho da imagem antiga

                        if (System.IO.File.Exists(caminhoImagemAntiga))
                        //Se o ficheiro existir, excluímos o ficheiro antigo
                        {
                            System.IO.File.Delete(caminhoImagemAntiga);
                        }
                    }

                    using var ficheiroStream = new FileStream(Path.Combine(caminho, nomeFicheiro), FileMode.Create); //Criamos um ficheiro no servidor web  para guardar a imagem da villa com using para garantir que o ficheiro é fechado após o uso
                    villa.Imagem.CopyTo(ficheiroStream); //Copiamos a imagem para o ficheiro

                    villa.ImagemUrl = "/images/VillaImagens/" + nomeFicheiro; //Definimos a propriedade ImagemUrl da villa com o caminho da imagem
                }
                //Nao queremos substituir a imagem antiga se o utilizador não carregar uma nova imagem
                //else
                //{
                //    villa.ImagemUrl = "https://placehold.co/600x400";
                //}

                _unitOfWork.Villas.Update(villa); //Atualizamos a villa no repositório
                _unitOfWork.Villas.Save(); //Salvamos as alterações no banco de dados
                TempData["success"] = "Villa foi atualizada com sucesso"; //Usamos TempData para enviar uma mensagem de sucesso para a próxima solicitação
                return RedirectToAction(nameof(Index)); //Se o modelo for válido, redirecionamos para a página de índice
            }

            TempData["error"] = "Erro ao atualizar a villa"; //Usamos TempData para enviar uma mensagem de erro para a próxima solicitação
            return View(); //Se o modelo não for válido mandar de volta para a página de criação
        }

        public IActionResult Apagar(int villaId)
        {
            Villa? villa = _unitOfWork.Villas.Get(v => v.Id == villaId); //Buscamos a villa pelo id
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
            Villa? villaDb = _unitOfWork.Villas.Get(v => v.Id == villa.Id); //Buscamos a villa pelo id
            if (villaDb is not null)
            {
                //Sempre que atualizamos, devemos excluir a imagem antiga
                if (!string.IsNullOrEmpty(villa.ImagemUrl)) //Se a propriedade ImagemUrl da villa não for nula ou vazia
                {
                    var caminhoImagem = Path.Combine(_webHostingEnvironment.WebRootPath, villa.ImagemUrl.TrimStart('/')); //Definimos o caminho da imagem

                    if (System.IO.File.Exists(caminhoImagem))
                    //Se o ficheiro existir, excluímos o ficheiro antigo
                    {
                        System.IO.File.Delete(caminhoImagem);
                    }
                }

                _unitOfWork.Villas.Remove(villaDb); //Removemos a villa do repositório
                _unitOfWork.Villas.Save(); //Salvamos as alterações no banco de dados
                TempData["success"] = "Villa foi removida com sucesso"; //Usamos TempData para enviar uma mensagem de sucesso para a próxima solicitação
                //palavra success tem que ser igual a que está no arquivo _Layout.cshtml
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Villa não encontrada"; //Usamos TempData para enviar uma mensagem de erro para a próxima solicitação
            return View();
        }
    }
}