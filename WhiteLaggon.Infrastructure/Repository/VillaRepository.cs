using CardosoResort.Application.Common.Interfaces;
using CardosoResort.Domain.Entities;
using CardosoResort.Infrastructure.Data;

namespace CardosoResort.Infrastructure.Repository
{
    //VillaRepository precisa de repositório da entidade Villa e implementar a interface IVillaRepository
    //Irá receber todas as operações do repositório da entidade Villa
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _db;

        //O código base(db) no construtor da classe VillaRepository chama o construtor da classe base Repository<Villa> e passa o parâmetro db para ele.
        //o código base(db) no construtor da classe VillaRepository chama o construtor da classe base Repository<Villa> e passa o contexto do banco de dados db para ele, permitindo que a classe VillaRepository herde as operações básicas de um repositório e tenha acesso ao contexto do banco de dados para interagir com o banco de dados.

        public VillaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges(); //Salvamos as alterações no banco de dados
        }

        public void Update(Villa entidade)
        {
            _db.Villas.Update(entidade); //Atualizamos a entidade no DbSet
        }

        //###########################Substituido pelo repositório genérico

        //Porque devemos abrastrair e usar este Get
        //In summary, if you're working on a large,
        //complex application and/or you want to make your code more testable, reusable, and
        //decoupled from the data access strategy, using a repository method like Get might be a better choice.

        //###########################Substituido pelo repositório genérico
        /*
         *
         * public VillaRepository(ApplicationDbContext db)
        {
            //Aqui, Dependency Injection é usada para injetar o contexto do banco de dados no controlador
            _db = db;
        }

        public void Add(Villa entidade)
        {
            _db.Villas.Add(entidade); //Adicionamos a entidade ao DbSet se,
        }

        public Villa Get(Expression<Func<Villa, bool>>? filter, string? includeProperties = null)
        {
            IQueryable<Villa> query = _db.Set<Villa>(); //IQeryable é uma interface que permite a execução de consultas em fontes de dados específicas em que o tipo de dados subjacente é conhecido
            if (filter != null)
            {
                query = query.Where(filter); //Se o filtro não for nulo, aplicamos o filtro à consulta
            }

            if (!string.IsNullOrEmpty(includeProperties)) //Se a propriedade de inclusão não for nula ou vazia
            {
                foreach (var includeProperty in includeProperties //Iteramos sobre as propriedades de inclusão
                    .Split(new char[] { ',' },                    //Dividimos a string de propriedades de inclusão em várias propriedades
                    StringSplitOptions.RemoveEmptyEntries))         //Removemos as entradas vazias da matriz de propriedades de inclusão
                {
                    query = query.Include(includeProperty); //Se a propriedade de inclusão não for nula, incluímos a propriedade na consulta
                }
            }
            return query.FirstOrDefault(); //Retornamos o primeiro objeto que atende ao critério do filtro
        }

        public IEnumerable<Villa> GetAll(Expression<Func<Villa, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<Villa> query = _db.Set<Villa>(); //IQeryable é uma interface que permite a execução de consultas em fontes de dados específicas em que o tipo de dados subjacente é conhecido
            if (filter != null)
            {
                query = query.Where(filter); //Se o filtro não for nulo, aplicamos o filtro à consulta
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties
                    .Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty); //Se a propriedade de inclusão não for nula, incluímos a propriedade na consulta
                }
            }
            return query.ToList(); //Retornamos a consulta como uma lista

            //Explica;ao
            /*O código selecionado é um método chamado GetAll na classe VillaRepository.Esse método retorna uma lista de objetos do tipo Villa com base em um filtro opcional e propriedades de inclusão também opcionais.
 A primeira linha do método cria uma variável chamada query do tipo IQueryable<Villa>. IQueryable é uma interface que permite a execução de consultas em fontes de dados específicas, onde o tipo de dados subjacente é conhecido.Nesse caso, a fonte de dados é o contexto do banco de dados _db e o tipo de dados subjacente é Villa.
 Em seguida, o código verifica se o parâmetro filter não é nulo. Se não for nulo, o filtro é aplicado à consulta usando o método Where do IQueryable.Isso significa que apenas os objetos Villa que atendem ao critério do filtro serão incluídos na lista retornada.
 Depois disso, o código verifica se a string includeProperties não é nula ou vazia.Se não for, a string é dividida em várias propriedades de inclusão usando o método Split. Em seguida, um loop é usado para iterar sobre essas propriedades de inclusão e cada uma delas é adicionada à consulta usando o método Include do IQueryable.Isso permite que você especifique quais propriedades relacionadas devem ser incluídas nos objetos Villa retornados.
 Por fim, a consulta é executada chamando o método ToList do IQueryable, que retorna os resultados da consulta como uma lista de objetos Villa.
 Em resumo, o método GetAll retorna uma lista de objetos Villa com base em um filtro opcional e propriedades de inclusão também opcionais. Ele usa a interface IQueryable para construir e executar a consulta no contexto do banco de dados _db.
        }

        public void Remove(Villa entidade)
        {
            _db.Villas.Remove(entidade); //Removemos a entidade do DbSet
        }
    */

        //###########################Substituido pelo repositório genérico
    }
}