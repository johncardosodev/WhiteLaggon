using CardosoResort.Application.Common.Interfaces;
using CardosoResort.Infrastructure.Data;

namespace CardosoResort.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork

    //wrapper sobre os repositórios
    {
        private readonly ApplicationDbContext _db; //
        public IVillaRepository Villas { get; private set; } //Propriedade Villas do tipo IVillaRepository que retorna um repositório de villas para interagir com as villas no banco de dados. A propriedade é privada e somente leitura, o que significa que ela só pode ser definida no construtor da classe.

        public IVillaFracaoRepository VillasFracao { get; private set; } //Propriedade VillasFracao do tipo IVillaFracaoRepository que retorna um repositório de villas fracao para interagir com as villas fracao no banco de dados. A propriedade é privada e somente leitura, o que significa que ela só pode ser definida no construtor da classe.

        public IExtraRepository Extras { get; private set; } //Propriedade Extras do tipo IExtraRepository que retorna um repositório de extras para interagir com os extras no banco de dados. A propriedade é privada e somente leitura, o que significa que ela só pode ser definida no construtor da classe.

        public UnitOfWork(ApplicationDbContext db) //Construtor da classe UnitOfWork que recebe o contexto do banco de dados como parâmetro
        {
            _db = db; //Atribuímos o contexto do banco de dados ao campo privado _db
            Villas = new VillaRepository(_db); //Atribuímos uma nova instância da classe VillaRepository ao repositório de villas
            VillasFracao = new VillaFracaoRepository(_db); //Atribuímos uma nova instância da classe VillaFracaoRepository ao repositório de villas fracao
            Extras = new ExtraRepository(_db); //Atribuímos uma nova instância da classe ExtraRepository ao repositório de extras
        }

        public void Save()
        {
            _db.SaveChanges(); //Chamamos o método SaveChanges do contexto do banco de dados para salvar as alterações no banco de dados
        }

        //Explicaçao do UnitOfWork
        //A classe UnitOfWork é responsável por agrupar todas as operações relacionadas a transações em um único objeto.
        //Neste caso, a classe UnitOfWork possui uma propriedade Villas do tipo IVillaRepository que retorna um repositório de villas para interagir com as villas no banco de dados.
    }
}