using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CardosoResort.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Adicionar_Extra_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Extras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VillaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Extras_Villas_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Extras",
                columns: new[] { "Id", "Descricao", "Nome", "VillaId" },
                values: new object[,]
                {
                    { 1, "Piscina privada", "Piscina", 1 },
                    { 2, "Jacuzzi privada", "Jacuzzi", 2 },
                    { 3, "Quartos familiares", "Quartos familiares", 1 },
                    { 4, "Terraço", "Terraço", 2 },
                    { 5, "Vista Mar", "Vista Mar", 1 },
                    { 6, "Ar condicionado", "Ar condicionado", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Extras_VillaId",
                table: "Extras",
                column: "VillaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Extras");
        }
    }
}
