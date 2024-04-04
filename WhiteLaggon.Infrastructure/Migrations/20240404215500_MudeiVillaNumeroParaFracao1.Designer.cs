﻿// <auto-generated />
using System;
using CardosoResort.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CardosoResort.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240404215500_MudeiVillaNumeroParaFracao1")]
    partial class MudeiVillaNumeroParaFracao1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CardosoResort.Domain.Entities.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Data_Atualizacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Data_Criacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ImagemUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Metros_Quadrados")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ocupacao")
                        .HasColumnType("int");

                    b.Property<double>("Preco")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Descricao = "Villa com Vista Mar Lateral",
                            ImagemUrl = "https://via.placeholder.com/150",
                            Metros_Quadrados = 100,
                            Nome = "Villa Albufeira",
                            Ocupacao = 3,
                            Preco = 1000.0
                        },
                        new
                        {
                            Id = 2,
                            Descricao = "Suite oceano",
                            ImagemUrl = "https://via.placeholder.com/150",
                            Metros_Quadrados = 180,
                            Nome = "Villa Deluxe",
                            Ocupacao = 3,
                            Preco = 1000.0
                        },
                        new
                        {
                            Id = 3,
                            Descricao = "Villa com Vista Mar",
                            ImagemUrl = "https://via.placeholder.com/150",
                            Metros_Quadrados = 200,
                            Nome = "Villa Mar",
                            Ocupacao = 3,
                            Preco = 1000.0
                        });
                });

            modelBuilder.Entity("CardosoResort.Domain.Entities.VillaFracao", b =>
                {
                    b.Property<int>("Villa_Fracao")
                        .HasColumnType("int");

                    b.Property<string>("Detalhes_Especiais")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VillaId")
                        .HasColumnType("int");

                    b.HasKey("Villa_Fracao");

                    b.HasIndex("VillaId");

                    b.ToTable("VillaFracoes");

                    b.HasData(
                        new
                        {
                            Villa_Fracao = 101,
                            Detalhes_Especiais = "Vista Mar Lateral",
                            VillaId = 1
                        },
                        new
                        {
                            Villa_Fracao = 102,
                            Detalhes_Especiais = "Suite Oceano",
                            VillaId = 2
                        });
                });

            modelBuilder.Entity("CardosoResort.Domain.Entities.VillaFracao", b =>
                {
                    b.HasOne("CardosoResort.Domain.Entities.Villa", "Villa")
                        .WithMany()
                        .HasForeignKey("VillaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Villa");
                });
#pragma warning restore 612, 618
        }
    }
}
