## Controller

### Notas
1	. Quando existir um navegation property e um modelState.IsValid for false, o EF Core tentará validar o objeto relacionado. Para evitar isso
devemos adicionar o ModelState.Remove("Classe") ou Criamos um [ValidadeNever] no Model. Ele pede o MVC.Core nugget package mas este está descontinado e foi migrado para AspnetCore.app.
Devemos ir ao Domain e adicionar o nugget package do AspNetCore.App. EXEMPLO:
<ItemGroup>
		<FrameworkReference Include="Microsoft.AspNetCore.App" />
	</ItemGroup>
1. Podemos adicionar TempData[""] para mandar mensagens temporarias para a view. No caso de um redirecionamento ou atualiza;ao, a mensagem é perdida.										
.Para usufruir do navegation property , devemos adicionar o Include() no método que busca os dados no banco de dados. Exemplo: _context.VillaFracoes.Include(v => v.Villa).ToList();	
	* Assim , o EF Core carregará os dados da villa juntamente com os dados da villa fracao. Podemos depois usar outros campos da villa na view como por exemplo nome e outros campos


## Model
1. Colocar o model a trabalhar 
## Views

### Tag Helpers
1.	asp-for: Este ajudante de tag é usado para vincular uma propriedade do modelo a um campo de formulário. Por exemplo, se você tem um modelo com uma propriedade Name, você pode vinculá-lo a uma caixa de texto assim: <input asp-for="Name" />. Quando o formulário é enviado, o valor inserido na caixa de texto será atribuído à propriedade Name do modelo.
2.	asp-validation-for: Este ajudante de tag é usado para exibir mensagens de erro de validação para uma propriedade específica do modelo. Por exemplo, <span asp-validation-for="Name"></span> exibirá quaisquer erros de validação para a propriedade Name.
3.	asp-items: Este ajudante de tag é usado com elementos select para gerar elementos option. O valor deve ser um IEnumerable<SelectListItem>. Por exemplo, se você tem uma lista de opções para uma lista suspensa, você pode vinculá-la assim: <select asp-for="Option" asp-items="Model.Options"></select>.
4.	asp-route-id: Este ajudante de tag é usado para passar um parâmetro para uma rota. Por exemplo, se você tem um link para uma página de detalhes que precisa do ID de um item, você pode usá-lo assim: <a asp-action="Details" asp-route-id="@Model.Id">Details</a>. Isso gerará um link como /Details/5, supondo que o Id seja 5.
5.	asp-action e asp-controller: Eles são usados para gerar URLs para ações e controladores específicos. Por exemplo, <a asp-action="Edit" asp-controller="Home">Edit</a> gerará um link para a ação Edit do controlador Home.
6.	asp-area: É usado para gerar URLs para áreas específicas em uma aplicação ASP.NET Core MVC. Por exemplo, <a asp-area="Admin" asp-controller="Home" asp-action="Index">Admin Home</a> gerará um link para a ação Index do controlador Home na área Admin.
7.	asp-route: É usado para gerar URLs com nomes de rota específicos. Por exemplo, <a asp-route="default">Home</a> gerará um link para a rota chamada default.

### Notas

1.	Devemos fazer bind do model diretamente na view e ViewBags e ViewData devem ser evitados. Assim, o que passa para o view está disponivel no model. Assim é uma maneira mais limpa de passar dados para a view.
	* Para tal, criamos um ViewModel que contém o model e os dados que queremos passar para a view.
	* Exemplo:     public class VillaFracaoVM
    {
        public VillaFracao? VillaFracao { get; set; } //Propriedade que armazena a villa fracao que será exibida na view

        [ValidateNever] //Validação nunca é executada para essa propriedade
        public IEnumerable<SelectListItem>? VillaLista { get; set; } //
    }
	* No controller, passamos o ViewModel para a view. Assim temos acesso ao model e aos dados que queremos passar para a view.

2.	

### Shared
1	. Notificação de erro com Toastr usando partial view _notification com CSS do toastr no _Layout.cshtml
### _imports
1	. Colocamos a localiza;ao @using CardosoResort.Domain.Entities para chamar as entidades do domínio. Assim as views podem acessar as entidades do domínio diretamente.

## HTML
### Notas
1. @item.id vs @Html.DisplayFor(modelItem=> item.Villa.id). O primeiro é mais rápido e o segundo é mais seguro e e possivel de ser usado em formata;oes etc.
### Ideias

##  Program.cs
1	. appSettings.json - Configurações da aplicação. Foi adicionado a configuração de conexão com o banco de dados.
1	. Foi adicionado project Reference para o projeto de Infraestrutura.

### Services
1	. Adicionei ApplicationDbContext para fazer a conexão com o banco de dados.
### Pipeline
	* Request pipeline significa quando uma requisição é feita, ela passa por uma série de middlewares antes de chegar ao controller.
	

## Clean Architecture
1. **Use Cases**: São as classes que representam as regras de negócio da aplicação. Elas são as classes que representam as operações que a aplicação executa.
1. **Interface Adapters**: São as classes que representam as interfaces da aplicação. Elas são as classes que representam as interfaces que a aplicação expõe.
1. **Frameworks and Drivers**: São as classes que representam os frameworks e drivers da aplicação. Elas são as classes que representam os frameworks e drivers que a aplicação utiliza.

### Domain Layer
1. **Entities**: São as classes que representam as entidades do domínio da aplicação. Elas são as classes que representam os objetos que a aplicação manipula.
1. 

### Infrastructure Layer
1	. Criar base de dados por add-migration e update-database, temos que alterar o default project para o projeto de infraestrutura no console do gerenciador de pacotes.
1	. Nunca alterar migration folder. Se queremos alterar propriedades ou outros, devemos adicionar uma nova migration.
1. Adicionar modelBuilder.Entity<Villa>().HasData() para adicionar dados iniciais no banco de dados.


### Nugget Packages
1. **Microsoft.EntityFrameworkCore.SqlServer**
1. **Microsoft.EntityFrameworkCore.Design**
1. **Microsoft.EntityFrameworkCore.Tools**

### Notas sobre Clean Architecture
Adicionei Class files para cada camada (Domain, Application, Infrastructure)
Temos que referenciar o projeto de infraestrutura no projeto de aplicação para que possamos usar o ApplicationDbContext.

## Nugget Packages
1. **Microsoft.EntityFrameworkCore.SqlServer**
1. **Microsoft.EntityFrameworkCore.Design**
