using CardosoResort.Domain.Entities;

namespace CardosoResort.Application.Common.Interfaces
{
    public interface IVillaRepository : IRepository<Villa>
    {
        //Necessiade deste interface é para que possamos implementar o padrão de repositório, que é uma abstração de uma coleção de objetos, permitindo que você manipule esses objetos sem se preocupar com os detalhes de como eles são armazenados ou recuperados.

        /*A interface IVillaRepository define um contrato para uma classe que será responsável por lidar com operações relacionadas a villas.
Dentro dessa interface, temos um método chamado GetAll que retorna uma coleção de objetos do tipo Villa. Esse método aceita dois parâmetros opcionais: filter e includeProperties.
O parâmetro filter é uma expressão que permite filtrar as villas com base em uma condição específica. Essa expressão é do tipo Expression<Func<Villa, bool>>, o que significa que pode passar uma função que recebe uma villa como argumento e retorna um valor booleano.
O parâmetro includeProperties é uma string que permite especificar quais propriedades relacionadas devem ser incluídas na consulta.
Em resumo, esse método permite obter todas as villas do repositório, com a opção de filtrar os resultados e incluir propriedades relacionadas*/

        //####Substituido pelo repositório genérico

        // IEnumerable<Villa> GetAll(Expression<Func<Villa, bool>>? filter = null, string? includeProperties = null);
        //Villa Get(Expression<Func<Villa, bool>>? filter, string? includeProperties = null);
        // void Add(Villa entidade);
        // void Remove(Villa entidade);

        void Update(Villa entidade);

        void Save();
    }
}