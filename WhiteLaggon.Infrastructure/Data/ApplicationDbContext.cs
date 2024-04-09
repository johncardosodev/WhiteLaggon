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

        public DbSet<VillaFracao> VillaFracoes { get; set; } //Adicionamos a propriedade DbSet para a entidade VillaFracoes

        public DbSet<Extra> Extras { get; set; } //Adicionamos a propriedade DbSet para a entidade Extra

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
                    Metros_Quadrados = 100,
                    Ocupacao = 3,
                    ImagemUrl = "https://via.placeholder.com/150",
                },
                new Villa
                {
                    Id = 2,
                    Nome = "Villa Deluxe",
                    Descricao = "Suite oceano",
                    Preco = 1000,
                    Metros_Quadrados = 180,
                    Ocupacao = 3,
                    ImagemUrl = "https://via.placeholder.com/150",
                },
                new Villa
                {
                    Id = 3,
                    Nome = "Villa Mar",
                    Descricao = "Villa com Vista Mar",
                    Preco = 1000,
                    Metros_Quadrados = 200,
                    Ocupacao = 3,
                    ImagemUrl = "https://via.placeholder.com/150",
                });
            modelBuilder.Entity<VillaFracao>().HasData(
    new VillaFracao
    {
        Villa_Fracao = 101,
        VillaId = 1,
        Detalhes_Especiais = "Vista Mar Lateral"
    },
    new VillaFracao
    {
        Villa_Fracao = 102,
        VillaId = 2,
        Detalhes_Especiais = "Suite Oceano"
    });

            modelBuilder.Entity<Extra>().HasData(
        new Extra
        {
            Id = 1,
            Nome = "Piscina",
            Descricao = "Piscina privada",
            VillaId = 1
        },
        new Extra
        {
            Id = 2,
            Nome = "Jacuzzi",
            Descricao = "Jacuzzi privada",
            VillaId = 2
        },
            new Extra
            {
                Id = 3,
                Nome = "Quartos familiares",
                Descricao = "Quartos familiares",
                VillaId = 1
            },
            new Extra
            {
                Id = 4,
                Nome = "Terraço",
                Descricao = "Terraço",
                VillaId = 2
            },
            new Extra
            {
                Id = 5,
                Nome = "Vista Mar",
                Descricao = "Vista Mar",
                VillaId = 1
            },
            new Extra
            {
                Id = 6,
                Nome = "Ar condicionado",
                Descricao = "Ar condicionado",
                VillaId = 2
            });
        }
    }
}