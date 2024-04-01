using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardosoResort.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AltereiPropriedadesVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "Villas",
                newName: "Data_Criacao");

            migrationBuilder.RenameColumn(
                name: "DataAtualizacao",
                table: "Villas",
                newName: "Data_Atualizacao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data_Criacao",
                table: "Villas",
                newName: "DataCriacao");

            migrationBuilder.RenameColumn(
                name: "Data_Atualizacao",
                table: "Villas",
                newName: "DataAtualizacao");
        }
    }
}
