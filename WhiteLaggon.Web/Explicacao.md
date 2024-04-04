## Controller

### Notas
1	. Quando existir um navegation property e um modelState.IsValid for false, o EF Core tentará validar o objeto relacionado. Para evitar isso
devemos adicionar o ModelState.Remove("Classe") ou Criamos um [ValidadeNever] no Model. Ele pede o MVC.Core nugget package mas este está descontinado e foi migrado para AspnetCore.app.
Devemos ir ao Domain e adicionar o nugget package do AspNetCore.App. EXEMPLO:
<ItemGroup>
		<FrameworkReference Include="Microsoft.AspNetCore.App" />
	</ItemGroup>
1. Podemos adicionar TempData[""] para mandar mensagens temporarias para a view. No caso de um redirecionamento ou atualiza;ao, a mensagem é perdida.




## Model
1. Colocar o model a trabalhar 
## Views

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
