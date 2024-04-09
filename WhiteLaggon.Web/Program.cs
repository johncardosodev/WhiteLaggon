using CardosoResort.Application.Common.Interfaces;
using CardosoResort.Infrastructure.Data;
using CardosoResort.Infrastructure.Repository;
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