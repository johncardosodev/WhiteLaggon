using CardosoResort.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
/*O c�digo selecionado adiciona um servi�o de contexto de banco de dados ao cont�iner de servi�os da aplica��o. Especificamente, est� adicionando um contexto de banco de dados do Entity Framework Core usando o provedor SQL Server. O contexto de banco de dados � respons�vel por interagir com o banco de dados, executar consultas e gerenciar as entidades do banco de dados.
A express�o option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")) configura o provedor SQL Server para o contexto de banco de dados. Ele usa a conex�o padr�o chamada "DefaultConnection" definida na configura��o da aplica��o para estabelecer a conex�o com o banco de dados.
Essa configura��o permite que a aplica��o acesse e manipule os dados do banco de dados usando o Entity Framework Core.*/
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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