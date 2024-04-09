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

### Imagens
1.	Para adicionar imagens.

	1. Criamos um nome de ficheiro único para a imagem da villa
	1. Definimos o caminho onde a imagem será guardada no servidor web
	1. Criamos um ficheiro no servidor web  para guardar a imagem da villa com using para garantir que o ficheiro é fechado após o uso
	1. Copiamos a imagem para o ficheiro
	1. Definimos a propriedade ImagemUrl da villa com o caminho da imagem
## Model
1. Colocar o model a trabalhar

### Notas 
* Para colocar imagens, usar IFormFile com annotations NotMapped para nao enviar apra a base de dados*
## Views

### Tag Helpers
1.	asp-for: Este ajudante de tag é usado para vincular uma propriedade do modelo a um campo de formulário. Por exemplo, se você tem um modelo com uma propriedade Name, você pode vinculá-lo a uma caixa de texto assim: <input asp-for="Name" />. Quando o formulário é enviado, o valor inserido na caixa de texto será atribuído à propriedade Name do modelo.
2.	asp-validation-for: Este ajudante de tag é usado para exibir mensagens de erro de validação para uma propriedade específica do modelo. Por exemplo, <span asp-validation-for="Name"></span> exibirá quaisquer erros de validação para a propriedade Name.
3.	asp-items: Este ajudante de tag é usado com elementos select para gerar elementos option. O valor deve ser um IEnumerable<SelectListItem>. Por exemplo, se você tem uma lista de opções para uma lista suspensa, você pode vinculá-la assim: <select asp-for="Option" asp-items="Model.Options"></select>.
4.	asp-route-id: Este ajudante de tag é usado para passar um parâmetro para uma rota. Por exemplo, se você tem um link para uma página de detalhes que precisa do ID de um item, você pode usá-lo assim: <a asp-action="Details" asp-route-id="@Model.Id">Details</a>. Isso gerará um link como /Details/5, supondo que o Id seja 5.
5.	asp-action e asp-controller: Eles são usados para gerar URLs para ações e controladores específicos. Por exemplo, <a asp-action="Edit" asp-controller="Home">Edit</a> gerará um link para a ação Edit do controlador Home.
6.	asp-area: É usado para gerar URLs para áreas específicas em uma aplicação ASP.NET Core MVC. Por exemplo, <a asp-area="Admin" asp-controller="Home" asp-action="Index">Admin Home</a> gerará um link para a ação Index do controlador Home na área Admin.
7.	asp-route: É usado para gerar URLs com nomes de rota específicos. Por exemplo, <a asp-route="default">Home</a> gerará um link para a rota chamada default.

### Images
Como adicionar imagens. Adicionamos na view form
<input asp-for="ImagemUrl" hidden class="form-control border shadow" />
<input asp-for="Imagem"  class="form-control border shadow" />
     Temos que colocar enctype="multipart/form-data"                

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

Clean Tips										
Devemos evitar magic strings. Devemos usar nameOf() para evitar erros de digitação. Exemplo: RedirectToAction(nameof(Index)) em vez de RedirectToAction("Index"). No caso de Index ser renomeado, o compilador irá detectar o erro e nos avisar.
Nao funciona com outros actions de outros controllers. Exemplo: RedirectToAction(nameof(HomeController.Index)) não funciona. Devemos usar RedirectToAction(nameof(HomeController.Index), "Home") para funcionar.
	
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
Não queremos dependecias circulares. Assim, o projeto de infraestrutura não deve referenciar o projeto de aplicação.

## Nugget Packages
1. **Microsoft.EntityFrameworkCore.SqlServer**
1. **Microsoft.EntityFrameworkCore.Design**

## Dependency Injection
Dependecy Injection é um padrão de projeto que permite a criação de objetos de uma classe sem a necessidade de instanciar diretamente a classe. Em vez disso, a classe é instanciada por um contêiner de injeção de dependência. O contêiner de injeção de dependência é responsável por criar e gerenciar as instâncias das classes.
Dependency Injection contem 3 tipos de injeção de dependência:
1. **Constructor Injection**: A dependência é injetada no construtor da classe.
1. **Property Injection**: A dependência é injetada em uma propriedade da classe.
1. **Method Injection**: A dependência é injetada em um método da classe.

## Interface
Vamos criar uma interface para o repositório de Villa. Assim, podemos usar a interface para injetar o repositório de Villa em outras classes. Isso torna o código mais limpo e fácil de manter.
interface IVillaRepository
 Necessiade deste interface é para que possamos implementar o padrão de repositório, que é uma abstração de uma coleção de objetos, permitindo que você manipule esses objetos sem se preocupar com os detalhes de como eles são armazenados ou recuperados.
 Para tal, devemos criar uma classe concreta que implementa a interface. Iremos adicionar uma pasta Repository no projeto de infraestrutura e adicionar uma classe concreta que implementa a interface que será VillaRepository.cs
 Iremos adicionar um método GetAll() que retorna uma lista de villas entre outros. 
 Devemos adicionar o VillaRepository.cs no program.cs para que possamos injetar a dependência em outras classes.
 Criamos depois um private readonly IVillaRepository _villaRepository no controller e injetamos a dependência no construtor do controller. Assim, podemos usar o repositório de villa no controller.
 Tem que ser o interface e não a classe concreta que é injetada no constror. Exemplo: private readonly IVillaRepository _villaRepository e nao private readonly VillaRepository _villaRepository
 Em dependencia de injeção, devemos usar interfaces e não classes concretas. Assim, podemos trocar a implementação sem alterar o código que usa a interface.
 No program.cs adicionamos services.AddScoped<IVillaRepository, VillaRepository>();. Significa que o contêiner de injeção de dependência irá criar uma nova instância do VillaRepository sempre que uma nova solicitação for feita.


