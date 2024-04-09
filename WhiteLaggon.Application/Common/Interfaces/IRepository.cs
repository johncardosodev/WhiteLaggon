using System.Linq.Expressions;

namespace CardosoResort.Application.Common.Interfaces
{
    /*//The code snippet you provided defines a public interface called IRepository<T>. This interface is generic, meaning it can work with different types of objects. The generic type parameter T is used to represent the type of objects that the repository will work with.
    The where T : class constraint after the interface name specifies that the generic type T must be a reference type(i.e., a class). This constraint ensures that the repository can only work with class objects and not value types like integers or structs.*/

    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

        T Get(Expression<Func<T, bool>>? filter, string? includeProperties = null);

        void Add(T entidade);

        void Remove(T entidade);

        //void Update(T entidade); Evitar usar update para não ter que fazer um update em todas as propriedades
    }

    /*A interface IRepository<T> é uma interface genérica que define um contrato para um repositório. Essa interface é usada para acessar e manipular dados de uma fonte de dados, como um banco de dados.
A interface possui vários métodos que permitem realizar operações comuns em um repositório, como obter todos os objetos de uma determinada entidade, obter um objeto específico com base em um filtro, adicionar um novo objeto, remover um objeto existente e salvar as alterações feitas no repositório.
Os métodos GetAll e Get são usados para obter objetos de uma entidade específica. O método GetAll retorna uma coleção de objetos, opcionalmente filtrados por uma expressão lambda. O método Get retorna um único objeto com base em uma expressão lambda.*/
}