using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardosoResort.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NovosCampos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "metrosQuadrados",
                table: "Villas",
                newName: "Numero_Quartos");

            migrationBuilder.AddColumn<int>(
                name: "Metros_Quadrados",
                table: "Villas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Descricao", "Metros_Quadrados", "Nome", "Numero_Quartos" },
                values: new object[] { "Villa com Vista Mar Lateral", 2, "Villa Albufeira", 0 });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Metros_Quadrados", "Nome", "Numero_Quartos" },
                values: new object[] { 2, "Villa Deluxe", 0 });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Metros_Quadrados", "Numero_Quartos" },
                values: new object[] { 2, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Metros_Quadrados",
                table: "Villas");

            migrationBuilder.RenameColumn(
                name: "Numero_Quartos",
                table: "Villas",
                newName: "metrosQuadrados");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Descricao", "Nome", "metrosQuadrados" },
                values: new object[] { "Quarto com Vista Mar Lateral", "Quarto Mar", 100 });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Nome", "metrosQuadrados" },
                values: new object[] { "Suite Mar", 100 });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "metrosQuadrados",
                value: 100);
        }
    }
}