### Generic interface
1. **IRepository<T>**: É uma interface genérica que representa um repositório de entidades. Ela define os métodos comuns que um repositório de entidades deve implementar, como GetAll, GetById, Add, Update e Delete.
where<T> : class: Restringe o tipo genérico T a classes de referência. Isso garante que o tipo genérico T seja uma classe de referência, o que é necessário para trabalhar com entidades do Entity Framework Core.  


### Unit of Work
1. **IUnitOfWork**: É uma interface que representa uma unidade de trabalho. Ela define um método SaveChanges que salva todas as alterações feitas no contexto do banco de dados. A interface IUnitOfWork é usada para agrupar várias operações de banco de dados em uma única transação. Isso garante que todas as operações sejam bem-sucedidas ou falhem juntas.
1. **UnitOfWork**: É uma classe concreta que implementa a interface IUnitOfWork. Ela fornece uma implementação do método SaveChanges que salva todas as alterações feitas no contexto do banco de dados	
Devemos adicionar o UnitOfWork no Program.cs para que possamos injetar a dependência em outras classes. 

### Beneficios do Unit of Work

A interface IUnitOfWork em C# serve como um contrato para o padrão Unit of Work, que é um padrão de design usado para encapsular um grupo de operações numa única transação. Este padrão é particularmente útil em cenários onde é necessário garantir a integridade e consistência dos dados em múltiplas operações, como transações de banco de dados.

A interface IUnitOfWork tipicamente inclui métodos para confirmar alterações no banco de dados, reverter alterações em caso de erro, e acessar repositórios para operações de dados. Aqui está uma descrição detalhada do seu propósito e funcionalidade:

Operações de Commit e Rollback: O método Commit é usado para salvar todas as alterações feitas dentro da unidade de trabalho no banco de dados. O método Rollback, por outro lado, é usado para desfazer todas as alterações feitas dentro da unidade de trabalho se ocorrer um erro ou se as alterações não forem mais necessárias 
1. Acesso ao Repositório: O método Repository<T> permite acessar um repositório para um tipo de entidade específico. Este método é crucial para realizar operações CRUD em entidades dentro da unidade de trabalho. O padrão de repositório, frequentemente usado em conjunto com o padrão Unit of Work, abstrai a lógica de acesso a dados, tornando a aplicação mais fácil de manter e testar 
1. Interface Disposable: Ao implementar a interface IDisposable, a IUnitOfWork garante que os recursos sejam devidamente liberados quando não forem mais necessários. Isso é importante para gerenciar eficientemente conexões de banco de dados e outros recursos 
1. Flexibilidade e Escalabilidade: O padrão Unit of Work, juntamente com o padrão Repository, fornece uma arquitetura flexível e escalável para acesso a dados. Permite aos desenvolvedores alterar a tecnologia de acesso a dados subjacente (por exemplo, de Entity Framework para Dapper) sem afetar a camada de lógica de negócios da aplicação. Esta separação de preocupações torna a aplicação mais fácil de manter e estender 
Em resumo, a interface IUnitOfWork em C# é um componente chave do padrão Unit of Work, fornecendo uma maneira estruturada de gerenciar transações e operações de dados dentro de uma aplicação. Ela garante a integridade dos dados, suporta o padrão de repositório para acesso a dados, e promove uma clara separação de preocupações, tornando a aplicação mais fácil de manter e escalável.

### Beneficios do interface
In terms of raw performance, there's typically not a significant difference between using a repository method and a direct query. Both ultimately generate and execute SQL queries against the database, and the performance of these queries depends more on factors like the database schema, indexes, and the specific SQL generated by the ORM.
However, there are some indirect ways that using a repository can potentially improve performance:
1.	Eager Loading: The Get method in your repository allows you to specify related entities to be included in the query using eager loading. This can reduce the number of round-trips to the database, which can significantly improve performance if you're retrieving related entities.
2.	Query Reuse: If you find yourself writing the same query in multiple places, moving it into a repository method can help ensure that you're using the most efficient query possible in all those places.
3.	Caching: A repository can provide a convenient place to implement caching, which can significantly improve performance for read-heavy workloads.
Remember, the primary benefits of using a repository are improved code organization, testability, and decoupling from the data access strategy, not performance. If you're looking to improve performance, you'll likely get more mileage out of optimizing your database schema and queries, using eager loading wisely, and implementing caching where appropriate.

	1.
 ## Repository Pattern
 O padrão de repositório é um padrão de projeto que abstrai a lógica de acesso a dados de uma aplicação. Ele fornece uma interface para acessar os dados de uma aplicação, permitindo que você manipule esses dados sem se preocupar com os detalhes de como eles são armazenados ou recuperados.
 O padrão de repositório é útil quando você precisa acessar dados de várias fontes, como bancos de dados, serviços da web ou arquivos. Ele permite que você altere a fonte de dados sem afetar o restante da aplicação.
 O padrão de repositório é composto por três partes principais: a interface do repositório, a classe concreta do repositório e a classe de contexto de dados. A interface do repositório define os métodos que a classe concreta do repositório deve implementar. A classe concreta do repositório implementa esses métodos e fornece a lógica para acessar os dados. A classe de contexto de dados fornece a conexão com a fonte de dados e executa as operações de acesso a dados.
