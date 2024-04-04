﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CardosoResort.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class VillaNumeroComIDSCorretosOutraVez4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VillaNumeros",
                columns: table => new
                {
                    Villa_Numero = table.Column<int>(type: "int", nullable: false),
                    VillaId = table.Column<int>(type: "int", nullable: false),
                    Detalhes_Especiais = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VillaNumeros", x => x.Villa_Numero);
                    table.ForeignKey(
                        name: "FK_VillaNumeros_Villas_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "VillaNumeros",
                columns: new[] { "Villa_Numero", "Detalhes_Especiais", "VillaId" },
                values: new object[,]
                {
                    { 101, "Vista Mar Lateral", 1 },
                    { 102, "Suite Oceano", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_VillaNumeros_VillaId",
                table: "VillaNumeros",
                column: "VillaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VillaNumeros");
        }
    }
}
