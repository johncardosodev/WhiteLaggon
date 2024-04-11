using CardosoResort.Application.Common.Interfaces;
using CardosoResort.Domain.Entities;
using CardosoResort.Infrastructure.Data;
using CardosoResort.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//########################################################################################################################################## Serviços
// Add services to the container.
builder.Services.AddControllersWithViews();
/*O código selecionado adiciona um serviço de contexto de banco de dados ao contêiner de serviços da aplicação. Especificamente, está adicionando um contexto de banco de dados do Entity Framework Core usando o provedor SQL Server. O contexto de banco de dados é responsável por interagir com o banco de dados, executar consultas e gerenciar as entidades do banco de dados.
A expressão option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")) configura o provedor SQL Server para o contexto de banco de dados. Ele usa a conexão padrão chamada "DefaultConnection" definida na configuração da aplicação para estabelecer a conexão com o banco de dados.
Essa configuração permite que a aplicação acesse e manipule os dados do banco de dados usando o Entity Framework Core.*/
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); //Adicionamos o serviço de UnitOfWork ao contêiner de serviços da aplicação. O UnitOfWork é responsável por agrupar todas as operações

//Alteracao para IUnitOfWork
//builder.Services.AddScoped<IVillaRepository, VillaRepository>(); //Adicionamos o serviço de repositório de Villa ao contêiner de serviços da aplicação. O repositório de Villa é responsável por interagir com a entidade Villa no banco de dados.

/*O código selecionado está adicionando serviços relacionados à autenticação e autorização no contêiner de serviços da aplicação.
A linha builder.Services.AddIdentity<IdentityUser, IdentityRole>() adiciona o serviço de identidade à aplicação. A identidade é responsável por gerenciar usuários, autenticação e autorização. Neste caso, está sendo utilizado o IdentityUser como modelo para representar os usuários e o IdentityRole para representar os papéis de autorização.
Em seguida, .AddEntityFrameworkStores<ApplicationDbContext>() configura o serviço de armazenamento da identidade para usar o ApplicationDbContext como o contexto de banco de dados. Isso permite que a identidade armazene e recupere informações relacionadas aos usuários e papéis no banco de dados.
Por fim, .AddDefaultTokenProviders() adiciona os provedores de token padrão à identidade. Os provedores de token são responsáveis por gerar e validar tokens de autenticação, que são usados para autenticar usuários em solicitações subsequentes.
Em resumo, esse trecho de código configura a autenticação e autorização da aplicação, permitindo que os usuários se autentiquem, sejam autorizados a acessar recursos específicos e gerencia seus dados de identidade.
*/

builder.Services.AddIdentity<ApplicationUser, IdentityRole>() //Adicionamos o serviço de identidade à aplicação, utilizando a classe ApplicationUser como modelo de usuário e IdentityRole como modelo de papel.
    .AddEntityFrameworkStores<ApplicationDbContext>() //Configuramos o serviço de armazenamento da identidade para usar o ApplicationDbContext como contexto de banco de dados.
    .AddDefaultTokenProviders(); //Adicionamos os provedores de token padrão à identidade.

/*O código selecionado está configurando o cookie de aplicação para a autenticação e autorização da aplicação.
Através do método ConfigureApplicationCookie, estamos definindo algumas opções para o cookie.
A propriedade AccessDeniedPath especifica o caminho para redirecionar o usuário caso ele tente acessar uma página para a qual não possui permissão. Neste caso, o caminho definido é "/Conta/AcessoNegado".
A propriedade LoginPath especifica o caminho para redirecionar o usuário caso ele precise fazer login para acessar uma determinada página. Neste caso, o caminho definido é "/Conta/Login".
Essas configurações são importantes para controlar o acesso dos usuários e direcioná-los para as páginas corretas em caso de permissões insuficientes ou necessidade de autenticação.*/

builder.Services.ConfigureApplicationCookie(option =>
{
    option.AccessDeniedPath = "/Conta/AcessoNegado";
    option.LoginPath = "/Conta/Login";
});

//Configuração de password
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

//########################################################################################################################################## Configuração
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