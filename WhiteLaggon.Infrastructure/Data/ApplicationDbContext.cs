using CardosoResort.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CardosoResort.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        //Construtor que recebe as opções do contexto que são passadas para a classe base
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //Propriedades DbSet que representam as tabelas do banco de dados que serão mapeadas para entidades
        public DbSet<Villa> Villas { get; set; }

        //Sobrescreve o método OnModelCreating para configurar o modelo do banco de dados
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Chama o método OnModelCreating da classe base

            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Nome = "Villa Albufeira",
                    Descricao = "Villa com Vista Mar Lateral",
                    Preco = 1000,
                    Numero_Quartos = 2,
                    Metros_Quadrados = 100,
                    Ocupacao = 10,
                    ImagemUrl = "https://via.placeholder.com/150",
                },
                new Villa
                {
                    Id = 2,
                    Nome = "Villa Deluxe",
                    Descricao = "Suite oceano",
                    Numero_Quartos = 2,
                    Preco = 1000,
                    Metros_Quadrados = 180,
                    Ocupacao = 10,
                    ImagemUrl = "https://via.placeholder.com/150",
                },
                new Villa
                {
                    Id = 3,
                    Nome = "Villa Mar",
                    Descricao = "Villa com Vista Mar",
                    Preco = 1000,
                    Numero_Quartos = 2,
                    Metros_Quadrados = 200,
                    Ocupacao = 10,
                    ImagemUrl = "https://via.placeholder.com/150",
                });
        }
    }
}