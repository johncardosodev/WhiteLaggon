using CardosoResort.Application.Common.Interfaces;
using CardosoResort.Application.Common.Utility;
using CardosoResort.Domain.Entities;
using CardosoResort.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CardosoResort.Web.Controllers
{
    public class ContaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;//IUnitOfWork é uma interface que contém métodos para manipular o banco de dados
        private readonly UserManager<ApplicationUser> _userManager; //UserManager é uma classe do Identity que contém métodos para manipular usuários
        private readonly SignInManager<ApplicationUser> _signInManager; //SignInManager é uma classe do Identity que contém métodos para autenticar usuários. Responsável por gerenciar o login do usuário

        private readonly RoleManager<IdentityRole> _roleManager; //RoleManager é uma classe do Identity que contém métodos para manipular roles

        public ContaController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Login(string redireccionarURL = null) //O parâmetro returnUrl é utilizado para redirecionar o usuário para a página que ele estava antes de ser redirecionado para a página de login
        {
            if (Request.Query.ContainsKey("ReturnUrl"))
            {
                redireccionarURL = Request.Query["ReturnUrl"].ToString();
            }

            LoginVM loginVM = new LoginVM
            {
                RedireccionarURL = redireccionarURL //Atribui a URL para onde o usuário vai ser redirecionado após o login
            };
            return View(loginVM); //Retorna a view de login
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                //O método PasswordSignInAsync é chamado no objeto _signInManager para autenticar o usuário com o email e a senha fornecidos no loginVM. Ele retorna uma tarefa (Task) que representa a operação assíncrona de autenticar o usuário.
                var result = await _signInManager //
                    .PasswordSignInAsync(loginVM.Email, loginVM.Password, loginVM.LembrarMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {   //pY9h@Hj$ZS
                    string nome = _signInManager.Context.User.Identity.Name;

                    TempData["SucessoLogin"] = $"Bem-vindo {nome}";
                    if (string.IsNullOrEmpty(loginVM.RedireccionarURL))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return LocalRedirect(loginVM.RedireccionarURL);
                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login inválido");
                TempData["ErroLogin"] = "Login inválido";
            }
            return View(loginVM); //Retorna a view de login
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync(); //O método SignOutAsync é chamado no objeto _signInManager para desautenticar o usuário. Ele retorna uma tarefa (Task) que representa a operação assíncrona de desautenticar o usuário.
            TempData["SucessoLogout"] = "Logout efetuado com sucesso";
            return RedirectToAction("Index", "Home"); //Redireciona o usuário para a página inicial do site
        }

        public IActionResult AcessoNegado()
        {
            return View(); //Retorna a view de acesso negado
        }

        public IActionResult Registar()
        {
            /*explicação passo a passo do que está acontecendo:
1.	_roleManager.RoleExistsAsync("Administrador"): O método RoleExistsAsync é chamado no objeto _roleManager para verificar se o papel chamado "Administrador" existe no banco de dados. Esse método retorna uma tarefa (Task) que representa a operação assíncrona de verificar a existência do papel.
2.	.GetAwaiter(): O método GetAwaiter() é chamado na tarefa retornada pelo método RoleExistsAsync. Ele retorna um objeto awaiter que permite aguardar a conclusão da tarefa assíncrona.
3.	.GetResult(): O método GetResult() é chamado no awaiter para obter o resultado da tarefa assíncrona. Nesse caso, ele bloqueia a execução do código até que a tarefa seja concluída e retorna o resultado.
4.	!: O operador de negação é usado para inverter o resultado retornado pelo método RoleExistsAsync. Se o papel "Administrador" não existir, o resultado será false, e a condição do if será verdadeira*/

            //Verifica se a role Admin e User existem no banco de dados e cria se não existirem na tabela AspNetRoles
            if (!_roleManager.RoleExistsAsync(SD.Role_Administrador).GetAwaiter().GetResult())
            {//Cria um role Admin no banco de dados se ele não existir
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Administrador)).Wait();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Utilizador)).Wait();
            }

            RegistoVM registoVM = new RegistoVM
            {
                //Obtém a lista de roles disponíveis no sistema e atribui à propriedade RolesLista
                RolesLista = _roleManager.Roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
            };
            return View(registoVM);
        }

        [HttpPost]
        public async Task<IActionResult> Registar(RegistoVM registoVM)
        {
            if (ModelState.IsValid) //Se o modelo for válido
            {
                //Criamos um objeto ApplicationUser com os dados do registoVM para ser inserido na tabela AspNetUsers
                ApplicationUser user = new ApplicationUser
                {
                    //Campos vem da tabela AspNetUsers
                    UserName = registoVM.Email,
                    Email = registoVM.Email,
                    Nome = registoVM.Nome,
                    PhoneNumber = registoVM.Telemovel,
                    Data_Criacao = DateTime.Now,
                    NormalizedUserName = registoVM.Email.ToUpper(),
                    NormalizedEmail = registoVM.Email.ToUpper()
                };

                //Cria um utilizador na tabela AspNetUsers com o helper
                var result = await _userManager.CreateAsync(user, registoVM.Password); //O método CreateAsync cria um novo usuário na tabela AspNetUsers com os dados fornecidos no objeto ApplicationUser e a senha fornecida no parâmetro password. Ele retorna uma tarefa (Task) que representa a operação assíncrona de criar o usuário.

                if (result.Succeeded)//Se o usuário for criado com sucesso
                {
                    if (!string.IsNullOrEmpty(registoVM.Role))
                    {
                        //Adiciona o utilizador à role selecionada
                        await _userManager.AddToRoleAsync(user, registoVM.Role);//O método AddToRoleAsync adiciona o usuário criado à role selecionada no parâmetro role. Ele retorna uma tarefa (Task) que representa a operação assíncrona de adicionar o usuário à role.
                    }
                    else
                    {
                        //Adiciona o utilizador à role Utilizador caso não tenha sido selecionada nenhuma role
                        await _userManager.AddToRoleAsync(user, SD.Role_Utilizador);
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);//O método SignInAsync autentica o usuário criado no sistema. Ele retorna uma tarefa (Task) que representa a operação assíncrona de autenticar o usuário. O parâmetro isPersistent indica se o cookie de autenticação deve ser persistente ou não.

                    if (string.IsNullOrEmpty(registoVM.RedireccionarURL)) //Se a propriedade RedireccionarURL for nula ou vazia
                    {
                        return RedirectToAction("Index", "Home"); //Redireciona o usuário para a página inicial do site
                    }
                    else
                    {
                        return LocalRedirect(registoVM.RedireccionarURL); //Redireciona o usuário para a URL fornecida no parâmetro RedireccionarURL pelo LocalRedirect (redireciona para uma URL local). Prevente ataques de redirecionamento aberto
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description); //Adiciona um erro ao ModelState para cada erro retornado pelo método CreateAsync
                }
            }

            //Se o modelo não for válido ou ocorrer algum erro, o objeto registoVM é retornado para a view

            //Objeto registoVM vai ser atualizado com a lista de roles disponíveis no sistema e o antigo registoVM vai ser mandado para garbage collection
            registoVM.RolesLista = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name
            });

            return View(registoVM);
        }
    }
}