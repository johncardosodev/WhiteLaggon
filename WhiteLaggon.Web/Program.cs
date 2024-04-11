using CardosoResort.Application.Common.Interfaces;
using CardosoResort.Domain.Entities;
using CardosoResort.Infrastructure.Data;
using CardosoResort.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//########################################################################################################################################## Servi�os
// Add services to the container.
builder.Services.AddControllersWithViews();
/*O c�digo selecionado adiciona um servi�o de contexto de banco de dados ao cont�iner de servi�os da aplica��o. Especificamente, est� adicionando um contexto de banco de dados do Entity Framework Core usando o provedor SQL Server. O contexto de banco de dados � respons�vel por interagir com o banco de dados, executar consultas e gerenciar as entidades do banco de dados.
A express�o option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")) configura o provedor SQL Server para o contexto de banco de dados. Ele usa a conex�o padr�o chamada "DefaultConnection" definida na configura��o da aplica��o para estabelecer a conex�o com o banco de dados.
Essa configura��o permite que a aplica��o acesse e manipule os dados do banco de dados usando o Entity Framework Core.*/
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); //Adicionamos o servi�o de UnitOfWork ao cont�iner de servi�os da aplica��o. O UnitOfWork � respons�vel por agrupar todas as opera��es

//Alteracao para IUnitOfWork
//builder.Services.AddScoped<IVillaRepository, VillaRepository>(); //Adicionamos o servi�o de reposit�rio de Villa ao cont�iner de servi�os da aplica��o. O reposit�rio de Villa � respons�vel por interagir com a entidade Villa no banco de dados.

/*O c�digo selecionado est� adicionando servi�os relacionados � autentica��o e autoriza��o no cont�iner de servi�os da aplica��o.
A linha builder.Services.AddIdentity<IdentityUser, IdentityRole>() adiciona o servi�o de identidade � aplica��o. A identidade � respons�vel por gerenciar usu�rios, autentica��o e autoriza��o. Neste caso, est� sendo utilizado o IdentityUser como modelo para representar os usu�rios e o IdentityRole para representar os pap�is de autoriza��o.
Em seguida, .AddEntityFrameworkStores<ApplicationDbContext>() configura o servi�o de armazenamento da identidade para usar o ApplicationDbContext como o contexto de banco de dados. Isso permite que a identidade armazene e recupere informa��es relacionadas aos usu�rios e pap�is no banco de dados.
Por fim, .AddDefaultTokenProviders() adiciona os provedores de token padr�o � identidade. Os provedores de token s�o respons�veis por gerar e validar tokens de autentica��o, que s�o usados para autenticar usu�rios em solicita��es subsequentes.
Em resumo, esse trecho de c�digo configura a autentica��o e autoriza��o da aplica��o, permitindo que os usu�rios se autentiquem, sejam autorizados a acessar recursos espec�ficos e gerencia seus dados de identidade.
*/

builder.Services.AddIdentity<ApplicationUser, IdentityRole>() //Adicionamos o servi�o de identidade � aplica��o, utilizando a classe ApplicationUser como modelo de usu�rio e IdentityRole como modelo de papel.
    .AddEntityFrameworkStores<ApplicationDbContext>() //Configuramos o servi�o de armazenamento da identidade para usar o ApplicationDbContext como contexto de banco de dados.
    .AddDefaultTokenProviders(); //Adicionamos os provedores de token padr�o � identidade.

/*O c�digo selecionado est� configurando o cookie de aplica��o para a autentica��o e autoriza��o da aplica��o.
Atrav�s do m�todo ConfigureApplicationCookie, estamos definindo algumas op��es para o cookie.
A propriedade AccessDeniedPath especifica o caminho para redirecionar o usu�rio caso ele tente acessar uma p�gina para a qual n�o possui permiss�o. Neste caso, o caminho definido � "/Conta/AcessoNegado".
A propriedade LoginPath especifica o caminho para redirecionar o usu�rio caso ele precise fazer login para acessar uma determinada p�gina. Neste caso, o caminho definido � "/Conta/Login".
Essas configura��es s�o importantes para controlar o acesso dos usu�rios e direcion�-los para as p�ginas corretas em caso de permiss�es insuficientes ou necessidade de autentica��o.*/

builder.Services.ConfigureApplicationCookie(option =>
{
    option.AccessDeniedPath = "/Conta/AcessoNegado";
    option.LoginPath = "/Conta/Login";
});

//Configura��o de password
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
});

//########################################################################################################################################## Configura��o
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();