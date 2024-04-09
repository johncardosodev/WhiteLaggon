using CardosoResort.Application.Common.Interfaces;
using CardosoResort.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CardosoResort.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet; //DbSet é uma classe genérica que representa uma coleção de entidades em um contexto de banco de dados do EF Core

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>(); //Set é um método genérico que retorna um DbSet para a entidade especificada do tipo genérico
        }

        public void Add(T entidade)
        {
            dbSet.Add(entidade); //Adicionamos a entidade ao DbSet
        }

        public T Get(Expression<Func<T, bool>>? filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet; //query tomará o valor do DbSet que foi passado no construtor da classe Repository
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

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet; //IQeryable é uma interface que permite a execução de consultas em fontes de dados específicas em que o tipo de dados subjacente é conhecido
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
        }

        public void Remove(T entidade)
        {
            dbSet.Remove(entidade); //Removemos a entidade do DbSet
        }
    }
}