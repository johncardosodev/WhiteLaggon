using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardosoResort.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Alterei_Novos_campos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Metros_Quadrados", "Numero_Quartos" },
                values: new object[] { 100, 2 });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Metros_Quadrados", "Numero_Quartos" },
                values: new object[] { 180, 2 });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Metros_Quadrados", "Numero_Quartos" },
                values: new object[] { 200, 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Metros_Quadrados", "Numero_Quartos" },
                values: new object[] { 2, 0 });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Metros_Quadrados", "Numero_Quartos" },
                values: new object[] { 2, 0 });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Metros_Quadrados", "Numero_Quartos" },
                values: new object[] { 2, 0 });
        }
    }
}
