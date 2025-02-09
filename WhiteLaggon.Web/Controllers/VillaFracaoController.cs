﻿using CardosoResort.Application.Common.Interfaces;
using CardosoResort.Domain.Entities;
using CardosoResort.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CardosoResort.Web.Controllers
{
    public class VillaFracaoController : Controller
    {
        //Usando UnitOfWork
        private readonly IUnitOfWork _unitOfWork; //Aqui estamos injetando a dependência do IUnitOfWork no controlador

        public VillaFracaoController(IUnitOfWork unitOfWork)
        {
            //Aqui, Dependency Injection é usada para injetar o contexto do banco de dados no controlador
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var villasFracoes = _unitOfWork.VillasFracao.GetAll(includeProperties: "Villa");//Aqui estamos chamando o método GetAll do repositório de VillaFracao para obter todas as villas fracao com a propriedade de navegação Villa carregada no includeProperties

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
                VillaLista = new SelectList(_unitOfWork.Villas.GetAll(), "Id", "Nome")
            };

            //Verificar se a base de dados tem alguma fracao
            if (_unitOfWork.VillasFracao.GetAll().Any())
            {
                //Iniciamos já a propriedade VillaFracao com o ultimo id+1 da base de dados
                villaFracaoVM.VillaFracao.Villa_Fracao = _unitOfWork.VillasFracao.GetAll().Max(vf => vf.Villa_Fracao) + 1;
            }

            return View(villaFracaoVM);
        }

        [HttpPost]
        public IActionResult Create(VillaFracaoVM objeto)
        {
            //ModelState.Remove("Villa"); //caso nao metemos [validadeNever] no propriedade  VillaFracao

            //Verificar se existe Villa_Fracao na base de dados com o mesmo numero da villaFracao
            bool villaFracaoExiste = _unitOfWork.VillasFracao.GetAll().Any(vf => vf.Villa_Fracao == objeto.VillaFracao.VillaId);
            if (villaFracaoExiste)
            {
                TempData["error"] = "Numero da Fração já existe"; //Usamos TempData para enviar uma mensagem de erro para a próxima solicitação
                ModelState.AddModelError("Villa_Fracao", "Numero da villa já existe"); //Adicionamos um erro ao modelo

                //Precisamos recarregar a lista de villas para o dropdownList após a validação, caso contrário, a lista será perdida
                //DropdownList para o ViewModel
                objeto.VillaLista = new SelectList(_unitOfWork.Villas.GetAll(), "Id", "Nome");
                return View(objeto); //Se o modelo não for válido mandar de volta para a página de criação
            }

            if (ModelState.IsValid && !villaFracaoExiste)
            {
                _unitOfWork.VillasFracao.Add(objeto.VillaFracao);
                _unitOfWork.VillasFracao.Save(); //Salvamos as alterações no banco de dados
                TempData["success"] = "Numero para a vila foi criada com sucesso"; //Usamos TempData para enviar uma mensagem de sucesso para a próxima solicitação
                return RedirectToAction(nameof(Index)); //Se o modelo for válido, redirecionamos para a página de índice
            }
            return View(); //Se o modelo não for válido mandar de volta para a página de criação
        }

        public IActionResult Atualizar(int villaFracaoId)
        {
            //DropdownList com viewModel
            VillaFracaoVM? villaFracaoVM = new VillaFracaoVM
            {
                VillaFracao = _unitOfWork.VillasFracao.Get(v => v.Villa_Fracao == villaFracaoId),
                VillaLista = new SelectList(_unitOfWork.Villas.GetAll(), "Id", "Nome")
            };

            //Verificar se a fracao foi encontrada
            if (villaFracaoVM.VillaFracao == null) //Se a fracao não for encontrada, retornamos um erro 404
            {
                return RedirectToAction("Error", "Home"); //Redirecionamos para a página de erro
            }

            return View(villaFracaoVM); //Se a fracao for encontrada, retornamos a view com o objeto
        }

        [HttpPost]
        public IActionResult Atualizar(VillaFracaoVM objeto)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.VillasFracao.Update(objeto.VillaFracao);
                _unitOfWork.VillasFracao.Save(); //Salvamos as alterações no banco de dados
                TempData["success"] = $"Fração {objeto.VillaFracao.Villa_Fracao} foi atualizada com sucesso"; //Usamos TempData para enviar uma mensagem de sucesso para a próxima solicitação
                return RedirectToAction(nameof(Index)); //Se o modelo for válido, redirecionamos para a página de índice
            }

            TempData["error"] = "Erro ao atualizar a fração"; //Usamos TempData para enviar uma mensagem de erro para a próxima solicitação
            objeto.VillaLista = new SelectList(_unitOfWork.Villas.GetAll(), "Id", "Nome"); //Recarregar a lista de villas para o dropdownList após a validação, caso contrário, a lista será perdida
            return View(objeto); //Se o modelo não for válido mandar de volta para a página de criação
        }

        public IActionResult Apagar(int villaFracaoId)
        {
            //DropdownList com viewModel
            VillaFracaoVM? villaFracaoVM = new VillaFracaoVM
            {
                VillaFracao = _unitOfWork.VillasFracao.Get(v => v.Villa_Fracao == villaFracaoId),
                VillaLista = new SelectList(_unitOfWork.Villas.GetAll(), "Id", "Nome")
            };

            //Verificar se a fracao foi encontrada
            if (villaFracaoVM.VillaFracao == null) //Se a fracao não for encontrada, retornamos um erro 404
            {
                return RedirectToAction("Error", "Home"); //Redirecionamos para a página de erro
            }

            return View(villaFracaoVM); //Se a fracao for encontrada, retornamos a view com o objeto
        }

        [HttpPost]
        public IActionResult Apagar(VillaFracaoVM objeto)
        {
            VillaFracao? objetoBd = _unitOfWork.VillasFracao.Get(v => v.Villa_Fracao == objeto.VillaFracao.Villa_Fracao);

            //Verificar se a fracao foi encontrada
            if (objetoBd is not null)
            {
                _unitOfWork.VillasFracao.Remove(objetoBd);
                _unitOfWork.VillasFracao.Save(); //Salvamos as alterações no banco de dados
                TempData["success"] = $"Fração {objeto.VillaFracao.Villa_Fracao} foi apagada com sucesso"; //Usamos TempData para enviar uma mensagem de sucesso para a próxima solicitação
                return RedirectToAction(nameof(Index)); //Se o modelo for válido, redirecionamos para a página de índice com o nameof(Index) para evitar erros de digitação
            }

            TempData["error"] = "Erro ao apagar a fração"; //Usamos TempData para enviar uma mensagem de erro para a próxima solicitação
            objeto.VillaLista = new SelectList(_unitOfWork.Villas.GetAll(), "Id", "Nome"); //Recarregar a lista de villas para o dropdownList após a validação, caso contrário, a lista será perdida
            return View(objeto); //Se o modelo não for válido mandar de volta para a página de criação
        }
    }
}