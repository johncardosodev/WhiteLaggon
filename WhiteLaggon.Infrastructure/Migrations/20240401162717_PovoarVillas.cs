using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CardosoResort.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PovoarVillas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Data_Atualizacao", "Data_Criacao", "Descricao", "ImagemUrl", "Nome", "Ocupacao", "Preco", "metrosQuadrados" },
                values: new object[,]
                {
                    { 1, null, null, "Quarto com Vista Mar Lateral", "https://via.placeholder.com/150", "Quarto Mar", 10, 1000.0, 100 },
                    { 2, null, null, "Suite oceano", "https://via.placeholder.com/150", "Suite Mar", 10, 1000.0, 100 },
                    { 3, null, null, "Villa com Vista Mar", "https://via.placeholder.com/150", "Villa Mar", 10, 1000.0, 100 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
